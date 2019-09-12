using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnumRun
{
    class DefaultLanguageSetting
    {
        public static SerializableDictionary<string, Language> Create()
        {
            SerializableDictionary<string, Language> langs = new SerializableDictionary<string, Language>();

            langs["exe"] = new Language()
            {
                Name = "exe",
                OS = "Windows",
                Extensions = new string[] { ".exe" },
                Command = null,
            };
            langs["cmd"] = new Language()
            {
                Name = "cmd",
                OS = "Windows",
                Extensions = new string[] { ".bat", ".cmd" },
                Command = "cmd",
                ArgsPrefix = "/c \"",
                ArgsMidWithoutArgs = "\"",
                ArgsMidWithArgs = "\" ",
                ArgsSuffix = "",
            };
            langs["PowerShell"] = new Language()
            {
                Name = "PowerShell",
                OS = "Windows",
                Extensions = new string[] { ".ps1" },
                Command = @"C:\Windows\System32\WindowsPowerShell\v1.0\powershell.exe",
                ArgsPrefix = "-ExecutionPolicy Unrestricted -File \"",
                ArgsMidWithoutArgs = "\"",
                ArgsMidWithArgs = "\" ",
                ArgsSuffix = "",
            };
            langs["WScript"] = new Language()
            {
                Name = "WScript",
                OS = "Windows",
                Extensions = new string[] { ".vbs", ".vbe", ".js", ".jse", ".wsf", ".wsh" },
                Command = @"C:\Windows\System32\wscript.exe",
                ArgsPrefix = "//nologo \"",
                ArgsMidWithoutArgs = "\"",
                ArgsMidWithArgs = "\" ",
                ArgsSuffix = ""
            };
            langs["Node"] = new Language()
            {
                Name = "Node",
                OS = "Windows",
                Extensions = new string[] { ".js" },
                Command = @"C:\Program Files\nodejs\node.exe",
                ArgsPrefix = "\"",
                ArgsMidWithoutArgs = "\"",
                ArgsMidWithArgs = "\" ",
                ArgsSuffix = ""
            };
            langs["Go"] = new Language()
            {
                Name = "Go",
                OS = "Windows",
                Extensions = new string[] { ".go" },
                Command = @"C:\Program Files\Go\bin\go.exe",
                ArgsPrefix = "\"",
                ArgsMidWithoutArgs = "\"",
                ArgsMidWithArgs = "\" ",
                ArgsSuffix = ""
            };
            return langs;
        }
    }
}
