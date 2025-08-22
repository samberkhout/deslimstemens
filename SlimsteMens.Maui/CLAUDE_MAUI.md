Je bent een senior .NET developer. Gebruik onderstaand projectplan (CLAUDE_MAUI.md) als strikte blauwdruk. Genereer een complete **.NET MAUI (.NET 9, Single Project)** applicatie in C#.

### Doel
- Twee vensters: PresenterWindow (primair) en AudienceWindow (fullscreen 2e scherm).
- Functies: rondes, vragen, scorebord, finale-logica, media-sync.
- Editor: CRUD rondes/vragen/opties, JSON save/load.
- Styling: look & feel van *De Slimste Mens* (donkere achtergrond, witte typografie, gouden accenten).

### Vereisten
- MVVM met **CommunityToolkit.Mvvm**
- **CommunityToolkit.Maui** + **CommunityToolkit.Maui.MediaElement**
- JSON opslag via **System.Text.Json**
- Services: `GameRepository`, `DisplayService`, `MediaSyncService`
- ViewModels: `PresenterViewModel`, `AudienceViewModel`, `EditorViewModel`, `ScoreboardViewModel`
- Data: `Data/game.sample.json` en `schema.md`
- Hotkeys (Windows): ←/→, 1–8, Space, Enter, S/D, F11

### Projectstructuur
```
SlimsteMens.Maui/
├─ App.xaml
├─ MauiProgram.cs
├─ Resources/Styles/{Colors.xaml, Styles.xaml}
├─ Models/{Game.cs, Round.cs, Question.cs, AnswerOption.cs, Team.cs, Enums.cs}
├─ Services/{GameRepository.cs, DisplayService.cs, MediaSyncService.cs}
├─ ViewModels/{BaseViewModel.cs, PresenterViewModel.cs, AudienceViewModel.cs, EditorViewModel.cs, ScoreboardViewModel.cs}
├─ Pages/{PresenterPage.xaml, AudiencePage.xaml, EditorPage.xaml, ScoreboardPage.xaml, FinalePage.xaml}
├─ Windows/{PresenterWindow.cs, AudienceWindow.cs}
├─ Platforms/
│  ├─ Windows/DisplayService.Windows.cs
│  ├─ Android/DisplayService.Android.cs (placeholder)
│  └─ iOS/DisplayService.iOS.cs (placeholder)
└─ Data/{game.sample.json, schema.md}
```

### Belangrijkste implementaties
1. **DisplayService** – multi-window en fullscreen op 2e scherm (Windows interop via `AppWindow`).
2. **MediaSyncService** – sync van Play/Pause/Position voor MediaElement.
3. **ViewModels** – Presenter: navigatie, timer, reveal; Audience: alleen lezen; Editor: CRUD; Scoreboard: secondenbeheer.
4. **Styling** – donkere achtergrond (#0B0C10), witte tekst (#FFFFFF), accent goud (#FFD700).
5. **Finale** – ronde klok (GraphicsView of ProgressBar) + ±10 seconden knoppen.
6. **Hotkeys** – via Windows-specifieke events.

### Output
- Volledige MAUI solution met bovengenoemde structuur.
- Compileerbaar en uitvoerbaar op Windows.
- AudienceWindow automatisch fullscreen op 2e scherm (fallback: gemaximaliseerd).
- Werkende demo met 2 rondes (3-6-9 + Open Deur).

### Runnen in Rider
1. Open de solution (`.sln`).
2. Zorg dat .NET 9 SDK en MAUI workloads geïnstalleerd zijn (`dotnet workload install maui`).
3. Kies Run Configuration: *Windows Machine* (SlimsteMens.Maui).
4. Run (Shift+F10). PresenterWindow opent primair; AudienceWindow fullscreen op 2e scherm.
