using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CloudBackup.Manager
{
    static class Utils
    {
        static public bool IsInput(Control control)
        {
            return
                control is Button ||
                control is TextBox ||
                control is ComboBox ||
                control is CheckBox ||
                control is RadioButton ||
                control is ListBox;
        }

        static public bool IsContainer(Control control)
        {
            return
                control is TabControl ||
                control is TabPage ||
                control is Panel;
        }


        static public void EnableDisablePanel(Control panel,bool status)
        {
            foreach (Control control in panel.Controls)
            {
                if (IsInput(control)) { control.Enabled = status; continue; }
                if (IsContainer(control)) EnableDisablePanel(control,status);
            }
        }

        static public void SetupRtfBox(RichTextBox target, string fileName)
        {
            using (var tmp = System.Reflection.Assembly.GetCallingAssembly().GetManifestResourceStream(
                typeof(Manager), fileName))
            {
                using (var txtReader = new StreamReader(tmp))
                {
                    target.Rtf = txtReader.ReadToEnd();
                }
            }
        }

    }
}
