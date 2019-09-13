using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.IO;
using System.Management;
using System.Security.Principal;
using System.Net.NetworkInformation;
using System.Threading;

namespace EnumRun
{
    class Functions
    {
        public static string[] SplitComma(string sourceText)
        {
            return Regex.Split(sourceText, @",\s*");
        }
        public static string[] SplitComma(string[] sourceTexts)
        {
            List<string> textList = new List<string>();
            foreach (string text in sourceTexts)
            {
                textList.AddRange(SplitComma(text));
            }
            return textList.ToArray();
        }

        /// <summary>
        /// 列挙実行開始
        /// </summary>
        /// <param name="processName">プロセス名</param>
        public static void StartEnumRun(string processName)
        {
            if (Item.Config.Ranges.ContainsKey(processName))
            {
                int startNum = Item.Config.Ranges[processName].StartNumber;
                int endNum = Item.Config.Ranges[processName].EndNumber;

                if (Directory.Exists(Item.Config.FilesPath))
                {
                    //  スクリプトファイルの列挙
                    List<Script> scriptList = new List<Script>();
                    foreach (string scriptFile in Directory.GetFiles(Item.Config.FilesPath))
                    {
                        Script script = new Script(scriptFile, startNum, endNum);
                        if (script.Enabled)
                        {
                            scriptList.Add(script);
                            //DataSerializer.Serialize<Script>(script, Console.Out, ".json");
                        }
                    }

                    //  スクリプトファイルの実行
                    foreach (Script script in scriptList)
                    {
                        script.Process();
                    }
                }
            }
        }

        /// <summary>
        /// Active Directoryドメインの名前を取得
        /// </summary>
        private static string _domainName = null;
        private static string GetDomainName()
        {
            if (_domainName == null)
            {
                ManagementObject mo = new ManagementClass("Win32_ComputerSystem").
                    GetInstances().
                    OfType<ManagementObject>().
                    FirstOrDefault(x => (bool)x["PartOfDomain"]);
                _domainName = mo == null ? "" : mo["Domain"] as string;
            }
            return _domainName;
        }

        /// <summary>
        /// 実行中PCがドメイン参加済みかどうか
        /// </summary>
        /// <returns>ドメイン参加済みであればtrue</returns>
        public static bool IsDomainMachine()
        {
            return !string.IsNullOrEmpty(GetDomainName());
        }

        /// <summary>
        /// システムアカウントかどうか
        /// 「System」「LocalService」「Network Service」「ホスト名$」が該当
        /// </summary>
        /// <returns>システムアカウントであればtrue</returns>
        public static bool IsSystemAccount()
        {
            return new string[] { "System", "Local Service", "Network Service", Environment.MachineName + "$" }.
                Any(x => x.Equals(Environment.UserName, StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// ドメインユーザーかどうか
        /// </summary>
        /// <returns>ドメインユーザーであればtrue</returns>
        public static bool IsDomainUser()
        {
            return !IsSystemAccount() && IsDomainMachine() && (Environment.UserDomainName != Environment.MachineName);
        }

        /// <summary>
        /// デフォルトゲートウェイへの導通可否確認
        /// </summary>
        /// <returns>導通可の場合true</returns>
        public static bool IsDefaultGatewayReachable()
        {
            int count = 4;
            int interval = 500;
            Ping ping = new Ping();
            foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
            {
                foreach (GatewayIPAddressInformation gw in nic.GetIPProperties().GatewayAddresses)
                {
                    for (int i = 0; i < count; i++)
                    {
                        PingReply reply = ping.Send(gw.Address);
                        if (reply.Status == IPStatus.Success)
                        {
                            return true;
                        }
                        Thread.Sleep(interval);
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// 管理者実行しているかどうかの確認
        /// </summary>
        /// <returns>管理者として実行しているのならばtrue</returns>
        public static bool IsRunAdministrator()
        {
            WindowsIdentity id = WindowsIdentity.GetCurrent();
            WindowsPrincipal principal = new WindowsPrincipal(id);
            bool isAdmin = principal.IsInRole(WindowsBuiltInRole.Administrator);
            return isAdmin;
        }
    }
}
