using UnityEngine;

namespace CGConsole
{
    public class ExampleScriptExecutor : MonoBehaviour
    {
        [ConsoleCmd("log_nice")]
        private void LogANiceStatement()
        {
            Debug.Log("I hope you have a great day!");
        }

        [ConsoleCmd("log_number {int}")]
        private void LogANumber(int number)
        {
            Debug.Log($"Great choice! Your number was: {number}");
        }
    }
}