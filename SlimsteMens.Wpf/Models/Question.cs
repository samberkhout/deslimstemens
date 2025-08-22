using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;

namespace SlimsteMens.Wpf.Models;

public partial class Question : ObservableObject
{
    [ObservableProperty]
    private string _text = string.Empty;

    [ObservableProperty]
    private ObservableCollection<AnswerOption> _options = new();

    [ObservableProperty]
    private string? _mediaPath;
}
