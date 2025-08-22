WPF Slimste Mens Quizapp
🎯 Doel

Een WPF (.NET 9) applicatie in de stijl van De Slimste Mens met twee schermen (Presenter & Audience), inclusief editor, scorebord, finale en media-sync.

👥 Agents en verantwoordelijkheden
1. Architect Agent

Stelt de projectstructuur op (mappen, bestanden).

Bewaakt het gebruik van MVVM en scheiding tussen View, ViewModel en Model.

Zorgt dat .NET 9 + WPF correct is ingesteld.

Documenteert beslissingen in CLAUDE.md.

2. Data Agent

Definieert Models (Game, Round, Question, AnswerOption, Team).

Ontwerpt en onderhoudt JSON schema (schema.md).

Levert voorbeeldbestand game.sample.json.

Zorgt voor backward compatibility bij wijzigingen.

3. UI/UX Agent

Ontwerpt de Audience- en Presenter-vensters in XAML.

Past styling toe geïnspireerd op De Slimste Mens:

Donkere achtergrond

Witte typografie

Gouden accenten

Ronde klok in Finale

Zorgt voor animaties (fade-ins, glow, aftelklok).

Bewaakt toegankelijkheid (grote letters, contrast).

4. Logic Agent

Bouwt PresenterViewModel: navigatie, timer, reveal answers.

Bouwt ScoreboardViewModel: teams, seconden, actief team, +/– functies.

Bouwt EditorViewModel: CRUD rondes/vragen/opties.

Verzorgt hotkeys: ←/→, 1–8, Space, Enter, S/D, F11.

Implementeert finale-logica (aftellen, winnaar bepalen).

5. Media Agent

Bouwt MediaSyncService: MediaPath, IsPlaying, Position.

Integreert WPF MediaElement in Presenter en Audience.

Zorgt voor sync van Play/Pause/Position.

Test videofragmenten, audio en afbeeldingen in Open Deur.

6. Multi-screen Agent

Bouwt DisplayService:

Detecteert 2e scherm (System.Windows.Forms.Screen.AllScreens).

Zet AudienceWindow fullscreen op 2e scherm (borderless, cursor verborgen).

Fallback: max op primair scherm + melding.

Luistert naar DisplaySettingsChanged voor hotplug.

7. QA/Test Agent

Stelt testcases op (zie Testplan in CLAUDE.md).

Verifieert:

JSON load/save correct.

Audience sync met Presenter.

Scorebord updates werken.

Finale klok + seconds logic correct.

Media sync stabiel.

Logt bugs en bevindingen.

8. Build & DevOps Agent

Zorgt dat project compileert met Rider & dotnet CLI.

Schrijft README.md met installatie- en run-instructies.

Zet Run Configurations correct.

Zorgt dat Data/game.sample.json op Copy Always staat.

Eventueel CI/CD setup (GitHub Actions of JetBrains Space).

🔄 Workflow

Architect Agent zet basisproject + structuur op.

Data Agent levert modellen en JSON.

UI/UX Agent ontwerpt views in XAML.

Logic Agent implementeert ViewModels + hotkeys.

Media Agent voegt Open Deur sync toe.

Multi-screen Agent maakt Audience fullscreen.

QA Agent test scenario’s en logt resultaten.

DevOps Agent borgt build & run (Rider/dotnet).

✅ Definition of Done

Twee vensters (Presenter + Audience) correct werkend op 2 schermen.

Editor, Scorebord, Finale en Media-sync volledig functioneel.

JSON save/load werkt en valideert tegen schema.

Styling herkenbaar als De Slimste Mens.

Project start in Rider met .NET 9 zonder fouten.    