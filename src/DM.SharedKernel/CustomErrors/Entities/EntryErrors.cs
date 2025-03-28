using DM.SharedKernel.Common;

namespace DM.SharedKernel.CustomErrors.Entities;

public static class EntryErrors
{
    public static readonly Error NotFound = new(
        Code: "Entry.NotFound",
        Description: "Entry was not found on our system."
    );

    public static readonly Error IsEmpty = new(
        Code: "Entry.IsEmpty",
        Description: "Theres no one entry in our database"
    );
}
