﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management.Automation;

namespace EnumRun.Cmdlet
{
    [Cmdlet(VerbsCommon.Add, "EnumRunRange")]
    public class AddEnumRunRange : PSCmdlet
    {
        [Parameter(ValueFromPipeline = true)]
        public Range Range { get; set; }
        [Parameter(Position = 0)]
        public string ProcessName { get; set; }
        [Parameter(Position = 1)]
        public int StartNumber { get; set; }
        [Parameter(Position = 2)]
        public int EndNumber { get; set; }

        protected override void BeginProcessing()
        {
            Item.Config = EnumRunConfig.Load();
        }

        protected override void ProcessRecord()
        {
            if (Range == null && !string.IsNullOrEmpty(ProcessName))
            {
                Range range = new Range()
                {
                    ProcessName = this.ProcessName,
                    StartNumber = this.StartNumber,
                    EndNumber = this.EndNumber
                };
                Item.Config.Ranges[ProcessName] = range;
            }
            else if (Range != null)
            {
                Item.Config.Ranges[Range.ProcessName] = Range;
            }
            Item.Config.Save();
        }
    }
}
