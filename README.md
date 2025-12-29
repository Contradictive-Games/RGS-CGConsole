
# CG Console

> CG Console is a developer console that allows you to create your own commands, and execute them from a console window or from scripts.


## Notes

This is a part of RGS (Re-usable Game Systems) that I have created to help potentially speed up game development.

Within the `Runtime/Samples` folder, there is a prefab that contains an example version of a DeveloperConsole with a working UI. If you are to use this, you can create a `ConsoleSettings` ScriptableObject that allows you to drag and drop different user-created themes. 

To create this ScriptableObject just simply right-click anywhere within the Project window, and in the ContextMenu select the 'CGConsole Settings" option.

This will of course not work with a custom made console, and only work with the demo version.

**It is highly recommended to use th

## Installation

### Install via .unitypackage

Download any version from releases.

[Click Here to go to Releases Page (https://github.com/Contradictive-Games/RGS-CGConsole/releases)](https://github.com/Contradictive-Games/RGS-CGConsole/releases)

Then either drag the `.unitypackage` file into your project folder, or open the PackageManager window and click the `+` sign to add from disk and select the downloaded file.

### Install via git URL

To install via git URL, open the PackageManager window within the Editor and click "intall via git url" and entering:

```
https://github.com/Contradictive-Games/RGS-CGConsole.git
```

<br>

## Usage

### Allow a MonoBehavior to Execute Commands

Any time you want to use the `ConsoleCmd` attribute, for it to work properly, you must implement the ICommandProvider interface.

```csharp
using UnityEngine;
using CGConsole;

public class ExampleMonoBehavior : MonoBehavior, ICommandProvider 
{ 
    ///Example class...
}
```

### Creating a Console Command

To be able to add commands to the registry, simply add the `ConsoleCmd` attribute above the function. Functions are **not** required to be `public`.

```csharp
[ConsoleCmd("log_normal")]
public void MyExampleCommand(){
    Debug.Log("We succesfully executed the `log_normal` command");
}

[ConsoleCmd("log_number {int}")]
public void MyExampleCommandWithArgs(int arg){
    Debug.Log($"Your number was: {arg}");
}

[ConsoleCmd("game_state", "Log the game's current state")]
public void MyExampleCommandThatHasADescription(){
    //Log the game's state
}
```

### Registering Commands

To begin using any commands you create, you must register these commands. There's 2 ways of doing so, and it is **recommended** you use both.

#### Register All Commands

```cs
ConsoleCommandRegistry.RegisterAllCommands();
```

This will register all `[ConsoleCmd]` attributes and their functions from all MonoBehaviors that have the ICommandProvider when this command was called.

The main limitation of only doing this would be that if you call this in the start function of a MonoBehavior, and spawn an object afterwards that has a `[ConsoleCmd]` - this command will not have been registered because this function was called before it existed.

#### Register Commands From MonoBehavior

```cs
ConsoleCommandRegistry.RegisterCommandsFrom(this)
```

This will register all `[ConsoleCmd]` attributes and their functions from the MonoBehavior we call this function from.

The main limitation of only doing this comes from having to call this from every single MonoBehavior whenever it is created, leading to potential issues with forgetting to call this function - and not being able to use a command as intended.


### Executing a Console Command

Commands can both be executed within a script and by typing the command within the DeveloperConsole's input field. 

Otherwise, they can also be executed within a script by doing the following

```csharp
ConsoleCommandRegistry.TryExecute("command_goes_here");
```

The `TryExecute` method returns a `CommandResponse` which looks like:

```cs
public enum ResponseType
{
    Success,
    Invalid,
    Error
}

public struct CommandResponse
{
    public ResponseType ResponseType;
    public string Message;

    public CommandResponse(ResponseType responseType, string message)
    {
        ResponseType = responseType;
        Message = message;
    }
}
```

The ConsoleCommandRegistry handles the building of the `CommandResponse`.

<br>
<br>

## FAQ

### Supported Types
Console Commands really only work with supporting basic types like `string`, `int`, `float`, and `bool`

### Why is my command not working?

First ensure that the class you are trying to execute the command from is a `MonoBehavior` and you are adding the `ICommandProvider` interface.

If that is done correctly, it may be that the script that has the `[ConsoleCmd]` attribute didn't register the command. Often times, this can be solved by adding this command into your OnEnable

```cs
private void OnEnable()
{
    ConsoleCommandRegistry.RegistCommandsFrom(this);
}
```

Ensure that this command properly registers by typing the `help` command into the console, which will list all available commands.

<br>
<br>


## Stretch

Features that are not currently in this package, but I would eventually like to add

- [ ] Support structs as console command arguments
- [ ] Support gathering commands automatically, rather than manual registration
- [ ] Add auto-complete when typing a command
- [ ] Support non MonoBehavior derived classes being able to have command functions
