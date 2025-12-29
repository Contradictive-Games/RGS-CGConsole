namespace CGConsole
{
    public struct ConsoleCommand
    {
        public string Command;
        public string Description;

        public ConsoleCommand(string command, string description = "")
        {
            Command = command;
            Description = description;
        }
    }
}