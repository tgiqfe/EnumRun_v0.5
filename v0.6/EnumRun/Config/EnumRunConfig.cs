using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnumRun
{
    public class EnumRunConfig
    {
        public string Name { get; set; }
        public string FileDirectoryPath { get; set; }
        public string LogDirectoryPath { get; set; }
        public List<Range> RangeList { get; set; }
        public SerializableDictionary<string, Language> LanguageList { get; set; }

        public EnumRunConfig() { }
    }
}
