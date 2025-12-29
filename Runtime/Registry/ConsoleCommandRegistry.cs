using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;


namespace CGConsole
{
public class ConsoleCommandRegistry
{
    private static readonly Dictionary<string, MethodInfo> commands = new();
    private static readonly Dictionary<string, object> targets = new();

    private static readonly List<ConsoleCommand> registeredCommands = new();


    public static void RegisterAllCommands(){
        MonoBehaviour[] objects = GameObject.FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None);
        foreach(var obj in objects){
            if(obj is ICommandProvider) RegisterCommandsFrom(obj);
        }
        Debug.Log($"Successfully registered {registeredCommands.Count} commands. Type `help` into the console to see all available commands.");
    }


    public static List<ConsoleCommand> GetAllCommands() => registeredCommands;


    public static void RegisterCommandsFrom(MonoBehaviour target)
    {
        var methods = target.GetType().GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

        foreach (var method in methods)
        {
            var attr = method.GetCustomAttribute<ConsoleCmdAttribute>();
            if (attr != null)
            {
                string cmdName = attr.CommandFormat.Split(' ')[0];
                commands[cmdName] = method;
                targets[cmdName] = target;

                registeredCommands.Add(new ConsoleCommand(cmdName, attr.Description));
            }
        }
    }

    public static CommandResponse TryExecute(string input)
    {
        string[] parts = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        if (parts.Length == 0)
        {
            return new CommandResponse(ResponseType.Invalid, "Command was empty");
        }

        string cmd = parts[0];
        if (!commands.TryGetValue(cmd, out MethodInfo method))
        {
            return new CommandResponse(ResponseType.Error, $"Command `{cmd}` was not found, or was not registered properly");
        }

        object target = targets[cmd];
        if(target == null)
        {
            return new CommandResponse(ResponseType.Error, $"Could not find any valid targets for `{cmd}");
        }

        ParameterInfo[] paramInfos = method.GetParameters();

        object[] parameters = new object[paramInfos.Length];
        for (int i = 0; i < paramInfos.Length; i++)
        {
            if (i + 1 >= parts.Length)
            {
                return new CommandResponse(ResponseType.Invalid, $"Not enough args for command '{cmd}'");
            }

            string arg = parts[i + 1];
            Type paramType = paramInfos[i].ParameterType;
            try
            {
                parameters[i] = Convert.ChangeType(arg, paramType);
            }
            catch
            {
                return new CommandResponse(ResponseType.Error, $"Failed to convert arg `{arg}` to type: {paramType.Name}");
            }
        }

        method.Invoke(target, parameters);
        return new CommandResponse(ResponseType.Success, "Success");
    }
}
}