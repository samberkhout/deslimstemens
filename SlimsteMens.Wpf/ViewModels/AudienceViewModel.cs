using CommunityToolkit.Mvvm.ComponentModel;

namespace SlimsteMens.Wpf.ViewModels;

public partial class AudienceViewModel : ObservableObject
{
    public PresenterViewModel Presenter { get; }

    public AudienceViewModel(PresenterViewModel presenter)
    {
        Presenter = presenter;
    }
}
