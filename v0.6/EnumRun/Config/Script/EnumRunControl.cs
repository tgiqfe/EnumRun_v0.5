using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace EnumRun
{
    class EnumRunControl
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
                FilesPath = Path.Combine(Item.WORK_DIR, "Files"),
                LogsPath = Path.Combine(Item.WORK_DIR, "Logs"),
                OutputPath = Path.Combine(Item.WORK_DIR, "Output"),
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

        /// <summary>
        /// 列挙実行開始
        /// </summary>
        /// <param name="processName">プロセス名</param>
        public static void StartEnumRun(string processName)
        {
            if (Item.Config.Ranges.ContainsKey(processName))
            {
                int startNum = Item.Config.Ranges[processName].StartNumber;
                int endNum = Item.Config.Ranges[processName].EndNumber;

                if (Directory.Exists(Item.Config.FilesPath))
                {
                    //  スクリプトファイルの列挙
                    List<Script> scriptList = new List<Script>();
                    foreach (string scriptFile in Directory.GetFiles(Item.Config.FilesPath))
                    {
                        Script script = new Script(scriptFile, startNum, endNum);
                        if (script.Enabled)
                        {
                            scriptList.Add(script);
                            //DataSerializer.Serialize<Script>(script, Console.Out, ".json");
                        }
                    }

                    //  スクリプトファイルの実行
                    foreach (Script script in scriptList)
                    {
                        script.Process();
                    }
                }
            }
        }
    }
}
