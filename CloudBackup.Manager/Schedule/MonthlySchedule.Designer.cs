namespace CloudBackup.Manager.Schedule
{
    partial class MonthlySchedule
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label4 = new System.Windows.Forms.Label();
            this.mtbEvery = new System.Windows.Forms.MaskedTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.mtbRuntime = new System.Windows.Forms.MaskedTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.rbFromBegin = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.mtbDay = new System.Windows.Forms.MaskedTextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.rbFromEnd = new System.Windows.Forms.RadioButton();
            this.rbFromSunday = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(79, 40);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 13);
            this.label4.TabIndex = 22;
            this.label4.Text = "month(s)";
            // 
            // mtbEvery
            // 
            this.mtbEvery.Enabled = false;
            this.mtbEvery.Location = new System.Drawing.Point(54, 37);
            this.mtbEvery.Mask = "00";
            this.mtbEvery.Name = "mtbEvery";
            this.mtbEvery.Size = new System.Drawing.Size(19, 20);
            this.mtbEvery.TabIndex = 21;
            this.mtbEvery.Text = "01";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 40);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(34, 13);
            this.label3.TabIndex = 20;
            this.label3.Text = "Every";
            // 
            // mtbRuntime
            // 
            this.mtbRuntime.Enabled = false;
            this.mtbRuntime.Location = new System.Drawing.Point(76, 7);
            this.mtbRuntime.Mask = "00:00";
            this.mtbRuntime.Name = "mtbRuntime";
            this.mtbRuntime.Size = new System.Drawing.Size(46, 20);
            this.mtbRuntime.TabIndex = 19;
            this.mtbRuntime.Text = "0000";
            this.mtbRuntime.ValidatingType = typeof(System.DateTime);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(26, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 13);
            this.label2.TabIndex = 18;
            this.label2.Text = "Start at:";
            // 
            // rbFromBegin
            // 
            this.rbFromBegin.AutoSize = true;
            this.rbFromBegin.Checked = true;
            this.rbFromBegin.Enabled = false;
            this.rbFromBegin.Location = new System.Drawing.Point(140, 26);
            this.rbFromBegin.Name = "rbFromBegin";
            this.rbFromBegin.Size = new System.Drawing.Size(151, 17);
            this.rbFromBegin.TabIndex = 23;
            this.rbFromBegin.TabStop = true;
            this.rbFromBegin.Text = "the beginning of the month";
            this.rbFromBegin.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(137, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 13);
            this.label1.TabIndex = 24;
            this.label1.Text = "On the";
            // 
            // mtbDay
            // 
            this.mtbDay.Enabled = false;
            this.mtbDay.Location = new System.Drawing.Point(176, 4);
            this.mtbDay.Mask = "00";
            this.mtbDay.Name = "mtbDay";
            this.mtbDay.Size = new System.Drawing.Size(19, 20);
            this.mtbDay.TabIndex = 25;
            this.mtbDay.Text = "01";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(201, 7);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(76, 13);
            this.label5.TabIndex = 26;
            this.label5.Text = "day relative to:";
            // 
            // rbFromEnd
            // 
            this.rbFromEnd.AutoSize = true;
            this.rbFromEnd.Enabled = false;
            this.rbFromEnd.Location = new System.Drawing.Point(140, 49);
            this.rbFromEnd.Name = "rbFromEnd";
            this.rbFromEnd.Size = new System.Drawing.Size(123, 17);
            this.rbFromEnd.TabIndex = 27;
            this.rbFromEnd.Text = "the end of the month";
            this.rbFromEnd.UseVisualStyleBackColor = true;
            // 
            // rbFromSunday
            // 
            this.rbFromSunday.AutoSize = true;
            this.rbFromSunday.Enabled = false;
            this.rbFromSunday.Location = new System.Drawing.Point(140, 72);
            this.rbFromSunday.Name = "rbFromSunday";
            this.rbFromSunday.Size = new System.Drawing.Size(160, 17);
            this.rbFromSunday.TabIndex = 28;
            this.rbFromSunday.Text = "the first Sunday of the month";
            this.rbFromSunday.UseVisualStyleBackColor = true;
            // 
            // MonthlySchedule
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.rbFromSunday);
            this.Controls.Add(this.rbFromEnd);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.mtbDay);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.rbFromBegin);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.mtbEvery);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.mtbRuntime);
            this.Controls.Add(this.label2);
            this.MaximumSize = new System.Drawing.Size(320, 120);
            this.MinimumSize = new System.Drawing.Size(320, 120);
            this.Name = "MonthlySchedule";
            this.Size = new System.Drawing.Size(320, 120);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.MaskedTextBox mtbEvery;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.MaskedTextBox mtbRuntime;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton rbFromBegin;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.MaskedTextBox mtbDay;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.RadioButton rbFromEnd;
        private System.Windows.Forms.RadioButton rbFromSunday;
    }
}
