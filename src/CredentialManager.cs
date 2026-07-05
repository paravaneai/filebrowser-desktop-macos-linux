namespace FileBrowserDesktop;

internal sealed record StoredCredential(string Username, string Password);

internal static class CredentialManager
{
    public static bool IsSupported => false;

    public static string FileBrowserTargetName(string profileId)
    {
        return $"FileBrowserDesktop/FileBrowser/{profileId}";
    }

    public static StoredCredential? ReadFileBrowserCredential(string profileId)
    {
        return null;
    }

    public static void WriteFileBrowserCredential(string profileId, string username, string password)
    {
        throw new PlatformNotSupportedException("Credential saving is disabled until macOS Keychain and Linux Secret Service support are implemented.");
    }

    public static void DeleteFileBrowserCredential(string profileId)
    {
        // No credential backend is active in the macOS/Linux edition yet.
    }
}
