using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management.Automation;
using System.IO;

namespace EnumRun.Cmdlet
{
    [Cmdlet(VerbsCommon.Enter, "LogonScript")]
    public class EnterLogonScript : PSCmdlet
    {
        const string ProcessName = "LogonScript";

        protected override void BeginProcessing()
        {
            Item.Config = EnumRunConfig.Load();
        }

        protected override void ProcessRecord()
        {
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
        }
    }
}
