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
                if (Item.Config.ContainsRange(Name))
                {
                    //  すでに同じ名前のRangeがある為、追加不可
                    return;
                }

                Item.Config.Ranges.Add(new Range()
                {
                    Name = this.Name,
                    StartNumber = this.StartNumber,
                    EndNumber = this.EndNumber
                });
            }
            else if (Range != null)
            {
                if (Item.Config.ContainsRange(Range))
                {
                    //  すでに同じ名前のLanguageがある為、追加不可
                    return;
                }
                Item.Config.Ranges.Add(Range);
            }
            Item.Config.Save();
        }
    }
}
