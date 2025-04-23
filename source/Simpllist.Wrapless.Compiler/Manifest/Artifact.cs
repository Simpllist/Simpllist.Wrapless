namespace Simpllist.Manifest;

/// <summary>
/// 
/// </summary>
/// <param name="UspFile"></param>

public sealed record Artifact(string UspFile)
{
    public string UshFile => UspFile.Replace(".usp", ".ush");
};