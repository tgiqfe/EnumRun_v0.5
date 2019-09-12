using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace EnumRun
{
    class EnumRunControl
    {
        public static EnumRunConfig ReadConf()
        {
            string confName = Path.Combine(Item.CONF_DIR, "Config.json");
            if (File.Exists(confName)) { return DataSerializer.Deserialize<EnumRunConfig>(confName); }

            confName = Path.Combine(Item.CONF_DIR, "Config.xml");
            if (File.Exists(confName)) { return DataSerializer.Deserialize<EnumRunConfig>(confName); }

            confName = Path.Combine(Item.CONF_DIR, "Config.yml");
            if (File.Exists(confName)) { return DataSerializer.Deserialize<EnumRunConfig>(confName); }

            return null;
        }
    }
}
