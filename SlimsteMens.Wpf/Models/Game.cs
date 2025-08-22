using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;

namespace SlimsteMens.Wpf.Models;

public partial class Game : ObservableObject
{
    [ObservableProperty]
    private ObservableCollection<Round> _rounds = new();

    [ObservableProperty]
    private ObservableCollection<Team> _teams = new();
}
