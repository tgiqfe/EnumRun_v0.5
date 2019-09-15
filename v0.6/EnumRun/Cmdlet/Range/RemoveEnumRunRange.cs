using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management.Automation;

namespace EnumRun.Cmdlet
{
    [Cmdlet(VerbsCommon.Remove, "EnumRunRange")]
    public class RemoveEnumRunRange : PSCmdlet
    {
        [Parameter(ValueFromPipeline = true)]
        public Range[] Range { get; set; }
        [Parameter(Position = 0)]
        public string Name { get; set; }

        protected override void BeginProcessing()
        {
            Item.Config = EnumRunConfig.Load();
        }

        protected override void ProcessRecord()
        {
            if (Range == null && !string.IsNullOrEmpty(Name))
            {
                foreach(Range range in Item.Config.GetRange(Name))
                {
                    Item.Config.Ranges.Remove(range);
                }
            }
            else if (Range != null)
            {
                //  名前判定せず、インスタンスの中身が一致したら削除
                foreach(Range range in Range)
                {
                    Item.Config.Ranges.Remove(range);
                }
            }
            Item.Config.Save();
        }
    }
}
