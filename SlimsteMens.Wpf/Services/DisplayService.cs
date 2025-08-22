using System;
using System.Linq;
using System.Windows;
using System.Windows.Forms;
using Microsoft.Win32;

namespace SlimsteMens.Wpf.Services;

public class DisplayService
{
    public DisplayService()
    {
        SystemEvents.DisplaySettingsChanged += (_, _) => OnDisplaySettingsChanged?.Invoke();
    }

    public event Action? OnDisplaySettingsChanged;

    public void ShowOnSecondary(Window window)
    {
        var screens = Screen.AllScreens;
        var target = screens.FirstOrDefault(s => !s.Primary) ?? screens.First();
        var area = target.WorkingArea;

        window.WindowStyle = WindowStyle.None;
        window.ResizeMode = ResizeMode.NoResize;
        window.Topmost = true;
        window.Cursor = System.Windows.Input.Cursors.None;
        window.Left = area.Left;
        window.Top = area.Top;
        window.Width = area.Width;
        window.Height = area.Height;
        window.WindowState = WindowState.Maximized;
    }
}
