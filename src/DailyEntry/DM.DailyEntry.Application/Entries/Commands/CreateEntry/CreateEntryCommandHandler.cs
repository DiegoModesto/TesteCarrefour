using DM.Application.Abstractions;
using DM.Application.Entries.Notifications;
using DM.Domain.Entities;
using DM.Domain.Interfaces;
using DM.Domain.Interfaces.Repositories;
using DM.SharedKernel.Common;
using MediatR;

namespace DM.Application.Entries.Commands.CreateEntry;

public sealed class CreateEntryCommandHandler(
    IUnitOfWork unitOfWork,
    IEntryRepository repository,
    IMediator mediator
)
    : ICommandHandler<CreateEntryCommand, Result<string>>
{
    public async Task<Result<string>> Handle(
        CreateEntryCommand request,
        CancellationToken cancellationToken
    )
    {
        var entity = Entry.Create(request.Balance, request.Type);

        repository.Create(entity);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        await mediator.Publish(new EntryCreatedNotification(
                Id: entity.Id,
                Balance: entity.Balance,
                Type: (int)entity.Type,
                CreatedDate: entity.CreatedDate
            ), 
            cancellationToken
        );
        
        return Result<string>.Success(entity.Id.ToString());
    }
}
