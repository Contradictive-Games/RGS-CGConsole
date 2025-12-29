using System;

namespace CGConsole
{
    [AttributeUsage(AttributeTargets.Method)]
    public class ConsoleCmdAttribute : Attribute
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