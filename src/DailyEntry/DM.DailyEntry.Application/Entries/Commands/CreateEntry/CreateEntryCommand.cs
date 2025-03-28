using DM.Application.Abstractions;
using DM.SharedKernel.Common;

namespace DM.Application.Entries.Commands.CreateEntry;

public sealed record CreateEntryCommand(decimal Balance, int Type) : ICommand<Result<string>>;
