namespace Tango.Api.WaniKani;

public class WaniKaniSource
{
    public required string Value { get; init; }
    public required int SubjectId { get; init; }
    // NOTE: 5 is passed (Guru 1), 9 is burned
    public required int SrsStage { get; init; }
}