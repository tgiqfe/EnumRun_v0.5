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
                //Language lang = Item.Config.Languages.FirstOrDefault(x =>
                //    x.Value.Name.Equals(Name, StringComparison.OrdinalIgnoreCase)).Value;
                //Language lang = Item.Config.Languages.FirstOrDefault(x =>
                //    x.Name.Equals(Name, StringComparison.OrdinalIgnoreCase));
                int index = Item.Config.Languages.FindIndex(x => x.Name.Equals(Name, StringComparison.OrdinalIgnoreCase));
                if (Extensions != null) { Item.Config.Languages[index].Extensions = Extensions; }
                if (Command != null) { Item.Config.Languages[index].Command = Command; }
                if (Command_x86 != null) { Item.Config.Languages[index].Command_x86 = Command_x86; }
                if (ArgsPrefix != null) { Item.Config.Languages[index].ArgsPrefix = ArgsPrefix; }
                if (ArgsMidWithoutArgs != null) { Item.Config.Languages[index].ArgsMidWithoutArgs = ArgsMidWithoutArgs; }
                if (ArgsMidWithArgs != null) { Item.Config.Languages[index].ArgsMidWithArgs = ArgsMidWithArgs; }
                if (ArgsSuffix != null) { Item.Config.Languages[index].ArgsSuffix = ArgsSuffix; }

                //  値を直接変更なので、代入し直す必要がないと思われる。

                //Item.Config.Languages[Name] = lang;
            }
            else if (Language != null)
            {
                if (Name != null) { Language.Name = Name; }
                if (Extensions != null) { Language.Extensions = Extensions; }
                if (Command != null) { Language.Command = Command; }
                if (Command_x86 != null) { Language.Command_x86 = Command_x86; }
                if (ArgsPrefix != null) { Language.ArgsPrefix = ArgsPrefix; }
                if (ArgsMidWithoutArgs != null) { Language.ArgsMidWithoutArgs = ArgsMidWithoutArgs; }
                if (ArgsMidWithArgs != null) { Language.ArgsMidWithArgs = ArgsMidWithArgs; }
                if (ArgsSuffix != null) { Language.ArgsSuffix = ArgsSuffix; }

                int index = Item.Config.Languages.FindIndex(x =>
                    x.Name.Equals(Name, StringComparison.OrdinalIgnoreCase));
                if(index >= 0)
                {

                }


                //Item.Config.Languages[Language.Name] = Language;
            }
            Item.Config.Save();
        }
    }
}
