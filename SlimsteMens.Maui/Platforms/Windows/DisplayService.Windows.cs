using Microsoft.UI;
using Microsoft.UI.Windowing;
using SlimsteMens.Maui.Windows;

namespace SlimsteMens.Maui.Services;

public partial class DisplayService
{
    public partial void ShowAudienceWindow()
    {
        var window = new AudienceWindow();
        window.Activate();
        if (AppWindow.TryGetFromWindowId(Win32Interop.GetWindowIdFromWindow(window.GetWindowHandle()), out var appWindow))
        {
            appWindow.SetPresenter(AppWindowPresenterKind.FullScreen);
        }
    }
}
