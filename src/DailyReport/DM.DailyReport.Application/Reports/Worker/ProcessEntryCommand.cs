using DM.Application.Abstractions;
using DM.SharedKernel.Common;

namespace DM.Application.Reports.Worker;

public sealed record ProcessEntryCommand(
    Guid Id, 
    decimal Balance, 
    int Type, 
    DateTimeOffset CreatedDate
) : ICommand<Result<string>>;
