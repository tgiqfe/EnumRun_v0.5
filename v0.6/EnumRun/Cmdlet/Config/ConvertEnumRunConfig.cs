using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management.Automation;
using System.IO;

namespace EnumRun.Cmdlet
{
    [Cmdlet(VerbsData.Convert, "EnumRunConfig")]
    public class ConvertEnumRunConfig : PSCmdlet
    {
        [Parameter, ValidateSet(Item.JSON, Item.XML, Item.YML)]
        public string DataType { get; set; } = Item.JSON;

        protected override void BeginProcessing()
        {
            Item.Config = EnumRunConfig.Load();

            DataType = new string[] { Item.JSON, Item.XML, Item.YML }.
                FirstOrDefault(x => x.Equals(DataType, StringComparison.OrdinalIgnoreCase));
        }

        protected override void ProcessRecord()
        {
            string jsonFile = Path.Combine(Item.CONF_DIR, "Config.json");
            string xmlFile = Path.Combine(Item.CONF_DIR, "Config.xml");
            string ymlFile = Path.Combine(Item.CONF_DIR, "Config.yml");
            if (File.Exists(jsonFile)) { File.Delete(jsonFile); }
            if (File.Exists(xmlFile)) { File.Delete(xmlFile); }
            if (File.Exists(ymlFile)) { File.Delete(ymlFile); }
            switch (DataType)
            {
                case Item.JSON:
                    File.Create(jsonFile).Close();
                    break;
                case Item.XML:
                    File.Create(xmlFile).Close();
                    break;
                case Item.YML:
                    File.Create(ymlFile).Close();
                    break;
            }
            Item.Config.Save();
        }
    }
}
