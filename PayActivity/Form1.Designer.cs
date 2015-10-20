namespace PayRegister
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.AddrLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.label6 = new System.Windows.Forms.Label();
            this.labelTotalTime = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.labDead = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.reminderLabel = new System.Windows.Forms.Label();
            this.failedLabel = new System.Windows.Forms.Label();
            this.SucLabel = new System.Windows.Forms.Label();
            this.totalLabel = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.chNeedNewStart = new System.Windows.Forms.CheckBox();
            this.chUseMode = new System.Windows.Forms.CheckBox();
            this.chMode = new System.Windows.Forms.CheckBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.button5 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.btnLoginSMS = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.statusBox = new System.Windows.Forms.RichTextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(80, 79);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(99, 42);
            this.button1.TabIndex = 0;
            this.button1.Text = "开始";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(80, 137);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(99, 42);
            this.button2.TabIndex = 1;
            this.button2.Text = "暂停";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AddrLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 549);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(543, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // AddrLabel
            // 
            this.AddrLabel.Name = "AddrLabel";
            this.AddrLabel.Size = new System.Drawing.Size(43, 17);
            this.AddrLabel.Text = "当前IP";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.statusBox);
            this.splitContainer1.Size = new System.Drawing.Size(543, 549);
            this.splitContainer1.SplitterDistance = 255;
            this.splitContainer1.TabIndex = 3;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.label6);
            this.splitContainer2.Panel1.Controls.Add(this.labelTotalTime);
            this.splitContainer2.Panel1.Controls.Add(this.label5);
            this.splitContainer2.Panel1.Controls.Add(this.labDead);
            this.splitContainer2.Panel1.Controls.Add(this.label4);
            this.splitContainer2.Panel1.Controls.Add(this.label3);
            this.splitContainer2.Panel1.Controls.Add(this.label2);
            this.splitContainer2.Panel1.Controls.Add(this.label1);
            this.splitContainer2.Panel1.Controls.Add(this.reminderLabel);
            this.splitContainer2.Panel1.Controls.Add(this.failedLabel);
            this.splitContainer2.Panel1.Controls.Add(this.SucLabel);
            this.splitContainer2.Panel1.Controls.Add(this.totalLabel);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.tabControl1);
            this.splitContainer2.Size = new System.Drawing.Size(255, 549);
            this.splitContainer2.SplitterDistance = 173;
            this.splitContainer2.TabIndex = 2;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(19, 55);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(67, 14);
            this.label6.TabIndex = 11;
            this.label6.Text = "累计用时";
            // 
            // labelTotalTime
            // 
            this.labelTotalTime.AutoSize = true;
            this.labelTotalTime.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelTotalTime.ForeColor = System.Drawing.Color.Blue;
            this.labelTotalTime.Location = new System.Drawing.Point(24, 81);
            this.labelTotalTime.Name = "labelTotalTime";
            this.labelTotalTime.Size = new System.Drawing.Size(12, 12);
            this.labelTotalTime.TabIndex = 10;
            this.labelTotalTime.Text = "0";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(47, 139);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(137, 12);
            this.label5.TabIndex = 9;
            this.label5.Text = "超过三次注册仍未成功：";
            // 
            // labDead
            // 
            this.labDead.AutoSize = true;
            this.labDead.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labDead.ForeColor = System.Drawing.Color.Red;
            this.labDead.Location = new System.Drawing.Point(190, 138);
            this.labDead.Name = "labDead";
            this.labDead.Size = new System.Drawing.Size(15, 14);
            this.labDead.TabIndex = 8;
            this.labDead.Text = "0";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(119, 110);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 7;
            this.label4.Text = "剩余数量：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(119, 81);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "失败数量：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(119, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "注册成功：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(83, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "预注册小号总数：";
            // 
            // reminderLabel
            // 
            this.reminderLabel.AutoSize = true;
            this.reminderLabel.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.reminderLabel.ForeColor = System.Drawing.Color.Blue;
            this.reminderLabel.Location = new System.Drawing.Point(190, 110);
            this.reminderLabel.Name = "reminderLabel";
            this.reminderLabel.Size = new System.Drawing.Size(31, 14);
            this.reminderLabel.TabIndex = 3;
            this.reminderLabel.Text = "111";
            // 
            // failedLabel
            // 
            this.failedLabel.AutoSize = true;
            this.failedLabel.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.failedLabel.ForeColor = System.Drawing.Color.Gold;
            this.failedLabel.Location = new System.Drawing.Point(190, 81);
            this.failedLabel.Name = "failedLabel";
            this.failedLabel.Size = new System.Drawing.Size(31, 14);
            this.failedLabel.TabIndex = 2;
            this.failedLabel.Text = "111";
            // 
            // SucLabel
            // 
            this.SucLabel.AutoSize = true;
            this.SucLabel.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.SucLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.SucLabel.Location = new System.Drawing.Point(190, 50);
            this.SucLabel.Name = "SucLabel";
            this.SucLabel.Size = new System.Drawing.Size(31, 14);
            this.SucLabel.TabIndex = 1;
            this.SucLabel.Text = "111";
            this.SucLabel.Click += new System.EventHandler(this.SucLabel_Click);
            // 
            // totalLabel
            // 
            this.totalLabel.AutoSize = true;
            this.totalLabel.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.totalLabel.Location = new System.Drawing.Point(190, 21);
            this.totalLabel.Name = "totalLabel";
            this.totalLabel.Size = new System.Drawing.Size(31, 14);
            this.totalLabel.TabIndex = 0;
            this.totalLabel.Text = "111";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(255, 372);
            this.tabControl1.TabIndex = 5;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.chNeedNewStart);
            this.tabPage1.Controls.Add(this.chUseMode);
            this.tabPage1.Controls.Add(this.chMode);
            this.tabPage1.Controls.Add(this.button2);
            this.tabPage1.Controls.Add(this.button1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(247, 346);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "自动版";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // chNeedNewStart
            // 
            this.chNeedNewStart.AutoSize = true;
            this.chNeedNewStart.Location = new System.Drawing.Point(22, 40);
            this.chNeedNewStart.Name = "chNeedNewStart";
            this.chNeedNewStart.Size = new System.Drawing.Size(48, 16);
            this.chNeedNewStart.TabIndex = 4;
            this.chNeedNewStart.Text = "重置";
            this.chNeedNewStart.UseVisualStyleBackColor = true;
            // 
            // chUseMode
            // 
            this.chUseMode.AutoSize = true;
            this.chUseMode.Location = new System.Drawing.Point(145, 18);
            this.chUseMode.Name = "chUseMode";
            this.chUseMode.Size = new System.Drawing.Size(72, 16);
            this.chUseMode.TabIndex = 3;
            this.chUseMode.Text = "循环使用";
            this.chUseMode.UseVisualStyleBackColor = true;
            // 
            // chMode
            // 
            this.chMode.AutoSize = true;
            this.chMode.Location = new System.Drawing.Point(22, 18);
            this.chMode.Name = "chMode";
            this.chMode.Size = new System.Drawing.Size(72, 16);
            this.chMode.TabIndex = 2;
            this.chMode.Text = "隐身模式";
            this.chMode.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.button5);
            this.tabPage2.Controls.Add(this.button4);
            this.tabPage2.Controls.Add(this.btnLoginSMS);
            this.tabPage2.Controls.Add(this.button3);
            this.tabPage2.Controls.Add(this.button6);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(247, 346);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "手动版";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(8, 124);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(99, 32);
            this.button5.TabIndex = 5;
            this.button5.Text = "导出未使用号";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click_3);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(129, 75);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(99, 32);
            this.button4.TabIndex = 4;
            this.button4.Text = "清理进程";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click_2);
            // 
            // btnLoginSMS
            // 
            this.btnLoginSMS.Location = new System.Drawing.Point(8, 75);
            this.btnLoginSMS.Name = "btnLoginSMS";
            this.btnLoginSMS.Size = new System.Drawing.Size(99, 32);
            this.btnLoginSMS.TabIndex = 3;
            this.btnLoginSMS.Text = "登录短信验证码";
            this.btnLoginSMS.UseVisualStyleBackColor = true;
            this.btnLoginSMS.Click += new System.EventHandler(this.btnLoginSMS_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(129, 24);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(99, 32);
            this.button3.TabIndex = 2;
            this.button3.Text = "重连网络";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click_2);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(8, 24);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(99, 32);
            this.button6.TabIndex = 1;
            this.button6.Text = "启动浏览器";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click_1);
            // 
            // statusBox
            // 
            this.statusBox.BackColor = System.Drawing.SystemColors.Info;
            this.statusBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.statusBox.Location = new System.Drawing.Point(0, 0);
            this.statusBox.Name = "statusBox";
            this.statusBox.Size = new System.Drawing.Size(284, 549);
            this.statusBox.TabIndex = 0;
            this.statusBox.Text = "";
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // timer2
            // 
            this.timer2.Interval = 1000;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(543, 571);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.statusStrip1);
            this.Location = new System.Drawing.Point(1000, 0);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Form1";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel AddrLabel;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.RichTextBox statusBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label reminderLabel;
        private System.Windows.Forms.Label failedLabel;
        private System.Windows.Forms.Label SucLabel;
        private System.Windows.Forms.Label totalLabel;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.CheckBox chMode;
        private System.Windows.Forms.CheckBox chUseMode;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label labDead;
        private System.Windows.Forms.Button btnLoginSMS;
        private System.Windows.Forms.CheckBox chNeedNewStart;
        private System.Windows.Forms.Label labelTotalTime;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
    }
}

