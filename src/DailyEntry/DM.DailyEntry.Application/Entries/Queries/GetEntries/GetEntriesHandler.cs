using System.Data;
using Dapper;
using DM.Application.Abstractions;
using DM.SharedKernel.Common;
using DM.SharedKernel.CustomErrors.Entities;

namespace DM.Application.Entries.Queries.GetEntries;

public sealed class GetEntriesHandler(IDbConnection dbConnection)
    : IQueryHandler<GetEntriesQuery, Result<EntriesResponse>>
{
    public async Task<Result<EntriesResponse>> Handle(
        GetEntriesQuery request,
        CancellationToken cancellationToken
    )
    {
        const string sqlQuery = $"""SELECT "Id", "Balance", "Type" FROM entry""";

        var entries = await dbConnection.QueryAsync<EntryResponse>(sqlQuery);

        var entryResponses = entries.ToList();
        return entryResponses.Count != 0
            ? Result<EntriesResponse>.Success(new EntriesResponse(entryResponses))
            : Result<EntriesResponse>.Failure(EntryErrors.IsEmpty);
    }
}
