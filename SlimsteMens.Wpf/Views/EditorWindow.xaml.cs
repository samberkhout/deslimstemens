using System.Windows;
using SlimsteMens.Wpf.Services;
using SlimsteMens.Wpf.ViewModels;

namespace SlimsteMens.Wpf.Views;

public partial class EditorWindow : Window
{
    public EditorWindow()
    {
        InitializeComponent();
        DataContext = new EditorViewModel(new GameRepository());
    }
}
