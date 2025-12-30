# CG Console Changelog


## [v0.6.123025] - 12-30-2025

This marks the second release of CGConsole.

### Features

- The `[ConsoleCmd]` attribute can now be used within a class
- `ConsoleCommand` can now have null targets with static methods, allowing for default commands to be created as well


### Fixes/Improvements

- `CGConsoleCommands` now caches and stores `ConsoleCommand` data better
- Command registration logs can be turned on/off in the `CGConsoleCommands` class
- `CommandResponse` will now log an `Assert` or `Error` based on the response type
- Update `Samples` and demo scripts to better fit new structure



### Misc. Changes

- `ConsoleCommandRegistry` renamed to `CGConsoleCommands`
- Root namespace change to `ContradictiveGames.CGConsole` from `CGConsole`
- The `Console` class is no longer an abstract, and is a concrete MonoBehavior that can be added to any GameObject within the game

## [v0.5] - 12-29-2025

This marks the first release of CGConsole.

### Features

- `CGConsole` was created, allowing users to:
    - Create custom console commands by using the attribute: <br>
    `ConsoleCmd("name", "optional-description")]`
    - Create a custom console by inheriting the `Console` class
    - Create a custom theme for the console by using the `ConsoleSettings` ScriptableObject
    - Type in custom commands by the cmd name into an InputField and pressing enter
    - Execute a custom command by calling: <br> `ConsoleCommandRegistry.TryExecute("cmd-name");`