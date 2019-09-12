using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management.Automation;
using System.IO;
using System.Diagnostics;

namespace EnumRun.Cmdlet
{
    [Cmdlet(VerbsCommon.Get, "EnumRunConfig")]
    public class SetEnumRunConfig : PSCmdlet
    {
        [Parameter]
        public string Name { get; set; }
        [Parameter]
        public string FileDirectoryPath { get; set; }
        [Parameter]
        public string LogDirectoryPath { get; set; }
        


        protected override void ProcessRecord()
        {
            EnumRunConfig erc = new EnumRunConfig();
            erc.Name = "EnumRunConfig";
            erc.FileDirectoryPath = "Files";
            erc.LogDirectoryPath = "Logs";
            erc.LanguageList = DefaultLanguageSetting.Create();
            erc.RangeList = DefaultRangeSettings.Create();


            string outputFile = Path.Combine(
                Environment.ExpandEnvironmentVariables("%PROGRAMDATA%"),
                Item.APPLICATION_NAME, "Conf.yml");
            if (!Directory.Exists(Path.GetDirectoryName(outputFile)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(outputFile));
            }
            DataSerializer.Serialize<EnumRunConfig>(erc, outputFile);



        }
    }
}
