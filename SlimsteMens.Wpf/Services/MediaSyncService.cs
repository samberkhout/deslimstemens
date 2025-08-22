using System;
using CommunityToolkit.Mvvm.ComponentModel;

namespace SlimsteMens.Wpf.Services;

public partial class MediaSyncService : ObservableObject
{
    [ObservableProperty]
    private string? _mediaPath;

    [ObservableProperty]
    private bool _isPlaying;

    [ObservableProperty]
    private TimeSpan _position;
}
