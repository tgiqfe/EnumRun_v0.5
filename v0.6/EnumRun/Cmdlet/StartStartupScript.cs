using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management.Automation;
using System.IO;

namespace EnumRun.Cmdlet
{
    [Cmdlet(VerbsCommon.Add, "EnumRunLanguage")]
    public class StartStartupScript : PSCmdlet
    {
        //  パラメータ無しでコマンドレットのみを実行

        protected override void BeginProcessing()
        {
            Item.Config = Functions.Read();
        }

        protected override void ProcessRecord()
        {
            if (Directory.Exists(Item.Config.FilesPath))
            {
                foreach (string scriptFile in Directory.GetFiles(Item.Config.FilesPath))
                {
                    Script script = new Script()
                    {
                        Name = Path.GetFileName(scriptFile),
                        File = scriptFile
                    };
                }
            }
        }
    }
}
