using Badhon_Member_Management_KU_Unit.UserControls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Badhon_Member_Management_KU_Unit.Forms
{
    public partial class Form_Dashboard : Form
    {
        int PanelWidth;
        bool isCollapsed;
        public Form_Dashboard()
        {
            InitializeComponent();
            timerTime.Start();
            PanelWidth = panelLeft.Width;
            isCollapsed = false;
            UC_Home uch = new UC_Home();
            AddControlsToPanel(uch);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if(isCollapsed)
            {
                button1.Location = new Point(button1.Location.X + 10, button1.Location.Y);
                panelLeft.Width = panelLeft.Width + 10;
                if(panelLeft.Width >= PanelWidth)
                {
                    timer1.Stop();
                    isCollapsed = false;
                    this.Refresh();
                }
            }
            else
            {
                button1.Location = new Point(button1.Location.X - 10, button1.Location.Y);
                panelLeft.Width = panelLeft.Width - 10;
                if(panelLeft.Width <= 58)
                {
                    timer1.Stop();
                    isCollapsed = true;
                    this.Refresh();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Start();
        }
        private void moveSidePanel(Control btn)
        {
            panelSide.Top = btn.Top;
            panelSide.Height = btn.Height;
        }

        private void AddControlsToPanel(Control c)
        {
            c.Dock = DockStyle.Fill;
            panelControls.Controls.Clear();
            panelControls.Controls.Add(c);
        }
        private void btnHome_Click(object sender, EventArgs e)
        {
            moveSidePanel(btnHome);
            UC_Home uch = new UC_Home();
            AddControlsToPanel(uch);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            moveSidePanel(btnSettings);
            UC_Settings uset = new UC_Settings();
            AddControlsToPanel(uset);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            moveSidePanel(btnAdd);
            UC_Add ua = new UC_Add();
            AddControlsToPanel(ua);
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            moveSidePanel(btnSearch);
            UC_Search us = new UC_Search();
            AddControlsToPanel(us);
        }

        private void timerTime_Tick(object sender, EventArgs e)
        {
            DateTime dt = DateTime.Now;
            labelTime.Text = dt.ToString("HH:mm:ss");
        }

        private void labelTime_Click(object sender, EventArgs e)
        {

        }

        private void panelControls_Paint(object sender, PaintEventArgs e)
        {

        }
        
        public void logout()
        {
            this.Dispose();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            UserControl ed = new UserControl1();
            AddControlsToPanel(ed);
        }
    }
}
