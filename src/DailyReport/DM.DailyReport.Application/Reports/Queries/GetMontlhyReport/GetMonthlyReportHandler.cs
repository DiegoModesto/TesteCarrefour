using System.Data;
using Dapper;
using DM.Application.Abstractions;
using DM.Application.Entries;
using DM.SharedKernel.Common;
using DM.SharedKernel.CustomErrors.Entities;

namespace DM.Application.Reports.Queries.GetMontlhyReport;

public sealed class GetMonthlyReportHandler(IDbConnection dbConnection)
    : IQueryHandler<GetMonthlyReportQuery, Result<ReportsResponse>>
{
    /**
     * Nota ao observador:
     *
     * É possível fazer uma View ou Subquery que retorne as informações de forma
     * mais abrangente, porém, note que para isso seria necessário ter um
     * controle maior dos dados, criando referências.
     *
     */
    public async Task<Result<ReportsResponse>> Handle(
        GetMonthlyReportQuery request,
        CancellationToken cancellationToken
    )
    {
        const string sqlGetSummary = $"""
        
            SELECT 
                SUM(CASE WHEN "EntryName" = 'Entrada' THEN "Balance" ELSE 0 END) AS "TotalIn",
                SUM(CASE WHEN "EntryName" = 'Saída' THEN "Balance" ELSE 0 END) AS "TotalOut"
            FROM public.report
            WHERE "RegisterDate" BETWEEN @entry_date AND @end_date;

        """;
        
        const string sqlGetConsolidated = $"""
                                           
            SELECT
                "Id",
                "ExternalId",
                "Balance",
                "EntryName",
                "RegisterDate" AS "OperationDate"
            FROM
                public.report
            WHERE "RegisterDate" BETWEEN @entry_date AND @end_date;                               
        """;
        
        var summary = await dbConnection.QueryFirstOrDefaultAsync<(decimal totalIn, decimal totalOut)>(
            sqlGetSummary,
            param: new
            {
                entry_date = request.EntryDate.Date,
                end_date = request.EndDate.Date
            }
        );

        var report = await dbConnection.QueryAsync<ReportResponse>(
            sqlGetConsolidated,
            param: new
            {
                entry_date = request.EntryDate.Date,
                end_date = request.EndDate.Date
            }
        );

        return Result<ReportsResponse>.Success(
            new ReportsResponse
            (
                Reports: report,
                TotalIn: summary.totalIn,
                TotalOut: summary.totalOut
            )    
        );
    }
}