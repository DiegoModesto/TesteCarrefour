namespace DM.Application.Entries;

public sealed record EntriesResponse(IEnumerable<EntryResponse> Responses);
