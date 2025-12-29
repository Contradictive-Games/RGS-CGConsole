using UnityEngine;

namespace CGConsole
{
    public class ExampleScriptWithCommands : MonoBehaviour, ICommandProvider
    {
        private void Awake()
        {
            Debug.Log("Hello from Awake!", this);
        }

        [ConsoleCmd("log_nice")]
        private void LogANiceStatement()
        {
            Debug.Log("I hope you have a great day!");
        }

        [ConsoleCmd("log_number {int}", "Log any number you would like to see")]
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
    }
}