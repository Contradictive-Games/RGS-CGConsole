using System.Reflection;

namespace ContradictiveGames.CGConsole
{
    public struct ConsoleCommand
    {
        public string Command;
        public string Description;
        public MethodInfo MethodToExecute;
        public object MonoBehaviorTarget;

        public ConsoleCommand(string command, string description, MethodInfo methodToExecute, object monoBehaviorTarget)
        {
            Command = command;
            Description = description;
            MethodToExecute = methodToExecute;
            MonoBehaviorTarget = monoBehaviorTarget;
        }
    }
}