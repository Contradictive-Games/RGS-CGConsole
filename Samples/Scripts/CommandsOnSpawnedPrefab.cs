using UnityEngine;

namespace ContradictiveGames.CGConsole
{
    public class CommandsOnSpawnedPrefab : MonoBehaviour, ICommandProvider
    {
        private void OnEnable()
        {
            Debug.Log("I have been spanwed, try to type the command order_66 - it should work now!");
            ConsoleCommandRegistry.RegisterCommandsFrom(this);
        }

        [ConsoleCmd("order_66")]
        private void LogAStatementFromPrefab()
        {
            Debug.Log("We have executed Order 66.");
        }
    }
}