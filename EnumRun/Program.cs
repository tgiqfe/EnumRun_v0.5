using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;

//  スクリプト順次実行
//      LastUpdate 2017/05/29
//      Version 0.05.002
namespace EnumRun
{
    class Program
    {
        //  フィールドパラメータ
        const string confFile = "Conf.xml";

        //	終了コード
        const int code_Success = 0;         //	正常終了
        const int code_Fail = 1;            //	以下のいずれにも該当しない異常終了
        const int code_NoArg = 2;           //	引数が指定されていない為、異常終了
        const int code_InvalidArg = 3;      //	引数の形式が正しくない為、異常終了
        const int code_ConnectFail = 4;     //	サーバとの接続失敗の為、異常終了
        const int code_TimeOut = 5;         //	処理タイムアウトの為、異常終了
        const int code_CoudNottGetInfo = 6; //	想定している情報が得られなかった為、異常終了
        const int code_FileNotFound = 7;    //	期待したファイル/フォルダーが見つからなかった為、異常終了
        const int code_IOError = 8;         //	ファイル出力やフォルダーの作成に失敗した為、異常終了
        const int code_Multiple = 9;        //	多重起動しようとした為、異常終了

        static void Main(string[] args)
        {
            //  引数確認
            ArgsParam.CheckArgs(args);

            //  実行ファイルの名前を取得
            string execName = Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().Location);

            using (Mutex mutex = new Mutex(false, execName))
            {
                //  同名の実行ファイルは多重起動禁止
                if (!mutex.WaitOne(0, false)) { return; }

                //  カレントディレクトリ
                Environment.CurrentDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

                //  設定ファイル読み込み
                DataLibrary dl = DataLibrary.Create(confFile);

                //  実行ファイル名がRangeListに含まれていなければ終了
                Range matchRange = dl.RangeList.FirstOrDefault(x => x.Name.Equals(execName, StringComparison.OrdinalIgnoreCase));
                if (matchRange == null)
                {
                    Environment.Exit(code_CoudNottGetInfo);
                }

                //  FileDirの場所がなければ終了
                if (!Directory.Exists(dl.FileDir))
                {
                    Environment.Exit(code_FileNotFound);
                }

                //  ログファイル出力開始
                LogWrite lg = new LogWrite(LogWrite.CreateLogFile(dl.LogDir + "\\" + execName + "_<yyyyMMdd>.log"));

                //  FileDir内の対象スクリプトをリスト化
                List<ScriptOption> scriptList = new List<ScriptOption>();
                Regex reg_path = new Regex(@"\d+(?=[^\\]+$)");
                Match tempMatch = null;
                foreach (string tempPath in Directory.GetFiles(dl.FileDir).Where(x => (tempMatch = reg_path.Match(x)).Success))
                {
                    int tempNum = Convert.ToInt32(tempMatch.Value);
                    if (tempNum >= matchRange.StartNum && tempNum <= matchRange.EndNum)
                    {
                        Extension matchExtension = dl.ExtensionList.
                            FirstOrDefault(x => x.Name.Equals(Path.GetExtension(tempPath), StringComparison.OrdinalIgnoreCase));
                        if (matchExtension != null)
                        {
                            scriptList.Add(new ScriptOption(Path.GetFullPath(tempPath), matchExtension, dl.FileDir));
                        }
                    }
                }

                //  スクリプトの実行
                foreach(ScriptOption so in scriptList)
                {
                    lg.WriteLine(so.LogMessage);
                    so.Run();
                }
            }
        }
    }
}
