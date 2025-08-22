using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SlimsteMens.Maui.Models;
using SlimsteMens.Maui.Services;

namespace SlimsteMens.Maui.ViewModels;

public partial class PresenterViewModel : BaseViewModel
{
    readonly GameRepository _repository;
    readonly DisplayService _displayService;

    [ObservableProperty]
    Game? game;

    public PresenterViewModel(GameRepository repository, DisplayService displayService)
    {
        _repository = repository;
        _displayService = displayService;
    }

    [RelayCommand]
    public async Task LoadGameAsync() => Game = await _repository.LoadAsync();

    [RelayCommand]
    public void ShowAudience() => _displayService.ShowAudienceWindow();
}
