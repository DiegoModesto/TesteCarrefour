using DM.Application.Entries;
using DM.Application.Reports.Queries.GetMontlhyReport;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DM.Presentation.Controllers;

[ApiController]
public class ReportController : ApiController
{
    [HttpGet]
    [ProducesResponseType(typeof(ReportsResponse), statusCode: StatusCodes.Status200OK)]
    [ProducesResponseType(statusCode: StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Get(
        [FromQuery] DateTimeOffset initialDate,
        [FromQuery] DateTimeOffset endDate,
        CancellationToken cancellationToken
    )
    {
        var query = new GetMonthlyReportQuery(initialDate, endDate);

        var reports = await Sender.Send(query, cancellationToken);

        return reports.IsSuccess 
            ? Ok(reports) 
            : NoContent();
    }
}
