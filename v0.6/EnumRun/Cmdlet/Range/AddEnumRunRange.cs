using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management.Automation;

namespace EnumRun.Cmdlet
{
    [Cmdlet(VerbsCommon.Add, "EnumRunRange")]
    public class AddEnumRunRange : PSCmdlet
    {
        [Parameter(ValueFromPipeline = true)]
        public Range Range { get; set; }
        [Parameter(Position = 0)]
        public string Name { get; set; }
        [Parameter(Position = 1)]
        public int StartNumber { get; set; }
        [Parameter(Position = 2)]
        public int EndNumber { get; set; }

        protected override void BeginProcessing()
        {
            Item.Config = EnumRunConfig.Load();
        }

        protected override void ProcessRecord()
        {
            if (Range == null && !string.IsNullOrEmpty(Name))
            {
                Range range = new Range()
                {
                    Name = this.Name,
                    StartNumber = this.StartNumber,
                    EndNumber = this.EndNumber
                };
                //  重複した場合は?
                Item.Config.Ranges.Add(range);
            }
            else if (Range != null)
            {
                Item.Config.Ranges.Add(Range);
            }
            Item.Config.Save();
        }
    }
}
