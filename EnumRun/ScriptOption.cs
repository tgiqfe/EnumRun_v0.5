using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.IO;
using System.Threading;
using System.Diagnostics;
using System.Net.NetworkInformation;

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
        private bool DontRun { get; set; }
        private bool WaitForExit { get; set; }
        private bool RunAdmin { get; set; }
        private int BeforeWait { get; set; } = 0;
        private int AfterWait { get; set; } = 0;
        private bool RunDomain { get; set; } = true;
        private bool RunLocal { get; set; } = true;
        private bool CheckNetwork { get; set; }

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
                this.DontRun = true;
                logOptList.Add("n:実行対象外");
                this.LogMessage += " <" + string.Join(",", logOptList) + ">";
                return;
            }

            //  終了待ち確認
            if (tempOpt.Contains("w"))
            {
                this.WaitForExit = true;
                logOptList.Add("w:終了待ち");
            }

            //  管理者として実行
            if (tempOpt.Contains("a"))
            {
                this.RunAdmin = true;
                logOptList.Add("a:管理者実行");
            }

            //  実行前待機時間
            Match match;
            if ((match = Regex.Match(tempOpt, @"\d{1,3}r")).Success)
            {
                this.BeforeWait = Convert.ToInt32(match.Value.TrimEnd('r'));
                logOptList.Add($"{this.BeforeWait}r:実行前待機{this.BeforeWait}秒");
            }

            //  実行後待機時間
            if ((match = Regex.Match(tempOpt, @"r\d{1,3}")).Success)
            {
                this.AfterWait = Convert.ToInt32(match.Value.TrimStart('r'));
                logOptList.Add($"r{this.AfterWait}:実行後待機{this.AfterWait}秒");
                this.WaitForExit = true;
            }

            //  ドメインユーザーのみ実行
            if (!tempOpt.Contains("l") && tempOpt.Contains("d"))
            {
                this.RunLocal = false;
                logOptList.Add("d:ドメインユーザーのみ実行");
            }

            //  ローカルユーザーのみ実行
            if (tempOpt.Contains("l") && !tempOpt.Contains("d"))
            {
                this.RunDomain = false;
                logOptList.Add("l:ローカルユーザーのみ実行");
            }

            //  ネットワーク接続可否確認
            if (tempOpt.Contains("p"))
            {
                this.CheckNetwork = true;
                logOptList.Add("p:ネットワーク接続可否確認");
            }

            this.LogMessage += " <" + string.Join(",", logOptList) + ">";
        }

        public void Run()
        {
            //  実行対象外の場合は終了 ※[n]
            if (this.DontRun) { return; }

            //  ローカル/ドメインユーザー実行可否確認 ※[l] or [[d]
            if ((!this.RunDomain && (!Environment.MachineName.Equals(Environment.UserDomainName, StringComparison.OrdinalIgnoreCase))) ||
                (!this.RunLocal && (Environment.MachineName.Equals(Environment.UserDomainName, StringComparison.OrdinalIgnoreCase))))
            {
                return;
            }

            //  ネットワーク接続可否確認
            Func<bool> checkGW = () =>
            {
                Ping ping = new Ping();
                foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
                {
                    foreach (GatewayIPAddressInformation gw in nic.GetIPProperties().GatewayAddresses)
                    {
                        for (int i = 0; i < 4; i++)
                        {
                            PingReply reply = ping.Send(gw.Address);
                            if (reply.Status == IPStatus.Success)
                            {
                                return true;
                            }
                            Thread.Sleep(100);
                        }
                    }
                }
                return false;
            };
            if (CheckNetwork)
            {
                if (!checkGW()) { return; }
            }

            //  実行前待機 ※[数字r]
            if (this.BeforeWait > 0)
            {
                Thread.Sleep(this.BeforeWait * 1000);
            }

            //  新プロセスでスクリプトを実行
            using (Process proc = new Process())
            {
                proc.StartInfo.FileName = this.fileName;
                proc.StartInfo.Arguments = this.arguments;
                proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                proc.StartInfo.WorkingDirectory = this.workingDir;
                if (this.RunAdmin)
                {
                    proc.StartInfo.Verb = "RunAs";
                }
                proc.Start();
                if (this.WaitForExit)
                {
                    proc.WaitForExit();
                }
            }

            //  実行後待機 ※[r数字]
            if (this.AfterWait > 0)
            {
                Thread.Sleep(this.AfterWait * 1000);
            }
        }
    }
}
