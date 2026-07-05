using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Media;

namespace FileBrowserDesktop;

internal sealed class UnsupportedPlatformWindow : Window
{
    public UnsupportedPlatformWindow()
    {
        Title = "File Browser Desktop";
        Width = 520;
        Height = 260;
        MinWidth = 520;
        MinHeight = 260;
        CanResize = false;
        WindowStartupLocation = WindowStartupLocation.CenterScreen;

        var closeButton = new Button
        {
            Content = "Close",
            Width = 110,
            Height = 34,
            HorizontalAlignment = HorizontalAlignment.Right,
        };
        closeButton.Click += (_, _) => Close();

        Content = new Border
        {
            Padding = new Thickness(28),
            Background = new SolidColorBrush(Color.Parse("#0F172A")),
            Child = new StackPanel
            {
                Spacing = 16,
                Children =
                {
                    new TextBlock
                    {
                        Text = "This edition is for macOS and Linux.",
                        FontSize = 22,
                        FontWeight = FontWeight.SemiBold,
                        Foreground = Brushes.White,
                    },
                    new TextBlock
                    {
                        Text = "File Browser Desktop Mac/Linux does not run on Windows. Use the Windows edition instead.",
                        TextWrapping = TextWrapping.Wrap,
                        Foreground = new SolidColorBrush(Color.Parse("#CBD5E1")),
                    },
                    closeButton,
                },
            },
        };
    }
}
