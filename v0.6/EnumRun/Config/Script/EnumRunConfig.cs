using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace EnumRun
{
    public class EnumRunConfig
    {
        public string FilesPath { get; set; }
        public string LogsPath { get; set; }
        public string OutputPath { get; set; }
        public bool DebugMode { get; set; }
        //public SerializableDictionary<string, Range> Ranges { get; set; }
        //public SerializableDictionary<string, Language> Languages { get; set; }
        public List<Range> Ranges { get; set; }
        public List<Language> Languages { get; set; }

        public EnumRunConfig() { }
        public EnumRunConfig(bool isDefault)
        {
            this.FilesPath = Path.Combine(Item.WORK_DIR, "Files");
            this.LogsPath = Path.Combine(Item.WORK_DIR, "Logs");
            this.OutputPath = Path.Combine(Item.WORK_DIR, "Output");
            this.DebugMode = false;
            this.Ranges = DefaultRangeSettings.Create();
            this.Languages = DefaultLanguageSetting.Create();
        }

        /// <summary>
        /// 設定ファイルからEnumRunConfigパラメータをロード
        /// </summary>
        /// <returns>読み込んEnumRunConfigインスタンス</returns>
        public static EnumRunConfig Load()
        {
            string fileName = new string[]
            {
                Path.Combine(Item.CONF_DIR, "Config.json"),
                Path.Combine(Item.CONF_DIR, "Config.xml"),
                Path.Combine(Item.CONF_DIR, "Config.yml")
            }.FirstOrDefault(x => File.Exists(x));
            return string.IsNullOrEmpty(fileName) || !File.Exists(fileName) ?
                new EnumRunConfig(true) :
                DataSerializer.Deserialize<EnumRunConfig>(fileName);
        }

        /// <summary>
        /// 設定を保存
        /// </summary>
        public void Save()
        {
            string fileName = new string[]
            {
                Path.Combine(Item.CONF_DIR, "Config.json"),
                Path.Combine(Item.CONF_DIR, "Config.xml"),
                Path.Combine(Item.CONF_DIR, "Config.yml")
            }.FirstOrDefault(x => File.Exists(x));

            if (fileName == null)
            {
                //  デフォルトでは C:\ProgramData\EnumRun\Conf\Config.json
                fileName = Path.Combine(Item.CONF_DIR, "Config.json");
            }
            if (!Directory.Exists(Path.GetDirectoryName(fileName)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(fileName));
            }
            DataSerializer.Serialize<EnumRunConfig>(this, fileName);
        }

        public Language GetLanguage(string name)
        {
            return Languages.FirstOrDefault(x => x.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }
        public Range GetRange(string name)
        {
            return Ranges.FirstOrDefault(x => x.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }
    }
}
