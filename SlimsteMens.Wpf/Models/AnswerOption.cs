using CommunityToolkit.Mvvm.ComponentModel;

namespace SlimsteMens.Wpf.Models;

public partial class AnswerOption : ObservableObject
{
    [ObservableProperty]
    private string _text = string.Empty;

    [ObservableProperty]
    private bool _isCorrect;
}
