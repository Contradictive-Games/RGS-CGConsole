using UnityEngine;

namespace ContradictiveGames.CGConsole
{
    public struct ExampleStruct
    {
        public int X;
        public int Y;

        public ExampleStruct(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
    public class ExampleScriptWithCommands : MonoBehaviour, ICommandProvider
    {
        private void Awake()
        {
            Debug.Log("Hello from Awake!", this);
            RegisterCommands();
        }

        [ConsoleCmd("log_nice")]
        private void LogANiceStatement()
        {
            Debug.Log("I hope you have a great day!");
        }

        [ConsoleCmd("log_number", "Log any number you would like to see")]
        private void LogANumber(int number)
        {
            Debug.Log($"Great choice! Your number was: {number}");
        }

        [ConsoleCmd("log_warning", "Log a scary warning")]
        private void LogAWarning()
        {
            Debug.LogWarning("We just logged a warning");
        }

        [ConsoleCmd("log_error", "Log an even scarier error")]
        private void LogAnError()
        {
            Debug.LogError($"We just logged an error!");
        }

        [ConsoleCmd("logstruct")]
        public void LogAStruct(ExampleStruct st)
        {
            Debug.LogWarning($"We got it! X: {st.X} // Y: {st.Y}");
        }
        //Interface implementation
        public void RegisterCommands()
        {
            CGConsoleCommands.RegisterCommandsFrom(this);
        }
    }
}