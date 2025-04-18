using System;
using System.Collections.Generic;

namespace Simpllist.Context;

public record TargetModuleInformationCaptureContext(
    string ModuleName,
    string? DealerName,
    string? SystemName,
    string? Programmer,
    string? Comments,
    string? PdfHelpFile);

internal class TargetModuleInformationCaptureComparer : IEqualityComparer<TargetModuleInformationCaptureContext>
{
    private static readonly Lazy<TargetModuleInformationCaptureComparer> Lazy = new(() => new TargetModuleInformationCaptureComparer());
    public static TargetModuleInformationCaptureComparer Instance => Lazy.Value;

    public bool Equals(TargetModuleInformationCaptureContext? x, TargetModuleInformationCaptureContext? y)
    {
        return x == y;
    }

    public int GetHashCode(TargetModuleInformationCaptureContext? obj)
    {
        var hashCode = obj != null ? obj.GetHashCode() : 0;
        return hashCode;
    }
}