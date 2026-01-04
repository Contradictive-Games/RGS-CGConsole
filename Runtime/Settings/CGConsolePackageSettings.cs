using System;
using System.Collections.Generic;
using UnityEngine;

namespace ContradictiveGames.CGConsole
{
    [Serializable]
    public class DefaultCommandSetting
    {
        public string CommandName;
        public bool Enabled = true;

        public DefaultCommandSetting(string name, bool enabled = true)
        {
            CommandName = name;
            Enabled = enabled;
        }
    }

    [CreateAssetMenu(fileName = "CGConsole Package Settings", menuName = "Contradictive Games/CG Console/Package Settings")]
    public class CGConsolePackageSettings : ScriptableObject
    {
        [Header("Logging")]
        public bool EnableLoggingForCommandRegistration = false;

        [Header("Default Commands")]
        public List<DefaultCommandSetting> DefaultCommands = new()
        {
            new DefaultCommandSetting("help", true),
            new DefaultCommandSetting("clear", true),
            new DefaultCommandSetting("quit", true)
        };


        private static CGConsolePackageSettings instance;

        public static CGConsolePackageSettings Instance
        {
            get
            {
                if(instance == null)
                {
                    instance = Resources.Load<CGConsolePackageSettings>("CGConsolePackageSettings");
#if UNITY_EDITOR
                    if(instance == null)
                    {
                        instance = CreateInstance<CGConsolePackageSettings>();
                        instance.ResetToDefaults();
                    }
#endif
                }

                return instance;

            }
        }

        public bool IsCommandEnabled(string commandName)
        {
            var command = DefaultCommands.Find(c => c.CommandName == commandName);
            return command?.Enabled ?? true;
        }

        
        public void ResetToDefaults()
        {
            EnableLoggingForCommandRegistration = false;
            DefaultCommands = new List<DefaultCommandSetting>
            {
                new DefaultCommandSetting("help", true),
                new DefaultCommandSetting("clear", true),
                new DefaultCommandSetting("quit", true)
            };
        }
    }
}