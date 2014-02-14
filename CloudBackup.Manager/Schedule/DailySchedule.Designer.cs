namespace CloudBackup.Manager.Schedule
{
    partial class DailySchedule
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
            this.SuspendLayout();
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(161, 63);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 13);
            this.label4.TabIndex = 17;
            this.label4.Text = "day(s)";
            // 
            // mtbEvery
            // 
            this.mtbEvery.Enabled = false;
            this.mtbEvery.Location = new System.Drawing.Point(136, 60);
            this.mtbEvery.Mask = "00";
            this.mtbEvery.Name = "mtbEvery";
            this.mtbEvery.Size = new System.Drawing.Size(19, 20);
            this.mtbEvery.TabIndex = 16;
            this.mtbEvery.Text = "01";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(96, 63);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(34, 13);
            this.label3.TabIndex = 15;
            this.label3.Text = "Every";
            // 
            // mtbRuntime
            // 
            this.mtbRuntime.Enabled = false;
            this.mtbRuntime.Location = new System.Drawing.Point(159, 40);
            this.mtbRuntime.Mask = "00:00";
            this.mtbRuntime.Name = "mtbRuntime";
            this.mtbRuntime.Size = new System.Drawing.Size(46, 20);
            this.mtbRuntime.TabIndex = 14;
            this.mtbRuntime.Text = "0000";
            this.mtbRuntime.ValidatingType = typeof(System.DateTime);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(109, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "Start at:";
            // 
            // DailySchedule
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label4);
            this.Controls.Add(this.mtbEvery);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.mtbRuntime);
            this.Controls.Add(this.label2);
            this.MaximumSize = new System.Drawing.Size(300, 120);
            this.MinimumSize = new System.Drawing.Size(300, 120);
            this.Name = "DailySchedule";
            this.Size = new System.Drawing.Size(300, 120);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.MaskedTextBox mtbEvery;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.MaskedTextBox mtbRuntime;
        private System.Windows.Forms.Label label2;
    }
}
