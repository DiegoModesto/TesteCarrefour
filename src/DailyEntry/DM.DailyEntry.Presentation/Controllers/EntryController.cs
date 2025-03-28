using DM.Application.Entries;
using DM.Application.Entries.Commands.CreateEntry;
using DM.Application.Entries.Queries.GetEntries;
using DM.Application.Entries.Queries.GetEntryById;
using DM.SharedKernel.Common;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DM.Presentation.Controllers;

[ApiController]
public class EntryController : ApiController
{
    [HttpGet]
    [ProducesResponseType(typeof(EntriesResponse), statusCode: StatusCodes.Status200OK)]
    [ProducesResponseType(statusCode: StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Get(CancellationToken cancellationToken)
    {
        var query = new GetEntriesQuery();

        var entries = await Sender.Send(query, cancellationToken);

        return entries.IsSuccess 
            ? Ok(entries.ResultSet) 
            : NoContent();
    }

    [HttpGet(template: "{id}")]
    [ProducesResponseType(typeof(EntryResponse), statusCode: StatusCodes.Status200OK)]
    [ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get(Guid id, CancellationToken cancellationToken)
    {
        var query = new GetEntryByIdQuery(id);

        var entry = await Sender.Send(query, cancellationToken);
        
        return entry.IsSuccess
            ? Ok(entry.ResultSet)
            : NotFound();
    }

    [HttpPost]
    [ProducesResponseType(typeof(Result<string>), statusCode: StatusCodes.Status201Created)]
    [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create(
        [FromBody] CreateEntryCommand request,
        CancellationToken cancellationToken
    )
    {
        var command = request.Adapt<CreateEntryCommand>();

        var response = await Sender.Send(command, cancellationToken);
        
        return response.IsSuccess
            ? Ok(response)
            : BadRequest();
    }
}
