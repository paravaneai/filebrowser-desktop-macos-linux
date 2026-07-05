using Avalonia;
using System.Threading;

namespace FileBrowserDesktop;

internal static class Program
{
    private const string SingleInstanceMutexName = "ParavaneLabs.FileBrowserDesktop.MacOsLinux";

    private static Mutex? singleInstanceMutex;
    private static bool ownsSingleInstanceMutex;

    [STAThread]
    public static void Main(string[] args)
    {
        App.StartupArgs = args;

        if (!IsSplashPreview(args) && !TryAcquireSingleInstance())
        {
            return;
        }

        try
        {
            BuildAvaloniaApp()
                .StartWithClassicDesktopLifetime(args);
        }
        finally
        {
            ReleaseSingleInstance();
        }
    }

    public static AppBuilder BuildAvaloniaApp()
    {
        return AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .WithInterFont()
            .LogToTrace();
    }

    private static bool IsSplashPreview(string[] args)
    {
        return args.Any(arg =>
            string.Equals(arg, "--splash-preview", StringComparison.OrdinalIgnoreCase) ||
            string.Equals(arg, "/splash-preview", StringComparison.OrdinalIgnoreCase));
    }

    private static bool TryAcquireSingleInstance()
    {
        singleInstanceMutex = new Mutex(false, SingleInstanceMutexName);

        try
        {
            ownsSingleInstanceMutex = singleInstanceMutex.WaitOne(0);
            return ownsSingleInstanceMutex;
        }
        catch (AbandonedMutexException)
        {
            ownsSingleInstanceMutex = true;
            return true;
        }
    }

    private static void ReleaseSingleInstance()
    {
        if (ownsSingleInstanceMutex)
        {
            singleInstanceMutex?.ReleaseMutex();
        }

        singleInstanceMutex?.Dispose();
    }
}
