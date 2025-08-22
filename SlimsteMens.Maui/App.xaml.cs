using Microsoft.Maui.Controls;

namespace SlimsteMens.Maui;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();
        MainPage = new NavigationPage(new Pages.PresenterPage());
    }
}
