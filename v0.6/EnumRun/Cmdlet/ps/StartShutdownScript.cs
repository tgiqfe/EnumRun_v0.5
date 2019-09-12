using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management.Automation;

namespace EnumRun.Cmdlet
{
    [Cmdlet(VerbsLifecycle.Start, "ShutdownScript")]
    public class StartShutdownScript : PSCmdlet
    {
        protected override void BeginProcessing()
        {
            Item.Config = EnumRunControl.Read();
        }

        protected override void ProcessRecord()
        {
            EnumRunControl.StartEnumRun("ShutdownScript");
        }
    }
}
