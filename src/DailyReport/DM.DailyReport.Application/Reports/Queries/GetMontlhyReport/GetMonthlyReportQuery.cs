using DM.Application.Abstractions;
using DM.Application.Entries;
using DM.SharedKernel.Common;

namespace DM.Application.Reports.Queries.GetMontlhyReport;

public sealed record GetMonthlyReportQuery(DateTimeOffset EntryDate, DateTimeOffset EndDate) : IQuery<Result<ReportsResponse>>;
