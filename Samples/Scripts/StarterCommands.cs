using UnityEngine;
using UnityEngine.SceneManagement;

namespace ContradictiveGames.CGConsole
{
    public class StarterCommands : MonoBehaviour
    {

        private void Awake()
        {
            CGConsoleCommands.RegisterCommandsFrom(this);
        }


        [ConsoleCmd("load", "Load a level by the level's name")]
        public void LoadScene(string sceneName)
        {
            if (SceneManager.GetSceneByName(sceneName) != null)
            {
                SceneManager.LoadScene(sceneName);
                Debug.Log($"Loading level: {sceneName}");
            }
        }

        [ConsoleCmd("set_intkey", "Set the value of a saved integer by the key name")]
        public void SetIntKey(string keyName, int value) {
            PlayerPrefs.SetInt(keyName, value);
            PlayerPrefs.Save();
        }
        [ConsoleCmd("set_floatkey", "Set the value of a saved float by the key name")]
        public void SetFloatKey(string keyName, float value) {
            PlayerPrefs.SetFloat(keyName, value);
            PlayerPrefs.Save();
        }
        [ConsoleCmd("set_stringkey", "Set the value of a saved string by the key name")]
        public void SetStringKey(string keyName, string value) {
            PlayerPrefs.SetString(keyName, value);
            PlayerPrefs.Save();
        }

        [ConsoleCmd("log_intkey")] public void LogIntKey(string keyName) => Debug.Log($"Value for `{keyName}`: {PlayerPrefs.GetInt(keyName)}");
        [ConsoleCmd("log_floatkey")] public void LogFloatKey(string keyName) => Debug.Log($"Value for `{keyName}`: {PlayerPrefs.GetFloat(keyName)}");
        [ConsoleCmd("log_stringkey")] public void LogStringKey(string keyName) => Debug.Log($"Value for `{keyName}`: {PlayerPrefs.GetString(keyName)}");
    }
}