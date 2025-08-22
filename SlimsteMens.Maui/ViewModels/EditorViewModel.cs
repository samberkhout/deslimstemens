using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SlimsteMens.Maui.Models;
using SlimsteMens.Maui.Services;

namespace SlimsteMens.Maui.ViewModels;

public partial class EditorViewModel : BaseViewModel
{
    readonly GameRepository _repository;

    [ObservableProperty]
    Game game = new();

    public EditorViewModel(GameRepository repository) => _repository = repository;

    [RelayCommand]
    public async Task SaveAsync() => await _repository.SaveAsync(Game);
}
