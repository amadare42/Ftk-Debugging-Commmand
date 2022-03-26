# Debugging Commands

Adapted from https://github.com/CarbonNikm/FTK-Debugging-Commands for Bepinex. 

By default, will enable chat for single player so these commands can be used. Can be disabled in config.

## Commands:
/give \[item_name]

/partyheal

/reveal

/unreveal

## Installation

### Thunderstore

1. Install [Thunderstore Mod Manager](https://www.overwolf.com/app/Thunderstore-Thunderstore_Mod_Manager)
2. Install `DebuggingCommands` mod
3. Start game by using Start Modded button

### Manual

1. Install latest [Bepinex](https://github.com/BepInEx/BepInEx/releases) 5.* version. You can refer to installation instructions [here](https://docs.bepinex.dev/articles/user_guide/installation/index.html)
2. Either install HookGenPatcher or add `MMHOOK_Assembly-CSharp.dll` to `BepInEx\plugins` folder from [this repo](https://github.com/ftk-modding/stripped-binaries)
3. Start game normally

## Build

1. You'll need to provide stripped and publicized binaries. Those are included as git submodule. In order to get those you can:
    - When cloning this repository use `git clone --recurse-submodules`
    - OR If repository is already checked out, use `git submodule update --init --recursive`
    - OR Download or build them yourself using instructions from [this repo](https://github.com/ftk-modding/stripped-binaries)
2. (optional) Set `BuiltPluginDestPath` property in project file so after build binaries will be copied to specified location.
3. You should be able to build solution now


## License

MIT