using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;


namespace ContradictiveGames.CGConsole
{
    public static class CGConsoleCommands
    {
        private static readonly Dictionary<string, ConsoleCommand> allCommands = new(){ 
            { 
                "help", 
                new ConsoleCommand(
                    "help", 
                    "List all available console commands", 
                    typeof(CGConsoleCommands).GetMethod(nameof(ShowHelp), BindingFlags.Static | BindingFlags.NonPublic), 
                    new ParameterInfo[0],
                    null
                ) 
            }
        };
        private static HashSet<string> commandsList = new();

        private static string commandHelpString;
        private static bool registeredDefaultCommandsForAutoComplete = false;
        private static bool enableLogging = false;


        #region Command Registration


        public static void RegisterAllCommands(bool mustBeCommandProvider = true)
        {
            MonoBehaviour[] objects = GameObject.FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None);
            foreach (var obj in objects)
            {
                if (mustBeCommandProvider && obj is ICommandProvider) RegisterCommandsFrom(obj);
                else if(!mustBeCommandProvider) RegisterCommandsFrom(obj);
            }
            if(enableLogging) Debug.Log($"(CG Console) Successfully registered {allCommands.Count} commands. Type `help` into the console to see all available commands.");
        }


        public static void RegisterCommandsFrom(object target)
        {
            var methods = target.GetType().GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            string cmdName = "";

            foreach (var method in methods)
            {
                var attr = method.GetCustomAttribute<ConsoleCmdAttribute>();
                
                if (attr != null)
                {
                    cmdName = attr.CommandFormat.Split(' ')[0].ToLower();
                    ParameterInfo[] @params = method.GetParameters();
                    
                    if(allCommands.ContainsKey(cmdName)) return;

                    allCommands.Add(cmdName, new ConsoleCommand(cmdName, attr.Description, method, @params, target));
                    commandsList.Add(cmdName);
                }
            }

            if(enableLogging) Debug.Log($"(CG Console) Registered `{cmdName}` command from {target}");

            if (!registeredDefaultCommandsForAutoComplete)
            {
                foreach(var (_cmdName, _) in allCommands)
                {
                    commandsList.Add(_cmdName);
                }
                registeredDefaultCommandsForAutoComplete = true;
            }

            UpdateHelpString();
        }


        #endregion

        #region Command Execution

        public static CommandResponse TryExecute(string input)
        {
            string[] parts = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length == 0)
            {
                return new CommandResponse(ResponseType.Invalid, "Command was empty");
            }

            string cmd = parts[0].ToLower();
            
            if (!allCommands.TryGetValue(cmd, out ConsoleCommand command))
            {
                return new CommandResponse(ResponseType.Error, $"Command `{cmd}` was not found, or was not registered properly");
            }

            if (command.Target == null && !command.MethodToExecute.IsStatic)
            {
                return new CommandResponse(ResponseType.Error, $"Could not find any valid targets for `{cmd}");
            }

            object[] parameters = new object[command.Parameters.Length];
            for (int i = 0; i < command.Parameters.Length; i++)
            {
                if (i + 1 >= parts.Length)
                {
                    return new CommandResponse(ResponseType.Invalid, $"Not enough args for command '{cmd}'");
                }

                string arg = parts[i + 1];
                Type paramType = command.Parameters[i].ParameterType;
                try
                {
                    parameters[i] = Convert.ChangeType(arg, paramType);
                }
                catch
                {
                    return new CommandResponse(ResponseType.Error, $"Failed to convert arg `{arg}` to type: {paramType.Name}");
                }
            }

            command.MethodToExecute.Invoke(command.Target, parameters);
            return new CommandResponse(ResponseType.Success, "Success");
        }


        #endregion


        #region Utilities

        public static List<string> GetCommandAutoComplete(string input)
        {
            List<string> commands = new();
            if(input.Length == 0) return commands;

            foreach(String s in commandsList)
            {
                if(s.Contains(input)) commands.Add(s);
            }

            return commands;
        }


        private static void ShowHelp()
        {
            Debug.Log(commandHelpString);
        }


        private static void UpdateHelpString()
        {
            string response = "Commands List: \n";
            foreach (var (_, cmd) in allCommands)
            {
                response += $"{cmd.Command}" + (!String.IsNullOrWhiteSpace(cmd.Description) ? "     |       description: " + cmd.Description : "") + "\n";
            }
            commandHelpString = response;

        }


        #endregion


    }
}