﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management.Automation;

namespace EnumRun.Cmdlet
{
    [Cmdlet(VerbsLifecycle.Start, "LogonScript")]
    public class StartLogonScript : PSCmdlet
    {
        protected override void BeginProcessing()
        {
            Item.Config = EnumRunConfig.Load();
        }

        protected override void ProcessRecord()
        {
            Functions.StartEnumRun("LogonScript");
        }
    }
}
