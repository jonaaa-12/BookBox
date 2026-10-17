namespace BookBox.Application.Features.UserBooks;

public static class ReadingStatus
{
    public const string Pending = "Pending";
    public const string Reading = "Reading";
    public const string Finished = "Finished";

    public static readonly string[] All = { Pending, Reading, Finished };
}
