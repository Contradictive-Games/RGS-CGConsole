using UnityEngine;

namespace ContradictiveGames.CGConsole
{
    [CreateAssetMenu(fileName = "Example Console Settings", menuName = "CGConsole Demo/Example Console Settings")]
    public class ExampleConsoleSettings : ConsoleSettings
    {
        [Header("Console Panel")]
        public Color TopBarColor = Color.black;
        public Color TitleTitleColor = Color.white;
        public Color BackgroundColor = Color.gray;
        
        [Header("Scroll Bar")]
        public Color ScrollbarHandleColor = Color.white;
        public Color ScrollbarBackgroundColor = Color.gray;
    }
}