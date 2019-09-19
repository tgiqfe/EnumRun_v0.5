﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management.Automation;
using System.IO;

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
        public bool? DebugMode { get; set; }
        [Parameter]
        public bool? RunOnce { get; set; }
        [Parameter]
        public Range[] Ranges { get; set; }
        [Parameter]
        public Language[] Languages { get; set; }
        [Parameter]
        public SwitchParameter DefaultSetting { get; set; }
        [Parameter, ValidateSet(Item.JSON, Item.XML, Item.YML)]
        public string DataType { get; set; }

        protected override void BeginProcessing()
        {
            Item.Config = EnumRunConfig.Load();

            DataType = new string[] { Item.JSON, Item.XML, Item.YML }.
                FirstOrDefault(x => x.Equals(DataType, StringComparison.OrdinalIgnoreCase));
        }

        protected override void ProcessRecord()
        {
            //  データタイプ指定
            if (DataType != null)
            {
                string jsonFile = Path.Combine(Item.WORK_DIR, Item.CONFIG_JSON);
                string xmlFile = Path.Combine(Item.WORK_DIR, Item.CONFIG_XML);
                string ymlFile = Path.Combine(Item.WORK_DIR, Item.CONFIG_YML);
                if (File.Exists(jsonFile)) { File.Delete(jsonFile); }
                if (File.Exists(xmlFile)) { File.Delete(xmlFile); }
                if (File.Exists(ymlFile)) { File.Delete(ymlFile); }
                switch (DataType)
                {
                    case Item.JSON:
                        File.Create(jsonFile).Close();
                        break;
                    case Item.XML:
                        File.Create(xmlFile).Close();
                        break;
                    case Item.YML:
                        File.Create(ymlFile).Close();
                        break;
                }
            }

            if (DefaultSetting)
            {
                Item.Config = new EnumRunConfig(true);
            }
            else
            {
                if (FilesPath != null) { Item.Config.FilesPath = this.FilesPath; }
                if (LogsPath != null) { Item.Config.LogsPath = this.LogsPath; }
                if (OutputPath != null) { Item.Config.OutputPath = this.OutputPath; }
                if (DebugMode != null) { Item.Config.DebugMode = (bool)this.DebugMode; }
                if (RunOnce != null) { Item.Config.RunOnce = (bool)this.RunOnce; }
                if (Ranges != null) { Item.Config.Ranges = new List<Range>(Ranges); }
                if (Languages != null) { Item.Config.Languages = new List<Language>(Languages); }
            }
            Item.Config.Save();
            WriteObject(Item.Config);
        }
    }
}
