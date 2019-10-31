using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

/*
 * ログ出力用クラス
 *		LastUpdate 2017/10/14
 *		Version 0.01.017
 */
namespace EnumRun
{
    class LogWrite
    {
        //	クラスパラメータ
        private ReaderWriterLock rwLock = null;
        private bool writeOK = false;
        private string compName = Environment.MachineName;
        private string userName = Environment.UserName;
        private string _LogPath;
        private string _LogDir;
        public Encoding Encode { get; set; }
        public bool StandardOut { get; set; }
        public string LogPath { get { return this._LogPath; } }
        public string LogDir { get { return this._LogDir; } }

        //  Progressログ用パラメータ
        public char ProgressChar { get; set; }
        public char PaddingChar { get; set; }
        public char BorderStartChar { get; set; }
        public char BorderEndChar { get; set; }
        public int MaxLength { get; set; }
        private int cursor = 0;

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
        public LogWrite(string logPath)
        {
            SetLogFile(logPath);
        }
        public LogWrite(string logDir, string logBaseName) : this(logDir + Path.DirectorySeparatorChar + logBaseName) { }

        //  ログファイルをセット
        public void SetLogFile(string logPath)
        {
            //  通常ログ記述用設定
            this._LogPath = Path.GetFullPath(logPath);
            this._LogDir = Path.GetDirectoryName(this._LogPath);
            try
            {
                if (!Directory.Exists(this._LogDir))
                {
                    Directory.CreateDirectory(this._LogDir);
                }

                //  書き込み可否確認
                File.Create(this._LogDir + Path.DirectorySeparatorChar + "LogWritableCheck.log").Close();
                File.Delete(this._LogDir + Path.DirectorySeparatorChar + "LogWritableCheck.log");
                writeOK = true;
            }
            catch { }

            this.Encode = Encoding.GetEncoding("Shift_JIS");
            this.StandardOut = true;
            rwLock = new ReaderWriterLock();

            //  Progressログ記述用設定
            this.ProgressChar = '#';
            this.PaddingChar = ' ';
            this.BorderStartChar = '[';
            this.BorderEndChar = ']';
            this.MaxLength = 100;
        }

        //	ログ記述
        public void WriteLine(string message)
        {
            WriteLine(message, this._LogType);
        }
        public void WriteLine(string message, int logType)
        {
            if (!writeOK) { return; }
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
                using (StreamWriter sw = new StreamWriter(this._LogPath, true, this.Encode))
                {
                    sw.Write(messageStr);
                }
                if (this.StandardOut)
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

        //  ファクトリ
        public static LogWrite Create(string namePattern)
        {
            return new LogWrite(CreateLogFile(namePattern));
        }

        //  Progressログ
        public void ProgressLog(double value)
        {
            //  ログ出力部分
            Action<string> WriteProgress = (messageStr) =>
            {
                if (!writeOK) { return; }
                try
                {
                    rwLock.AcquireWriterLock(10000);
                    using (StreamWriter sw = new StreamWriter(this._LogPath, true, this.Encode))
                    {
                        sw.Write(messageStr);
                    }
                    if (this.StandardOut)
                    {
                        Console.Write(messageStr);
                    }
                }
                catch { }
                finally
                {
                    rwLock.ReleaseWriterLock();
                }
            };

            //  Progressログ開始
            int percent = value * 100 >= MaxLength ? MaxLength : (int)Math.Floor(value * 100);
            if (percent > 0)
            {
                if (cursor <= 0 || cursor >= MaxLength)
                {
                    WriteProgress(BorderStartChar.ToString());
                    cursor = 0;
                }
                if (percent == cursor) { return; }
                if (percent < cursor)
                {
                    WriteProgress(
                        new string(PaddingChar, MaxLength - cursor) +
                        BorderEndChar.ToString() + "\r\n" +
                        BorderStartChar.ToString());
                    cursor = 0;
                }
                WriteProgress(new string(ProgressChar, percent - cursor));
                cursor = percent;
            }
            if (cursor >= MaxLength)
            {
                WriteProgress(BorderEndChar.ToString() + "\r\n");
                cursor = 0;
            }
        }
    }
}