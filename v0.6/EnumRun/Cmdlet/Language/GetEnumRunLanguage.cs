using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management.Automation;

namespace EnumRun.Cmdlet
{
    [Cmdlet(VerbsCommon.Get, "EnumRunLanguage")]
    public class GetEnumRunLanguage : PSCmdlet
    {
        [Parameter(Position = 0)]
        public string Name { get; set; }

        protected override void BeginProcessing()
        {
            Item.Config = EnumRunConfig.Load();
        }

        protected override void ProcessRecord()
        {
            if(Name == null)
            {
                WriteObject(Item.Config.Languages);
            }
            else
            {
                WriteObject(Item.Config.GetLanguage(Name));
            }
        }
    }
}
