using Badhon_Member_Management_KU_Unit.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Badhon_Member_Management_KU_Unit
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            using (Form2 form_new = new Form2())
            {
                form_new.ShowDialog();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string user = "admin";
            string pass = "admin";
            if((textBoxUserName.Text == user) && (textBoxPassword.Text == pass))
            {
                using (Form_Dashboard fd = new Form_Dashboard())
                {
                    fd.ShowDialog();
                }
                this.Dispose();
            }
            else
            {
                MessageBox.Show("Incorrect username or password. Try again.");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //nothing
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
