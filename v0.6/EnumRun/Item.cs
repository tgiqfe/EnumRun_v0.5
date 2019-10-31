using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;
using NLog;

namespace EnumRun
{
    class Item
    {
        //  静的パラメータ
        public const string APPLICATION_NAME = "EnumRun";

        //  複数オブジェクトからアクセスする予定のあるパラメータ
        public static EnumRunConfig Config = null;
        public static Logger Logger = null;
        public static DateTime StartTime;

        //  ファイル名関連
        public const string SESSION_FILE = "session.json";
        public const string CONFIG_JSON = "Config.json";
        public const string CONFIG_XML = "Config.xml";
        public const string CONFIG_YML = "Config.yml";

        //  データタイプ
        public const string JSON = "Json";
        public const string XML = "Xml";
        public const string YML = "Yml";
    }
}
