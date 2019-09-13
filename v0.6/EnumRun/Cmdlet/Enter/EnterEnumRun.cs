using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management.Automation;

namespace EnumRun.Cmdlet
{
    [Cmdlet(VerbsCommon.Enter, "EnumRun")]
    public class EnterEnumRun : PSCmdlet
    {
        [Parameter(Mandatory = true, Position = 0)]
        public string ProcessName { get; set; }

        protected override void BeginProcessing()
        {
            Item.Config = EnumRunConfig.Load();
        }

        protected override void ProcessRecord()
        {
            Functions.StartEnumRun(ProcessName);
        }
    }
}
