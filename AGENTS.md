# CLAUDE\_MAUI.md – .NET MAUI Twee‑scherm Quizapp (Presenter + Audience) met Editor, Scorebord, Finale & Media‑sync

> **Doel**: Bouw een **.NET MAUI (.NET 9)** app in de stijl van *De Slimste Mens* die primair op **Windows** draait met twee vensters (Presenter + Audience), en die later uitbreidbaar is naar Android/iOS (met platform‑specifieke beperkingen). Inclusief editor, scorebord, finale en media‑sync.

---

## 0) Acceptatiecriteria (MVP Windows)

* **Twee vensters**: `PresenterWindow` (primair scherm) en `AudienceWindow` (fullscreen op 2e scherm).
* **Realtime sync**: vraag/opties/timer/scorebord tussen Presenter ↔ Audience via gedeelde ViewModels/Services.
* **Editor**: CRUD rondes, vragen, opties; **JSON load/save**.
* **Finale**: aftelklok + knoppen ±10 s per team; winnaar bij 0 s.
* **Open Deur**: video/audio/beeld afgespeeld in Presenter, synchroon in Audience.
* **Styling**: donkere achtergrond, witte typografie, gouden accenten (tv‑look).
* **Hotkeys (Windows)**: ←/→, 1–8, Space, Enter, S/D, F11.

> **Opmerking**: Multi‑window + tweede monitor is **volledig ondersteund op Windows**. Op Android/iOS is een 2e beeldscherm zeldzaam en anders aangestuurd; zie §9.

---

## 1) Projectstructuur

```
SlimsteMens.Maui/
├─ SlimsteMens.Maui.csproj
├─ App.xaml
├─ App.xaml.cs
├─ Resources/
│  ├─ Styles/Colors.xaml        # palet (donker, wit, goud)
│  └─ Styles/Styles.xaml        # Buttons, Labels, Progress/Graphics
├─ Models/
│  ├─ Enums.cs                  # RoundType
│  ├─ AnswerOption.cs
│  ├─ Question.cs
│  ├─ Round.cs
│  ├─ Team.cs                   # Scorebord
│  └─ Game.cs
├─ Services/
│  ├─ GameRepository.cs         # JSON load/save (async)
│  ├─ MediaSyncService.cs       # MediaPath/IsPlaying/Position
│  ├─ DisplayService.cs         # IDisplayService + partial platform impl
│  └─ NavigationService.cs      # (optioneel) shell‑navigatie/VM route
├─ ViewModels/
│  ├─ BaseViewModel.cs
│  ├─ PresenterViewModel.cs
│  ├─ AudienceViewModel.cs
│  ├─ EditorViewModel.cs
│  └─ ScoreboardViewModel.cs
├─ Pages/
│  ├─ PresenterPage.xaml(.cs)
│  ├─ AudiencePage.xaml(.cs)
│  ├─ EditorPage.xaml(.cs)
│  ├─ ScoreboardPage.xaml(.cs)
│  └─ FinalePage.xaml(.cs)
├─ Windows/
│  ├─ PresenterWindow.cs        # MAUI Window wrapper voor PresenterPage
│  └─ AudienceWindow.cs         # MAUI Window wrapper voor AudiencePage
├─ Platforms/
│  ├─ Windows/
│  │  ├─ DisplayService.Windows.cs  # plaats Audience op 2e scherm met WinUI interop
│  │  └─ App.xaml.cs (gegenereerd door MAUI)
│  ├─ Android/
│  │  └─ DisplayService.Android.cs  # (optioneel) Presentation op externe display
│  └─ iOS/
│     └─ DisplayService.iOS.cs      # (optioneel) UIScreen/UIScene
└─ Data/
   ├─ game.sample.json          # Copy to Output: Copy always
   └─ schema.md
```

---

## 2) Tech stack & NuGet

* **.NET 9**, **.NET MAUI** (Single‑project).
* **CommunityToolkit.Mvvm** – MVVM (ObservableObject, RelayCommand).
* **CommunityToolkit.Maui** – extra controls/effects.
* **CommunityToolkit.Maui.MediaElement** – **MediaElement** voor video/audio (Open Deur).
* **System.Text.Json** – JSON load/save (geïndenteerd, ignore nulls).

