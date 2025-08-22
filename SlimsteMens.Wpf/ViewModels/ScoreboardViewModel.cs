using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SlimsteMens.Wpf.Models;

namespace SlimsteMens.Wpf.ViewModels;

public partial class ScoreboardViewModel : ObservableObject
{
    public ObservableCollection<Team> Teams { get; } = new();

    [ObservableProperty]
    private Team? _activeTeam;

    [RelayCommand]
    public void AddSeconds(Team team, int seconds)
    {
        team.Seconds += seconds;
    }

    [RelayCommand]
    public void Reset()
    {
        foreach (var team in Teams)
            team.Seconds = 0;
    }
}
