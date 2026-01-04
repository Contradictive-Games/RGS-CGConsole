using UnityEditor;
using UnityEngine;

namespace ContradictiveGames.CGConsole.Editor
{
    [CustomEditor(typeof(CGConsolePackageSettings))]
    public class CGConsolePackageSettingsEditor : UnityEditor.Editor
    {
        private SerializedProperty enableLogging;
        private SerializedProperty defaultCommands;

        private void OnEnable()
        {
            if(target == null || serializedObject == null) return;

            enableLogging = serializedObject.FindProperty("EnableLoggingForCommandRegistration");
            defaultCommands = serializedObject.FindProperty("DefaultCommands");
        }

        public override void OnInspectorGUI()
        {
            if(target == null || serializedObject == null) {
                EditorGUILayout.HelpBox("We currently do not have any settings scriptable object", MessageType.Warning);
                return;
            }
            serializedObject.Update();
            EditorGUILayout.Space(10);

            EditorGUILayout.LabelField("CG Console Package Settings", EditorStyles.boldLabel);
            EditorGUILayout.HelpBox("These settings control the behavior of the CG Console package.", MessageType.Info);
            EditorGUILayout.Space(5);

            EditorGUILayout.LabelField("Logging", EditorStyles.boldLabel);
            EditorGUI.indentLevel++;
                EditorGUILayout.PropertyField(enableLogging);
                EditorGUILayout.HelpBox("Do you want it to be logged every single time a command is registered?", MessageType.Info);
            EditorGUI.indentLevel--;
            EditorGUILayout.Space(10);
            

            EditorGUILayout.LabelField("Default Commands", EditorStyles.boldLabel);
                EditorGUI.indentLevel++;

                if (defaultCommands.arraySize == 0)
                {
                    EditorGUILayout.HelpBox("No default commands configured", MessageType.Info);
                }
                else
                {
                    for (int i = 0; i < defaultCommands.arraySize; i++)
                    {
                        SerializedProperty command = defaultCommands.GetArrayElementAtIndex(i);
                        SerializedProperty commandName = command.FindPropertyRelative("CommandName");
                        SerializedProperty isEnabled = command.FindPropertyRelative("Enabled");

                        EditorGUILayout.BeginHorizontal();

                        EditorGUILayout.PropertyField(isEnabled, GUIContent.none, GUILayout.Width(20));
                        EditorGUILayout.LabelField(commandName.stringValue, GUILayout.ExpandWidth(true));

                        EditorGUILayout.EndHorizontal();
                    }
                }
                EditorGUILayout.Space(5);
    

            EditorGUI.indentLevel--;
            EditorGUILayout.Space(10);


            // Reset Button
            if (GUILayout.Button("Reset to Defaults", GUILayout.Height(25)))
            {
                if (EditorUtility.DisplayDialog("Reset Settings", 
                    "Are you sure you want to reset all settings to their default values?", 
                    "Reset", "Cancel"))
                {
                    (target as CGConsolePackageSettings)?.ResetToDefaults();
                    EditorUtility.SetDirty(target);
                }
            }


            serializedObject.ApplyModifiedProperties();
        }
    }
}