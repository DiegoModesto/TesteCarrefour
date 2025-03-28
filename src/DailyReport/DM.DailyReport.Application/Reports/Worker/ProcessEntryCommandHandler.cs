using DM.Application.Abstractions;
using DM.Domain.Entities;
using DM.Domain.Interfaces;
using DM.Domain.Interfaces.Repositories;
using DM.SharedKernel.Common;

namespace DM.Application.Reports.Worker;

public sealed class ProcessEntryCommandHandler(
    IUnitOfWork unitOfWork,
    IReportRepository repository    
) : ICommandHandler<ProcessEntryCommand, Result<string>>
{
    public async Task<Result<string>> Handle(
        ProcessEntryCommand request, 
        CancellationToken cancellationToken
    )
    {
        var entity = Report.Create(
            externalId: request.Id,
            balance: request.Balance,
            type: request.Type,
            registerDate: request.CreatedDate
        );

        repository.Create(entity);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<string>.Success(entity.Id.ToString());
    }
}