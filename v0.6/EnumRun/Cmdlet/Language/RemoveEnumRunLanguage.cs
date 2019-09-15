using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management.Automation;

namespace EnumRun.Cmdlet
{
    [Cmdlet(VerbsCommon.Remove, "EnumRunLanguage")]
    public class RemoveEnumRunLanguage : PSCmdlet
    {
        [Parameter(ValueFromPipeline = true)]
        public Language[] Language { get; set; }
        [Parameter(Position = 0)]
        public string Name { get; set; }

        protected override void BeginProcessing()
        {
            Item.Config = EnumRunConfig.Load();
        }

        protected override void ProcessRecord()
        {
            if (Language == null && !string.IsNullOrEmpty(Name))
            {
                foreach (Language lang in Item.Config.GetLanguage(Name))
                {
                    Item.Config.Languages.Remove(lang);
                }
            }
            else if (Language != null)
            {
                //  名前判定せず、インスタンスの中身が一致したら削除
                foreach (Language lang in Language)
                {
                    Item.Config.Languages.Remove(lang);
                }
            }
            Item.Config.Save();
        }
    }
}
