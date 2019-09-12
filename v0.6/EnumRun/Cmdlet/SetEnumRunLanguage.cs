using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management.Automation;

namespace EnumRun.Cmdlet
{
    [Cmdlet(VerbsCommon.Set, "EnumRunLanguage")]
    public class SetEnumRunLanguage : PSCmdlet
    {
        [Parameter(Mandatory = true)]
        public Language Language { get; set; }
        [Parameter]
        public SwitchParameter DefaultSetting { get; set; }




    }
}
