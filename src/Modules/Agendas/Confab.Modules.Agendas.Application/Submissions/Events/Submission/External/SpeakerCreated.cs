using System;
using Confab.Shared.Abstractions.Events;

namespace Confab.Modules.Agendas.Application.Events.Submission.External
{
    public record SpeakerCreated(Guid Id, string FullName) : IEvent;
}
