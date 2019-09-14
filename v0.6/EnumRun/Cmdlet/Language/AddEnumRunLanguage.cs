using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management.Automation;

namespace EnumRun.Cmdlet
{
    [Cmdlet(VerbsCommon.Add, "EnumRunLanguage")]
    public class AddEnumRunLanguage : PSCmdlet
    {
        [Parameter(ValueFromPipeline = true)]
        public Language[] Language { get; set; }
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
                Language[] langs = Item.Config.GetLanguage(Name);
                if (langs != null && langs.Length > 0)
                {
                    //  すでに同じ名前のLanguageがある為、追加不可
                    return;
                }
                else
                {
                    Item.Config.Languages.Add(new Language()
                    {
                        Name = this.Name,
                        Extensions = this.Extensions,
                        Command = this.Command,
                        Command_x86 = this.Command_x86,
                        ArgsPrefix = this.ArgsPrefix,
                        ArgsMidWithoutArgs = this.ArgsMidWithoutArgs,
                        ArgsMidWithArgs = this.ArgsMidWithArgs,
                        ArgsSuffix = this.ArgsSuffix
                    });
                }
            }
            else if (Language != null)
            {
                //  Languageインスタンスの追加についての処理を検討中
                /*
                foreach (Language lang in Language)
                {
                    

                    Language[] langs = Item.Config.GetLanguage(lang.Name);
                    if (langs != null && langs.Length > 0)
                    {
                        //  すでに同じ名前のLanguageがある為、追加不可
                        return;
                    }
                    else
                    {
                        foreach()
                    }
                }

                foreach (Language addLang in Language)
                {
                    Language lang = Item.Config.GetLanguage(addLang.Name);
                    if (lang == null)
                    {
                        Item.Config.Languages.Add(addLang);
                    }
                }
                */
            }
            Item.Config.Save();
        }
    }
}
