using System.Data;
using Dapper;
using DM.Application.Abstractions;
using DM.SharedKernel.Common;
using DM.SharedKernel.CustomErrors.Entities;

namespace DM.Application.Entries.Queries.GetEntryById;

public sealed class GetEntryByIdHandler(IDbConnection dbConnection)
    : IQueryHandler<GetEntryByIdQuery, Result<EntryResponse>>
{
    public async Task<Result<EntryResponse>> Handle(
        GetEntryByIdQuery request,
        CancellationToken cancellationToken
    )
    {
        const string sqlQuery = $"""SELECT "Id", "Balance", "Type" FROM entry WHERE "Id" = @entry_id""";

        var entry = await dbConnection.QueryFirstOrDefaultAsync<EntryResponse>(
            sqlQuery,
            param: new
            {
                entry_id = request.Id
            }
        );

        return entry is not null
            ? Result<EntryResponse>.Success(entry)
            : Result<EntryResponse>.Failure(EntryErrors.NotFound);
    }
}
