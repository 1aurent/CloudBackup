namespace CloudBackup.Manager
{
    partial class Manager
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabInfos = new System.Windows.Forms.TabControl();
            this.tabPageArchive = new System.Windows.Forms.TabPage();
            this.spltCtrlArchive = new System.Windows.Forms.SplitContainer();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btnNewArchive = new System.Windows.Forms.Button();
            this.btnDelArchive = new System.Windows.Forms.Button();
            this.lbAllSchedule = new System.Windows.Forms.ListBox();
            this.btnResetStatus = new System.Windows.Forms.Button();
            this.btnRunNow = new System.Windows.Forms.Button();
            this.btnDelSchedule = new System.Windows.Forms.Button();
            this.btnNewSchedule = new System.Windows.Forms.Button();
            this.cbAllSchedules = new System.Windows.Forms.ComboBox();
            this.btnSaveSchedule = new System.Windows.Forms.Button();
            this.tbArchiveJobName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbRootFolder = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panelSchedule = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.rdSchedDaily = new System.Windows.Forms.RadioButton();
            this.rdSchedWeekly = new System.Windows.Forms.RadioButton();
            this.rdSchedMonthly = new System.Windows.Forms.RadioButton();
            this.tabPageConfig = new System.Windows.Forms.TabPage();
            this.btnApplyChanges = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtSshUsername = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.cbIsGlacier = new System.Windows.Forms.CheckBox();
            this.txtSshRootPath = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtSshPwd = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtSshHost = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cbDisplayPasswords = new System.Windows.Forms.CheckBox();
            this.txtMasterPassword = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.label11 = new System.Windows.Forms.Label();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.label14 = new System.Windows.Forms.Label();
            this.textBox7 = new System.Windows.Forms.TextBox();
            this.textBox8 = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.textBox9 = new System.Windows.Forms.TextBox();
            this.textBox10 = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.tabInfos.SuspendLayout();
            this.tabPageArchive.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spltCtrlArchive)).BeginInit();
            this.spltCtrlArchive.Panel1.SuspendLayout();
            this.spltCtrlArchive.Panel2.SuspendLayout();
            this.spltCtrlArchive.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.tabPageConfig.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabInfos
            // 
            this.tabInfos.Controls.Add(this.tabPageArchive);
            this.tabInfos.Controls.Add(this.tabPageConfig);
            this.tabInfos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabInfos.Location = new System.Drawing.Point(5, 5);
            this.tabInfos.Name = "tabInfos";
            this.tabInfos.SelectedIndex = 0;
            this.tabInfos.Size = new System.Drawing.Size(624, 543);
            this.tabInfos.TabIndex = 0;
            // 
            // tabPageArchive
            // 
            this.tabPageArchive.Controls.Add(this.spltCtrlArchive);
            this.tabPageArchive.Location = new System.Drawing.Point(4, 22);
            this.tabPageArchive.Name = "tabPageArchive";
            this.tabPageArchive.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageArchive.Size = new System.Drawing.Size(616, 517);
            this.tabPageArchive.TabIndex = 0;
            this.tabPageArchive.Text = "Archive Jobs";
            this.tabPageArchive.UseVisualStyleBackColor = true;
            // 
            // spltCtrlArchive
            // 
            this.spltCtrlArchive.BackColor = System.Drawing.Color.Transparent;
            this.spltCtrlArchive.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spltCtrlArchive.Location = new System.Drawing.Point(3, 3);
            this.spltCtrlArchive.Name = "spltCtrlArchive";
            // 
            // spltCtrlArchive.Panel1
            // 
            this.spltCtrlArchive.Panel1.Controls.Add(this.tableLayoutPanel1);
            this.spltCtrlArchive.Panel1.Controls.Add(this.lbAllSchedule);
            this.spltCtrlArchive.Panel1MinSize = 167;
            // 
            // spltCtrlArchive.Panel2
            // 
            this.spltCtrlArchive.Panel2.Controls.Add(this.tabControl1);
            this.spltCtrlArchive.Panel2.Controls.Add(this.btnResetStatus);
            this.spltCtrlArchive.Panel2.Controls.Add(this.btnRunNow);
            this.spltCtrlArchive.Panel2.Controls.Add(this.btnSaveSchedule);
            this.spltCtrlArchive.Panel2.Controls.Add(this.tbArchiveJobName);
            this.spltCtrlArchive.Panel2.Controls.Add(this.label3);
            this.spltCtrlArchive.Panel2.Controls.Add(this.tbRootFolder);
            this.spltCtrlArchive.Panel2.Controls.Add(this.label2);
            this.spltCtrlArchive.Size = new System.Drawing.Size(610, 511);
            this.spltCtrlArchive.SplitterDistance = 224;
            this.spltCtrlArchive.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.btnNewArchive, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnDelArchive, 1, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(5, 485);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(216, 26);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // btnNewArchive
            // 
            this.btnNewArchive.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNewArchive.Location = new System.Drawing.Point(3, 3);
            this.btnNewArchive.Name = "btnNewArchive";
            this.btnNewArchive.Size = new System.Drawing.Size(102, 20);
            this.btnNewArchive.TabIndex = 0;
            this.btnNewArchive.Text = "New";
            this.btnNewArchive.UseVisualStyleBackColor = true;
            this.btnNewArchive.Click += new System.EventHandler(this.btnNewArchive_Click);
            // 
            // btnDelArchive
            // 
            this.btnDelArchive.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDelArchive.Enabled = false;
            this.btnDelArchive.Location = new System.Drawing.Point(111, 3);
            this.btnDelArchive.Name = "btnDelArchive";
            this.btnDelArchive.Size = new System.Drawing.Size(102, 20);
            this.btnDelArchive.TabIndex = 1;
            this.btnDelArchive.Text = "Delete";
            this.btnDelArchive.UseVisualStyleBackColor = true;
            this.btnDelArchive.Click += new System.EventHandler(this.btnDelArchive_Click);
            // 
            // lbAllSchedule
            // 
            this.lbAllSchedule.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbAllSchedule.FormattingEnabled = true;
            this.lbAllSchedule.IntegralHeight = false;
            this.lbAllSchedule.Location = new System.Drawing.Point(5, 2);
            this.lbAllSchedule.Name = "lbAllSchedule";
            this.lbAllSchedule.Size = new System.Drawing.Size(216, 475);
            this.lbAllSchedule.TabIndex = 0;
            this.lbAllSchedule.SelectedIndexChanged += new System.EventHandler(this.lbAllSchedule_SelectedIndexChanged);
            // 
            // btnResetStatus
            // 
            this.btnResetStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnResetStatus.Location = new System.Drawing.Point(86, 485);
            this.btnResetStatus.Name = "btnResetStatus";
            this.btnResetStatus.Size = new System.Drawing.Size(83, 23);
            this.btnResetStatus.TabIndex = 13;
            this.btnResetStatus.Text = "Reset Status";
            this.btnResetStatus.UseVisualStyleBackColor = true;
            this.btnResetStatus.Click += new System.EventHandler(this.btnResetStatus_Click);
            // 
            // btnRunNow
            // 
            this.btnRunNow.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnRunNow.Location = new System.Drawing.Point(5, 485);
            this.btnRunNow.Name = "btnRunNow";
            this.btnRunNow.Size = new System.Drawing.Size(75, 23);
            this.btnRunNow.TabIndex = 12;
            this.btnRunNow.Text = "Run Now";
            this.btnRunNow.UseVisualStyleBackColor = true;
            this.btnRunNow.Click += new System.EventHandler(this.btnRunNow_Click);
            // 
            // btnDelSchedule
            // 
            this.btnDelSchedule.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDelSchedule.Location = new System.Drawing.Point(300, 4);
            this.btnDelSchedule.Name = "btnDelSchedule";
            this.btnDelSchedule.Size = new System.Drawing.Size(62, 23);
            this.btnDelSchedule.TabIndex = 11;
            this.btnDelSchedule.Text = "Remove";
            this.btnDelSchedule.UseVisualStyleBackColor = true;
            this.btnDelSchedule.Click += new System.EventHandler(this.btnDelSchedule_Click);
            // 
            // btnNewSchedule
            // 
            this.btnNewSchedule.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNewSchedule.Location = new System.Drawing.Point(252, 4);
            this.btnNewSchedule.Name = "btnNewSchedule";
            this.btnNewSchedule.Size = new System.Drawing.Size(42, 23);
            this.btnNewSchedule.TabIndex = 10;
            this.btnNewSchedule.Text = "Add";
            this.btnNewSchedule.UseVisualStyleBackColor = true;
            this.btnNewSchedule.Click += new System.EventHandler(this.btnNewSchedule_Click);
            // 
            // cbAllSchedules
            // 
            this.cbAllSchedules.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbAllSchedules.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbAllSchedules.FormattingEnabled = true;
            this.cbAllSchedules.Location = new System.Drawing.Point(10, 6);
            this.cbAllSchedules.Name = "cbAllSchedules";
            this.cbAllSchedules.Size = new System.Drawing.Size(236, 21);
            this.cbAllSchedules.TabIndex = 8;
            this.cbAllSchedules.SelectedIndexChanged += new System.EventHandler(this.cbAllSchedules_SelectedIndexChanged);
            // 
            // btnSaveSchedule
            // 
            this.btnSaveSchedule.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaveSchedule.Location = new System.Drawing.Point(302, 485);
            this.btnSaveSchedule.Name = "btnSaveSchedule";
            this.btnSaveSchedule.Size = new System.Drawing.Size(75, 23);
            this.btnSaveSchedule.TabIndex = 7;
            this.btnSaveSchedule.Text = "Save";
            this.btnSaveSchedule.UseVisualStyleBackColor = true;
            this.btnSaveSchedule.Click += new System.EventHandler(this.btnSaveSchedule_Click);
            // 
            // tbArchiveJobName
            // 
            this.tbArchiveJobName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbArchiveJobName.Location = new System.Drawing.Point(76, 11);
            this.tbArchiveJobName.Name = "tbArchiveJobName";
            this.tbArchiveJobName.Size = new System.Drawing.Size(301, 20);
            this.tbArchiveJobName.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(32, 14);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Name:";
            // 
            // tbRootFolder
            // 
            this.tbRootFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbRootFolder.Location = new System.Drawing.Point(76, 37);
            this.tbRootFolder.Name = "tbRootFolder";
            this.tbRootFolder.Size = new System.Drawing.Size(301, 20);
            this.tbRootFolder.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Root Folder:";
            // 
            // panelSchedule
            // 
            this.panelSchedule.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelSchedule.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelSchedule.Location = new System.Drawing.Point(9, 66);
            this.panelSchedule.MinimumSize = new System.Drawing.Size(306, 126);
            this.panelSchedule.Name = "panelSchedule";
            this.panelSchedule.Size = new System.Drawing.Size(353, 126);
            this.panelSchedule.TabIndex = 2;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel1.Controls.Add(this.rdSchedDaily);
            this.flowLayoutPanel1.Controls.Add(this.rdSchedWeekly);
            this.flowLayoutPanel1.Controls.Add(this.rdSchedMonthly);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(10, 33);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(352, 27);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // rdSchedDaily
            // 
            this.rdSchedDaily.AutoSize = true;
            this.rdSchedDaily.Location = new System.Drawing.Point(3, 3);
            this.rdSchedDaily.Name = "rdSchedDaily";
            this.rdSchedDaily.Size = new System.Drawing.Size(48, 17);
            this.rdSchedDaily.TabIndex = 0;
            this.rdSchedDaily.Text = "Daily";
            this.rdSchedDaily.UseVisualStyleBackColor = true;
            this.rdSchedDaily.CheckedChanged += new System.EventHandler(this.rdSchedDaily_CheckedChanged);
            // 
            // rdSchedWeekly
            // 
            this.rdSchedWeekly.AutoSize = true;
            this.rdSchedWeekly.Checked = true;
            this.rdSchedWeekly.Location = new System.Drawing.Point(57, 3);
            this.rdSchedWeekly.Name = "rdSchedWeekly";
            this.rdSchedWeekly.Size = new System.Drawing.Size(61, 17);
            this.rdSchedWeekly.TabIndex = 1;
            this.rdSchedWeekly.TabStop = true;
            this.rdSchedWeekly.Text = "Weekly";
            this.rdSchedWeekly.UseVisualStyleBackColor = true;
            this.rdSchedWeekly.CheckedChanged += new System.EventHandler(this.rdSchedWeekly_CheckedChanged);
            // 
            // rdSchedMonthly
            // 
            this.rdSchedMonthly.AutoSize = true;
            this.rdSchedMonthly.Location = new System.Drawing.Point(124, 3);
            this.rdSchedMonthly.Name = "rdSchedMonthly";
            this.rdSchedMonthly.Size = new System.Drawing.Size(62, 17);
            this.rdSchedMonthly.TabIndex = 2;
            this.rdSchedMonthly.Text = "Monthly";
            this.rdSchedMonthly.UseVisualStyleBackColor = true;
            this.rdSchedMonthly.CheckedChanged += new System.EventHandler(this.rdSchedMonthly_CheckedChanged);
            // 
            // tabPageConfig
            // 
            this.tabPageConfig.Controls.Add(this.btnApplyChanges);
            this.tabPageConfig.Controls.Add(this.groupBox1);
            this.tabPageConfig.Controls.Add(this.cbDisplayPasswords);
            this.tabPageConfig.Controls.Add(this.txtMasterPassword);
            this.tabPageConfig.Controls.Add(this.label4);
            this.tabPageConfig.Location = new System.Drawing.Point(4, 22);
            this.tabPageConfig.Name = "tabPageConfig";
            this.tabPageConfig.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageConfig.Size = new System.Drawing.Size(751, 479);
            this.tabPageConfig.TabIndex = 1;
            this.tabPageConfig.Text = "Configuration";
            this.tabPageConfig.UseVisualStyleBackColor = true;
            // 
            // btnApplyChanges
            // 
            this.btnApplyChanges.Location = new System.Drawing.Point(393, 230);
            this.btnApplyChanges.Name = "btnApplyChanges";
            this.btnApplyChanges.Size = new System.Drawing.Size(113, 23);
            this.btnApplyChanges.TabIndex = 6;
            this.btnApplyChanges.Text = "Apply Changes";
            this.btnApplyChanges.UseVisualStyleBackColor = true;
            this.btnApplyChanges.Click += new System.EventHandler(this.btnApplyChanges_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtSshUsername);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.cbIsGlacier);
            this.groupBox1.Controls.Add(this.txtSshRootPath);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.txtSshPwd);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.txtSshHost);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Location = new System.Drawing.Point(46, 61);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(412, 163);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Cloud Backup Server";
            // 
            // txtSshUsername
            // 
            this.txtSshUsername.Location = new System.Drawing.Point(107, 53);
            this.txtSshUsername.Name = "txtSshUsername";
            this.txtSshUsername.Size = new System.Drawing.Size(299, 20);
            this.txtSshUsername.TabIndex = 9;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(15, 56);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(86, 13);
            this.label8.TabIndex = 8;
            this.label8.Text = "SSH User name:";
            // 
            // cbIsGlacier
            // 
            this.cbIsGlacier.AutoSize = true;
            this.cbIsGlacier.Location = new System.Drawing.Point(107, 131);
            this.cbIsGlacier.Name = "cbIsGlacier";
            this.cbIsGlacier.Size = new System.Drawing.Size(178, 17);
            this.cbIsGlacier.TabIndex = 5;
            this.cbIsGlacier.Text = "Check if the target is a \"Glacier\"";
            this.cbIsGlacier.UseVisualStyleBackColor = true;
            // 
            // txtSshRootPath
            // 
            this.txtSshRootPath.Location = new System.Drawing.Point(107, 105);
            this.txtSshRootPath.Name = "txtSshRootPath";
            this.txtSshRootPath.Size = new System.Drawing.Size(299, 20);
            this.txtSshRootPath.TabIndex = 4;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(18, 108);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(83, 13);
            this.label7.TabIndex = 7;
            this.label7.Text = "SSH Root Path:";
            // 
            // txtSshPwd
            // 
            this.txtSshPwd.Location = new System.Drawing.Point(107, 79);
            this.txtSshPwd.Name = "txtSshPwd";
            this.txtSshPwd.Size = new System.Drawing.Size(299, 20);
            this.txtSshPwd.TabIndex = 3;
            this.txtSshPwd.UseSystemPasswordChar = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(20, 82);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(81, 13);
            this.label6.TabIndex = 5;
            this.label6.Text = "SSH Password:";
            // 
            // txtSshHost
            // 
            this.txtSshHost.Location = new System.Drawing.Point(107, 27);
            this.txtSshHost.Name = "txtSshHost";
            this.txtSshHost.Size = new System.Drawing.Size(299, 20);
            this.txtSshHost.TabIndex = 2;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(46, 30);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(55, 13);
            this.label5.TabIndex = 3;
            this.label5.Text = "SSH host:";
            // 
            // cbDisplayPasswords
            // 
            this.cbDisplayPasswords.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cbDisplayPasswords.AutoSize = true;
            this.cbDisplayPasswords.Location = new System.Drawing.Point(628, 454);
            this.cbDisplayPasswords.Name = "cbDisplayPasswords";
            this.cbDisplayPasswords.Size = new System.Drawing.Size(113, 17);
            this.cbDisplayPasswords.TabIndex = 7;
            this.cbDisplayPasswords.Text = "Display passwords";
            this.cbDisplayPasswords.UseVisualStyleBackColor = true;
            this.cbDisplayPasswords.CheckedChanged += new System.EventHandler(this.cbDisplayPasswords_CheckedChanged);
            // 
            // txtMasterPassword
            // 
            this.txtMasterPassword.Location = new System.Drawing.Point(153, 18);
            this.txtMasterPassword.Name = "txtMasterPassword";
            this.txtMasterPassword.Size = new System.Drawing.Size(260, 20);
            this.txtMasterPassword.TabIndex = 1;
            this.txtMasterPassword.UseSystemPasswordChar = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(56, 21);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(91, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Master Password:";
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "SFTP",
            "SCP",
            "FTP"});
            this.comboBox1.Location = new System.Drawing.Point(10, 19);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(52, 21);
            this.comboBox1.TabIndex = 0;
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.Location = new System.Drawing.Point(68, 19);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(230, 20);
            this.textBox1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(66, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Host:";
            // 
            // textBox2
            // 
            this.textBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox2.Location = new System.Drawing.Point(304, 19);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(58, 20);
            this.textBox2.TabIndex = 3;
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(304, 3);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(29, 13);
            this.label9.TabIndex = 4;
            this.label9.Text = "Port:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(31, 48);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(32, 13);
            this.label10.TabIndex = 5;
            this.label10.Text = "Path:";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBox3
            // 
            this.textBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox3.Location = new System.Drawing.Point(69, 45);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(293, 20);
            this.textBox3.TabIndex = 6;
            // 
            // checkBox1
            // 
            this.checkBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(248, 367);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(114, 17);
            this.checkBox1.TabIndex = 16;
            this.checkBox1.Text = "Display Passwords";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(4, 74);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(63, 13);
            this.label11.TabIndex = 7;
            this.label11.Text = "User Name:";
            // 
            // textBox4
            // 
            this.textBox4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox4.Location = new System.Drawing.Point(69, 71);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(293, 20);
            this.textBox4.TabIndex = 8;
            // 
            // textBox5
            // 
            this.textBox5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox5.Location = new System.Drawing.Point(69, 97);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(293, 20);
            this.textBox5.TabIndex = 9;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(7, 100);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(56, 13);
            this.label12.TabIndex = 10;
            this.label12.Text = "Password:";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(7, 126);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(76, 13);
            this.label13.TabIndex = 17;
            this.label13.Text = "ZIP Password:";
            // 
            // textBox6
            // 
            this.textBox6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox6.Location = new System.Drawing.Point(88, 123);
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new System.Drawing.Size(274, 20);
            this.textBox6.TabIndex = 18;
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(3, 63);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(376, 416);
            this.tabControl1.TabIndex = 19;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.cbAllSchedules);
            this.tabPage1.Controls.Add(this.btnNewSchedule);
            this.tabPage1.Controls.Add(this.btnDelSchedule);
            this.tabPage1.Controls.Add(this.panelSchedule);
            this.tabPage1.Controls.Add(this.flowLayoutPanel1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(368, 353);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Schedule";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.label16);
            this.tabPage2.Controls.Add(this.textBox9);
            this.tabPage2.Controls.Add(this.textBox10);
            this.tabPage2.Controls.Add(this.label17);
            this.tabPage2.Controls.Add(this.label14);
            this.tabPage2.Controls.Add(this.textBox7);
            this.tabPage2.Controls.Add(this.textBox8);
            this.tabPage2.Controls.Add(this.label15);
            this.tabPage2.Controls.Add(this.checkBox2);
            this.tabPage2.Controls.Add(this.label12);
            this.tabPage2.Controls.Add(this.checkBox1);
            this.tabPage2.Controls.Add(this.textBox6);
            this.tabPage2.Controls.Add(this.label13);
            this.tabPage2.Controls.Add(this.textBox5);
            this.tabPage2.Controls.Add(this.label1);
            this.tabPage2.Controls.Add(this.textBox4);
            this.tabPage2.Controls.Add(this.comboBox1);
            this.tabPage2.Controls.Add(this.label11);
            this.tabPage2.Controls.Add(this.textBox1);
            this.tabPage2.Controls.Add(this.textBox3);
            this.tabPage2.Controls.Add(this.textBox2);
            this.tabPage2.Controls.Add(this.label10);
            this.tabPage2.Controls.Add(this.label9);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(368, 390);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Target";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(10, 165);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(148, 17);
            this.checkBox2.TabIndex = 19;
            this.checkBox2.Text = "Connect via a SSH proxy:";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(7, 191);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(32, 13);
            this.label14.TabIndex = 21;
            this.label14.Text = "Host:";
            // 
            // textBox7
            // 
            this.textBox7.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox7.Location = new System.Drawing.Point(45, 188);
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new System.Drawing.Size(253, 20);
            this.textBox7.TabIndex = 20;
            // 
            // textBox8
            // 
            this.textBox8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox8.Location = new System.Drawing.Point(304, 188);
            this.textBox8.Name = "textBox8";
            this.textBox8.Size = new System.Drawing.Size(58, 20);
            this.textBox8.TabIndex = 22;
            // 
            // label15
            // 
            this.label15.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(304, 172);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(29, 13);
            this.label15.TabIndex = 23;
            this.label15.Text = "Port:";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(7, 243);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(56, 13);
            this.label16.TabIndex = 27;
            this.label16.Text = "Password:";
            // 
            // textBox9
            // 
            this.textBox9.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox9.Location = new System.Drawing.Point(69, 240);
            this.textBox9.Name = "textBox9";
            this.textBox9.Size = new System.Drawing.Size(293, 20);
            this.textBox9.TabIndex = 26;
            // 
            // textBox10
            // 
            this.textBox10.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox10.Location = new System.Drawing.Point(69, 214);
            this.textBox10.Name = "textBox10";
            this.textBox10.Size = new System.Drawing.Size(293, 20);
            this.textBox10.TabIndex = 25;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(4, 217);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(63, 13);
            this.label17.TabIndex = 24;
            this.label17.Text = "User Name:";
            // 
            // Manager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(634, 553);
            this.Controls.Add(this.tabInfos);
            this.MinimumSize = new System.Drawing.Size(650, 560);
            this.Name = "Manager";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.Text = "Laurent Dupuis\'s CloudBackup Manager";
            this.tabInfos.ResumeLayout(false);
            this.tabPageArchive.ResumeLayout(false);
            this.spltCtrlArchive.Panel1.ResumeLayout(false);
            this.spltCtrlArchive.Panel2.ResumeLayout(false);
            this.spltCtrlArchive.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spltCtrlArchive)).EndInit();
            this.spltCtrlArchive.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.tabPageConfig.ResumeLayout(false);
            this.tabPageConfig.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabInfos;
        private System.Windows.Forms.TabPage tabPageArchive;
        private System.Windows.Forms.SplitContainer spltCtrlArchive;
        private System.Windows.Forms.ListBox lbAllSchedule;
        private System.Windows.Forms.TabPage tabPageConfig;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button btnNewArchive;
        private System.Windows.Forms.Button btnDelArchive;
        private System.Windows.Forms.FlowLayoutPanel panelSchedule;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.RadioButton rdSchedDaily;
        private System.Windows.Forms.RadioButton rdSchedWeekly;
        private System.Windows.Forms.RadioButton rdSchedMonthly;
        private System.Windows.Forms.TextBox tbArchiveJobName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbRootFolder;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox cbDisplayPasswords;
        private System.Windows.Forms.TextBox txtMasterPassword;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnSaveSchedule;
        private System.Windows.Forms.ComboBox cbAllSchedules;
        private System.Windows.Forms.Button btnDelSchedule;
        private System.Windows.Forms.Button btnNewSchedule;
        private System.Windows.Forms.Button btnRunNow;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtSshHost;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtSshRootPath;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtSshPwd;
        private System.Windows.Forms.CheckBox cbIsGlacier;
        private System.Windows.Forms.Button btnApplyChanges;
        private System.Windows.Forms.TextBox txtSshUsername;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnResetStatus;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox textBox9;
        private System.Windows.Forms.TextBox textBox10;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox textBox7;
        private System.Windows.Forms.TextBox textBox8;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.CheckBox checkBox2;
    }
}

