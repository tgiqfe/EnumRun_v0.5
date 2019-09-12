using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace EnumRun
{
    [Flags]
    public enum EnumRunOption
    {
        None = 0,                   //  オプション無し
        NoRun = 1,                  //  [n] 実行しない
        WaitForExit = 2,            //  [w] 終了待ち
        RunAsAdmin = 4,             //  [a] 管理者として実行
        DomainUserOnly = 8,         //  [d] ドメインユーザーの場合のみ実行
        LocalUserOnly = 16,         //  [l] ローカルユーザーの場合のみ実行
        SystemAccountOnly = 32,     //  [s] システムアカウント(SYSTEM等)の場合のみ実行
        DGReachableOnly = 64,       //  [p] デフォルトゲートウェイとの通信可能な場合のみ実行
        TrustedOnly = 128,          //  [t] 管理者に昇格している場合のみ実行
        WorkgroupPCOnly = 256,      //  [k] ワークグループPCの場合のみ実行
        DomainPCOnly = 512,         //  [m] ドメイン参加PCの場合のみ実行
        BeforeWait = 1024,          //  [\dr] 実行前待機
        AfterWait = 2048,           //  [r\d] 実行後待機
    }

    public class Script
    {
        private Language _Lang = null;

        public bool Enabled { get; set; }
        public string Name { get; set; }
        public string File { get; set; }
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

        public void Run()
        {

        }

    }
}
