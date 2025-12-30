using System.Reflection;

namespace ContradictiveGames.CGConsole
{
    public class ConsoleCommand
    {
        public string Command;
        public string Description;
        public MethodInfo MethodToExecute;
        public ParameterInfo[] Parameters;
        public object Target;

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