using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;
using System.Text.RegularExpressions;

/*
 * ログ出力用クラス
 *		LastUpdate 2017/02/03
 *		Version 0.01.006
 */
namespace EnumRun
{
    class LogWrite
    {
        //	フィールドパラメータ
        private ReaderWriterLock rwLock = null;
        private string compName = Environment.MachineName;
        private string userName = Environment.UserName;
        private Encoding _Encode = Encoding.GetEncoding("Shift_JIS");
        private bool _StandardOut = true;
        private string _LogPath;
        private string _LogDir;
        private string _LogBaseName;
        public Encoding Encode { set { this._Encode = value; } get { return this._Encode; } }
        public bool StandardOut { set { this._StandardOut = value; } get { return this._StandardOut; } }
        public string LogPath { get { return this._LogPath; } }
        public string LogDir { get { return this._LogDir; } }
        public string LogBaseName { get { return this._LogBaseName; } }
        private bool writeOK = false;

        //	ログ記述タイプ
        public const int LOG_STANDARD = 0;          //	[yyyy/MM/dd HH:mm:ss] message return
        public const int LOG_NORETURN = 1;          //	[yyyy/MM/dd HH:mm:ss] message
        public const int LOG_RAW = 2;               //	message
        public const int LOG_RAWRETURN = 3;         //	messagee return
        public const int LOG_STANDARD_COMP = 4;     //	[yyyy/MM/dd HH:mm:ss][computername] message return
        public const int LOG_STANDARD_USER = 5;     //	[yyyy/MM/dd HH:mm:ss][username] message return
        public const int LOG_STANDARD_COMPUSER = 6; //	[yyyy/MM/dd HH:mm:ss][computername][username] message return
        private int _LogType = LOG_STANDARD;
        public int LogType { set { this._LogType = value; } get { return this._LogType; } }

        //	コンストラクタ
        public LogWrite() { }
        public LogWrite(string logPath) :
            this(Path.GetDirectoryName(Path.GetFullPath(logPath)), Path.GetFileName(logPath))
        { }
        public LogWrite(string logDir, string logBaseName)
        {
            this._LogPath = Path.GetFullPath(logDir + Path.DirectorySeparatorChar + logBaseName);
            this._LogDir = Path.GetFullPath(logDir);
            this._LogBaseName = logBaseName;
            try
            {
                if (!Directory.Exists(this._LogDir))
                {
                    Directory.CreateDirectory(this._LogDir);
                }
                writeOK = true;
            }
            catch { }
            rwLock = new ReaderWriterLock();
        }

