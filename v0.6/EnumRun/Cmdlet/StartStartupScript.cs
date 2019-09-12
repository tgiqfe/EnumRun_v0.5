using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management.Automation;
using System.IO;

namespace EnumRun.Cmdlet
{
    [Cmdlet(VerbsLifecycle.Start, "StartupScript")]
    public class StartStartupScript : PSCmdlet
    {
        const string ProcessName = "StartupScript";

        //  パラメータ無しでコマンドレットのみを実行

        protected override void BeginProcessing()
        {
            Item.Config = Functions.Read();
        }

        protected override void ProcessRecord()
        {
            int startNum = Item.Config.Ranges[ProcessName].StartNumber;
            int endNum = Item.Config.Ranges[ProcessName].EndNumber;

            if (Directory.Exists(Item.Config.FilesPath))
            {
                //  スクリプトファイルの列挙
                List<Script> scriptList = new List<Script>();
                foreach (string scriptFile in Directory.GetFiles(Item.Config.FilesPath))
                {
                    Script script = new Script(scriptFile, startNum, endNum);
                    if (script.Enabled)
                    {
                        scriptList.Add(script);
                        //DataSerializer.Serialize<Script>(script, Console.Out, ".json");
                    }
                }

                //  スクリプトファイルの実行
                foreach(Script script in scriptList)
                {

                }

            }
        }
    }
}
