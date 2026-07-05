using System.IO;
using System.Text.Json;

namespace FileBrowserDesktop;

public sealed class ProfileStore
{
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        WriteIndented = true,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
    };

    public string? SelectedProfileId { get; set; }
    public List<ConnectionProfile> Profiles { get; set; } = [];

    public static string DirectoryPath => Path.Combine(GetConfigRoot(), "FileBrowserDesktop");

    public static string FilePath => Path.Combine(DirectoryPath, "profiles.json");

    public static ProfileStore Load()
    {
        try
        {
            if (!File.Exists(FilePath))
            {
                return new ProfileStore();
            }

            var json = File.ReadAllText(FilePath);
            return JsonSerializer.Deserialize<ProfileStore>(json, JsonOptions) ?? new ProfileStore();
        }
        catch
        {
            return new ProfileStore();
        }
    }

    public void Save()
    {
        Directory.CreateDirectory(DirectoryPath);
        File.WriteAllText(FilePath, JsonSerializer.Serialize(this, JsonOptions));
    }

    public ConnectionProfile? GetSelectedProfile()
    {
        if (!string.IsNullOrWhiteSpace(SelectedProfileId))
        {
            var selected = Profiles.FirstOrDefault(profile => profile.Id == SelectedProfileId);
            if (selected is not null)
            {
                return selected;
            }
        }

        return Profiles.FirstOrDefault();
    }

    private static string GetConfigRoot()
    {
        var home = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        if (string.IsNullOrWhiteSpace(home))
        {
            home = Environment.GetEnvironmentVariable("HOME") ?? ".";
        }

        if (OperatingSystem.IsMacOS())
        {
            return Path.Combine(home, "Library", "Application Support");
        }

        var xdgConfigHome = Environment.GetEnvironmentVariable("XDG_CONFIG_HOME");
        if (!string.IsNullOrWhiteSpace(xdgConfigHome))
        {
            return xdgConfigHome;
        }

        return Path.Combine(home, ".config");
    }
}
