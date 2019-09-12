using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.IO;

namespace EnumRun
{
    class Functions
    {
        /// <summary>
        /// デフォルトパラメータでのEnumRunConfを取得
        /// </summary>
        /// <returns>デフォルトパラメータのEnumRunConfインスタンス</returns>
        public static EnumRunConfig GetDefault()
        {
            return new EnumRunConfig()
            {
                Name = "EnumRun",
                FilesPath = "Files",
                LogsPath = "Logs",
                Ranges = DefaultRangeSettings.Create(),
                Languages = DefaultLanguageSetting.Create()
            };
        }

        /// <summary>
        /// 設定ファイルをEnumRunConfigパラメータを読み込み
        /// </summary>
        /// <returns>読み込んEnumRunConfigインスタンス</returns>
        public static EnumRunConfig Read()
        {
            string confPath = new string[]
            {
                Path.Combine(Item.CONF_DIR, "Config.json"),
                Path.Combine(Item.CONF_DIR, "Config.xml"),
                Path.Combine(Item.CONF_DIR, "Config.yml")
            }.FirstOrDefault(x => File.Exists(x));
            if (confPath == null)
            {
                return GetDefault();
            }
            else
            {
                return DataSerializer.Deserialize<EnumRunConfig>(confPath);
            }
        }

        public static string[] SplitComma(string sourceText)
        {
            return Regex.Split(sourceText, @",\s*");
        }
        public static string[] SplitComma(string[] sourceTexts)
        {
            List<string> textList = new List<string>();
            foreach(string text in sourceTexts)
            {
                textList.AddRange(SplitComma(text));
            }
            return textList.ToArray();
        }

        
    }
}
