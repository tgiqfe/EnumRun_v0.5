using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management.Automation;

namespace EnumRun.Cmdlet
{
    [Cmdlet(VerbsCommon.Set, "EnumRunRange")]
    public class SetEnumRunRange : PSCmdlet
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
                Range range = Item.Config.GetRange(Name);
                if(range == null)
                {
                    //  存在しない場合は何もしない
                    return;
                }
                else
                {
                    range.StartNumber = StartNumber;
                    range.EndNumber = EndNumber;
                }
            }
            else if(Range != null)
            {
                Range range = Item.Config.GetRange(Name);
                if(range == null)
                {
                    //  存在しない場合は何もしない
                    return;
                }
                else
                {
                    range = Range;
                }
            }
            Item.Config.Save();
        }
    }
}