        //	ログ記述
        public void WriteLine(string message)
        {
            WriteLine(message, this._LogType);
        }
        public void WriteLine(string message, int logType)
        {
            string messageStr = "";
            switch (logType)
            {
                case LOG_STANDARD:
                    messageStr = $"[{DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")}] {message}\r\n";
                    break;
                case LOG_NORETURN:
                    messageStr = $"[{DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")}] {message}";
                    break;
                case LOG_RAW:
                    messageStr = message;
                    break;
                case LOG_RAWRETURN:
                    messageStr = message + "\r\n";
                    break;
                case LOG_STANDARD_COMP:
                    messageStr = $"[{DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")}][{compName}] {message}\r\n";
                    break;
                case LOG_STANDARD_USER:
                    messageStr = $"[{DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")}][{userName}] {message}\r\n";
                    break;
                case LOG_STANDARD_COMPUSER:
                    messageStr = $"[{DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")}][{compName}][{userName}] {message}\r\n";
                    break;
            }
            try
            {
                rwLock.AcquireWriterLock(10000);
                if (writeOK)
                {
                    using (StreamWriter sw = new StreamWriter(this._LogPath, true, this._Encode))
                    {
                        sw.Write(messageStr);
                    }
                }
                if (this._StandardOut)
                {
                    Console.WriteLine(" # " + message);
                }
            }
            catch { }
            finally
            {
                rwLock.ReleaseWriterLock();
            }
        }

        //	名前パターンからログファイル名を返す
        /*  [名前パターンのルール]
		 *      <yyyy>      ⇒ 西暦4桁
		 *      <yy>        ⇒ 西暦の下一桁
		 *      <MM>        ⇒ 月(1桁の場合は0埋め。以降も同じ)
		 *      <dd>        ⇒ 日
		 *      <HH>        ⇒ 時間(24時間制)
		 *      <mm>        ⇒ 分
		 *      <ss>        ⇒ 秒
		 *      <yyyyMM>    ⇒ 年月
		 *      <yyyyMMdd>  ⇒ 年月日
		 *      <HHmm>      ⇒ 時分
		 *      <HHmmss>    ⇒ 自分秒
		 *      <user>      ⇒ 実行中のユーザー名
		 *      <USER>      ⇒ 〃
		 *      <User>      ⇒ 〃
		 *      <computer>  ⇒ 実行しているコンピューター名
		 *      <COMPUTER>  ⇒ 〃
		 *      <Computer>  ⇒ 〃
		 */
        public static string CreateLogFile(string namePattern)
        {
            string resultStr = namePattern;
            foreach (Match match in Regex.Matches(namePattern, "<[^<>]+>"))
            {
                foreach (Group group in match.Groups)
                {
                    switch (group.ToString())
                    {
                        case "<yyyy>":
                            resultStr = resultStr.Replace("<yyyy>", DateTime.Now.ToString("yyyy"));
                            break;
                        case "<yy>":
                            resultStr = resultStr.Replace("<yy>", DateTime.Now.ToString("yy"));
                            break;
                        case "<MM>":
                            resultStr = resultStr.Replace("<MM>", DateTime.Now.ToString("MM"));
                            break;
                        case "<dd>":
                            resultStr = resultStr.Replace("<dd>", DateTime.Now.ToString("dd"));
                            break;
                        case "<HH>":
                            resultStr = resultStr.Replace("<HH>", DateTime.Now.ToString("HH"));
                            break;
                        case "<mm>":
                            resultStr = resultStr.Replace("<mm>", DateTime.Now.ToString("mm"));
                            break;
                        case "<ss>":
                            resultStr = resultStr.Replace("<ss>", DateTime.Now.ToString("ss"));
                            break;
                        case "<yyyyMM>":
                            resultStr = resultStr.Replace("<yyyyMM>", DateTime.Now.ToString("yyyyMM"));
                            break;
                        case "<yyyyMMdd>":
                            resultStr = resultStr.Replace("<yyyyMMdd>", DateTime.Now.ToString("yyyyMMdd"));
                            break;
                        case "<HHmm>":
                            resultStr = resultStr.Replace("<HHmm>", DateTime.Now.ToString("HHmm"));
                            break;
                        case "<HHmmss>":
                            resultStr = resultStr.Replace("<HHmmss>", DateTime.Now.ToString("HHmmss"));
                            break;
                        case "<user>":
                            resultStr = resultStr.Replace("<user>", Environment.UserName);
                            break;
                        case "<USER>":
                            resultStr = resultStr.Replace("<USER>", Environment.UserName);
                            break;
                        case "<User>":
                            resultStr = resultStr.Replace("<User>", Environment.UserName);
                            break;
                        case "<computer>":
                            resultStr = resultStr.Replace("<computer>", Environment.MachineName);
                            break;
                        case "<COMPUTER>":
                            resultStr = resultStr.Replace("<COMPUTER>", Environment.MachineName);
                            break;
                        case "<Computer>":
                            resultStr = resultStr.Replace("<Computer>", Environment.MachineName);
                            break;
                    }
                }
            }
            return resultStr;
        }
    }
}



/*

マルチプロセス用に、ログキャッシュ
フォームアプリケーション用に、標準出力以外にもログ一時表示を

*/
