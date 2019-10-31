namespace SetConfWindow
{
    partial class Form1
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btn_LogDir_ref = new System.Windows.Forms.Button();
            this.btn_FileDir_ref = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_LogDir = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_FileDir = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.nud_RangeEnd = new System.Windows.Forms.NumericUpDown();
            this.nud_RangeStart = new System.Windows.Forms.NumericUpDown();
            this.btn_RangeApply = new System.Windows.Forms.Button();
            this.cmb_RangeName = new System.Windows.Forms.ComboBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btn_ExtensionApply = new System.Windows.Forms.Button();
            this.txt_ArgsAfter = new System.Windows.Forms.TextBox();
            this.txt_ArgsBefore = new System.Windows.Forms.TextBox();
            this.txt_Program = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cmb_ExtensionName = new System.Windows.Forms.ComboBox();
            this.lbl_Version = new System.Windows.Forms.Label();
            this.btn_Exit = new System.Windows.Forms.Button();
            this.btn_Save = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nud_RangeEnd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_RangeStart)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btn_LogDir_ref);
            this.groupBox1.Controls.Add(this.btn_FileDir_ref);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txt_LogDir);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txt_FileDir);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(400, 91);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Global";
            // 
            // btn_LogDir_ref
            // 
            this.btn_LogDir_ref.Location = new System.Drawing.Point(322, 50);
            this.btn_LogDir_ref.Name = "btn_LogDir_ref";
            this.btn_LogDir_ref.Size = new System.Drawing.Size(60, 23);
            this.btn_LogDir_ref.TabIndex = 3;
            this.btn_LogDir_ref.Text = "参照";
            this.btn_LogDir_ref.UseVisualStyleBackColor = true;
            this.btn_LogDir_ref.Click += new System.EventHandler(this.btn_LogDir_ref_Click);
            // 
            // btn_FileDir_ref
            // 
            this.btn_FileDir_ref.Location = new System.Drawing.Point(322, 21);
            this.btn_FileDir_ref.Name = "btn_FileDir_ref";
            this.btn_FileDir_ref.Size = new System.Drawing.Size(60, 23);
            this.btn_FileDir_ref.TabIndex = 1;
            this.btn_FileDir_ref.Text = "参照";
            this.btn_FileDir_ref.UseVisualStyleBackColor = true;
            this.btn_FileDir_ref.Click += new System.EventHandler(this.btn_FileDir_ref_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 15);
            this.label2.TabIndex = 3;
            this.label2.Text = "LogDir";
            // 
            // txt_LogDir
            // 
            this.txt_LogDir.Location = new System.Drawing.Point(63, 50);
            this.txt_LogDir.Name = "txt_LogDir";
            this.txt_LogDir.Size = new System.Drawing.Size(253, 23);
            this.txt_LogDir.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "FileDir";
            // 
            // txt_FileDir
            // 
            this.txt_FileDir.Location = new System.Drawing.Point(63, 21);
            this.txt_FileDir.Name = "txt_FileDir";
            this.txt_FileDir.Size = new System.Drawing.Size(253, 23);
            this.txt_FileDir.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.nud_RangeEnd);
            this.groupBox2.Controls.Add(this.nud_RangeStart);
            this.groupBox2.Controls.Add(this.btn_RangeApply);
            this.groupBox2.Controls.Add(this.cmb_RangeName);
            this.groupBox2.Location = new System.Drawing.Point(12, 109);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(400, 86);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Range";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(253, 19);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 15);
            this.label4.TabIndex = 11;
            this.label4.Text = "End";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(187, 19);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 15);
            this.label3.TabIndex = 10;
            this.label3.Text = "Start";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // nud_RangeEnd
            // 
            this.nud_RangeEnd.Location = new System.Drawing.Point(253, 44);
            this.nud_RangeEnd.Name = "nud_RangeEnd";
            this.nud_RangeEnd.Size = new System.Drawing.Size(60, 23);
            this.nud_RangeEnd.TabIndex = 2;
            // 
            // nud_RangeStart
            // 
            this.nud_RangeStart.Location = new System.Drawing.Point(187, 44);
            this.nud_RangeStart.Name = "nud_RangeStart";
            this.nud_RangeStart.Size = new System.Drawing.Size(60, 23);
            this.nud_RangeStart.TabIndex = 1;
            // 
            // btn_RangeApply
            // 
            this.btn_RangeApply.Location = new System.Drawing.Point(322, 44);
            this.btn_RangeApply.Name = "btn_RangeApply";
            this.btn_RangeApply.Size = new System.Drawing.Size(60, 23);
            this.btn_RangeApply.TabIndex = 3;
            this.btn_RangeApply.Text = "適用";
            this.btn_RangeApply.UseVisualStyleBackColor = true;
            this.btn_RangeApply.Click += new System.EventHandler(this.btn_RangeApply_Click);
            // 
            // cmb_RangeName
            // 
            this.cmb_RangeName.FormattingEnabled = true;
            this.cmb_RangeName.Location = new System.Drawing.Point(15, 43);
            this.cmb_RangeName.Name = "cmb_RangeName";
            this.cmb_RangeName.Size = new System.Drawing.Size(166, 23);
            this.cmb_RangeName.TabIndex = 0;
            this.cmb_RangeName.SelectedIndexChanged += new System.EventHandler(this.cmb_RangeName_SelectedIndexChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btn_ExtensionApply);
            this.groupBox3.Controls.Add(this.txt_ArgsAfter);
            this.groupBox3.Controls.Add(this.txt_ArgsBefore);
            this.groupBox3.Controls.Add(this.txt_Program);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.cmb_ExtensionName);
            this.groupBox3.Location = new System.Drawing.Point(12, 201);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(400, 148);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Extension";
            // 
            // btn_ExtensionApply
            // 
            this.btn_ExtensionApply.Location = new System.Drawing.Point(322, 21);
            this.btn_ExtensionApply.Name = "btn_ExtensionApply";
            this.btn_ExtensionApply.Size = new System.Drawing.Size(60, 23);
            this.btn_ExtensionApply.TabIndex = 5;
            this.btn_ExtensionApply.Text = "適用";
            this.btn_ExtensionApply.UseVisualStyleBackColor = true;
            this.btn_ExtensionApply.Click += new System.EventHandler(this.btn_ExtensionApply_Click);
            // 
            // txt_ArgsAfter
            // 
            this.txt_ArgsAfter.Location = new System.Drawing.Point(142, 106);
            this.txt_ArgsAfter.Name = "txt_ArgsAfter";
            this.txt_ArgsAfter.Size = new System.Drawing.Size(240, 23);
            this.txt_ArgsAfter.TabIndex = 4;
            // 
            // txt_ArgsBefore
            // 
            this.txt_ArgsBefore.Location = new System.Drawing.Point(142, 80);
            this.txt_ArgsBefore.Name = "txt_ArgsBefore";
            this.txt_ArgsBefore.Size = new System.Drawing.Size(240, 23);
            this.txt_ArgsBefore.TabIndex = 3;
            // 
            // txt_Program
            // 
            this.txt_Program.Location = new System.Drawing.Point(142, 54);
            this.txt_Program.Name = "txt_Program";
            this.txt_Program.Size = new System.Drawing.Size(240, 23);
            this.txt_Program.TabIndex = 2;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 109);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(114, 15);
            this.label7.TabIndex = 5;
            this.label7.Text = "Arguments [After]";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 83);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(123, 15);
            this.label6.TabIndex = 4;
            this.label6.Text = "Arguments [Before]";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 57);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(57, 15);
            this.label5.TabIndex = 3;
            this.label5.Text = "Program";
            // 
            // cmb_ExtensionName
            // 
            this.cmb_ExtensionName.FormattingEnabled = true;
            this.cmb_ExtensionName.Location = new System.Drawing.Point(15, 22);
            this.cmb_ExtensionName.Name = "cmb_ExtensionName";
            this.cmb_ExtensionName.Size = new System.Drawing.Size(80, 23);
            this.cmb_ExtensionName.TabIndex = 1;
            this.cmb_ExtensionName.SelectedIndexChanged += new System.EventHandler(this.cmb_ExtensionName_SelectedIndexChanged);
            // 
            // lbl_Version
            // 
            this.lbl_Version.AutoSize = true;
            this.lbl_Version.Location = new System.Drawing.Point(24, 367);
            this.lbl_Version.Name = "lbl_Version";
            this.lbl_Version.Size = new System.Drawing.Size(28, 15);
            this.lbl_Version.TabIndex = 2;
            this.lbl_Version.Text = "Ver.";
            // 
            // btn_Exit
            // 
            this.btn_Exit.Location = new System.Drawing.Point(324, 361);
            this.btn_Exit.Name = "btn_Exit";
            this.btn_Exit.Size = new System.Drawing.Size(70, 27);
            this.btn_Exit.TabIndex = 3;
            this.btn_Exit.Text = "閉じる";
            this.btn_Exit.UseVisualStyleBackColor = true;
            this.btn_Exit.Click += new System.EventHandler(this.btn_Exit_Click);
            // 
            // btn_Save
            // 
            this.btn_Save.Location = new System.Drawing.Point(248, 361);
            this.btn_Save.Name = "btn_Save";
            this.btn_Save.Size = new System.Drawing.Size(70, 27);
            this.btn_Save.TabIndex = 4;
            this.btn_Save.Text = "保存";
            this.btn_Save.UseVisualStyleBackColor = true;
            this.btn_Save.Click += new System.EventHandler(this.btn_Save_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(428, 402);
            this.Controls.Add(this.btn_Save);
            this.Controls.Add(this.btn_Exit);
            this.Controls.Add(this.lbl_Version);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SetConf";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nud_RangeEnd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_RangeStart)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btn_LogDir_ref;
        private System.Windows.Forms.Button btn_FileDir_ref;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_LogDir;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_FileDir;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown nud_RangeEnd;
        private System.Windows.Forms.NumericUpDown nud_RangeStart;
        private System.Windows.Forms.Button btn_RangeApply;
        private System.Windows.Forms.ComboBox cmb_RangeName;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btn_ExtensionApply;
        private System.Windows.Forms.TextBox txt_ArgsAfter;
        private System.Windows.Forms.TextBox txt_ArgsBefore;
        private System.Windows.Forms.TextBox txt_Program;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmb_ExtensionName;
        private System.Windows.Forms.Label lbl_Version;
        private System.Windows.Forms.Button btn_Exit;
        private System.Windows.Forms.Button btn_Save;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
    }
}

