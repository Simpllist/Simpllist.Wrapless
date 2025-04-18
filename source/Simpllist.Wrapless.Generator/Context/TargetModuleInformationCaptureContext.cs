using System;
using System.Collections.Generic;

namespace Simpllist.Context;

public class TargetModuleInformationCaptureContext : IComparable<TargetModuleInformationCaptureContext>, IEquatable<TargetModuleInformationCaptureContext>
{
    public string? DealerName { get; set; }
    public string ModuleName { get; set; }

    public TargetModuleInformationCaptureContext(
        string moduleName,
        string? dealerName)
    {
        ModuleName = moduleName;
        DealerName = dealerName;
    }

    public int CompareTo(TargetModuleInformationCaptureContext? other)
    {
        if (other == null) return -1;

        if (other.ModuleName != ModuleName) return -1;
        if (other.DealerName != DealerName) return -1;

        return 0;
    }

    public bool Equals(TargetModuleInformationCaptureContext? other)
    {
        if (other == null) return false;

        if (other.ModuleName != ModuleName) return false;
        if (other.DealerName != DealerName) return false;

        return true;
    }
}

internal class TargetModuleInformationCaptureComparer : IEqualityComparer<TargetModuleInformationCaptureContext>
{
    private static readonly Lazy<TargetModuleInformationCaptureComparer> _lazy = new(() => new TargetModuleInformationCaptureComparer());
    public static TargetModuleInformationCaptureComparer Instance => _lazy.Value;

    public bool Equals(TargetModuleInformationCaptureContext? x, TargetModuleInformationCaptureContext? y)
    {
        if (ReferenceEquals(x, y)) return true;
        if (ReferenceEquals(x, null)) return false;
        if (ReferenceEquals(y, null)) return false;
        if (x.GetType() != y.GetType()) return false;

        return x.Equals(y);
    }

    public int GetHashCode(TargetModuleInformationCaptureContext? obj)
    {
        unchecked
        {
            var hashCode = obj?.DealerName != null ? obj.DealerName.GetHashCode() : 0;
            hashCode = hashCode * 397 ^ (obj?.ModuleName != null ? obj.ModuleName.GetHashCode() : 0);
           
            return hashCode;
        }
    }
}