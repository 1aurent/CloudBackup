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
            this.tabJobSettings = new System.Windows.Forms.TabControl();
            this.tabPageJobSchedule = new System.Windows.Forms.TabPage();
            this.cbForceFullbackup = new System.Windows.Forms.CheckBox();
            this.cbAllSchedules = new System.Windows.Forms.ComboBox();
            this.btnNewSchedule = new System.Windows.Forms.Button();
            this.btnDelSchedule = new System.Windows.Forms.Button();
            this.panelSchedule = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.rdSchedDaily = new System.Windows.Forms.RadioButton();
            this.rdSchedWeekly = new System.Windows.Forms.RadioButton();
            this.rdSchedMonthly = new System.Windows.Forms.RadioButton();
            this.tabPageJobTarget = new System.Windows.Forms.TabPage();
            this.cbTargetSelfclean = new System.Windows.Forms.CheckBox();
            this.label16 = new System.Windows.Forms.Label();
            this.txtSshProxyPwd = new System.Windows.Forms.TextBox();
            this.txtSshProxyUser = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.txtSshProxyHost = new System.Windows.Forms.TextBox();
            this.txtSshProxyPort = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.cbUseSshProxy = new System.Windows.Forms.CheckBox();
            this.label12 = new System.Windows.Forms.Label();
            this.cbTargetShowPass = new System.Windows.Forms.CheckBox();
            this.txtTargetZipPwd = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txtTargetPwd = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtTargetUser = new System.Windows.Forms.TextBox();
            this.cbTargetProtocol = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtTargetHost = new System.Windows.Forms.TextBox();
            this.txtTargetPath = new System.Windows.Forms.TextBox();
            this.txtTargetPort = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.btnResetStatus = new System.Windows.Forms.Button();
            this.btnRunNow = new System.Windows.Forms.Button();
            this.btnSaveSchedule = new System.Windows.Forms.Button();
            this.tbArchiveJobName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbRootFolder = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tabPageAbout = new System.Windows.Forms.TabPage();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.rtbLicence = new System.Windows.Forms.RichTextBox();
            this.tabInfos.SuspendLayout();
            this.tabPageArchive.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spltCtrlArchive)).BeginInit();
            this.spltCtrlArchive.Panel1.SuspendLayout();
            this.spltCtrlArchive.Panel2.SuspendLayout();
            this.spltCtrlArchive.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tabJobSettings.SuspendLayout();
            this.tabPageJobSchedule.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.tabPageJobTarget.SuspendLayout();
            this.tabPageAbout.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabInfos
            // 
            this.tabInfos.Controls.Add(this.tabPageArchive);
            this.tabInfos.Controls.Add(this.tabPageAbout);
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
            this.spltCtrlArchive.Panel2.Controls.Add(this.tabJobSettings);
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
            // tabJobSettings
            // 
            this.tabJobSettings.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabJobSettings.Controls.Add(this.tabPageJobSchedule);
            this.tabJobSettings.Controls.Add(this.tabPageJobTarget);
            this.tabJobSettings.Location = new System.Drawing.Point(3, 63);
            this.tabJobSettings.Name = "tabJobSettings";
            this.tabJobSettings.SelectedIndex = 0;
            this.tabJobSettings.Size = new System.Drawing.Size(376, 416);
            this.tabJobSettings.TabIndex = 19;
            // 
            // tabPageJobSchedule
            // 
            this.tabPageJobSchedule.Controls.Add(this.cbForceFullbackup);
            this.tabPageJobSchedule.Controls.Add(this.cbAllSchedules);
            this.tabPageJobSchedule.Controls.Add(this.btnNewSchedule);
            this.tabPageJobSchedule.Controls.Add(this.btnDelSchedule);
            this.tabPageJobSchedule.Controls.Add(this.panelSchedule);
            this.tabPageJobSchedule.Controls.Add(this.flowLayoutPanel1);
            this.tabPageJobSchedule.Location = new System.Drawing.Point(4, 22);
            this.tabPageJobSchedule.Name = "tabPageJobSchedule";
            this.tabPageJobSchedule.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageJobSchedule.Size = new System.Drawing.Size(368, 390);
            this.tabPageJobSchedule.TabIndex = 0;
            this.tabPageJobSchedule.Text = "Schedule";
            this.tabPageJobSchedule.UseVisualStyleBackColor = true;
            // 
            // cbForceFullbackup
            // 
            this.cbForceFullbackup.AutoSize = true;
            this.cbForceFullbackup.Location = new System.Drawing.Point(13, 198);
            this.cbForceFullbackup.Name = "cbForceFullbackup";
            this.cbForceFullbackup.Size = new System.Drawing.Size(245, 17);
            this.cbForceFullbackup.TabIndex = 12;
            this.cbForceFullbackup.Text = "Force full back-up instead of an increment one";
            this.cbForceFullbackup.UseVisualStyleBackColor = true;
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
            // tabPageJobTarget
            // 
            this.tabPageJobTarget.Controls.Add(this.cbTargetSelfclean);
            this.tabPageJobTarget.Controls.Add(this.label16);
            this.tabPageJobTarget.Controls.Add(this.txtSshProxyPwd);
            this.tabPageJobTarget.Controls.Add(this.txtSshProxyUser);
            this.tabPageJobTarget.Controls.Add(this.label17);
            this.tabPageJobTarget.Controls.Add(this.label14);
            this.tabPageJobTarget.Controls.Add(this.txtSshProxyHost);
            this.tabPageJobTarget.Controls.Add(this.txtSshProxyPort);
            this.tabPageJobTarget.Controls.Add(this.label15);
            this.tabPageJobTarget.Controls.Add(this.cbUseSshProxy);
            this.tabPageJobTarget.Controls.Add(this.label12);
            this.tabPageJobTarget.Controls.Add(this.cbTargetShowPass);
            this.tabPageJobTarget.Controls.Add(this.txtTargetZipPwd);
            this.tabPageJobTarget.Controls.Add(this.label13);
            this.tabPageJobTarget.Controls.Add(this.txtTargetPwd);
            this.tabPageJobTarget.Controls.Add(this.label1);
            this.tabPageJobTarget.Controls.Add(this.txtTargetUser);
            this.tabPageJobTarget.Controls.Add(this.cbTargetProtocol);
            this.tabPageJobTarget.Controls.Add(this.label11);
            this.tabPageJobTarget.Controls.Add(this.txtTargetHost);
            this.tabPageJobTarget.Controls.Add(this.txtTargetPath);
            this.tabPageJobTarget.Controls.Add(this.txtTargetPort);
            this.tabPageJobTarget.Controls.Add(this.label10);
            this.tabPageJobTarget.Controls.Add(this.label9);
            this.tabPageJobTarget.Location = new System.Drawing.Point(4, 22);
            this.tabPageJobTarget.Name = "tabPageJobTarget";
            this.tabPageJobTarget.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageJobTarget.Size = new System.Drawing.Size(368, 390);
            this.tabPageJobTarget.TabIndex = 1;
            this.tabPageJobTarget.Text = "Target";
            this.tabPageJobTarget.UseVisualStyleBackColor = true;
            // 
            // cbTargetSelfclean
            // 
            this.cbTargetSelfclean.AutoSize = true;
            this.cbTargetSelfclean.Location = new System.Drawing.Point(10, 149);
            this.cbTargetSelfclean.Name = "cbTargetSelfclean";
            this.cbTargetSelfclean.Size = new System.Drawing.Size(196, 17);
            this.cbTargetSelfclean.TabIndex = 28;
            this.cbTargetSelfclean.Text = "Remove unneccesary files on target";
            this.cbTargetSelfclean.UseVisualStyleBackColor = true;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(7, 264);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(56, 13);
            this.label16.TabIndex = 27;
            this.label16.Text = "Password:";
            // 
            // txtSshProxyPwd
            // 
            this.txtSshProxyPwd.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSshProxyPwd.Location = new System.Drawing.Point(69, 261);
            this.txtSshProxyPwd.Name = "txtSshProxyPwd";
            this.txtSshProxyPwd.Size = new System.Drawing.Size(293, 20);
            this.txtSshProxyPwd.TabIndex = 26;
            this.txtSshProxyPwd.UseSystemPasswordChar = true;
            // 
            // txtSshProxyUser
            // 
            this.txtSshProxyUser.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSshProxyUser.Location = new System.Drawing.Point(69, 235);
            this.txtSshProxyUser.Name = "txtSshProxyUser";
            this.txtSshProxyUser.Size = new System.Drawing.Size(293, 20);
            this.txtSshProxyUser.TabIndex = 25;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(4, 238);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(63, 13);
            this.label17.TabIndex = 24;
            this.label17.Text = "User Name:";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(7, 212);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(32, 13);
            this.label14.TabIndex = 21;
            this.label14.Text = "Host:";
            // 
            // txtSshProxyHost
            // 
            this.txtSshProxyHost.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSshProxyHost.Location = new System.Drawing.Point(45, 209);
            this.txtSshProxyHost.Name = "txtSshProxyHost";
            this.txtSshProxyHost.Size = new System.Drawing.Size(253, 20);
            this.txtSshProxyHost.TabIndex = 20;
            // 
            // txtSshProxyPort
            // 
            this.txtSshProxyPort.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSshProxyPort.Location = new System.Drawing.Point(304, 209);
            this.txtSshProxyPort.Name = "txtSshProxyPort";
            this.txtSshProxyPort.Size = new System.Drawing.Size(58, 20);
            this.txtSshProxyPort.TabIndex = 22;
            // 
            // label15
            // 
            this.label15.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(304, 193);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(29, 13);
            this.label15.TabIndex = 23;
            this.label15.Text = "Port:";
            // 
            // cbUseSshProxy
            // 
            this.cbUseSshProxy.AutoSize = true;
            this.cbUseSshProxy.Location = new System.Drawing.Point(10, 186);
            this.cbUseSshProxy.Name = "cbUseSshProxy";
            this.cbUseSshProxy.Size = new System.Drawing.Size(148, 17);
            this.cbUseSshProxy.TabIndex = 19;
            this.cbUseSshProxy.Text = "Connect via a SSH proxy:";
            this.cbUseSshProxy.UseVisualStyleBackColor = true;
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
            // cbTargetShowPass
            // 
            this.cbTargetShowPass.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cbTargetShowPass.AutoSize = true;
            this.cbTargetShowPass.Location = new System.Drawing.Point(248, 367);
            this.cbTargetShowPass.Name = "cbTargetShowPass";
            this.cbTargetShowPass.Size = new System.Drawing.Size(114, 17);
            this.cbTargetShowPass.TabIndex = 16;
            this.cbTargetShowPass.Text = "Display Passwords";
            this.cbTargetShowPass.UseVisualStyleBackColor = true;
            this.cbTargetShowPass.CheckedChanged += new System.EventHandler(this.cbTargetShowPass_CheckedChanged);
            // 
            // txtTargetZipPwd
            // 
            this.txtTargetZipPwd.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTargetZipPwd.Location = new System.Drawing.Point(88, 123);
            this.txtTargetZipPwd.Name = "txtTargetZipPwd";
            this.txtTargetZipPwd.Size = new System.Drawing.Size(274, 20);
            this.txtTargetZipPwd.TabIndex = 18;
            this.txtTargetZipPwd.UseSystemPasswordChar = true;
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
            // txtTargetPwd
            // 
            this.txtTargetPwd.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTargetPwd.Location = new System.Drawing.Point(69, 97);
            this.txtTargetPwd.Name = "txtTargetPwd";
            this.txtTargetPwd.Size = new System.Drawing.Size(293, 20);
            this.txtTargetPwd.TabIndex = 9;
            this.txtTargetPwd.UseSystemPasswordChar = true;
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
            // txtTargetUser
            // 
            this.txtTargetUser.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTargetUser.Location = new System.Drawing.Point(69, 71);
            this.txtTargetUser.Name = "txtTargetUser";
            this.txtTargetUser.Size = new System.Drawing.Size(293, 20);
            this.txtTargetUser.TabIndex = 8;
            // 
            // cbTargetProtocol
            // 
            this.cbTargetProtocol.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTargetProtocol.FormattingEnabled = true;
            this.cbTargetProtocol.Items.AddRange(new object[] {
            "SFTP",
            "FTP",
            "FTPS",
            "FTPES"});
            this.cbTargetProtocol.Location = new System.Drawing.Point(10, 19);
            this.cbTargetProtocol.Name = "cbTargetProtocol";
            this.cbTargetProtocol.Size = new System.Drawing.Size(63, 21);
            this.cbTargetProtocol.TabIndex = 0;
            this.cbTargetProtocol.SelectedIndexChanged += new System.EventHandler(this.cbTargetProtocol_SelectedIndexChanged);
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
            // txtTargetHost
            // 
            this.txtTargetHost.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTargetHost.Location = new System.Drawing.Point(79, 19);
            this.txtTargetHost.Name = "txtTargetHost";
            this.txtTargetHost.Size = new System.Drawing.Size(219, 20);
            this.txtTargetHost.TabIndex = 1;
            // 
            // txtTargetPath
            // 
            this.txtTargetPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTargetPath.Location = new System.Drawing.Point(69, 45);
            this.txtTargetPath.Name = "txtTargetPath";
            this.txtTargetPath.Size = new System.Drawing.Size(293, 20);
            this.txtTargetPath.TabIndex = 6;
            // 
            // txtTargetPort
            // 
            this.txtTargetPort.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTargetPort.Location = new System.Drawing.Point(304, 19);
            this.txtTargetPort.Name = "txtTargetPort";
            this.txtTargetPort.Size = new System.Drawing.Size(58, 20);
            this.txtTargetPort.TabIndex = 3;
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
            // tabPageAbout
            // 
            this.tabPageAbout.Controls.Add(this.linkLabel1);
            this.tabPageAbout.Controls.Add(this.label7);
            this.tabPageAbout.Controls.Add(this.label6);
            this.tabPageAbout.Controls.Add(this.label5);
            this.tabPageAbout.Controls.Add(this.label4);
            this.tabPageAbout.Controls.Add(this.rtbLicence);
            this.tabPageAbout.Location = new System.Drawing.Point(4, 22);
            this.tabPageAbout.Name = "tabPageAbout";
            this.tabPageAbout.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageAbout.Size = new System.Drawing.Size(616, 517);
            this.tabPageAbout.TabIndex = 1;
            this.tabPageAbout.Text = "About";
            this.tabPageAbout.UseVisualStyleBackColor = true;
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(477, 96);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(133, 13);
            this.linkLabel1.TabIndex = 5;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Embedded libraries licence";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(13, 124);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(194, 13);
            this.label7.TabIndex = 4;
            this.label7.Text = "Licenced under the term of the GPL v3:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(13, 96);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(153, 13);
            this.label6.TabIndex = 3;
            this.label6.Text = "Copyright 2015 Laurent Dupuis";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Arial Rounded MT Bold", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(6, 30);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(341, 55);
            this.label5.TabIndex = 2;
            this.label5.Text = "CloudBackup";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(6, 12);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(143, 18);
            this.label4.TabIndex = 1;
            this.label4.Text = "Laurent Dupuis\'s";
            // 
            // rtbLicence
            // 
            this.rtbLicence.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtbLicence.Location = new System.Drawing.Point(6, 140);
            this.rtbLicence.Name = "rtbLicence";
            this.rtbLicence.ReadOnly = true;
            this.rtbLicence.Size = new System.Drawing.Size(604, 371);
            this.rtbLicence.TabIndex = 0;
            this.rtbLicence.Text = "";
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
            this.tabJobSettings.ResumeLayout(false);
            this.tabPageJobSchedule.ResumeLayout(false);
            this.tabPageJobSchedule.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.tabPageJobTarget.ResumeLayout(false);
            this.tabPageJobTarget.PerformLayout();
            this.tabPageAbout.ResumeLayout(false);
            this.tabPageAbout.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabInfos;
        private System.Windows.Forms.TabPage tabPageArchive;
        private System.Windows.Forms.SplitContainer spltCtrlArchive;
        private System.Windows.Forms.ListBox lbAllSchedule;
        private System.Windows.Forms.TabPage tabPageAbout;
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
        private System.Windows.Forms.Button btnSaveSchedule;
        private System.Windows.Forms.ComboBox cbAllSchedules;
        private System.Windows.Forms.Button btnDelSchedule;
        private System.Windows.Forms.Button btnNewSchedule;
        private System.Windows.Forms.Button btnRunNow;
        private System.Windows.Forms.Button btnResetStatus;
        private System.Windows.Forms.TextBox txtTargetPort;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtTargetHost;
        private System.Windows.Forms.ComboBox cbTargetProtocol;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtTargetPath;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.CheckBox cbTargetShowPass;
        private System.Windows.Forms.TextBox txtTargetUser;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtTargetPwd;
        private System.Windows.Forms.TextBox txtTargetZipPwd;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TabControl tabJobSettings;
        private System.Windows.Forms.TabPage tabPageJobSchedule;
        private System.Windows.Forms.TabPage tabPageJobTarget;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox txtSshProxyPwd;
        private System.Windows.Forms.TextBox txtSshProxyUser;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtSshProxyHost;
        private System.Windows.Forms.TextBox txtSshProxyPort;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.CheckBox cbUseSshProxy;
        private System.Windows.Forms.RichTextBox rtbLicence;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.CheckBox cbForceFullbackup;
        private System.Windows.Forms.CheckBox cbTargetSelfclean;
    }
}

