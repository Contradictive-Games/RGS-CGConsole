using System.Reflection;

namespace ContradictiveGames.CGConsole
{
    public sealed class ConsoleCommand
    {
        public readonly string Command;
        public readonly string Description;
        public readonly MethodInfo MethodToExecute;
        public readonly ParameterInfo[] Parameters;
        public readonly object Target;

        public ConsoleCommand(string command, string description, MethodInfo methodToExecute, ParameterInfo[] parameters, object target)
        {
            Command = command;
            Description = description;
            MethodToExecute = methodToExecute;
            Parameters = parameters;
            Target = target;
        }
    }
}