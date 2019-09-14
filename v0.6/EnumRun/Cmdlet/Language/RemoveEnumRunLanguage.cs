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
        public Language Language { get; set; }
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
                Language lang = Item.Config.GetLanguage(Name);
                if (lang != null)
                {
                    Item.Config.Languages.Remove(lang);
                }
            }
            else if (Language != null)
            {
                Language lang = Item.Config.GetLanguage(Language.Name);
                if (lang != null)
                {
                    int index = Item.Config.Languages.FindIndex(x =>
                        x.Name.Equals(Language.Name, StringComparison.OrdinalIgnoreCase));
                    Item.Config.Languages.RemoveAt(index);
                }
            }
            Item.Config.Save();
        }
    }
}
