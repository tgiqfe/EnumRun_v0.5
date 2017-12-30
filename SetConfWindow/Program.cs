using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;
using System.IO;
using System.Threading;

namespace SetConfWindow
{
    static class Program
    {
        //  フィールドパラメータ
        const string confFile = "Conf.xml";

        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main()
        {
            using(Mutex mutex = new Mutex())
            {
                //  多重禁止
                if(!mutex.WaitOne(0, false)) { return; }

                //  カレントディレクトリ
                Environment.CurrentDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

                //  設定ファイル読み込み
                DataLibrary dl = DataLibrary.Create(confFile);

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Form1(dl));
            }
        }
    }
}
