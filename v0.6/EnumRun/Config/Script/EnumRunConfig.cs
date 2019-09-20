using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace EnumRun
{
    public class EnumRunConfig
    {
        public string FilesPath { get; set; }
        public string LogsPath { get; set; }
        public string OutputPath { get; set; }
        public bool DebugMode { get; set; }
        public bool RunOnce { get; set; }
        public List<Range> Ranges { get; set; }
        public List<Language> Languages { get; set; }

        public EnumRunConfig() : this(null, false) { }
        public EnumRunConfig(bool loadDefault) : this(null, loadDefault) { }
        public EnumRunConfig(string confDir, bool loadDefault)
        {
            if (confDir == null)
            {
                confDir = Path.Combine(Environment.ExpandEnvironmentVariables("%PROGRAMDATA%"), Item.APPLICATION_NAME);
            }
            if (loadDefault)
            {
                this.FilesPath = Path.Combine(confDir, "Files");
                this.LogsPath = Path.Combine(confDir, "Logs");
                this.OutputPath = Path.Combine(confDir, "Output");
                this.DebugMode = false;
                this.RunOnce = false;
                this.Ranges = DefaultRangeSettings.Create();
                this.Languages = DefaultLanguageSetting.Create();
            }
        }

        /// <summary>
        /// 設定ファイルからEnumRunConfigパラメータをロード
        /// </summary>
        /// <returns>読み込んEnumRunConfigインスタンス</returns>
        public static EnumRunConfig Load()
        {
            return Load(null);
        }
        public static EnumRunConfig Load(string confDir)
        {
            if (confDir == null)
            {
                confDir = Path.Combine(Environment.ExpandEnvironmentVariables("%PROGRAMDATA%"), Item.APPLICATION_NAME);
            }
            string fileName = new string[]
            {
                Path.Combine(confDir, Item.CONFIG_JSON),
                Path.Combine(confDir, Item.CONFIG_XML),
                Path.Combine(confDir, Item.CONFIG_YML)
            }.FirstOrDefault(x => File.Exists(x));
            return string.IsNullOrEmpty(fileName) || !File.Exists(fileName) ?
                new EnumRunConfig(true) :
                DataSerializer.Deserialize<EnumRunConfig>(fileName);
        }

        /// <summary>
        /// 設定を保存
        /// </summary>
        public void Save() { Save(null); }
        public void Save(string confDir)
        {
            if (confDir == null)
            {
                confDir = Path.Combine(Environment.ExpandEnvironmentVariables("%PROGRAMDATA%"), Item.APPLICATION_NAME);
            }
            string fileName = new string[]
            {
                Path.Combine(confDir, Item.CONFIG_JSON),
                Path.Combine(confDir, Item.CONFIG_XML),
                Path.Combine(confDir, Item.CONFIG_YML)
            }.FirstOrDefault(x => File.Exists(x));

            if (fileName == null)
            {
                //  デフォルトでは C:\ProgramData\EnumRun\Config.json
                fileName = Path.Combine(confDir, "Config.json");
            }
            if (!Directory.Exists(confDir))
            {
                Directory.CreateDirectory(confDir);
            }
            DataSerializer.Serialize<EnumRunConfig>(this, fileName);
        }


        /// <summary>
        /// 一致する名前のLanguage配列を取得
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Language[] GetLanguage(string name)
        {
            string patternString = Regex.Replace(name, ".",
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

            return Languages.Where(x => regPattern.IsMatch(x.Name)).ToArray();
        }

        /// <summary>
        /// 一致する名前のRange配列を取得
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Range[] GetRange(string name)
        {
            string patternString = Regex.Replace(name, ".",
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

            return Ranges.Where(x => regPattern.IsMatch(x.Name)).ToArray();
        }
    }
}
