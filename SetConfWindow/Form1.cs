using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;

namespace SetConfWindow
{
    public partial class Form1 : Form
    {
        //  フィールドパラメータ
        DataLibrary dl = null;

        //  コンストラクタ
        public Form1() { InitializeComponent(); }
        public Form1(DataLibrary dl)
        {
            InitializeComponent();
            this.dl = dl;

            //  データ反映
            txt_FileDir.DataBindings.Add("Text", dl, "FileDir", false, DataSourceUpdateMode.OnPropertyChanged);
            txt_LogDir.DataBindings.Add("Text", dl, "LogDir", false, DataSourceUpdateMode.OnPropertyChanged);
            dl.RangeList.ForEach(x => cmb_RangeName.Items.Add(x.Name));
            dl.ExtensionList.ForEach(x => cmb_ExtensionName.Items.Add(x.Name));
        }

        //  ロード
        private void Form1_Load(object sender, EventArgs e)
        {
            this.Show();
            if (cmb_RangeName.Items.Count > 0) { cmb_RangeName.SelectedIndex = 0; }
            if (cmb_ExtensionName.Items.Count > 0) { cmb_ExtensionName.SelectedIndex = 0; }
            Version ver = Assembly.GetExecutingAssembly().GetName().Version;
            lbl_Version.Text = $"Ver. {ver.Major}.{ver.Minor}.{ver.Build}.{ver.Revision}";
            txt_FileDir.Focus();
        }

        //  閉じるボタン
        private void btn_Exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        //  保存ボタン
        private void btn_Save_Click(object sender, EventArgs e)
        {
            dl.Save();
        }

        //  キー押下時動作
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    btn_Exit_Click(sender, e);
                    break;
            }
        }

        //  FileDirの参照ボタン
        private void btn_FileDir_ref_Click(object sender, EventArgs e)
        {
            if(folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                txt_FileDir.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        //  LogDirの参照ボタン
        private void btn_LogDir_ref_Click(object sender, EventArgs e)
        {
            if(folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                txt_LogDir.Text = folderBrowserDialog1.SelectedPath;
            }
        }
    }
}
