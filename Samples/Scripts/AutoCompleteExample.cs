using UnityEngine;

namespace ContradictiveGames.CGConsole
{
    public class AutoCompleteExample : MonoBehaviour, ICommandProvider
    {
        [ConsoleCmd("test_complete_1")]
        public void TestAutoComplete1()
        {
            Debug.Log("Log!");
        }
        [ConsoleCmd("test_complete_2")]
        public void TestAutoComplete2()
        {
            Debug.Log("Log!");
        }
        [ConsoleCmd("test_complete_3")]
        public void TestAutoComplete3()
        {
            Debug.Log("Log!");
        }
        [ConsoleCmd("test_complete_4")]
        public void TestAutoComplete4()
        {
            Debug.Log("Log!");
        }
        [ConsoleCmd("test_complete_5")]
        public void TestAutoComplete5()
        {
            Debug.Log("Log!");
        }
    }
}