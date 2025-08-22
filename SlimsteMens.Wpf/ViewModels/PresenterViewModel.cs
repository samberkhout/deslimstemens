using System;
using System.Windows.Threading;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SlimsteMens.Wpf.Models;

namespace SlimsteMens.Wpf.ViewModels;

public partial class PresenterViewModel : ObservableObject
{
    private readonly DispatcherTimer _timer = new();

    public Game Game { get; } = new();

    [ObservableProperty]
    private int _roundIndex;

    [ObservableProperty]
    private int _questionIndex;

    [ObservableProperty]
    private int _remainingSeconds;

    [ObservableProperty]
    private bool _isTimerRunning;

    public PresenterViewModel()
    {
        _timer.Interval = TimeSpan.FromSeconds(1);
        _timer.Tick += (_, _) =>
        {
            if (RemainingSeconds > 0)
                RemainingSeconds--;
            else
                StopTimer();
        };
    }

    [RelayCommand]
    private void NextQuestion()
    {
        if (RoundIndex >= Game.Rounds.Count) return;
        var round = Game.Rounds[RoundIndex];
        if (QuestionIndex < round.Questions.Count - 1)
            QuestionIndex++;
    }

    [RelayCommand]
    private void PrevQuestion()
    {
        if (QuestionIndex > 0)
            QuestionIndex--;
    }

    [RelayCommand]
    private void StartTimer()
    {
        if (IsTimerRunning) return;
        IsTimerRunning = true;
        _timer.Start();
    }

    [RelayCommand]
    private void StopTimer()
    {
        if (!IsTimerRunning) return;
        IsTimerRunning = false;
        _timer.Stop();
    }

    [RelayCommand]
    private void ResetTimer() => RemainingSeconds = 0;
}
