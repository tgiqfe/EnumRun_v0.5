using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management.Automation;

namespace EnumRun.Cmdlet
{
    [Cmdlet(VerbsCommon.Set, "EnumRunConfig")]
    public class SetEnumRunConfig : PSCmdlet
    {
        [Parameter]
        public string FilesPath { get; set; }
        [Parameter]
        public string LogsPath { get; set; }
        [Parameter]
        public string OutputPath { get; set; }
        [Parameter]
        public SwitchParameter DebugMode { get; set; }
        [Parameter]
        public SerializableDictionary<string, Range> Ranges { get; set; }
        [Parameter]
        public SerializableDictionary<string, Language> Languages { get; set; }
        [Parameter]
        public SwitchParameter DefaultSetting { get; set; }

        protected override void BeginProcessing()
        {
            Item.Config = EnumRunConfig.Load();
        }

        protected override void ProcessRecord()
        {
            if (DefaultSetting)
            {
                Item.Config = new EnumRunConfig(true);
            }
            else
            {
                if (FilesPath != null) { Item.Config.FilesPath = this.FilesPath; }
                if (LogsPath != null) { Item.Config.LogsPath = this.LogsPath; }
                if (OutputPath != null) { Item.Config.OutputPath = this.OutputPath; }
                if (DebugMode) { Item.Config.DebugMode = true; }
                if (Ranges != null) { Item.Config.Ranges = this.Ranges; }
                if (Languages != null) { Item.Config.Languages = this.Languages; }
            }
            Item.Config.Save();
            WriteObject(Item.Config);
        }
    }
}
