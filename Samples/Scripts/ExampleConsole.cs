using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ContradictiveGames.CGConsole
{
    public class TestingClass
    {
        [ConsoleCmd("testing")]
        public void TestingFunction()
        {
            Debug.Log("We just successfully tested a function within a class that had their commands registered in the constructor");
        }

        public TestingClass()
        {
            ConsoleCommandRegistry.RegisterCommandsFrom(this);
        }
    }

    public class TestingClassNumberTwo
    {
        [ConsoleCmd("testing_again")]
        public void TestingFunctionNumberTwo()
        {
            Debug.Log("We just succesfully tested a function within a class that had their commands registered in the MonoBehavior");
        }
    }

    public class ExampleConsole : Console
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

        public TestingClass @class = new();
        public TestingClassNumberTwo class2 = new();


        protected override void Start()
        {
            base.Start();

            ConsoleCommandRegistry.RegisterCommandsFrom(class2);
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


        protected override void OnValidate()
        {
            base.OnValidate();

            if(settings == null) Debug.LogError($"We currently do not have a console settings asset");
            else ApplyConsoleTheme();
        }
    }
}
