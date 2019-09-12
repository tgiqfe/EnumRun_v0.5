using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management.Automation;

namespace EnumRun.Cmdlet
{
    [Cmdlet(VerbsLifecycle.Start, "LogonScript")]
    public class StartLogonScript : PSCmdlet
    {
        protected override void BeginProcessing()
        {
            Item.Config = EnumRunControl.Read();
        }

        protected override void ProcessRecord()
        {
            EnumRunControl.StartEnumRun("LogonScript");
        }
    }
}
