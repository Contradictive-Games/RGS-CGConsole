
# CG Console

CG Console is a developer console that allows you to create your own commands, and execute them from a console window.


## Notes
This is a part of RGS (Re-usable Game Systems) that I have created to help potentially speed up game development.


## Installation

This is where installation details would go

## Example Usage

### Creating a Console Command

```csharp
[ConsoleCmd("help")]
public void MyExampleCommand(){
    //Do something
}

[ConsoleCmd("help {int}")]
public void MyExampleCommandWithArgs(int arg){
    //Do something
}

[ConsoleCmd("game_state", "Log the game's current state")]
public void MyExampleCommandThatHasADescription(){
    //Log the game's state
}
```


### Executing a Console Command

```csharp
```

## FAQ

### Supported Types
Console Commands really only work with supporting basic types like ```string```, ```int```, ```float```, and ```bool```

## Stretch

Features that are not currently in this package, but I would eventually like to add

- [ ] Support structs as console command arguments