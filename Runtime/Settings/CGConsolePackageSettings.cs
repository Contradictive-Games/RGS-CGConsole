using System;
using UnityEditor;
using UnityEngine;

namespace ContradictiveGames.CGConsole
{
    [Serializable]
    internal class DefaultCommandSetting
    {
        public string CommandName;
        public bool Enabled = true;

        public DefaultCommandSetting(string name, bool enabled = true)
        {
            CommandName = name;
            Enabled = enabled;
        }
    }

    [CreateAssetMenu(fileName = "CGConsole Package Settings", menuName = "Contradictive Games/CGConsole/Package Settings")]
    internal class CGConsolePackageSettings : ScriptableObject
    {
        public const string k_PackageSettingsPath = "Assets/Resources/CGConsole/CGConsolePackageSettings.asset";


        [Header("Logging")]
        [Tooltip("If you want to see the logs for every time a command is registered")]
        public bool EnableLoggingForCommandRegistration = false;

        [Header("ICommandProvider Interface")]
        [Tooltip("If you want to enforce that every time you register a command, that class must implement the ICommandProvider interface")]
        public bool RequireInterfaceForRegistration = true;

        [Header("Command Naming")]
        [Tooltip("If you want to name your commands with more than just the basic alphanumeric and underscores, you can create your own custom regex")]
        public bool UseCustomRegexForCommandNaming = false;
        [Tooltip("Your custom regex string")]
        public string Regex = "^[a-zA-Z0-9_]+$";

        [Header("Default Commands")]
        [Tooltip("You can toggle which of the default commands you would like added")]
        [SerializeField] private DefaultCommandSetting[] DefaultCommands = new DefaultCommandSetting[]
        {
            new DefaultCommandSetting("help", true),
            new DefaultCommandSetting("clear", true),
            new DefaultCommandSetting("quit", true),
            new DefaultCommandSetting("load", true),
            new DefaultCommandSetting("set_intkey", true),
            new DefaultCommandSetting("set_floatkey", true),
            new DefaultCommandSetting("set_stringkey", true),
            new DefaultCommandSetting("get_intkey", true),
            new DefaultCommandSetting("get_floatkey", true),
            new DefaultCommandSetting("get_stringkey", true),
        };


        internal static CGConsolePackageSettings GetOrCreateSettings()
        {
            var settings = AssetDatabase.LoadAssetAtPath<CGConsolePackageSettings>(Utilities.k_SettingsAssetPath);
            if(settings == null)
            {
                settings = ScriptableObject.CreateInstance<CGConsolePackageSettings>();
                settings.ResetToDefaults();
                AssetDatabase.CreateAsset(settings, Utilities.k_SettingsAssetPath);
                AssetDatabase.SaveAssets();
            }

            return settings;
        }

        internal static SerializedObject GetSerializedSettings()
        {
            return new SerializedObject(GetOrCreateSettings());
        }


        public bool IsCommandEnabled(string commandName)
        {
            foreach(var cmd in DefaultCommands)
            {
                if(cmd.CommandName == commandName && cmd.Enabled) return true;
            }
            return false;
        }

        [ContextMenu("Reset All Settings to Defaults")]
        public void ResetToDefaults()
        {
            EnableLoggingForCommandRegistration = false;
            RequireInterfaceForRegistration = true;
            UseCustomRegexForCommandNaming = false;
            Regex = Utilities.k_DefaultCommandNameRegex;
            DefaultCommands = new DefaultCommandSetting[]
            {
                new DefaultCommandSetting("help", true),
                new DefaultCommandSetting("clear", true),
                new DefaultCommandSetting("quit", true),
                new DefaultCommandSetting("load", true),
                new DefaultCommandSetting("set_intkey", true),
                new DefaultCommandSetting("set_floatkey", true),
                new DefaultCommandSetting("set_stringkey", true),
                new DefaultCommandSetting("get_intkey", true),
                new DefaultCommandSetting("get_floatkey", true),
                new DefaultCommandSetting("get_stringkey", true),
            };
        }
    }
}