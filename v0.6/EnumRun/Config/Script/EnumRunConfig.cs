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
        public EnumRunConfig(string confFile, bool loadDefault)
        {
            if (confFile == null)
            {
                confFile = Path.Combine(
                    Environment.ExpandEnvironmentVariables("%PROGRAMDATA%"),
                    Item.APPLICATION_NAME,
                    Item.CONFIG_JSON);
            }
            string confDir = Path.GetDirectoryName(confFile);
            if (loadDefault || !File.Exists(confFile))
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
        public static EnumRunConfig Load() { return Load(null); }
        public static EnumRunConfig Load(string confFile)
        {
            if (confFile == null)
            {
                confFile = Path.Combine(
                    Environment.ExpandEnvironmentVariables("%PROGRAMDATA%"),
                    Item.APPLICATION_NAME,
                    Item.CONFIG_JSON);
            }
            /*
            string fileName = new string[]
            {
                Path.Combine(confDir, Item.CONFIG_JSON),
                Path.Combine(confDir, Item.CONFIG_XML),
                Path.Combine(confDir, Item.CONFIG_YML)
            }.FirstOrDefault(x => File.Exists(x));
            */

            return !File.Exists(confFile) ?
                new EnumRunConfig(true) :
                DataSerializer.Deserialize<EnumRunConfig>(confFile);
        }

        /// <summary>
        /// 設定を保存
        /// </summary>
        public void Save() { Save(null); }
        public void Save(string confFile)
        {
            if (confFile == null)
            {
                confFile = Path.Combine(
                    Environment.ExpandEnvironmentVariables("%PROGRAMDATA%"),
                    Item.APPLICATION_NAME,
                    Item.CONFIG_JSON);
            }
            /*
            string fileName = new string[]
            {
                Path.Combine(confDir, Item.CONFIG_JSON),
                Path.Combine(confDir, Item.CONFIG_XML),
                Path.Combine(confDir, Item.CONFIG_YML)
            }.FirstOrDefault(x => File.Exists(x));
            */
            if (!Directory.Exists(Path.GetDirectoryName(confFile)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(confFile));
            }
            DataSerializer.Serialize<EnumRunConfig>(this, confFile);
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
