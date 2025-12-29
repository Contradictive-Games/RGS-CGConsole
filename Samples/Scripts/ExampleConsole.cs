using TMPro;
using UnityEngine;
using System;
using UnityEngine.UI;
using System.Collections.Generic;

namespace CGConsole
{
    public class ExampleConsole : Console, ICommandProvider
    {
        [Header("Settings")]
        private ExampleConsoleSettings settings => Settings as ExampleConsoleSettings;

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


        protected override void OnValidate()
        {
            base.OnValidate();

            if(settings == null) Debug.LogError($"We currently do not have a console settings asset");
            else ApplyConsoleTheme();
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
            CreateConsoleOutput(response);
        }


        #endregion
    }
}
