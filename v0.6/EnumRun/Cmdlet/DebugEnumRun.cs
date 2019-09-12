using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management.Automation;

namespace EnumRun.Cmdlet
{
    [Cmdlet(VerbsDiagnostic.Debug, "EnumRun")]
    public class DebugEnumRun : PSCmdlet
    {
        protected override void ProcessRecord()
        {

            EnumRunConfig erc = new EnumRunConfig();
            erc.Name = "EnumRun";
            erc.FilesPath = "Files";
            erc.LogsPath = "Logs";
            erc.Languages = DefaultLanguageSetting.Create();
            erc.Ranges = DefaultRangeSettings.Create();


            WriteObject(erc);


        }
    }
}
