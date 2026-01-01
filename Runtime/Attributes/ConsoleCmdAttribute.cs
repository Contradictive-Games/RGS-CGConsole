using System;

namespace ContradictiveGames.CGConsole
{
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class ConsoleCmdAttribute : Attribute
    {
        public string CommandFormat { get; }
        public string Description { get; }

        public ConsoleCmdAttribute(string commandFormat, string description = "")
        {
            CommandFormat = commandFormat;
            Description = description;
        }
    }
}