In `MauiProgram.cs` registreren:

```csharp
builder.UseMauiApp<App>()
       .UseMauiCommunityToolkit()
       .UseMauiCommunityToolkitMediaElement();
```

---

## 3) Data‑model (identiek aan WPF/WinUI varianten)

```csharp
public enum RoundType { ThreeSixNine, OpenDoor, Puzzle, Finale }
public class AnswerOption { public string Text { get; set; } = string.Empty; public bool IsCorrect { get; set; } }
public class Question { public string Title { get; set; } = string.Empty; public List<AnswerOption> Options { get; set; } = new(); public string? MediaPath { get; set; } }
public class Round { public RoundType Type { get; set; } public string Name { get; set; } = string.Empty; public int TimeSeconds { get; set; } = 60; public List<Question> Questions { get; set; } = new(); }
public class Team { public string Name { get; set; } = "Team"; public int Seconds { get; set; } = 60; public bool IsActive { get; set; } }
public class Game { public string Title { get; set; } = "Quiz"; public List<Team> Teams { get; set; } = new(); public List<Round> Rounds { get; set; } = new(); }
```

---

## 4) Windows & multi‑window (Windows platform)

### 4.1 Twee vensters openen in MAUI

```csharp
// App.xaml.cs
protected override async void OnStart()
{
    var repo = new Services.GameRepository();
    var path = FileSystem.AppDataDirectory + "/game.sample.json"; // of meegeleverd uit Resources/Data
    var game = File.Exists(path) ? await repo.LoadAsync(path) : new Models.Game();

    var presenterVm = new ViewModels.PresenterViewModel(game);
    var audienceVm  = new ViewModels.AudienceViewModel(presenterVm);

    var presenterWin = new Windows.PresenterWindow(new Pages.PresenterPage { BindingContext = presenterVm });
    var audienceWin  = new Windows.AudienceWindow (new Pages.AudiencePage  { BindingContext = audienceVm  });

    // Open beide windows
    Application.Current?.OpenWindow(presenterWin);
    Application.Current?.OpenWindow(audienceWin);

    // Windows-specifiek: plaats Audience op 2e scherm fullscreen
    await Services.DisplayService.Instance.FullscreenAudienceOnSecondaryAsync(audienceWin);
}
```

### 4.2 Positioneren op 2e scherm (Windows)

In `Platforms/Windows/DisplayService.Windows.cs` gebruik **WinUI interop**:

```csharp
#if WINDOWS
using Microsoft.UI;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using WinRT.Interop;

namespace SlimsteMens.Maui.Services;

public partial class DisplayService
{
    public static DisplayService Instance { get; } = new();

    public async Task FullscreenAudienceOnSecondaryAsync(Window audienceWin)
    {
        var native = audienceWin.Handler!.PlatformView as Microsoft.UI.Xaml.Window;
        if (native is null) return;
        var hwnd = WindowNative.GetWindowHandle(native);
        var id = Win32Interop.GetWindowIdFromWindow(hwnd);
        var appWin = AppWindow.GetFromWindowId(id);

        var areas = DisplayArea.FindAll();
        var secondary = areas.FirstOrDefault(a => !a.IsPrimary) ?? areas.First();
        var wa = secondary.WorkArea;
        appWin.MoveAndResize(new Windows.Graphics.RectInt32(wa.X, wa.Y, wa.Width, wa.Height));
        if (appWin.Presenter is OverlappedPresenter ov) ov.SetBorderAndTitleBar(false, false);
        appWin.SetPresenter(AppWindowPresenterKind.FullScreen);
        await Task.CompletedTask;
    }
}
#endif
```

> **Note**: Dit werkt op **Windows 11**. Voor Windows 10 fallback: Maximize en handmatig verplaatsen.

---

## 5) Pages (UI)

* **PresenterPage**: Navigatie (prev/next), timer knoppen, vraag + opties (toggle correct), statusbalk (progress).
* **AudiencePage**: Grote vraag (48–64pt), opties (28–36pt), ✓ bij correct, subtiele animaties.
* **ScoreboardPage**: Teams met seconden, ±10 knoppen (Presenter) en grote weergave (Audience).
* **FinalePage**: ronde klok + ±10 per team; Audience toont centrale klok en standen.
* **EditorPage**: CRUD rondes/vragen/opties, tijd per ronde, MediaPath.

