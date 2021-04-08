using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Confab.Modules.Conferences.Core.DTO;
using Confab.Modules.Conferences.Core.Entities;
using Confab.Modules.Conferences.Core.Exceptions;
using Confab.Modules.Conferences.Core.Policies;
using Confab.Modules.Conferences.Core.Repositories;

namespace Confab.Modules.Conferences.Core.Services
{
    internal class ConferenceService : IConferenceService
    {
        private readonly IConferenceRepository _conferenceRepository;
        private readonly IHostRepository _hostRepository;
        private readonly IConferenceDeletionPolicy _conferenceDeletionPolicy;

        public ConferenceService(IConferenceRepository conferenceRepository,
            IHostRepository hostRepository,
            IConferenceDeletionPolicy conferenceDeletionPolicy)
        {
            _conferenceRepository = conferenceRepository;
            _hostRepository = hostRepository;
            _conferenceDeletionPolicy = conferenceDeletionPolicy;
        }

        public async Task AddAsync(ConferenceDto dto)
        {
            if(await _hostRepository.GetAsync(dto.HostId) is null)
            {
                throw new HostNotFoundException(dto.HostId);
            }

            dto.Id = Guid.NewGuid();
            var conference = Map(dto);

            await _conferenceRepository.AddAsync(conference);
        }

        public async Task<IReadOnlyList<ConferenceDto>> BrowseAsync()
        {
            var conferences = await _conferenceRepository.BrowseAsync();

            return conferences.Select(Map<ConferenceDto>).ToList();
        }

        public async Task<ConferenceDetailsDto> GetAsync(Guid id)
        {
            var conference = await _conferenceRepository.GetAsync(id);

            if (conference is null)
            {
                return null;
            }

            var conferenceDetails = Map<ConferenceDetailsDto>(conference);
            conferenceDetails.Description = conference.Description;

            return conferenceDetails;
        }

        public async Task DeleteAsync(Guid id) 
        {
            var conference = await _conferenceRepository.GetAsync(id);

            if (conference is null)
            {
                throw new ConferenceNotFoundException(id);
            }


            if(await _conferenceDeletionPolicy.CanDeleteAsync(conference) is false)
            {
                throw new CannotDeleteConferenceException(id);
            }
        
            await _conferenceRepository.DeleteAsync(conference);
        }

        public async Task UpdateAsync(ConferenceDto dto)
        {
            var conference = await _conferenceRepository.GetAsync(dto.Id);

            if (conference is null)
            {
                throw new ConferenceNotFoundException(dto.Id);
            }

            var conferenceUpdated = Map(dto);

            await _conferenceRepository.UpdateAsync(conferenceUpdated);
        }

        private Conference Map(ConferenceDto conferenceDto)
        {
            return new Conference
            {
                Id = conferenceDto.Id,
                HostId = conferenceDto.HostId,
                Name = conferenceDto.Name,
                Location = conferenceDto.Location,
                LogoUrl = conferenceDto.LogoUrl,
                ParticipantsLimit = conferenceDto.ParticipantsLimit,
                From = conferenceDto.From,
                To = conferenceDto.To
            };
        }

        private static T Map<T>(Conference conference) where T : ConferenceDto, new()
        {
            return new T
            {
                Id = conference.Id,
                HostId = conference.HostId,
                Name = conference.Name,
                HostName = conference.Host.Name,
                Location = conference.Location,
                LogoUrl = conference.LogoUrl,
                ParticipantsLimit = conference.ParticipantsLimit,
                From = conference.From,
                To = conference.To
            };
        }
    }
}
