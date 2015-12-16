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
            this.btnHalfFinish = new System.Windows.Forms.Button();
            this.chHalfAuto = new System.Windows.Forms.CheckBox();
            this.btnDeadLoop = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioBtnB = new System.Windows.Forms.RadioButton();
            this.radioBtnC = new System.Windows.Forms.RadioButton();
            this.btnClearFailed = new System.Windows.Forms.Button();
            this.btnClearAll = new System.Windows.Forms.Button();
            this.btnReload = new System.Windows.Forms.Button();
            this.chUseVPN = new System.Windows.Forms.CheckBox();
            this.chUseMode = new System.Windows.Forms.CheckBox();
            this.chMode = new System.Windows.Forms.CheckBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.btnReleaseAllPhones = new System.Windows.Forms.Button();
            this.btnAddIgnore = new System.Windows.Forms.Button();
            this.btnGetMessage = new System.Windows.Forms.Button();
            this.btnGetPhoneNumber = new System.Windows.Forms.Button();
            this.btnExportDead = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.btnLoginSMS = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.btnVPNState = new System.Windows.Forms.Button();
            this.btnDisconnect = new System.Windows.Forms.Button();
            this.btnLogin = new System.Windows.Forms.Button();
            this.statusBox = new System.Windows.Forms.RichTextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.btnTestNetwork = new System.Windows.Forms.Button();
            this.button9 = new System.Windows.Forms.Button();
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
            this.groupBox1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(8, 79);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(99, 42);
            this.button1.TabIndex = 0;
            this.button1.Text = "开始";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(129, 79);
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
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(255, 372);
            this.tabControl1.TabIndex = 5;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.btnHalfFinish);
            this.tabPage1.Controls.Add(this.chHalfAuto);
            this.tabPage1.Controls.Add(this.btnDeadLoop);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Controls.Add(this.btnClearFailed);
            this.tabPage1.Controls.Add(this.btnClearAll);
            this.tabPage1.Controls.Add(this.btnReload);
            this.tabPage1.Controls.Add(this.chUseVPN);
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
            // btnHalfFinish
            // 
            this.btnHalfFinish.Location = new System.Drawing.Point(14, 225);
            this.btnHalfFinish.Name = "btnHalfFinish";
            this.btnHalfFinish.Size = new System.Drawing.Size(99, 32);
            this.btnHalfFinish.TabIndex = 16;
            this.btnHalfFinish.Text = "输入完成";
            this.btnHalfFinish.UseVisualStyleBackColor = true;
            this.btnHalfFinish.Click += new System.EventHandler(this.btnHalfFinish_Click);
            // 
            // chHalfAuto
            // 
            this.chHalfAuto.AutoSize = true;
            this.chHalfAuto.Location = new System.Drawing.Point(144, 40);
            this.chHalfAuto.Name = "chHalfAuto";
            this.chHalfAuto.Size = new System.Drawing.Size(84, 16);
            this.chHalfAuto.TabIndex = 15;
            this.chHalfAuto.Text = "半自动注册";
            this.chHalfAuto.UseVisualStyleBackColor = true;
            // 
            // btnDeadLoop
            // 
            this.btnDeadLoop.Location = new System.Drawing.Point(125, 180);
            this.btnDeadLoop.Name = "btnDeadLoop";
            this.btnDeadLoop.Size = new System.Drawing.Size(99, 32);
            this.btnDeadLoop.TabIndex = 14;
            this.btnDeadLoop.Text = "循环死号";
            this.btnDeadLoop.UseVisualStyleBackColor = true;
            this.btnDeadLoop.Click += new System.EventHandler(this.btnDeadLoop_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioBtnB);
            this.groupBox1.Controls.Add(this.radioBtnC);
            this.groupBox1.Location = new System.Drawing.Point(8, 267);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(226, 63);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "IP切换规则";
            // 
            // radioBtnB
            // 
            this.radioBtnB.AutoSize = true;
            this.radioBtnB.Location = new System.Drawing.Point(21, 28);
            this.radioBtnB.Name = "radioBtnB";
            this.radioBtnB.Size = new System.Drawing.Size(65, 16);
            this.radioBtnB.TabIndex = 11;
            this.radioBtnB.Tag = "2";
            this.radioBtnB.Text = "B段地址";
            this.radioBtnB.UseVisualStyleBackColor = true;
            // 
            // radioBtnC
            // 
            this.radioBtnC.AutoSize = true;
            this.radioBtnC.Checked = true;
            this.radioBtnC.Location = new System.Drawing.Point(136, 28);
            this.radioBtnC.Name = "radioBtnC";
            this.radioBtnC.Size = new System.Drawing.Size(65, 16);
            this.radioBtnC.TabIndex = 10;
            this.radioBtnC.TabStop = true;
            this.radioBtnC.Tag = "3";
            this.radioBtnC.Text = "C段地址";
            this.radioBtnC.UseVisualStyleBackColor = true;
            // 
            // btnClearFailed
            // 
            this.btnClearFailed.Location = new System.Drawing.Point(125, 141);
            this.btnClearFailed.Name = "btnClearFailed";
            this.btnClearFailed.Size = new System.Drawing.Size(99, 32);
            this.btnClearFailed.TabIndex = 8;
            this.btnClearFailed.Text = "清理失败";
            this.btnClearFailed.UseVisualStyleBackColor = true;
            this.btnClearFailed.Click += new System.EventHandler(this.btnClearFailed_Click);
            // 
            // btnClearAll
            // 
            this.btnClearAll.Location = new System.Drawing.Point(14, 180);
            this.btnClearAll.Name = "btnClearAll";
            this.btnClearAll.Size = new System.Drawing.Size(99, 32);
            this.btnClearAll.TabIndex = 7;
            this.btnClearAll.Text = "清理所有";
            this.btnClearAll.UseVisualStyleBackColor = true;
            this.btnClearAll.Click += new System.EventHandler(this.btnClearAll_Click);
            // 
            // btnReload
            // 
            this.btnReload.Location = new System.Drawing.Point(14, 138);
            this.btnReload.Name = "btnReload";
            this.btnReload.Size = new System.Drawing.Size(99, 32);
            this.btnReload.TabIndex = 6;
            this.btnReload.Text = "重新加载";
            this.btnReload.UseVisualStyleBackColor = true;
            this.btnReload.Click += new System.EventHandler(this.btnReload_Click);
            // 
            // chUseVPN
            // 
            this.chUseVPN.AutoSize = true;
            this.chUseVPN.Location = new System.Drawing.Point(22, 40);
            this.chUseVPN.Name = "chUseVPN";
            this.chUseVPN.Size = new System.Drawing.Size(66, 16);
            this.chUseVPN.TabIndex = 4;
            this.chUseVPN.Text = "使用VPN";
            this.chUseVPN.UseVisualStyleBackColor = true;
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
            this.tabPage2.Controls.Add(this.btnReleaseAllPhones);
            this.tabPage2.Controls.Add(this.btnAddIgnore);
            this.tabPage2.Controls.Add(this.btnGetMessage);
            this.tabPage2.Controls.Add(this.btnGetPhoneNumber);
            this.tabPage2.Controls.Add(this.btnExportDead);
            this.tabPage2.Controls.Add(this.button8);
            this.tabPage2.Controls.Add(this.button7);
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
            // btnReleaseAllPhones
            // 
            this.btnReleaseAllPhones.Location = new System.Drawing.Point(129, 308);
            this.btnReleaseAllPhones.Name = "btnReleaseAllPhones";
            this.btnReleaseAllPhones.Size = new System.Drawing.Size(99, 32);
            this.btnReleaseAllPhones.TabIndex = 12;
            this.btnReleaseAllPhones.Text = "释放所有电话号码";
            this.btnReleaseAllPhones.UseVisualStyleBackColor = true;
            this.btnReleaseAllPhones.Click += new System.EventHandler(this.btnReleaseAllPhones_Click);
            // 
            // btnAddIgnore
            // 
            this.btnAddIgnore.Location = new System.Drawing.Point(129, 265);
            this.btnAddIgnore.Name = "btnAddIgnore";
            this.btnAddIgnore.Size = new System.Drawing.Size(99, 32);
            this.btnAddIgnore.TabIndex = 11;
            this.btnAddIgnore.Text = "加黑当前电话号码";
            this.btnAddIgnore.UseVisualStyleBackColor = true;
            this.btnAddIgnore.Click += new System.EventHandler(this.btnAddIgnore_Click);
            // 
            // btnGetMessage
            // 
            this.btnGetMessage.Location = new System.Drawing.Point(8, 308);
            this.btnGetMessage.Name = "btnGetMessage";
            this.btnGetMessage.Size = new System.Drawing.Size(99, 32);
            this.btnGetMessage.TabIndex = 10;
            this.btnGetMessage.Text = "获取短信验证码";
            this.btnGetMessage.UseVisualStyleBackColor = true;
            this.btnGetMessage.Click += new System.EventHandler(this.btnGetMessage_Click);
            // 
            // btnGetPhoneNumber
            // 
            this.btnGetPhoneNumber.Location = new System.Drawing.Point(8, 267);
            this.btnGetPhoneNumber.Name = "btnGetPhoneNumber";
            this.btnGetPhoneNumber.Size = new System.Drawing.Size(99, 32);
            this.btnGetPhoneNumber.TabIndex = 9;
            this.btnGetPhoneNumber.Text = "获取电话号码";
            this.btnGetPhoneNumber.UseVisualStyleBackColor = true;
            this.btnGetPhoneNumber.Click += new System.EventHandler(this.btnGetPhoneNumber_Click);
            // 
            // btnExportDead
            // 
            this.btnExportDead.Location = new System.Drawing.Point(129, 176);
            this.btnExportDead.Name = "btnExportDead";
            this.btnExportDead.Size = new System.Drawing.Size(99, 32);
            this.btnExportDead.TabIndex = 8;
            this.btnExportDead.Text = "导出死号";
            this.btnExportDead.UseVisualStyleBackColor = true;
            this.btnExportDead.Click += new System.EventHandler(this.btnExportDead_Click);
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(8, 124);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(99, 32);
            this.button8.TabIndex = 7;
            this.button8.Text = "导出IP段";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button6_Click);
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(129, 124);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(99, 32);
            this.button7.TabIndex = 6;
            this.button7.Text = "获取随机账号";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click_1);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(8, 72);
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
            this.btnLoginSMS.Location = new System.Drawing.Point(8, 224);
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
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.button9);
            this.tabPage3.Controls.Add(this.btnTestNetwork);
            this.tabPage3.Controls.Add(this.btnVPNState);
            this.tabPage3.Controls.Add(this.btnDisconnect);
            this.tabPage3.Controls.Add(this.btnLogin);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(247, 346);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "VPN";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // btnVPNState
            // 
            this.btnVPNState.Location = new System.Drawing.Point(134, 290);
            this.btnVPNState.Name = "btnVPNState";
            this.btnVPNState.Size = new System.Drawing.Size(99, 32);
            this.btnVPNState.TabIndex = 8;
            this.btnVPNState.Text = "当前状态（记事本）";
            this.btnVPNState.UseVisualStyleBackColor = true;
            this.btnVPNState.Click += new System.EventHandler(this.btnVPNState_Click);
            // 
            // btnDisconnect
            // 
            this.btnDisconnect.Location = new System.Drawing.Point(134, 61);
            this.btnDisconnect.Name = "btnDisconnect";
            this.btnDisconnect.Size = new System.Drawing.Size(99, 32);
            this.btnDisconnect.TabIndex = 7;
            this.btnDisconnect.Text = "断开";
            this.btnDisconnect.UseVisualStyleBackColor = true;
            this.btnDisconnect.Click += new System.EventHandler(this.btnDisconnect_Click);
            // 
            // btnLogin
            // 
            this.btnLogin.Location = new System.Drawing.Point(18, 61);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(99, 32);
            this.btnLogin.TabIndex = 6;
            this.btnLogin.Text = "登录";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
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
            // btnTestNetwork
            // 
            this.btnTestNetwork.Location = new System.Drawing.Point(18, 182);
            this.btnTestNetwork.Name = "btnTestNetwork";
            this.btnTestNetwork.Size = new System.Drawing.Size(99, 32);
            this.btnTestNetwork.TabIndex = 9;
            this.btnTestNetwork.Text = "测试网速";
            this.btnTestNetwork.UseVisualStyleBackColor = true;
            this.btnTestNetwork.Click += new System.EventHandler(this.btnTestNetwork_Click);
            // 
            // button9
            // 
            this.button9.Location = new System.Drawing.Point(18, 122);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(99, 32);
            this.button9.TabIndex = 10;
            this.button9.Text = "当前状态";
            this.button9.UseVisualStyleBackColor = true;
            this.button9.Click += new System.EventHandler(this.button9_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(543, 571);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.statusStrip1);
            this.Location = new System.Drawing.Point(800, 0);
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
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
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
        private System.Windows.Forms.CheckBox chUseVPN;
        private System.Windows.Forms.Label labelTotalTime;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button btnClearFailed;
        private System.Windows.Forms.Button btnClearAll;
        private System.Windows.Forms.Button btnReload;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radioBtnB;
        private System.Windows.Forms.RadioButton radioBtnC;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Button btnDeadLoop;
        private System.Windows.Forms.Button btnExportDead;
        private System.Windows.Forms.CheckBox chHalfAuto;
        private System.Windows.Forms.Button btnHalfFinish;
        private System.Windows.Forms.Button btnAddIgnore;
        private System.Windows.Forms.Button btnGetMessage;
        private System.Windows.Forms.Button btnGetPhoneNumber;
        private System.Windows.Forms.Button btnReleaseAllPhones;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Button btnDisconnect;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Button btnVPNState;
        private System.Windows.Forms.Button btnTestNetwork;
        private System.Windows.Forms.Button button9;
    }
}

