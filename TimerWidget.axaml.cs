using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Threading;
using System;

namespace MultiTimerApp;

public partial class TimerWidget : UserControl
{
    private DispatcherTimer _timer;
    private TimeSpan _currentTime;
    private bool _isCountdown;

    public TimerWidget()
    {
        InitializeComponent();
        _timer = new DispatcherTimer
        {
            Interval = TimeSpan.FromSeconds(1)
        };
        _timer.Tick += Timer_Tick;
    }

    private void ModeChanged(object? sender, RoutedEventArgs e)
    {
        if (RbCountdown?.IsChecked == true)
        {
            _isCountdown = true;
            if (CountdownSetupPanel != null) CountdownSetupPanel.IsVisible = true;
        }
        else
        {
            _isCountdown = false;
            if (CountdownSetupPanel != null) CountdownSetupPanel.IsVisible = false;
        }
        ResetTimer();
    }

    private void Timer_Tick(object? sender, EventArgs e)
    {
        if (_isCountdown)
        {
            if (_currentTime.TotalSeconds > 0)
            {
                _currentTime = _currentTime.Subtract(TimeSpan.FromSeconds(1));
            }
            else
            {
                _timer.Stop(); // Stops at 00:00:00
            }
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
        _timer.Start();
        UpdateDisplay();
    }

    private void BtnStop_Click(object? sender, RoutedEventArgs e)
    {
        _timer.Stop();
    }

    private void BtnReset_Click(object? sender, RoutedEventArgs e)
    {
        ResetTimer();
    }

    private void ResetTimer()
    {
        _timer?.Stop();
        _currentTime = TimeSpan.Zero;
        
        if (_isCountdown && TbMinutes != null && int.TryParse(TbMinutes.Text, out int mins))
        {
            _currentTime = TimeSpan.FromMinutes(mins);
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