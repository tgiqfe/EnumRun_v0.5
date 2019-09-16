using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;

namespace EnumRun
{
    public class BootAndLogonSession
    {
        public string ProcessName { get; set; }
        public string BootUpTime { get; set; }
        public List<string> LogonIdList { get; set; }

        public BootAndLogonSession() { }
        public BootAndLogonSession(string processName)
        {
            this.ProcessName = processName;

            //  起動時間
            foreach (ManagementObject mo in new ManagementClass("Win32_OperatingSystem").
                GetInstances().
                OfType<ManagementObject>())
            {
                this.BootUpTime = mo["LastBootUpTime"] as string;
            }

            //  ログオン時間
            this.LogonIdList = new List<string>();
            foreach (ManagementObject mo in new ManagementClass("Win32_LoggedOnUser").
                GetInstances().
                OfType<ManagementObject>())
            {
                //  Win32_Account
                ManagementObject moA = new ManagementObject(mo["Antecedent"] as string);
                if (moA["Name"] as string == Environment.UserName)
                {
                    //  Win32_LogonSession
                    ManagementObject moB = new ManagementObject(mo["Dependent"] as string);
                    this.LogonIdList.Add(moB["LogonId"] as string);
                }
            }
            LogonIdList.Sort();
        }


    }
}
