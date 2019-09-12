using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace EnumRun
{
    public enum EnumRunOption
    {
        RunAsAdmin = 0,             //  [a] 管理者として実行
        WaitForExit = 2,            //  [w] 終了待ち
    }

    public enum EnumRunRestriction
    {
        None = 0,                   //  [n]どの場合でも実行しない
        TrustedOnly = 2,            //  [t]管理者に昇格している場合のみ実行
        DGReachableOnly = 4,        //  [p]デフォルトゲートウェイとの通信可能な場合のみ実行
        WorkgroupPCOnly = 8,        //  [p]ワークグループPCの場合のみ実行
        DomainPCOnly = 16,          //  [m]ドメイン参加PCの場合のみ実行
        DomainUserOnly = 32,        //  [d]ドメインユーザーの場合のみ実行
        LocalUserOnly = 64,         //  [l]ローカルユーザーの場合のみ実行
        SystemAccountOnly = 128,    //  [s]システムアカウント(SYSTEM等)の場合のみ実行
    }

    public class Script
    {
        private Language _Lang = null;

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
        public EnumRunRestriction Restriction { get; set; }
        public int BeforeTime { get; set; }
        public int AfterTime { get; set; }

        public Script() { }



    }
}
