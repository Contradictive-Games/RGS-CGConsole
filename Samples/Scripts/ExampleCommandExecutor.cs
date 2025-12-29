using UnityEngine;
using UnityEngine.UI;

namespace CGConsole
{
    public class ExampleCommandExecutor : MonoBehaviour
    {
        [Header("Prefab")]
        public GameObject Prefab;
        public Button ExecuteCommandFromSpawnedPrefabButton;
        public Button LogWarningButton;
        public Button LogErrorButton;

        private void Start()
        {
            if(ExecuteCommandFromSpawnedPrefabButton != null)
            {
                ExecuteCommandFromSpawnedPrefabButton.onClick.AddListener(
                    delegate { LogAStatementFromASpawnedPrefab(); }
                );
            }
            if (LogWarningButton != null) LogWarningButton.onClick.AddListener(() => ConsoleCommandRegistry.TryExecute("log_warning"));
            if (LogErrorButton != null) LogErrorButton.onClick.AddListener(() => ConsoleCommandRegistry.TryExecute("log_error"));
        }

        public void LogStatementButtonPress()
        {
            ConsoleCommandRegistry.TryExecute("log_nice");
        }

        public void LogNumberFiveButtonPress()
        {
            ConsoleCommandRegistry.TryExecute("log_number 5");
        }


        public void SpawnAnObjectWithCommands()
        {
            Instantiate(Prefab);
            ExecuteCommandFromSpawnedPrefabButton.interactable = true;
        }

        private void LogAStatementFromASpawnedPrefab()
        {
            ConsoleCommandRegistry.TryExecute("order_66");
        }
        
    }
}