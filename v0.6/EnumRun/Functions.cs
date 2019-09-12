using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace EnumRun
{
    class Functions
    {
        public static string[] SplitComma(string sourceText)
        {
            return Regex.Split(sourceText, @",\s*");
        }
        public static string[] SplitComma(string[] sourceTexts)
        {
            List<string> textList = new List<string>();
            foreach(string text in sourceTexts)
            {
                textList.AddRange(SplitComma(text));
            }
            return textList.ToArray();
        }

        
    }
}
