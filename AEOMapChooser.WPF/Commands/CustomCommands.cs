using System.Windows.Input;

namespace AEOMapChooser.WPF.Commands
{
    public static class CustomCommands
    {
        public static readonly RoutedUICommand GenerateRounds = new RoutedUICommand
        (
            "Generate rounds",
            "Generate rounds",
            typeof(CustomCommands),
            null
        );

        public static readonly RoutedUICommand CopyRoundsAsMarkdown = new RoutedUICommand
        (
            "Copy as markdown",
            "Copy as markdown",
            typeof(CustomCommands),
            null
        );
    }
}
