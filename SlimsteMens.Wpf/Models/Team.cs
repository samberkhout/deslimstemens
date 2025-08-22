using CommunityToolkit.Mvvm.ComponentModel;

namespace SlimsteMens.Wpf.Models;

public partial class Team : ObservableObject
{
    [ObservableProperty]
    private string _name = string.Empty;

    [ObservableProperty]
    private int _seconds;
}
