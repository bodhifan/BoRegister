﻿namespace PayRegister
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
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.AddrLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.btnReloadData = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tbYCoord = new System.Windows.Forms.TextBox();
            this.tbXCoord = new System.Windows.Forms.TextBox();
            this.button6 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.statusBox = new System.Windows.Forms.RichTextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.btnClearAll = new System.Windows.Forms.Button();
            this.btnClearFailed = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.FailedLabel = new System.Windows.Forms.Label();
            this.ReminderLabel = new System.Windows.Forms.Label();
            this.SucLabel = new System.Windows.Forms.Label();
            this.TotalLabel = new System.Windows.Forms.Label();
            this.ConsumeTimesLabel = new System.Windows.Forms.Label();
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
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AddrLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 555);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(528, 22);
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
            this.splitContainer1.Size = new System.Drawing.Size(528, 555);
            this.splitContainer1.SplitterDistance = 272;
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
            this.splitContainer2.Panel1.Controls.Add(this.ConsumeTimesLabel);
            this.splitContainer2.Panel1.Controls.Add(this.FailedLabel);
            this.splitContainer2.Panel1.Controls.Add(this.ReminderLabel);
            this.splitContainer2.Panel1.Controls.Add(this.SucLabel);
            this.splitContainer2.Panel1.Controls.Add(this.TotalLabel);
            this.splitContainer2.Panel1.Controls.Add(this.label4);
            this.splitContainer2.Panel1.Controls.Add(this.label3);
            this.splitContainer2.Panel1.Controls.Add(this.label2);
            this.splitContainer2.Panel1.Controls.Add(this.label1);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.tabControl1);
            this.splitContainer2.Size = new System.Drawing.Size(272, 555);
            this.splitContainer2.SplitterDistance = 200;
            this.splitContainer2.TabIndex = 2;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(272, 351);
            this.tabControl1.TabIndex = 4;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.btnClearFailed);
            this.tabPage1.Controls.Add(this.btnClearAll);
            this.tabPage1.Controls.Add(this.btnReloadData);
            this.tabPage1.Controls.Add(this.button2);
            this.tabPage1.Controls.Add(this.button1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(264, 325);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "自动";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // btnReloadData
            // 
            this.btnReloadData.Location = new System.Drawing.Point(163, 24);
            this.btnReloadData.Name = "btnReloadData";
            this.btnReloadData.Size = new System.Drawing.Size(80, 25);
            this.btnReloadData.TabIndex = 6;
            this.btnReloadData.Text = "重新加载";
            this.btnReloadData.UseVisualStyleBackColor = true;
            this.btnReloadData.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(18, 92);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(99, 42);
            this.button2.TabIndex = 5;
            this.button2.Text = "暂停";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(18, 32);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(99, 42);
            this.button1.TabIndex = 4;
            this.button1.Text = "开始";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.tbYCoord);
            this.tabPage2.Controls.Add(this.tbXCoord);
            this.tabPage2.Controls.Add(this.button6);
            this.tabPage2.Controls.Add(this.button5);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(289, 252);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "手动";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tbYCoord
            // 
            this.tbYCoord.Location = new System.Drawing.Point(9, 109);
            this.tbYCoord.Name = "tbYCoord";
            this.tbYCoord.Size = new System.Drawing.Size(100, 21);
            this.tbYCoord.TabIndex = 8;
            // 
            // tbXCoord
            // 
            this.tbXCoord.Location = new System.Drawing.Point(9, 69);
            this.tbXCoord.Name = "tbXCoord";
            this.tbXCoord.Size = new System.Drawing.Size(100, 21);
            this.tbXCoord.TabIndex = 7;
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(124, 18);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(96, 33);
            this.button6.TabIndex = 6;
            this.button6.Text = "合并图片";
            this.button6.UseVisualStyleBackColor = true;
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(146, 97);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(96, 33);
            this.button5.TabIndex = 5;
            this.button5.Text = "重新点击";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // statusBox
            // 
            this.statusBox.BackColor = System.Drawing.SystemColors.Info;
            this.statusBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.statusBox.Location = new System.Drawing.Point(0, 0);
            this.statusBox.Name = "statusBox";
            this.statusBox.Size = new System.Drawing.Size(252, 555);
            this.statusBox.TabIndex = 0;
            this.statusBox.Text = "";
            // 
            // timer1
            // 
            this.timer1.Interval = 2000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // btnClearAll
            // 
            this.btnClearAll.Location = new System.Drawing.Point(163, 68);
            this.btnClearAll.Name = "btnClearAll";
            this.btnClearAll.Size = new System.Drawing.Size(80, 25);
            this.btnClearAll.TabIndex = 7;
            this.btnClearAll.Text = "清除所有";
            this.btnClearAll.UseVisualStyleBackColor = true;
            this.btnClearAll.Click += new System.EventHandler(this.btnClearAll_Click);
            // 
            // btnClearFailed
            // 
            this.btnClearFailed.Location = new System.Drawing.Point(163, 111);
            this.btnClearFailed.Name = "btnClearFailed";
            this.btnClearFailed.Size = new System.Drawing.Size(80, 25);
            this.btnClearFailed.TabIndex = 8;
            this.btnClearFailed.Text = "清除失败";
            this.btnClearFailed.UseVisualStyleBackColor = true;
            this.btnClearFailed.Click += new System.EventHandler(this.btnClearFailed_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(112, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "总计注册数量：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(112, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "成功注册数量：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(112, 154);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "剩余注册数量：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(112, 107);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(89, 12);
            this.label4.TabIndex = 3;
            this.label4.Text = "失败注册数量：";
            // 
            // FailedLabel
            // 
            this.FailedLabel.AutoSize = true;
            this.FailedLabel.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FailedLabel.ForeColor = System.Drawing.Color.Red;
            this.FailedLabel.Location = new System.Drawing.Point(204, 107);
            this.FailedLabel.Name = "FailedLabel";
            this.FailedLabel.Size = new System.Drawing.Size(18, 12);
            this.FailedLabel.TabIndex = 7;
            this.FailedLabel.Text = "０";
            // 
            // ReminderLabel
            // 
            this.ReminderLabel.AutoSize = true;
            this.ReminderLabel.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ReminderLabel.ForeColor = System.Drawing.Color.Blue;
            this.ReminderLabel.Location = new System.Drawing.Point(204, 154);
            this.ReminderLabel.Name = "ReminderLabel";
            this.ReminderLabel.Size = new System.Drawing.Size(18, 12);
            this.ReminderLabel.TabIndex = 6;
            this.ReminderLabel.Text = "０";
            // 
            // SucLabel
            // 
            this.SucLabel.AutoSize = true;
            this.SucLabel.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.SucLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.SucLabel.Location = new System.Drawing.Point(204, 60);
            this.SucLabel.Name = "SucLabel";
            this.SucLabel.Size = new System.Drawing.Size(18, 12);
            this.SucLabel.TabIndex = 5;
            this.SucLabel.Text = "０";
            // 
            // TotalLabel
            // 
            this.TotalLabel.AutoSize = true;
            this.TotalLabel.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.TotalLabel.Location = new System.Drawing.Point(204, 16);
            this.TotalLabel.Name = "TotalLabel";
            this.TotalLabel.Size = new System.Drawing.Size(18, 12);
            this.TotalLabel.TabIndex = 4;
            this.TotalLabel.Text = "０";
            // 
            // ConsumeTimesLabel
            // 
            this.ConsumeTimesLabel.AutoSize = true;
            this.ConsumeTimesLabel.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ConsumeTimesLabel.ForeColor = System.Drawing.Color.Blue;
            this.ConsumeTimesLabel.Location = new System.Drawing.Point(20, 87);
            this.ConsumeTimesLabel.Name = "ConsumeTimesLabel";
            this.ConsumeTimesLabel.Size = new System.Drawing.Size(17, 12);
            this.ConsumeTimesLabel.TabIndex = 8;
            this.ConsumeTimesLabel.Text = "０";
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
            this.ClientSize = new System.Drawing.Size(528, 577);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.statusStrip1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
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
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel AddrLabel;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.RichTextBox statusBox;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Button btnReloadData;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.TextBox tbYCoord;
        private System.Windows.Forms.TextBox tbXCoord;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label ConsumeTimesLabel;
        private System.Windows.Forms.Label FailedLabel;
        private System.Windows.Forms.Label ReminderLabel;
        private System.Windows.Forms.Label SucLabel;
        private System.Windows.Forms.Label TotalLabel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnClearFailed;
        private System.Windows.Forms.Button btnClearAll;
        private System.Windows.Forms.Timer timer2;
    }
}

