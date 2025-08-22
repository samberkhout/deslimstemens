namespace SlimsteMens.Maui.Services;

public class MediaSyncService
{
    public string? MediaPath { get; set; }
    public bool IsPlaying { get; set; }
    public TimeSpan Position { get; set; }
}
