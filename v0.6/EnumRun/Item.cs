using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;

namespace EnumRun
{
    class Item
    {
        //  静的パラメータ
        public const string APPLICATION_NAME = "EnumRun";
        public readonly static string WORK_DIR = Path.Combine(
            Environment.ExpandEnvironmentVariables("%PROGRAMDATA%"), APPLICATION_NAME);
        public readonly static string TEMP_DIR = Path.Combine(
            Environment.ExpandEnvironmentVariables("%TEMP%"), APPLICATION_NAME);

        //  ファイル名関連
        public const string SESSION_FILE = "session.json";
        public const string CONFIG_JSON = "Config.json";
        public const string CONFIG_XML = "Config.xml";
        public const string CONFIG_YML = "Config.yml";


        //  設定ファイルから読み込んだパラメータ
        public static EnumRunConfig Config = null;

        //  データタイプ
        public const string JSON = "Json";
        public const string XML = "Xml";
        public const string YML = "Yml";
    }
}
