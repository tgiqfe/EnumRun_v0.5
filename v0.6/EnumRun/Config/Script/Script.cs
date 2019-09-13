using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using System.Diagnostics;

namespace EnumRun
{
    public class Script
    {
        private Language _Lang = null;

        public bool Enabled { get; set; }
        public string Name { get; set; }
        public string File { get; set; }
        public string Args { get; set; }
        public string Lang
        {
            get
            {
                if (this._Lang == null && Item.Config != null && !string.IsNullOrEmpty(this.File))
                {
                    string extension = Path.GetExtension(File);
                    this._Lang = Item.Config.Languages.FirstOrDefault(x =>
                        x.Value.Extensions.Any(y =>
                            y.Equals(extension, StringComparison.OrdinalIgnoreCase))).Value;
                }
                return _Lang == null ? "" : _Lang.ToString();
            }
            set
            {
                this._Lang = Item.Config.Languages.FirstOrDefault(x =>
                x.Value.Name.Equals(value, StringComparison.OrdinalIgnoreCase)).Value;
            }
        }
        public EnumRunOption Option { get; set; }
        public int BeforeTime { get; set; }
        public int AfterTime { get; set; }

        public Script() { }
        public Script(string scriptFile, int startNum, int endNum)
        {
            this.Name = Path.GetFileName(scriptFile);
            this.File = scriptFile;

            //  実行可否確認
            this.Enabled = CheckEnable(startNum, endNum);
            if (!Enabled) { return; }

            //  実行時オプション確認
            DetectOption();
        }

        /// <summary>
        /// 実行可否を確認
        /// </summary>
        private bool CheckEnable(int startNum, int endNum)
        {
            Match tempMatch;
            if ((tempMatch = Regex.Match(Name, @"^\d+(?=_)")).Success)
            {
                //  ファイル名の先頭が数字かどうか
                int fileNumber = int.Parse(tempMatch.Value);
                if (fileNumber < startNum || fileNumber > endNum)
                {
                    return false;
                }

                //  拡張子が事前登録している言語のものかどうか
                return !string.IsNullOrEmpty(this.Lang);
            }
            return false;
        }

        /// <summary>
        /// ファイル名末尾から実行時オプションを解析
        /// </summary>
        private void DetectOption()
        {
            Match tempMatch;
            if ((tempMatch = Regex.Match(Name, @"(\[[0-9a-zA-Z]+\])+(?=\..+$)")).Success)
            {
                string matchString = tempMatch.Value;

                //  実行時オプション
                if (matchString.Contains("n"))
                {
                    Option |= EnumRunOption.NoRun;
                    return;
                }
                if (matchString.Contains("w")) { Option |= EnumRunOption.WaitForExit; }
                if (matchString.Contains("a")) { Option |= EnumRunOption.RunAsAdmin; }
                if (matchString.Contains("d")) { Option |= EnumRunOption.DomainUserOnly; }
                if (matchString.Contains("l")) { Option |= EnumRunOption.LocalUserOnly; }
                if (matchString.Contains("s")) { Option |= EnumRunOption.SystemAccountOnly; }
                if (matchString.Contains("p")) { Option |= EnumRunOption.DGReachableOnly; }
                if (matchString.Contains("t")) { Option |= EnumRunOption.TrustedOnly; }
                if (matchString.Contains("k")) { Option |= EnumRunOption.WorkgroupPCOnly; }
                if (matchString.Contains("m")) { Option |= EnumRunOption.DomainPCOnly; }
                if (matchString.Contains("o")) { Option |= EnumRunOption.Output; }

                //  実行前/実行後待機時間
                Match match;
                if ((match = Regex.Match(matchString, @"\d{1,3}(?=r)")).Success)
                {
                    this.BeforeTime = int.Parse(match.Value);
                    Option |= EnumRunOption.BeforeWait;
                }
                if ((match = Regex.Match(matchString, @"(?<=r)\d{1,3}")).Success)
                {
                    this.AfterTime = int.Parse(match.Value);
                    Option |= EnumRunOption.AfterWait;
                    Option |= EnumRunOption.WaitForExit;
                }
            }
        }

