using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management.Automation;

namespace EnumRun.Cmdlet
{
    [Cmdlet(VerbsCommon.Get, "EnumRunConfig")]
    public class GetEnumRunConfig : PSCmdlet
    {
        [Parameter]
        public string ConfigPath { get; set; }

        protected override void BeginProcessing()
        {
            Item.Config = EnumRunConfig.Load(ConfigPath);
        }

        protected override void ProcessRecord()
        {
            WriteObject(Item.Config);
        }
    }
}
