using System.Windows;
using SlimsteMens.Wpf.Services;
using SlimsteMens.Wpf.ViewModels;

namespace SlimsteMens.Wpf.Views;

public partial class PresenterWindow : Window
{
    private readonly AudienceWindow _audience;
    private readonly DisplayService _displayService = new();

    public PresenterViewModel ViewModel { get; } = new();

    public PresenterWindow()
    {
        InitializeComponent();
        DataContext = ViewModel;
        _audience = new AudienceWindow(ViewModel);
        Loaded += (_, _) => _displayService.ShowOnSecondary(_audience);
        _audience.Show();
    }
}