**Media (Open Deur)**: gebruik `CommunityToolkit.Maui.MediaElement` op beide pages; bind aan `MediaSyncService` (`MediaPath`, `IsPlaying`, `Position`). MVP: startsync bij vraagstart; optioneel periodieke pos‑sync.

---

## 6) ViewModels

* **PresenterViewModel**: `Game`, `RoundIndex`, `QuestionIndex`, `RemainingSeconds`, `IsTimerRunning` + Commands (`Next`, `Prev`, `Start`, `Stop`, `Reset`, `RevealAll`, `ResetAnswers`). Timer via `IDispatcherTimer`.
* **AudienceViewModel**: referentie naar PresenterVM/gedeelde state; alleen‑lezen bindings.
* **EditorViewModel**: `Game`, `SelectedRound`, `SelectedQuestion` + CRUD + Save/Load.
* **ScoreboardViewModel**: `ObservableCollection<Team>`, `ActiveTeam`, `AddSeconds(Team,int)`.

---

## 7) Styling (tv‑look)

* **Colors.xaml**: achtergrond `#0B0C10`, tekst `#FFFFFF`, accent `#FFD700`/`#FFAA00`.
* **Styles.xaml**: Buttons met CornerRadius, Labels met grote font sizes, `ProgressBar`/`GraphicsView` voor klok/balken.
* **Animaties**: `Fade`/`Scale` op opties; **Finale‑klok** via `GraphicsView` of `ProgressBar` met converter.

---

## 8) Hotkeys (Windows)

In `Platforms/Windows` kun je keyboard events koppelen aan de actieve `PresenterWindow` en commands oproepen (via handler events).

Mapping: Left/Right → Prev/Next; D1..D8 → toggle; Space → Start/Stop; Enter → Next; S/D → −10/+10; F11 → opnieuw fullscreen.

---

## 9) Platform‑notities (Android/iOS)

* **Android**: externe displays via `DisplayManager` + `Presentation`; niet gegarandeerd beschikbaar. Voor nu negeren of placeholder melding.
* **iOS**: extra scherm via `UIScreen`/`UIWindow` met scenes; zeldzaam. Voor nu negeren of placeholder.

> MVP richt zich op **Windows**. Houd de services **partial** zodat later per platform specifieke implementaties toegevoegd kunnen worden.

---

## 10) Runnen in Rider

1. Open de solution (`.sln`).
2. Zorg dat **.NET 9 SDK** en **MAUI workloads** geïnstalleerd zijn:

    * `dotnet workload install maui`
3. Kies **Run Configuration**: *Windows Machine* (SlimsteMens.Maui).
4. Run (Shift+F10). PresenterWindow opent; AudienceWindow wordt fullscreen op 2e scherm gezet (of gemaximaliseerd bij 1 scherm).

---

## 11) Testplan

* **Twee‑scherm**: Audience op 2e, borderless fullscreen.
* **Sync**: Presenter toggles zichtbaar in Audience (opties, ✓, timer, scorebord).
* **Editor**: CRUD werkt; JSON Save/Load rondes/teams/vragen.
* **Finale**: klok telt af; ±10 knoppen passen seconden aan; winnaar bij einde.
* **Media**: Presenter Play/Pause → Audience volgt (MVP startsync).

---

## 12) Bijlagen

* `Data/game.sample.json` (gebruik de versie uit je WPF/WinUI documentatie; zet **Copy always** in csproj of embed als Content).
* `schema.md` (veld‑beschrijvingen + JSON Schema Draft‑07).

---

## 13) Definition of Done

* Twee vensters (Presenter + Audience) draaien op Windows en plaatsen Audience fullscreen 2e scherm.
* Editor, Scorebord, Finale en Media‑sync werken volgens acceptatiecriteria.
* JSON save/load correct; styling tv‑look; hotkeys actief (Windows).
* Project buildt en start in **Rider** zonder fouten.
