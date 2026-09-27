using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Threading;
using Avalonia;
using System;

namespace MultiTimerApp;

public partial class TimerWidget : UserControl
{
    private DispatcherTimer _timer;
    private TimeSpan _currentTime;
    private bool _isCountdown;

    public TimerWidget() : this(false) { }

    public TimerWidget(bool isCountdown)
    {
        InitializeComponent();
        _isCountdown = isCountdown;
        
        if (_isCountdown && CountdownSetupPanel != null)
        {
            CountdownSetupPanel.IsVisible = true;
        }

        _timer = new DispatcherTimer
        {
            Interval = TimeSpan.FromSeconds(1)
        };
        _timer.Tick += Timer_Tick;
    }

    private void OnSizeChanged(object? sender, SizeChangedEventArgs e)
    {
        // Responsive logic: If the application width is squeezed to "pen width" (less than 140px), 
        // hide the input boxes and buttons. Show ONLY the running timer text.
        bool isSquished = e.NewSize.Width < 230;

        if (TbName != null) TbName.IsVisible = !isSquished;
        if (ControlsPanel != null) ControlsPanel.IsVisible = !isSquished;
        
        if (_isCountdown && CountdownSetupPanel != null)
        {
            CountdownSetupPanel.IsVisible = !isSquished && _currentTime.TotalSeconds == 0;
        }

        // Remove the extra padding when squished to make it fit perfectly
        if (RootBorder != null)
        {
            RootBorder.Padding = isSquished ? new Thickness(2, 5) : new Thickness(10);
        }
    }

    private void Timer_Tick(object? sender, EventArgs e)
    {
        if (_isCountdown)
        {
            if (_currentTime.TotalSeconds > 0)
                _currentTime = _currentTime.Subtract(TimeSpan.FromSeconds(1));
            else
                _timer.Stop();
        }
        else
        {
            _currentTime = _currentTime.Add(TimeSpan.FromSeconds(1));
        }
        UpdateDisplay();
    }

    private void BtnStart_Click(object? sender, RoutedEventArgs e)
    {
        if (_isCountdown && _currentTime.TotalSeconds == 0)
        {
            if (int.TryParse(TbMinutes.Text, out int mins))
            {
                _currentTime = TimeSpan.FromMinutes(mins);
            }
        }
        
        // Hide the minute setup box when running to save space
        if (_isCountdown && CountdownSetupPanel != null) CountdownSetupPanel.IsVisible = false;
        
        _timer.Start();
        UpdateDisplay();
    }

    private void BtnStop_Click(object? sender, RoutedEventArgs e)
    {
        _timer.Stop();
    }

    private void BtnReset_Click(object? sender, RoutedEventArgs e)
    {
        _timer.Stop();
        _currentTime = TimeSpan.Zero;
        
        if (_isCountdown && TbMinutes != null && int.TryParse(TbMinutes.Text, out int mins))
        {
            _currentTime = TimeSpan.FromMinutes(mins);
            if (CountdownSetupPanel != null) CountdownSetupPanel.IsVisible = true;
        }
        UpdateDisplay();
    }

    private void UpdateDisplay()
    {
        if (TxtDisplay != null)
        {
            TxtDisplay.Text = _currentTime.ToString(@"hh\:mm\:ss");
        }
    }
}