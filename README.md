# Schedule I Trainer

MelonLoader mod for **Schedule I** — internal trainer with modern UI, money editor, combat cheats and police ESP.

## Features

| Feature | Description |
|---------|-------------|
| **Money Editor** | Add or remove $1K / $10K / $100K / $1M in cash or bank |
| **God Mode** | Keeps health at max |
| **Infinite Energy** | Never run out of energy |
| **Infinite Ammo** | Unlimited magazine size with auto-reload |
| **No Reload** | Skip reload animations |
| **Speed x3** | Boosted walk & sprint speed |
| **Police Ignore** | Officers won't pursue you |
| **Clear Wanted** | Reset wanted level to zero |
| **Police ESP** | See all officers through walls with distance |

## Controls

| Key | Action |
|-----|--------|
| `F1` | Toggle mod menu |
| `F2` | Toggle police ESP |
| `ESC` | Close menu |

## Installation

### Download (recommended)
1. Install [MelonLoader v0.7.1](https://melonloader.co/) on Schedule I
2. Launch the game once, then close it
3. Download `Schedule1Mod.dll` from the [latest release](https://github.com/ihabontop/schedule1_trainer/releases/latest)
4. Copy it to your game's `Mods/` folder:
   ```
   C:\Program Files (x86)\Steam\steamapps\common\Schedule I\Mods\
   ```
5. Launch the game — press **F1** to open the trainer

### Build from source
1. Install MelonLoader on `Schedule I.exe` and launch the game once
2. Clone this repo
3. Build:
   ```
   dotnet build -c Release
   ```
4. Copy `bin/Release/net472/Schedule1Mod.dll` to your game's `Mods/` folder
5. Launch the game — press **F1** to open the trainer

> **Note:** The `GamePath` in `Schedule1Mod.csproj` defaults to the standard Steam install path. Update it if your game is installed elsewhere.

## Screenshots

> Press F1 in-game to open the trainer menu. Press F2 for police ESP overlay.

<img width="1920" height="1080" alt="image" src="https://github.com/user-attachments/assets/2ad10826-4e45-458b-9d81-bae0f6b7b1e9" />

## Project Structure

```
Schedule1Mod/
├── Mod.cs              # Entry point, hotkeys, cheat loop
├── ModMenu.cs          # Modern animated UI (custom IMGUI rendering)
├── MoneyManager.cs     # Cash & bank balance read/write
├── Cheats.cs           # God mode, speed, energy, ammo, police
├── PoliceESP.cs        # Police officer ESP with boxes & labels
├── Properties/
│   └── AssemblyInfo.cs # MelonLoader mod metadata
└── Schedule1Mod.csproj # Project file with game assembly refs
```
