using System;
using System.Collections.Generic;

namespace Simpllist.Context;

public record TargetLibraryCaptureContext(
    string LibraryName,
    string? DealerName,
    string? SystemName,
    string? Programmer,
    string? Comments,
    string? PdfHelpFile);

internal class TargetLibraryCaptureContextComparer : IEqualityComparer<TargetLibraryCaptureContext>
{
    private static readonly Lazy<TargetLibraryCaptureContextComparer> Lazy = new(() => new TargetLibraryCaptureContextComparer());
    public static TargetLibraryCaptureContextComparer Instance => Lazy.Value;

    public bool Equals(TargetLibraryCaptureContext? x, TargetLibraryCaptureContext? y)
    {
        return x == y;
    }

    public int GetHashCode(TargetLibraryCaptureContext? obj)
    {
        var hashCode = obj != null ? obj.GetHashCode() : 0;
        return hashCode;
    }
}