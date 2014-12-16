using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CloudBackup.Manager
{
    public partial class LegalNoticies : Form
    {
        public LegalNoticies()
        {
            InitializeComponent();
            Utils.SetupRtfBox(rtbNoticies,"legal.rtf");
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
