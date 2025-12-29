using TMPro;
using UnityEngine;
using System;
using UnityEngine.UI;
using System.Collections.Generic;

namespace CGConsole
{
    public class DeveloperConsole : MonoBehaviour, ICommandProvider
    {
        [Header("Settings")]
        [SerializeField] private ConsoleSettings settings;
        [SerializeField] private GameObject logEntryPrefab;


        [Header("Refs")]
        [SerializeField] private Transform logEntriesContainer;
        [SerializeField] private TMP_InputField consoleInput;
        [SerializeField] private ScrollRect scrollRect;

        [Header("Panel Components")]
        [SerializeField] private Image background;
        [SerializeField] private Image topBar;
        [SerializeField] private TMP_Text titleText;
        [SerializeField] private Image inputFieldBackground;
        [SerializeField] private TMP_Text inputFieldText;
        [SerializeField] private TMP_Text inputFieldPlaceholder;
        [SerializeField] private Image scrollBarHandle;
        [SerializeField] private Image scrollBarBackground;


        private List<ConsoleCommand> allCommands;


        private void OnEnable()
        {
            Application.logMessageReceived += HandleUnityLog;
        }


        private void OnDisable()
        {
            Application.logMessageReceived -= HandleUnityLog;
        }


        private void Start()
        {
            consoleInput.onSubmit.AddListener(OnCommandEntered);
            RegisterCommands();
        }

        [ContextMenu("Apply Theme")]
        private void ApplyConsoleTheme()
        {
            if(settings == null) return;

            if(background != null) background.color = settings.BackgroundColor;
            if(titleText != null) titleText.color = settings.TitleTitleColor;
            if(topBar != null) topBar.color = settings.TopBarColor;
            if(inputFieldBackground != null) inputFieldBackground.color = settings.InputFieldBackgroundColor;
            if(inputFieldText != null) inputFieldText.color = settings.InputTextColor;
            if(inputFieldPlaceholder != null) inputFieldPlaceholder.color = settings.PlaceHolderTextColor;
            if(scrollBarHandle != null) scrollBarHandle.color = settings.ScrollbarHandleColor;
            if(scrollBarBackground != null) scrollBarBackground.color = settings.ScrollbarBackgroundColor;
        }


        private void OnValidate()
        {
            if(settings == null) Debug.LogError($"We currently do not have a console settings asset");
            else ApplyConsoleTheme();
        }


        private void RegisterCommands()
        {
            ConsoleCommandRegistry.RegisterAllCommands();
            allCommands = ConsoleCommandRegistry.GetAllCommands();
        }


        private void OnCommandEntered(string input)
        {
            CommandResponse response = ConsoleCommandRegistry.TryExecute(input);
            if(settings.ShowCommandResponseLogs && response.ResponseType == ResponseType.Success)
            {
                AddLogLine($"<color=green>{response.Message}</color>", LogType.Log);
            } 
            else if(settings.ShowCommandResponseLogs) AddLogLine($"<color=red> {response.Message}</color>", LogType.Error);

            consoleInput.text = "";
            consoleInput.ActivateInputField();
        }


        private void HandleUnityLog(string message, string s2, LogType type)
        {
            AddLogLine(message, type);
        }


        private void AddLogLine(string message, LogType logType = LogType.Log)
        {
            if (logEntriesContainer.childCount >= settings.MaxLines)
            {
                Destroy(logEntriesContainer.GetChild(0).gameObject);
            }
            GameObject log = Instantiate(logEntryPrefab, logEntriesContainer);
            TMP_Text logText = log.GetComponent<TMP_Text>();

            logText.SetText($"<color=#{LogColor(logType)}>[{DateTime.Now.ToString("HH:mm:ss")}]: {message}</color>");

            Canvas.ForceUpdateCanvases();
            scrollRect.verticalNormalizedPosition = 0f;
        }


        private string LogColor(LogType logType)
        {
            switch (logType)
            {
                case LogType.Log:
                    return ColorUtility.ToHtmlStringRGBA(settings.NormalTextColor);
                case LogType.Warning:
                    return ColorUtility.ToHtmlStringRGBA(settings.WarningTextColor);
                case LogType.Error:
                    return ColorUtility.ToHtmlStringRGBA(settings.ErrorTextColor);
                default:
                    return "white";
            }
        }


        #region Developer Commands


        [ConsoleCmd("killall 1", "Will kill all nearby Enemies")]
        private void KillAll()
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (var enemy in enemies)
            {
                enemy.SetActive(false);
            }
        }

        [ConsoleCmd("help")]
        private void GetAllCommands()
        {
            if(allCommands.Count == 0)
            {
                allCommands = ConsoleCommandRegistry.GetAllCommands();
            }
            // AddLogLine($"List of all commands: ");
            string response = "Commands List: \n";

            foreach(var cmd in allCommands)
            {
                response += $"{cmd.Command}" + (!String.IsNullOrWhiteSpace(cmd.Description) ? "     |       description: " + cmd.Description : "") + "\n";
            }
            AddLogLine(response);
        }


        #endregion
    }
}