        /// <summary>
        /// 対象のオプションが含まれているかどうか
        /// </summary>
        /// <param name="targetOption"></param>
        /// <returns></returns>
        private bool CheckOption(EnumRunOption targetOption)
        {
            return (Option & targetOption) == targetOption;
        }

        /// <summary>
        /// プロセス実行用タスク
        /// </summary>
        public void Process()
        {
            //  実行対象外
            if (CheckOption(EnumRunOption.NoRun)) { return; }

            //  ドメイン参加済みPCのみ ro ワークグループPCのみ
            if(
                (CheckOption(EnumRunOption.DomainPCOnly) && !CheckOption(EnumRunOption.WorkgroupPCOnly) && !Functions.IsDomainMachine()) ||
                (!CheckOption(EnumRunOption.DomainPCOnly) && CheckOption(EnumRunOption.WorkgroupPCOnly) && Functions.IsDomainMachine()))
            {
                return;
            }

            //  システムアカウントのみ
            if (CheckOption(EnumRunOption.SystemAccountOnly) && !Functions.IsSystemAccount())
            {
                return;
            }

            //  ドメインユーザーのみ or ローカルユーザーのみ
            if ((CheckOption(EnumRunOption.DomainUserOnly) && !CheckOption(EnumRunOption.LocalUserOnly) && !Functions.IsDomainUser()) ||
                (!CheckOption(EnumRunOption.DomainUserOnly) && CheckOption(EnumRunOption.LocalUserOnly) && Functions.IsDomainUser()))
            {
                return;
            }

            //  デフォルトゲートウェイとの通信可否を確認
            if (CheckOption(EnumRunOption.DGReachableOnly) && !Functions.IsDefaultGatewayReachable())
            {
                return;
            }

            //  管理者として実行しているかどうかの確認
            if (CheckOption(EnumRunOption.TrustedOnly) && !Functions.IsRunAdministrator())
            {
                return;
            }

            //  実行前待機
            if (BeforeTime > 0) { Thread.Sleep(BeforeTime * 1000); }

            //  プロセス開始
            Task task = CheckOption(EnumRunOption.Output) ?
                ProcessThread("aaaaaaaaaaaaaaaa.txt") :
                ProcessThread();
            if (CheckOption(EnumRunOption.WaitForExit)) { task.Wait(); }

            //  実行後待機
            if (AfterTime > 0) { Thread.Sleep(AfterTime * 1000); }
        }

        /// <summary>
        /// プロセス実行するメソッド
        /// </summary>
        /// <returns></returns>
        private async Task ProcessThread()
        {
            await Task.Run(() =>
            {
                using (Process proc = _Lang.GetProcess(File, Args))
                {
                    proc.StartInfo.Verb = CheckOption(EnumRunOption.RunAsAdmin) ? "RunAs" : "";
                    proc.StartInfo.CreateNoWindow = true;
                    proc.StartInfo.UseShellExecute = false;
                    proc.Start();
                    proc.WaitForExit();
                    //proc.ExitCode;    ←戻り値の扱いを検討中
                }
            });
        }
        private async Task ProcessThread(string outputFile)
        {
            await Task.Run(() =>
            {
                using (Process proc = _Lang.GetProcess(File, Args))
                using (StreamWriter sw = new StreamWriter(outputFile, true, Encoding.GetEncoding("Shift_JIS")))
                {
                    proc.StartInfo.Verb = CheckOption(EnumRunOption.RunAsAdmin) ? "RunAs" : "";
                    proc.StartInfo.CreateNoWindow = true;
                    proc.StartInfo.UseShellExecute = false;
                    proc.StartInfo.RedirectStandardOutput = true;
                    proc.StartInfo.RedirectStandardError = true;
                    proc.StartInfo.RedirectStandardInput = false;
                    proc.OutputDataReceived += (sender, e) => { sw.WriteLine(e.Data); };
                    proc.ErrorDataReceived += (sender, e) => { sw.WriteLine(e.Data); };
                    proc.Start();
                    proc.BeginOutputReadLine();
                    proc.BeginErrorReadLine();
                    proc.WaitForExit();
                    //proc.ExitCode;    ←戻り値の扱いを検討中
                }
            });
        }


    }
}
