using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.IO;
using System.Threading;
using System.Diagnostics;

namespace EnumRun
{
    class ScriptOption
    {
        //  フィールドパラメータ
        private string fileName = "";
        private string arguments = "";
        private string workingDir = "";
        public string LogMessage { get; set; }

        //  実行オプション用パラメータ
        private bool dontRun = false;
        private bool waitForExit = false;
        private bool runAdmin = false;
        private int beforeWait = 0;
        private int afterWait = 0;
        private bool runDomain = true;
        private bool runLocal = true;

        //  コンストラクタ
        public ScriptOption() { }
        public ScriptOption(string scriptName, Extension extension, string workingDir)
        {
            SetProgramArgs(scriptName, extension);
            this.LogMessage = Path.GetFileName(scriptName);
            this.workingDir = workingDir;

            //  実行時オプションを登録
            Match tempOptMatch;
            if ((tempOptMatch = Regex.Match(scriptName, @"(\[[0-9a-zA-Z]+\])+(?=\..+$)")).Success)
            {
                SetOption(tempOptMatch.Value.ToLower());
            }
        }

        //  実行時の引数決定
        private void SetProgramArgs(string fileName, Extension extension)
        {
            if (extension.Program == "")
            {
                this.fileName = fileName;
                this.arguments = "";
            }
            else
            {
                this.fileName = extension.Program;
                if (extension.Arg_Before != "")
                {
                    this.arguments += extension.Arg_Before + " ";
                }
                this.arguments += "\"" + fileName + "\"";
                if (extension.Arg_After != "")
                {
                    this.arguments += " " + extension.Arg_After;
                }
            }
        }

        //  実行時オプションを登録
        private void SetOption(string tempOpt)
        {
            List<string> logOptList = new List<string>();

            //  実行対象外かどうかを確認
            if (tempOpt.Contains("n"))
            {
                this.dontRun = true;
                logOptList.Add("n:実行対象外");
                this.LogMessage += " <" + string.Join(",", logOptList) + ">";
                return;
            }

            //  終了待ち確認
            if (tempOpt.Contains("w"))
            {
                this.waitForExit = true;
                logOptList.Add("w:終了待ち");
            }

            //  管理者として実行
            if (tempOpt.Contains("a"))
            {
                this.runAdmin = true;
                logOptList.Add("a:管理者実行");
            }

            //  実行前待機時間
            Match match;
            if ((match = Regex.Match(tempOpt, @"\d{1,3}r")).Success)
            {
                this.beforeWait = Convert.ToInt32(match.Value.TrimEnd('r'));
                logOptList.Add($"{this.beforeWait}r:実行前待機{this.beforeWait}秒");
            }

            //  実行後待機時間
            if ((match = Regex.Match(tempOpt, @"r\d{1,3}")).Success)
            {
                this.afterWait = Convert.ToInt32(match.Value.TrimStart('r'));
                logOptList.Add($"r{this.afterWait}:実行後待機{this.afterWait}秒");
                this.waitForExit = true;
            }

            //  ドメインユーザーのみ実行
            if (!tempOpt.Contains("l") && tempOpt.Contains("d"))
            {
                this.runLocal = false;
                logOptList.Add("d:ドメインユーザーのみ実行");
            }

            //  ローカルユーザーのみ実行
            if (tempOpt.Contains("l") && !tempOpt.Contains("d"))
            {
                this.runDomain = false;
                logOptList.Add("l:ローカルユーザーのみ実行");
            }

            this.LogMessage += " <" + string.Join(",", logOptList) + ">";
        }

        public void Run()
        {
            //  実行対象外の場合は終了 ※[n]
            if (this.dontRun) { return; }

            //  ローカル/ドメインユーザー実行可否確認 ※[l] or [[d]
            if ((!this.runDomain && (!Environment.MachineName.Equals(Environment.UserDomainName, StringComparison.OrdinalIgnoreCase))) ||
                (!this.runLocal && (Environment.MachineName.Equals(Environment.UserDomainName, StringComparison.OrdinalIgnoreCase))))
            {
                return;
            }

            //  実行前待機 ※[数字r]
            if (this.beforeWait > 0)
            {
                Thread.Sleep(this.beforeWait * 1000);
            }

            //  新プロセスでスクリプトを実行
            using (Process proc = new Process())
            {
                proc.StartInfo.FileName = this.fileName;
                proc.StartInfo.Arguments = this.arguments;
                proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                proc.StartInfo.WorkingDirectory = this.workingDir;
                if (this.runAdmin)
                {
                    proc.StartInfo.Verb = "RunAs";
                }
                proc.Start();
                if (this.waitForExit)
                {
                    proc.WaitForExit();
                }
            }

            //  実行後待機 ※[r数字]
            if (this.afterWait > 0)
            {
                Thread.Sleep(this.afterWait * 1000);
            }
        }
    }
}

