using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using SlimsteMens.Wpf.Models;

namespace SlimsteMens.Wpf.Services;

public class GameRepository
{
    private readonly JsonSerializerOptions _options = new()
    {
        WriteIndented = true,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
    };

    public async Task SaveAsync(string path, Game game)
    {
        await using FileStream stream = File.Create(path);
        await JsonSerializer.SerializeAsync(stream, game, _options);
    }

    public async Task<Game> LoadAsync(string path)
    {
        await using FileStream stream = File.OpenRead(path);
        var game = await JsonSerializer.DeserializeAsync<Game>(stream, _options);
        return game ?? new Game();
    }
}
