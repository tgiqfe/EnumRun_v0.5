using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnumRun
{
    class Item
    {
        //  静的パラメータ
        public const string ApplicationName = "EnumRun";

        public const string WINDOWS = "Windows";
        public const string MAC = "Mac";
        public const string LINUX = "Linux";


        //  プロセス引数から取得するパラメータ
        public static string LanguageFile = "Language.xml";

        //  設定ファイルから読み込んだパラメータ
        public static SerializableDictionary<string, Language> Languages = null;

        //  実行環境
        public static string OS = null;
        public static string MachineDomainName = null;


    }
}
