namespace EffectiveMobile.AdPlatforms.Domain.Models;

public static class Errors
{
    public static Error InvalidFileSplitSymbol(int lineNumber) =>
        new Error("001", $"Invalid file format: only 1 symbol ':' need on line {lineNumber}");

    public static Error InvalidFileFormatLocation(int lineNumber) =>
        new Error("002", $"Invalid file format: missing '/' before location on {lineNumber}");

    public static Error MissingLocationParameter { get; } =
        new Error("003", "Location parameter is required");
    
    public static Error TaskCanceled { get; } =
        new Error("999", "Task cancelled");
}