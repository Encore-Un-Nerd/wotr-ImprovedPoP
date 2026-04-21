# Improved Prophet of Pestilence

A mod for Pathfinder: Wrath of the Righteous which improves gameplay for the Prophet of Pestilence archetype and fixes a major bug.

## Features
- **Extra Mutation**: Allows to take extra Plague of Abaddon mutations instead of standard shaman hexes.
- **Virulent Plague**: A feat which adds +1 DC to all disease effects. Exists as a greater version, like the spell focus feat.
- **Mythic Virulent Plague**: A mythic feat which adds +1 DC to all disease effects (or +2 with Greater Virulent Plague), just like Mythic Spell Focus works.
- **A Thousand Diseases fix**: Fixes the capstone aura so the disease status persists correctly for as many rounds as the shaman's class level, instead of being instant as it was initially implemented by OwlCat.

## Supported Languages (looking for contributors to add more)
- English
- French

## Requirements
- Pathfinder: Wrath of the Righteous
- A Dance of Masks DLC
- Unity Mod Manager

## Installation
1. Download the latest release zip
2. Open Unity Mod Manager, go to the Mods tab
3. Drag and drop the zip onto the window

## Building from source
- .NET 4.8
- change the `WrathPath` and `UnityPath` in the .csproj file
- Run `dotnet build`
