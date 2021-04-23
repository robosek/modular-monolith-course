using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Confab.Modules.Speakers.Core.DTO;
using Confab.Modules.Speakers.Core.Entities;
using Confab.Modules.Speakers.Core.Events;
using Confab.Modules.Speakers.Core.Exceptions;
using Confab.Modules.Speakers.Core.Repositories;
using Confab.Shared.Abstractions.Messaging;

namespace Confab.Modules.Speakers.Core.Services
{
    internal class SpeakerService : ISpeakerService
    {
        private readonly ISpeakerRepository speakerRepository;
        private readonly IMessageBroker messageBroker;

        public SpeakerService(ISpeakerRepository speakerRepository, IMessageBroker messageBroker)
        {
            this.speakerRepository = speakerRepository;
            this.messageBroker = messageBroker;
        }

        public async Task AddAsync(SpeakerDto dto)
        {
            var exists = await speakerRepository.ExistsAsync(dto.Id);

            if (exists)
            {
                throw new SpeakerAlreadyExistsException(dto.Id);
            }

            dto.Id = Guid.NewGuid();
            var speaker = Map(dto);

            await speakerRepository.AddAsync(speaker);
            await messageBroker.PublishAsync(new SpeakerCreated(speaker.Id, speaker.FullName));
        }

        public async Task<IReadOnlyList<SpeakerDto>> BrowseAsync()
        {
            var speakers = await speakerRepository.BrowseAsync();

            return speakers.Select(Map<SpeakerDto>).ToList();
        }

        public async Task DeleteAsync(Guid id)
        {
            var speaker = await speakerRepository.GetAsync(id);

            if (speaker is null)
            {
                throw new SpeakerNotFoundException(id);
            }

            await speakerRepository.DeleteAsync(speaker);
        }

        public async Task<SpeakerDto> GetAsync(Guid id)
        {
            var speaker = await speakerRepository.GetAsync(id);

            if (speaker is null)
            {
                return null;
            }

            return Map<SpeakerDto>(speaker);
        }

        public async Task UpdateAsync(SpeakerDto dto)
        {
            var exists = await speakerRepository.ExistsAsync(dto.Id);

            if (!exists)
            {
                throw new SpeakerNotFoundException(dto.Id);
            }

            var speakerUpdated = Map(dto);

            await speakerRepository.UpdateAsync(speakerUpdated);
        }

        private Speaker Map(SpeakerDto speakerDto)
        {
            return new Speaker
            {
                Id = speakerDto.Id,
                Email = speakerDto.Email,
                FullName = speakerDto.FullName,
                Bio = speakerDto.Bio,
                AvatarUrl = speakerDto.AvatarUrl
            };
        }

        private static T Map<T>(Speaker Speaker) where T : SpeakerDto, new()
        {
            return new T
            {
                Id = Speaker.Id,
                Email = Speaker.Email,
                FullName = Speaker.FullName,
                Bio = Speaker.Bio,
                AvatarUrl = Speaker.AvatarUrl
            };
        }
    }
}
