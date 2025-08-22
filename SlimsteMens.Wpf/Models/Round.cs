using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;

namespace SlimsteMens.Wpf.Models;

public partial class Round : ObservableObject
{
    [ObservableProperty]
    private string _name = string.Empty;

    [ObservableProperty]
    private RoundType _type;

    [ObservableProperty]
    private ObservableCollection<Question> _questions = new();
}
