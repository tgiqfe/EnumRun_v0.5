﻿using System;
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
                if (Item.Config.ContainsLanguage(Name))
                {
                    int index = Item.Config.Languages.FindIndex(x => 
                        x.Name.Equals(Name, StringComparison.OrdinalIgnoreCase));
                    Item.Config.Languages.RemoveAt(index);
                }
            }
            else if (Language != null)
            {
                if (Item.Config.ContainsLanguage(Language))
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
