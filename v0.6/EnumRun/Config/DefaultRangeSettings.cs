using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnumRun
{
    class DefaultRangeSettings
    {
        public static List<Range> Create()
        {
            List<Range> ranges = new List<Range>();
            ranges.Add(new Range()
            {
                Name = "StartupScript",
                StartNumber = 0,
                EndNumber = 9,
                ProcessName = "StartupScript"
            });
            ranges.Add(new Range()
            {
                Name = "LogonScript",
                StartNumber = 11,
                EndNumber = 29,
                ProcessName = "LogonScript"
            });
            ranges.Add(new Range()
            {
                Name = "LogoffScript",
                StartNumber = 81,
                EndNumber = 89,
                ProcessName = "LogoffScript"
            });
            ranges.Add(new Range()
            {
                Name = "ShutdownScript",
                StartNumber = 91,
                EndNumber = 99,
                ProcessName = "ShutdownScript"
            });
            return ranges;
        }
    }
}
