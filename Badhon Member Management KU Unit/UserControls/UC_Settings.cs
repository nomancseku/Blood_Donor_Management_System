using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Badhon_Member_Management_KU_Unit.Forms;

namespace Badhon_Member_Management_KU_Unit.UserControls
{
    public partial class UC_Settings : UserControl
    {
        public UC_Settings()
        {
            InitializeComponent();
        }

        private void panel7_Paint(object sender, PaintEventArgs e)
        {
            // nothing will be added
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (Form1 f = new Form1())
            {
                f.ShowDialog();
                //this.Dispose();
                //Form_Dashboard.logout();
            }
        }
    }
}
