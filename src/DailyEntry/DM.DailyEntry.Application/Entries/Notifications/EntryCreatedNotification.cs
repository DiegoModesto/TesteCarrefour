using MediatR;

namespace DM.Application.Entries.Notifications;

public record EntryCreatedNotification(Guid Id, decimal Balance, int Type, DateTimeOffset CreatedDate) : INotification;