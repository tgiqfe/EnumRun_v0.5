using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management.Automation;

namespace EnumRun.Cmdlet
{
    [Cmdlet(VerbsCommon.Get, "EnumRunSetting")]
    public class GetEnumRunSetting : PSCmdlet
    {
        [Parameter(Position = 0), Alias("Path")]
        public string SettingPath { get; set; }

        protected override void BeginProcessing()
        {
            Item.Config = EnumRunConfig.Load(SettingPath);
        }

        protected override void ProcessRecord()
        {
            WriteObject(Item.Config);
        }
    }
}
