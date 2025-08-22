using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SlimsteMens.Wpf.Models;
using SlimsteMens.Wpf.Services;

namespace SlimsteMens.Wpf.ViewModels;

public partial class EditorViewModel : ObservableObject
{
    private readonly GameRepository _repository;

    [ObservableProperty]
    private Game _game = new();

    public EditorViewModel(GameRepository repository)
    {
        _repository = repository;
    }

    [RelayCommand]
    public Task SaveAsync(string path) => _repository.SaveAsync(path, Game);

    [RelayCommand]
    public async Task LoadAsync(string path) => Game = await _repository.LoadAsync(path);
}
