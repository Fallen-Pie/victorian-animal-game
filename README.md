# Victorian Animal Game

A Victorian-era simulation game built with Godot and C#.

## Overview

Victorian Animal Game is a grand strategy/simulation game set in the Victorian era, featuring animal characters ("Critters"). The engine handles complex systems including map generation, economic markets, production methods, and population growth.

## Requirements

- [Godot Engine 4.6](https://godotengine.org/) (Standard or .NET version)
- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)
- An IDE with C# support (JetBrains Rider, VS Code with C# Dev Kit, or Visual Studio 2022+)

## Setup

1. Clone the repository:
   ```bash
   git clone https://github.com/your-repo/victorian-animal-game.git
   cd victorian-animal-game
   ```

2. Restore .NET dependencies:
   ```bash
   dotnet restore
   ```

3. Open the project in Godot:
   - Launch Godot Engine.
   - Import the `project.godot` file located in the `VictorianAnimalGame/` directory.

## Running the Game

### From Godot Editor
- Press **F5** or the **Play** button in the top right corner.
- Main scene: `VictorianAnimalGame/Scenes/MainMap.tscn`

### From Command Line
```bash
godot --path VictorianAnimalGame
```