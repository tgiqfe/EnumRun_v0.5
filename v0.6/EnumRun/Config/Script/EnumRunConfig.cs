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
        public string FilesPath { get; set; }
        public string LogsPath { get; set; }
        public string OutputPath { get; set; }
        public bool DebugMode { get; set; }
        public SerializableDictionary<string, Range> Ranges { get; set; }
        public SerializableDictionary<string, Language> Languages { get; set; }

        public EnumRunConfig() { }
    }
}
