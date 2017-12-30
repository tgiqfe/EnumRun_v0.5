using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;

namespace SetConfWindow
{
    public class DataLibrary
    {
        public string FileDir { get; set; }
        public string LogDir { get; set; }
        public List<Range> RangeList { get; set; }
        public List<Extension> ExtensionList { get; set; }

        [XmlIgnore]
        public string FileName { get; set; }

        //  コンストラクタ
        public DataLibrary() { }

        //  初回実行時のみ
        public void Init()
        {
            this.RangeList = new List<Range>();
            this.RangeList.Add(new Range("StartupScript", 1, 9));
            this.RangeList.Add(new Range("LogonScript", 11, 29));
            this.RangeList.Add(new Range("LogoffScript", 81, 89));
            this.RangeList.Add(new Range("ShutdownScript", 91, 99));

            this.ExtensionList = new List<Extension>();
            this.ExtensionList.Add(new Extension(".vbs", @"C:\Windows\System32\wscript.exe", "", ""));
            this.ExtensionList.Add(new Extension(".vbe", @"C:\Windows\System32\wscript.exe", "", ""));
            this.ExtensionList.Add(new Extension(".js", @"C:\Windows\System32\wscript.exe", "", ""));
            this.ExtensionList.Add(new Extension(".jse", @"C:\Windows\System32\wscript.exe", "", ""));
            this.ExtensionList.Add(new Extension(".wsf", @"C:\Windows\System32\wscript.exe", "", ""));
            this.ExtensionList.Add(new Extension(".jar", @"C:\Program Files\Java\jre1.8.0\bin\java.exe", "-jar", ""));
            this.ExtensionList.Add(new Extension(".exe", "", "", ""));
            this.ExtensionList.Add(new Extension(".bat", @"C:\Windows\System32\cmd.exe", "/c", ""));
            this.ExtensionList.Add(new Extension(".cmd", @"C:\Windows\System32\cmd.exe", "/c", ""));
            this.ExtensionList.Add(new Extension(".ps1", @"C:\Windows\System32\WindowsPowerShell\v1.0\powershell.exe", "-NoProfile -ExecutionPolicy Unrestricted", ""));
        }

        //  ファクトリ
        public static DataLibrary Create(string fileName)
        {
            DataLibrary dl = null;
            try
            {
                using (StreamReader sr = new StreamReader(fileName, Encoding.UTF8))
                {
                    dl = new XmlSerializer(typeof(DataLibrary)).Deserialize(sr) as DataLibrary;
                }
            }
            catch { }
            finally
            {
                if (dl == null)
                {
                    dl = new DataLibrary();
                    dl.Init();
                }
            }
            dl.FileName = fileName;
            return dl;
        }

        //  設定保存
        public void Save()
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(this.FileName, false, Encoding.UTF8))
                {
                    new XmlSerializer(typeof(DataLibrary)).Serialize(sw, this);
                }
            }
            catch { }
        }
    }

    //  Range用
    public class Range
    {
        [XmlAttribute("Name")]
        public string Name { get; set; }
        [XmlAttribute("Start")]
        public int StartNum { get; set; }
        [XmlAttribute("End")]
        public int EndNum { get; set; }
        public Range() { }
        public Range(string name, int startnum, int endnum)
        {
            this.Name = name;
            this.StartNum = startnum;
            this.EndNum = endnum;
        }
    }

    //  Extension用
    public class Extension
    {
        [XmlAttribute("Name")]
        public string Name { get; set; }
        public string Program { get; set; }
        public string Arg_Before { get; set; }
        public string Arg_After { get; set; }
        public Extension() { }
        public Extension(string name, string program, string arg_bef, string arg_aft)
        {
            this.Name = name;
            this.Program = program;
            this.Arg_Before = arg_bef;
            this.Arg_After = arg_aft;
        }
    }
}
