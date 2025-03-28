using DM.Application.Abstractions;
using DM.SharedKernel.Common;

namespace DM.Application.Entries.Queries.GetEntries;

public sealed record GetEntriesQuery : IQuery<Result<EntriesResponse>>;
