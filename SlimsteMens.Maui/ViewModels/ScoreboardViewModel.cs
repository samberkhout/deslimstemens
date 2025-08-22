using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SlimsteMens.Maui.Models;

namespace SlimsteMens.Maui.ViewModels;

public partial class ScoreboardViewModel : BaseViewModel
{
    [ObservableProperty]
    public partial Team? ActiveTeam { get; set; }

    public ObservableCollection<Team> Teams { get; } = new();

    [RelayCommand]
    void AddSeconds(int seconds)
    {
        if (ActiveTeam is null) return;
        ActiveTeam.Seconds += seconds;
        OnPropertyChanged(nameof(Teams));
    }
}
