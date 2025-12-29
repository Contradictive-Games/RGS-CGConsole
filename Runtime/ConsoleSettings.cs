using UnityEngine;

namespace CGConsole
{
    [CreateAssetMenu(fileName = "Console Settings", menuName = "Custom/Dev/Console Settings")]
    public class ConsoleSettings : ScriptableObject
    {
        [Header("Console Panel")]
        public Color ConsoleDragBar = Color.black;
        public Color ConsoleBackground = Color.gray;

        [Header("Input Field")]
        public Color InputFieldBackground = Color.white;
        public Color InputText = Color.black;
        public Color PlaceHolderText = Color.black;

        [Header("Scroll Bar")]
        public Color ScrollbarHandle = Color.white;
        public Color ScrollbarBackground = Color.gray;

        [Header("Text Colors")]        
        public Color NormalText = Color.white;
        public Color SuccessText = Color.green;
        public Color WarningText = Color.yellow;
        public Color ErrorText = Color.red;

        [Header("Additional Settings")]
        public int MaxLines = 200;

    }
}
