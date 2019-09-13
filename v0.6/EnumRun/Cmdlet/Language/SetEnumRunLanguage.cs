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
        [Parameter(ValueFromPipeline = true)]
        public Language Language { get; set; }
        [Parameter(Position = 0)]
        public string Name { get; set; }
        [Parameter]
        public string[] Extensions { get; set; }
        [Parameter]
        public string Command { get; set; }
        [Parameter]
        public string Command_x86 { get; set; }
        [Parameter]
        public string ArgsPrefix { get; set; }
        [Parameter]
        public string ArgsMidWithoutArgs { get; set; }
        [Parameter]
        public string ArgsMidWithArgs { get; set; }
        [Parameter]
        public string ArgsSuffix { get; set; }

        protected override void BeginProcessing()
        {
            Item.Config = EnumRunConfig.Load();
        }

        protected override void ProcessRecord()
        {
            if (Language == null && !string.IsNullOrEmpty(Name))
            {
                Language lang = Item.Config.Languages.FirstOrDefault(x =>
                    x.Value.Name.Equals(Name, StringComparison.OrdinalIgnoreCase)).Value;
                if (Extensions != null) { lang.Extensions = Extensions; }
                if (Command != null) { lang.Command = Command; }
                if (Command_x86 != null) { lang.Command_x86 = Command_x86; }
                if (ArgsPrefix != null) { lang.ArgsPrefix = ArgsPrefix; }
                if (ArgsMidWithoutArgs != null) { lang.ArgsMidWithoutArgs = ArgsMidWithoutArgs; }
                if (ArgsMidWithArgs != null) { lang.ArgsMidWithArgs = ArgsMidWithArgs; }
                if (ArgsSuffix != null) { lang.ArgsSuffix = ArgsSuffix; }
                Item.Config.Languages[Name] = lang;
            }
            else if (Language != null)
            {
                if(Name != null) { Language.Name = Name; }
                if (Extensions != null) { Language.Extensions = Extensions; }
                if (Command != null) { Language.Command = Command; }
                if (Command_x86 != null) { Language.Command_x86 = Command_x86; }
                if (ArgsPrefix != null) { Language.ArgsPrefix = ArgsPrefix; }
                if (ArgsMidWithoutArgs != null) { Language.ArgsMidWithoutArgs = ArgsMidWithoutArgs; }
                if (ArgsMidWithArgs != null) { Language.ArgsMidWithArgs = ArgsMidWithArgs; }
                if (ArgsSuffix != null) { Language.ArgsSuffix = ArgsSuffix; }
                Item.Config.Languages[Language.Name] = Language;
            }
            Item.Config.Save();
        }
    }
}
