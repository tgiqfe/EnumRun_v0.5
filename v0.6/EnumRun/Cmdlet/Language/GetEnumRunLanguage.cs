using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management.Automation;
using System.Text.RegularExpressions;

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
            if (string.IsNullOrEmpty(Name))
            {
                WriteObject(Item.Config.Languages);
            }
            else
            {
                string patternString = Regex.Replace(Name, ".",
                    x =>
                    {
                        string y = x.Value;
                        if (y.Equals("?")) { return "."; }
                        else if (y.Equals("*")) { return ".*"; }
                        else { return Regex.Escape(y); }
                    });
                if (!patternString.StartsWith("*")) { patternString = "^" + patternString; }
                if (!patternString.EndsWith("*")) { patternString = patternString + "$"; }
                Regex regPattern = new Regex(patternString, RegexOptions.IgnoreCase);

                SerializableDictionary<string, Language> retDictionary = new SerializableDictionary<string, Language>();
                foreach (KeyValuePair<string, Language> kv in Item.Config.Languages)
                {
                    if (regPattern.IsMatch(kv.Value.Name))
                    {
                        retDictionary[kv.Key] = kv.Value;
                    }
                }
                WriteObject(retDictionary);
            }
        }
    }
}
