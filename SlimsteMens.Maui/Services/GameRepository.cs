using System.Text.Json;
using SlimsteMens.Maui.Models;

namespace SlimsteMens.Maui.Services;

public class GameRepository
{
    const string SamplePath = "Data/game.sample.json";

    public async Task<Game?> LoadAsync(string path = SamplePath)
    {
        if (!File.Exists(path)) return null;
        using var stream = File.OpenRead(path);
        return await JsonSerializer.DeserializeAsync<Game>(stream);
    }

    public async Task SaveAsync(Game game, string path = SamplePath)
    {
        using var stream = File.Create(path);
        await JsonSerializer.SerializeAsync(stream, game, new JsonSerializerOptions { WriteIndented = true });
    }
}
