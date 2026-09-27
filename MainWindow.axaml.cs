using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Layout;

namespace MultiTimerApp;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void BtnAdd_Click(object? sender, RoutedEventArgs e)
    {
        // Default quick-add creates a stopwatch
        TimersPanel.Children.Add(new TimerWidget(false));
    }

    private void NewStopwatch_Click(object? sender, RoutedEventArgs e)
    {
        TimersPanel.Children.Add(new TimerWidget(false));
    }

    private void NewCountdown_Click(object? sender, RoutedEventArgs e)
    {
        TimersPanel.Children.Add(new TimerWidget(true));
    }

    private void LayoutHorizontal_Click(object? sender, RoutedEventArgs e)
    {
        // WrapPanel will seat them side-by-side like text words
        TimersPanel.Orientation = Orientation.Horizontal;
    }

    private void LayoutPortrait_Click(object? sender, RoutedEventArgs e)
    {
        // WrapPanel will force them to stack up and down strictly
        TimersPanel.Orientation = Orientation.Vertical;
    }
}