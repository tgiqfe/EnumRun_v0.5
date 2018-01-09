using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EnumRun;

namespace SetConfWindow
{
    public partial class Form1
    {
        //  Range選択が変更されたとき
        private void cmb_RangeName_SelectedIndexChanged(object sender, EventArgs e)
        {
            nud_RangeStart.Value = dl.RangeList[cmb_RangeName.SelectedIndex].StartNum;
            nud_RangeEnd.Value = dl.RangeList[cmb_RangeName.SelectedIndex].EndNum;
        }

        //  Range適用ボタン
        private void btn_RangeApply_Click(object sender, EventArgs e)
        {
            int matchIndex = -1;
            for (int i = 0; i < cmb_RangeName.Items.Count; i++)
            {
                if (cmb_RangeName.Items[i].ToString().Equals(cmb_RangeName.Text, StringComparison.OrdinalIgnoreCase))
                {
                    matchIndex = i;
                    break;
                }
            }
            if (matchIndex >= 0)
            {
                dl.RangeList[matchIndex].StartNum = (int)nud_RangeStart.Value;
                dl.RangeList[matchIndex].EndNum = (int)nud_RangeEnd.Value;
            }
            else
            {
                dl.RangeList.Add(new Range(cmb_RangeName.Text, (int)nud_RangeStart.Value, (int)nud_RangeEnd.Value));
                cmb_RangeName.Items.Add(cmb_RangeName.Text);
            }
        }
    }
}
