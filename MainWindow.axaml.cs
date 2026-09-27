using Avalonia.Controls;
using Avalonia.Interactivity;

namespace MultiTimerApp;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void AddTimer_Click(object? sender, RoutedEventArgs e)
    {
        // Creates a new timer widget and adds it to the window
        TimersPanel.Children.Add(new TimerWidget());
    }
}