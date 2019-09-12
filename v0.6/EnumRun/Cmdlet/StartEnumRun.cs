using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management.Automation;

namespace EnumRun.Cmdlet
{
    [Cmdlet(VerbsLifecycle.Start, "EnumRun")]
    public class StartEnumRun : PSCmdlet
    {
        [Parameter(Mandatory = true, Position = 0)]
        public string ProcessName { get; set; }

        protected override void BeginProcessing()
        {
            Item.Config = EnumRunControl.Read();
        }

        protected override void ProcessRecord()
        {
            EnumRunControl.StartEnumRun(ProcessName);
        }
    }
}
