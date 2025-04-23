using System.Text.Json;

namespace Simpllist.Manifest;

/// <summary>
/// 
/// </summary>
/// <param name="Directory"></param>
/// <param name="Files"></param>
public sealed record Artifacts(string Directory, ICollection<Artifact> Files)
{
    private const string WraplessManifestJson = "wrapless_manifest.json";
    private const string SplsWork = "SPlsWork";

    public string SplsBinaryDirectory => Path.Combine(Directory, SplsWork);

    public static async Task SaveArtifactsFile(string directory, ICollection<Artifact> files)
    {
        var path = Path.Combine(directory, WraplessManifestJson);
        await File.WriteAllBytesAsync(path, JsonSerializer.SerializeToUtf8Bytes(new Artifacts(directory, files)));
    }

    public static async Task<Artifacts?> LoadArtifactsFile(string directory)
    {
        var path = Path.Combine(directory, WraplessManifestJson);

        if (!File.Exists(path))
        {
            return null;
        }

        var bytes = await File.ReadAllBytesAsync(path);
        var artifacts = JsonSerializer.Deserialize<Artifacts>(bytes);

        return artifacts;
    }

    public void CleanArtifacts()
    {
        if (System.IO.Directory.Exists(SplsBinaryDirectory))
        {
            System.IO.Directory.Delete(SplsBinaryDirectory, true);
        }

        foreach (var artifact in Files)
        {
            if (File.Exists(artifact.UshFile))
            {
                File.Delete(artifact.UshFile);
            }

            if (File.Exists(artifact.UspFile))
            {
                File.Delete(artifact.UspFile);
            }
        }
    }
};