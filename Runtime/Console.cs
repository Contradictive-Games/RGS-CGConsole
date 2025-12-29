using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CGConsole
{
    [AddComponentMenu("CGConsole/Console")]
    public class Console : MonoBehaviour, ICommandProvider
    {
        [Header("Console Components")]
        [SerializeField] private TMP_InputField consoleInput;
        [SerializeField] private RectTransform consoleOutputContainer;
        [SerializeField] private ScrollRect scrollRect;
        [SerializeField] private TMP_Text consoleOutputPrefab;

        [Header("Settings")]
        public ConsoleSettings Settings;


        #region Enable/Disable


        protected virtual void OnEnable()
        {
            Application.logMessageReceived += CreateConsoleOutput;
        }


        protected virtual void OnDisable()
        {
            Application.logMessageReceived -= CreateConsoleOutput;
        }


        #endregion

        #region Initialization


        protected virtual void Awake()
        {
            if(consoleOutputPrefab == null)
            {
                Debug.LogWarning($"We are creating a basic ConsoleOutput Prefab, as none we assigned within the inspector");
                GameObject obj = new GameObject("consoleoutput");
                consoleOutputPrefab = obj.AddComponent<TMP_Text>();
                consoleOutputPrefab.fontSize = Settings.TextSize;
                consoleOutputPrefab.gameObject.SetActive(false);
            }
            if(consoleInput != null)
            {
                consoleInput.onSubmit.AddListener(OnCommandSubmitted);
            }
        }


        protected virtual void Start()
        {
            ConsoleCommandRegistry.RegisterAllCommands();
        }


        #endregion

        #region Validation


#if UNITY_EDITOR
        protected virtual void OnValidate()
        {
            if(consoleInput == null) Debug.LogWarning($"We currently do not have any input field added to the Developer Console", this);
            if(consoleOutputPrefab == null) Debug.LogWarning($"We currently do not have any console output prefab created within the inspector. One will be created by default but it is recommended to make one yourself", this);
            if(consoleOutputContainer == null) Debug.LogError($"We currently do not have a parent container for console output", this);
            if(scrollRect == null) Debug.LogWarning($"We currently do not have a scroll rect set for the console", this);
        }
#endif


        #endregion

        #region LogHandling


        protected virtual void CreateConsoleOutput(string message, string stackTrace = "", LogType type = LogType.Log)
        {
            if (consoleOutputContainer.childCount >= Settings.MaxLines)
            {
                Destroy(consoleOutputContainer.GetChild(0).gameObject);
            }

            TMP_Text output = Instantiate(consoleOutputPrefab, consoleOutputContainer);
            string log = $"<color=#{Settings.GetLogColorByLogType(type)}>[{DateTime.Now.ToString("HH:mm:ss")}]: {message} {(Settings.ShowStackTrace ? $"Stack Trace: {stackTrace}" : "")}</color>";

            output.SetText(log);

            Canvas.ForceUpdateCanvases();
            if(scrollRect != null) scrollRect.verticalNormalizedPosition = 0f;
        }


        #endregion

        #region InputField Handling


        protected virtual void OnCommandSubmitted(string input)
        {
            CommandResponse respone = ConsoleCommandRegistry.TryExecute(input);
            if(Settings.ShowCommandResponseLogs) {
                CreateConsoleOutput(respone.Message);
            }

            if(respone.ResponseType == ResponseType.Success)
            {
                consoleInput.text = "";
            }

            consoleInput.ActivateInputField();
        }


        #endregion


    }
}