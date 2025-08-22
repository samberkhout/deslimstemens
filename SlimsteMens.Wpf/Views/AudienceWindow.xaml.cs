using System.Windows;
using SlimsteMens.Wpf.ViewModels;

namespace SlimsteMens.Wpf.Views;

public partial class AudienceWindow : Window
{
    public AudienceWindow(PresenterViewModel presenter)
    {
        InitializeComponent();
        DataContext = new AudienceViewModel(presenter);
    }
}
