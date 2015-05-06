using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CloudBackup.Manager
{
    public partial class BackupReportViewer : Form
    {
        public BackupReportViewer(string text)
        {
            InitializeComponent();

            txtReport.Text = text;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
