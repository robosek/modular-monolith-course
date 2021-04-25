using System;
using System.Collections.Generic;
using System.Linq;
using Confab.Modules.Agendas.Domain.Submissions.Consts;
using Confab.Modules.Agendas.Domain.Submissions.Events;
using Confab.Modules.Agendas.Domain.Submissions.Exceptions;
using Confab.Shared.Abstractions.Kernel.Types;

namespace Confab.Modules.Agendas.Domain.Submissions.Entities
{
    public sealed class Submission : AgregateRoot
    {
        public ConferenceId ConferenceId { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public int Level { get; private set; }
        public string Status { get; private set; }
        public IEnumerable<string> Tags { get; private set; }
        public IEnumerable<Speaker> Speakers => _speakers;

        private ICollection<Speaker> _speakers;


        public Submission(AggregateId id ,
            ConferenceId conferenceId,
            string title,
            string description,
            int level,
            string status,
            IEnumerable<string> tags,
            ICollection<Speaker> speakers,
            int version = 0)
        {
            ConferenceId = conferenceId;
            Title = title;
            Description = description;
            Level = level;
            Status = status;    
            Tags = tags;
            Version = version;
            Id = id; 
        }

        public Submission(AggregateId id, ConferenceId conferenceId)
            => (Id, ConferenceId) = (id, conferenceId);

        public static Submission Create(AggregateId id,
            ConferenceId conferenceId,
            string title,
            string description,
            int level,
            IEnumerable<string> tags,
            ICollection<Speaker> speakers)
        {

            var submission = new Submission(id, conferenceId);
            submission.ChangeTitle(title);
            submission.ChangeTitle(description);
            submission.ChangeLevel(level);
            submission.Status = SubmissionStatus.Pending;
            submission.Tags = tags;
            submission.ChangeSpeakers(speakers);
            submission.ClearEvents();
            submission.Version = 0;

            submission.AddEvent(new SubmissionAdded(submission));

            return submission;
        }


        public void ChangeTitle(string title)
        {
            if(string.IsNullOrEmpty(title))
            {
                throw new EmptySubmissionTitleException(Id.Value);
            }

            Title = title;
            IncrementVersion();
        }

        public void ChangeDescription(string description)
        {
            if (string.IsNullOrEmpty(description))
            {
                throw new EmptySubmissionDescriptionException(Id.Value);
            }

            Description = description;
            IncrementVersion();
        }

        public void ChangeLevel(int level)
        {
            if(IsNotInRange())
            {
                throw new InvalidSubmissionLevelException(Id.Value);
            }

            Level = level;
            IncrementVersion();

            bool IsNotInRange() => level < 1 || level > 6;
        }

        public void Approve()
        {
            ChangeStatus(SubmissionStatus.Approved, SubmissionStatus.Rejected);

        }

        public void Reject()
        {
            ChangeStatus(SubmissionStatus.Rejected, SubmissionStatus.Approved);

        }

        public void ChangeSpeakers(IEnumerable<Speaker> speakers)
        {
            if(speakers is null || !speakers.Any())
            {
                throw new MissingSubmissionSpeakersException(Id.Value);
            }

            _speakers = speakers.ToList();


        }

        private void ChangeStatus(string status, string invalidStatus)
        {
            if (Status == invalidStatus)
            {
                throw new InvalidSubmissionStatusException(Id.Value);
            }

            Status = status;
            AddEvent(new SubmissionStatusChanged(this, status));
        }
    }  
}
