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
        protected override void BeginProcessing()
        {
            Item.Config = EnumRunControl.Read();
        }

        protected override void ProcessRecord()
        {
            EnumRunControl.StartEnumRun("StartupScript");
        }
    }
}
