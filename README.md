# CG Console

> CG Console is a developer console that allows you to create your own commands, and execute them from a console window or from scripts.



## Notes

This is a part of RGS (Re-usable Game Systems) that I have created to help potentially speed up game development.

Within the `Runtime/Samples` folder, there is a prefab that contains an example version of a custom Console with a working UI. If you are to use this, you can create a `ExampleConsoleSettings` ScriptableObject that allows you to drag and drop different user-created themes. 

To create this ScriptableObject just simply right-click anywhere within the Project window, and in the ContextMenu select the 'CGConsole Settings" option.

This will of course not work with a custom made console, and only work with the demo version.



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



## Getting Started

This package includes a base `Console` class that you can inherit from. `Console.cs` is a MonoBehavior script that will handle all the basics of the Console. You can add this to any component by going into the component menu and going to `CGConsole/Console`, which will then add the proper component. 

The Console does require some minimal setup. The first thing we will require is an `TMP_InputField`. This is where the user can type in commands and submit them.

The second thing we require is a `RectTransform` in which the console's output logs will be parented to. It is generally recommended that you have some kind of basic handling for scrolling, content size fitting, etc. While it is not required, the base `Console` MonoBehavior will additionally have a `ScrollRect` field you can set within the inspector, all it will do by default is scroll to the bottom of the view when creating logs.



## Example Command Usage and Creation


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
CGConsoleCommands.RegisterAllCommands();
```

This will register all `[ConsoleCmd]` attributes and their functions from all MonoBehaviors that have implemented `ICommandProvider` and are actively in the scene when this function was called. If you are utilizing the `Console` class in any way - this is called within the `Start` function.

> **NOTE:** This command will by default ***only*** register MonoBehaviors that are using the `ICommandProvider` interface. However, you can override by adding `true` as the argument and it will register any MonoBehavior whether or not it implements the interface. It is best to never override it, but it is an option.


#### Manually Register Commands

When manually registering commands, just do:

```cs
CGConsoleCommands.RegisterCommandsFrom(this)
```

Calling this function does **not** require the class to implement `ICommandProvider`.

This will register all `[ConsoleCmd]` attributes and their functions from the MonoBehavior we call this function from.


#### Registering Commands From A Class

Classes can support console commands as well, and are not required to implement `ICommandProvider`. More often than not, I would just recommend registering commands within the class's constructor.

```cs
public class TestClass
{
    //Properties...
    
    [ConsoleCmd("test_command")]
    public void MyCommandFunctionWithinAClass(){
        //Do something
    }

    //Constructor
    public TestClass(){
        //Set values
        ConsoleCommandRegister.RegisterCommandsFrom(this);
    }
}
```

You can of course also do the following if you would rather not do it within the constructor:

```cs
public class AnotherTestClass
{
    //Properties
    [ConsoleCmd("another_test_command")]
    public void ExampleFunction(){
        //Do something
    }
}


public class TestMonoBehavior : MonoBehavior
{

    public AnotherTestClass MyClass = new();
    
    private void Start()
    {
        RegisterCommandsInMyTestClass();
    }

    private void RegisterCommandsInMyTestClass()
    {
        ConsoleCommandRegister.RegisterCommandsFrom(MyClass);
    }

}
```


### Executing a Console Command

Commands can both be executed within a script and by typing the command within the `Console` input field. 

To execute a command within a script you can do:

```csharp
CGConsoleCommands.TryExecute("command_goes_here");
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

The `CGConsoleCommands` class handles the building of the `CommandResponse`.



## Console Usage

Implementing the `Console` is pretty simple, and it can 

The base class comes with a few useful methods. I recommend you look through the class in the `Runtime/` folder to see all of its core functionality.

Additionally, if you'd like to create your own settings - you must inherit the `ConsoleSettings` ScriptableObject, and then type cast when using any of your custom settings.



## FAQ

### What Types Are Supported For the Command's Args?

Console Commands really only work with supporting basic types like `string`, `int`, `float`, and `bool`

### Are There Any Default Commands?

There is one default command being `help` - which will log all commands and their descriptions.


## Stretch

Features that are not currently in this package, but I would eventually like to add

- [ ] Support structs as console command arguments
- [ ] Support gathering commands automatically, rather than manual registration
- [ ] Add auto-complete when typing a command in Console input field
- [x] Support non-MonoBehavior classes being able to utilize commands
