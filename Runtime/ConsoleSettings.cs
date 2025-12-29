using UnityEngine;

namespace CGConsole
{
    [CreateAssetMenu(fileName = "CGConsole Settings", menuName = "CGConsole Settings")]
    public class ConsoleSettings : ScriptableObject
    {
        [Header("Console Panel")]
        public Color TopBarColor = Color.black;
        public Color TitleTitleColor = Color.white;
        public Color BackgroundColor = Color.gray;

        [Header("Input Field")]
        public Color InputFieldBackgroundColor = Color.white;
        public Color InputTextColor = Color.black;
        public Color PlaceHolderTextColor = Color.black;

        [Header("Scroll Bar")]
        public Color ScrollbarHandleColor = Color.white;
        public Color ScrollbarBackgroundColor = Color.gray;

        [Header("Text Colors")]        
        public Color NormalTextColor = Color.white;
        public Color SuccessTextColor = Color.green;
        public Color WarningTextColor = Color.yellow;
        public Color ErrorTextColor = Color.red;

        [Header("Additional Settings")]
        [Tooltip("How many output logs can be held within the parent container at most?")]
        public int MaxLines = 800;

    }
}
