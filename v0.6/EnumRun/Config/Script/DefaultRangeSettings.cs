using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnumRun
{
    class DefaultRangeSettings
    {
        public static SerializableDictionary<string, Range> Create()
        {
            SerializableDictionary<string, Range> ranges = new SerializableDictionary<string, Range>();

            string startupScript = "StartupScript";
            string logonScript = "LogonScript";
            string logoffScript = "LogoffScript";
            string shutdownScript = "ShutdownScript";

            ranges[startupScript] = new Range()
            {
                ProcessName = startupScript,
                StartNumber = 0,
                EndNumber = 9
            };
            ranges[logonScript] = new Range()
            {
                ProcessName = logonScript,
                StartNumber = 11,
                EndNumber = 29
            };
            ranges[logoffScript] =new Range()
            {
                ProcessName = logoffScript,
                StartNumber = 81,
                EndNumber = 89
            };
            ranges[shutdownScript] =new Range()
            {
                ProcessName = shutdownScript,
                StartNumber = 91,
                EndNumber = 99,
            };
            return ranges;
        }
    }
}
