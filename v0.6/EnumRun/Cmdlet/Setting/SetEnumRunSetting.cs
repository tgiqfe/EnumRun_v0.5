﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management.Automation;
using System.IO;

namespace EnumRun.Cmdlet
{
    [Cmdlet(VerbsCommon.Set, "EnumRunSetting")]
    public class SetEnumRunSetting : PSCmdlet
    {
        [Parameter(Position = 0), Alias("Path")]
        public string SettingPath { get; set; }
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
            Item.Config = EnumRunSetting.Load(SettingPath);

            DataType = new string[] { Item.JSON, Item.XML, Item.YML }.
                FirstOrDefault(x => x.Equals(DataType, StringComparison.OrdinalIgnoreCase));
        }

        protected override void ProcessRecord()
        {
            //  パラメータ設定
            if (DefaultSetting)
            {
                Item.Config = new EnumRunSetting(true);
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

            //  データタイプ変更
            if (DataType == null)
            {
                Item.Config.Save(SettingPath);
            }
            else
            {
                Dictionary<string, string> extensions = new Dictionary<string, string>()
                {
                    { Item.JSON, ".json" }, { Item.XML, ".xml" },{ Item.YML, ".yml" }
                };

                string saveConfigPath = SettingPath == null ?
                    Path.Combine(
                        Environment.ExpandEnvironmentVariables("%PROGRAMDATA%"),
                        Item.APPLICATION_NAME,
                        "Config" + extensions[DataType]) :
                    Path.Combine(
                        Path.GetDirectoryName(SettingPath),
                        Path.GetFileNameWithoutExtension(SettingPath) + extensions[DataType]);
                Item.Config.Save(saveConfigPath);
            }
            WriteObject(Item.Config);
        }
    }
}
