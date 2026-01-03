using System;

namespace ContradictiveGames.CGConsole
{
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class ConsoleCmdAttribute : Attribute
    {
        public string CommandFormat { get; }
        public string Description { get; }
        public bool HideFromAutoComplete { get; }

        public ConsoleCmdAttribute(string commandFormat, string description = "", bool hideFromAutoComplete = false)
        {
            CommandFormat = commandFormat;
            Description = description;
            HideFromAutoComplete = hideFromAutoComplete;
        }
    }
}