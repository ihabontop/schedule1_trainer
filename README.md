# Schedule I Trainer

MelonLoader mod for **Schedule I** — internal trainer with mod menu and police ESP.

## Features

| Feature | Description |
|---------|-------------|
| **Money Editor** | Add $1K / $10K / $100K / $1M cash instantly |
| **God Mode** | Keeps health at max |
| **Infinite Energy** | Never run out of energy |
| **Speed x3** | Boosted walk & sprint speed |
| **Police Ignore** | Officers won't pursue you |
| **Clear Wanted** | Reset wanted level to zero |
| **Police ESP** | See all officers through walls with distance |

## Controls

| Key | Action |
|-----|--------|
| `F1` | Toggle mod menu |
| `F2` | Toggle police ESP |

## Installation

### Prerequisites
- [MelonLoader v0.7.1](https://melonloader.co/) installed for Schedule I
- Launch the game once after installing MelonLoader to generate proxy assemblies

### Setup
1. Install MelonLoader on `Schedule I.exe`
2. Launch the game once, then close it
3. Build the project:
   ```
   dotnet build -c Release
   ```
4. Copy `bin/Release/net472/Schedule1Mod.dll` to your game's `Mods/` folder:
   ```
   [Steam]/steamapps/common/Schedule I/Mods/
   ```
5. Launch the game — press F1 to open the trainer

### Build Notes
- Target: .NET Framework 4.7.2
- The `GamePath` in `Schedule1Mod.csproj` defaults to the standard Steam install path. Update it if your game is installed elsewhere.

## Screenshots

> Press F1 in-game to open the trainer menu. Press F2 for police ESP overlay.<img width="1920" height="1080" alt="image" src="https://github.com/user-attachments/assets/9035f9e8-4d74-4fc6-b724-33866cd2dff4" />


## Project Structure

```
Schedule1Mod/
├── Mod.cs              # Entry point, hotkeys, cheat loop
├── ModMenu.cs          # OnGUI trainer menu with styled UI
├── MoneyManager.cs     # Cash/online balance read & write
├── PoliceESP.cs        # Police officer ESP with boxes & labels
├── Cheats.cs           # God mode, speed, energy, police ignore
├── Properties/
│   └── AssemblyInfo.cs # MelonLoader mod metadata
└── Schedule1Mod.csproj # Project file with game assembly refs
```
