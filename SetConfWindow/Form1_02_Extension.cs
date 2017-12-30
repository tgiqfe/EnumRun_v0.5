using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SetConfWindow
{
    public partial class Form1
    {

        //  Extension選択が変更されたとき
        private void cmb_ExtensionName_SelectedIndexChanged(object sender, EventArgs e)
        {
            txt_Program.Text = dl.ExtensionList[cmb_ExtensionName.SelectedIndex].Program;
            txt_ArgsBefore.Text = dl.ExtensionList[cmb_ExtensionName.SelectedIndex].Arg_Before;
            txt_ArgsAfter.Text = dl.ExtensionList[cmb_ExtensionName.SelectedIndex].Arg_After;
        }

        //  Extension適用ボタン
        private void btn_ExtensionApply_Click(object sender, EventArgs e)
        {
            int matchIndex = -1;
            string tempStr = cmb_ExtensionName.Text.StartsWith(".") ?
                cmb_ExtensionName.Text.ToLower() :
                "." + cmb_ExtensionName.Text.ToLower();
            for (int i = 0; i < cmb_ExtensionName.Items.Count; i++)
            {
                if(cmb_ExtensionName.Items[i].ToString().Equals(cmb_ExtensionName.Text, StringComparison.OrdinalIgnoreCase))
                {
                    matchIndex = i;
                    break;
                }
            }
            if (matchIndex >= 0)
            {
                dl.ExtensionList[matchIndex].Program = txt_Program.Text;
                dl.ExtensionList[matchIndex].Arg_Before = txt_ArgsBefore.Text;
                dl.ExtensionList[matchIndex].Arg_After = txt_ArgsAfter.Text;
            }
            else
            {
                dl.ExtensionList.Add(new Extension(cmb_ExtensionName.Text, txt_Program.Text, txt_ArgsBefore.Text, txt_ArgsAfter.Text));
                cmb_ExtensionName.Items.Add(cmb_ExtensionName.Text);
            }

        }
    }
}
