﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management.Automation;
using System.IO;

namespace EnumRun.Cmdlet
{
    [Cmdlet(VerbsCommon.Enter, "ShutdownScript")]
    public class EnterShutdownScript : PSCmdlet
    {
        const string ProcessName = "ShutdownScript";

        protected override void BeginProcessing()
        {
            Item.Config = EnumRunConfig.Load();
            Item.Logger = Function.SetLogger(ProcessName);
        }

        protected override void ProcessRecord()
        {
            if (Item.Config.RunOnce && !BootAndLogonSession.Check(ProcessName))
            {
                Item.Logger.Warn("RunOnce有効で2回以上実行しようとした為、終了");
                return;
            }

            Item.Logger.Debug("開始 {0}", ProcessName);

            Range range = Item.Config.Ranges.FirstOrDefault(x => x.Name.Equals(ProcessName, StringComparison.OrdinalIgnoreCase));
            if (range != null)
            {
                if (Directory.Exists(Item.Config.FilesPath))
                {
                    //  スクリプトファイルの列挙
                    List<Script> scriptList = new List<Script>();
                    foreach (string scriptFile in Directory.GetFiles(Item.Config.FilesPath))
                    {
                        Script script = new Script(scriptFile, range.StartNumber, range.EndNumber);
                        if (script.Enabled)
                        {
                            script.Process();
                        }
                    }
                }
            }

            Item.Logger.Debug("終了 {0}", ProcessName);
        }
    }
}
