using DM.Application.Abstractions;
using DM.SharedKernel.Common;

namespace DM.Application.Entries.Queries.GetEntryById;

public sealed record GetEntryByIdQuery(Guid Id) : IQuery<Result<EntryResponse>>;
