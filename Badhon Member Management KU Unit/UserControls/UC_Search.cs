using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Badhon_Member_Management_KU_Unit.badhanClasses;
using System.Data.SqlClient;
using System.Configuration;

namespace Badhon_Member_Management_KU_Unit.UserControls
{
    public partial class UC_Search : UserControl
    {
        bool _checked = false;
        public UC_Search()
        {
            InitializeComponent();
        }
        DonorClass d = new DonorClass();
        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //Getting the data from datagridview and load to the textboxes respectively
            //identify the row on which mouse is clicked
            int rowIndex = e.RowIndex;
            textBoxDonorID.Text = dataGridView1.Rows[rowIndex].Cells[0].Value.ToString();
            textBoxName.Text = dataGridView1.Rows[rowIndex].Cells[1].Value.ToString();
            textBoxInstitute.Text = dataGridView1.Rows[rowIndex].Cells[4].Value.ToString();
            textBoxStudentID.Text = dataGridView1.Rows[rowIndex].Cells[5].Value.ToString();
            comboBoxBloodGroup.Text = dataGridView1.Rows[rowIndex].Cells[6].Value.ToString();
            //textBoxDisease.Text = dataGridView1.Rows[rowIndex].Cells[6].Value.ToString();
            textBoxContactNo.Text = dataGridView1.Rows[rowIndex].Cells[2].Value.ToString();
            textBoxAddress.Text = dataGridView1.Rows[rowIndex].Cells[3].Value.ToString();
            textBoxWeight.Text = dataGridView1.Rows[rowIndex].Cells[8].Value.ToString();
            if(dataGridView1.Rows[rowIndex].Cells[7].Value.ToString() != " ")
            {
                dateTimePickerLastGiven.Text = dataGridView1.Rows[rowIndex].Cells[7].Value.ToString();
            }
            
        }
        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //this code is for bug fixing
            if (textBoxDonorID.Text == "")
            {
                // this is for bug fixing. if we do not assign it then it will
                //create bug in 52 line and the code is
                // d.DonorID = int.Parse(textBoxDonorID.Text);

                textBoxDonorID.Text = "0";

                MessageBox.Show("Please select any donor first");
                goto here1;
            }
            //Get the data from textboxes
            d.DonorID = int.Parse(textBoxDonorID.Text);
            d.Name = textBoxName.Text;
            d.Institute = textBoxInstitute.Text;
            d.StudentID = textBoxStudentID.Text;
            d.ContactNo = textBoxContactNo.Text;
            d.Address = textBoxAddress.Text;
            d.BloodGroup = comboBoxBloodGroup.Text;
            d.Weight = textBoxWeight.Text;
            //d.Disease = textBoxDisease.Text;
            d.LastGivenDate = dateTimePickerLastGiven.Text;
            //Update data into database
            bool success = d.Update(d);
            if(success == true)
            {
                //Updated successfully
                MessageBox.Show("Updated successfully");
                Clear();
            }
            else
            {
                //Failed to update
                MessageBox.Show("Failed to update");
            }
            // Load data on datagrid view
            DataTable dt = d.Select();
            dataGridView1.DataSource = dt;
        here1:;
        }

        private void UC_Search_Load_1(object sender, EventArgs e)
        {
            // Load data on datagrid view
            DataTable dt = d.Select();
            dataGridView1.DataSource = dt;

            // Date time picker format
            //dateTimePickerLastGiven.Format = DateTimePickerFormat.Custom;
            //dateTimePickerLastGiven.CustomFormat = "dd/MM/yyyy";
        }

        //Method to clear fields
        public void Clear()
        {
            textBoxDonorID.Text = "";
            textBoxName.Text = "";
            textBoxContactNo.Text = "";
            textBoxAddress.Text = "";
            textBoxInstitute.Text = "";
            textBoxStudentID.Text = "";
            //textBoxDisease.Text = "";
            comboBoxBloodGroup.Text = "";
            dateTimePickerLastGiven.Text = "";
            textBoxWeight.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //this code is for bug fixing
            if(textBoxDonorID.Text == "")
            {
                // this is for bug fixing. if we do not assign it then it will
                //create bug in line-52 and the code is
                // d.DonorID = int.Parse(textBoxDonorID.Text);

                textBoxDonorID.Text = "0";

                MessageBox.Show("Please select any donor first");
                goto here;
            }

            // get data from the datagrid view from the textbox
            d.DonorID = int.Parse(textBoxDonorID.Text);
            
            bool success = d.Delete(d);
            if(success==true)
            {
                MessageBox.Show("Successfully Deleted");
                // refresh the datagrid view
                DataTable dt = d.Select();
                dataGridView1.DataSource = dt;
                Clear();
            }
            else
            {
                MessageBox.Show("Not Deleted, Try again");
            }
        here:;
        }

        static string myconnstr = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //get the value from textbox
            string keyword = textBoxSearch.Text;
            SqlConnection conn = new SqlConnection(myconnstr);
            SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM tblDonor WHERE Name like '%"+keyword+"%' OR ContactNo like '%"+keyword+"%' OR Address like '%"+keyword+"%' OR Institute like '%"+keyword+ "%' OR StudentID like '%" + keyword + "%' OR BloodGroup like '%" + keyword + "%' OR Weight like '%" + keyword + "%'", conn);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void dateTimePickerLastGiven_ValueChanged(object sender, EventArgs e)
        {
            // Date time picker format
            dateTimePickerLastGiven.Format = DateTimePickerFormat.Custom;
            dateTimePickerLastGiven.CustomFormat = "dd/MM/yyyy";
        }

        private void dateTimePickerLastGiven_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Back) || (e.KeyCode == Keys.Delete))
            {
                dateTimePickerLastGiven.CustomFormat = " ";
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            Clear();

            string bloodGroup = comboBloodGroup.Text;
            string institute = comboInstitute.Text;
            // there can be total 9 combinations... hahaha
            if(_checked == true)
            {
                //Getting the day from system
                string dateToday = DateTime.Now.ToString("dd");
                string monthToday = DateTime.Now.ToString("MM");
                string yearToday = DateTime.Now.ToString("yyyy");

                //converting those into integer to check
                int date1 = 1;
                int date2 = 1;
                int date3 = 1;
                int date4 = Convert.ToInt32(dateToday);

                int month1 = Convert.ToInt32(monthToday);
                int month2, month3, month4;
                int year1, year2, year3, year4;
                year1 = year2 = year3 = year4 = Convert.ToInt32(yearToday);

                month2 = month1 - 1;
                month3 = month1 - 2;
                month4 = month1 - 3;

                if (month2 < 1)
                {
                    month2 = month2 + 12;
                    year2 = year2 - 1;
                }
                if (month3 < 1)
                {
                    month3 = month3 + 12;
                    year3 = year3 - 1;
                }
                if (month4 < 1)
                {
                    month4 = month4 + 12;
                    year4 = year4 - 1;
                }

                //converting into string
                string d1 = Convert.ToString(date1);
                string d2 = Convert.ToString(date2);
                string d3 = Convert.ToString(date3);
                string d4 = Convert.ToString(date4);

                string m1 = Convert.ToString(month1);
                string m2 = Convert.ToString(month2);
                string m3 = Convert.ToString(month3);
                string m4 = Convert.ToString(month4);

                string y1 = Convert.ToString(year1);
                string y2 = Convert.ToString(year2);
                string y3 = Convert.ToString(year3);
                string y4 = Convert.ToString(year4);

                if (bloodGroup == "")
                {
                    if(institute == "")
                    {
                        // here bloodGroup = all , institute = all , availability = marked
                        //MessageBox.Show("here bloodGroup = all , institute = all , availability = marked");

                        string sql = "SELECT * FROM tblDonor WHERE LastGivenDate < '" + d1 + "/" + m1 + "/" + y1 + "' AND LastGivenDate < '" + d2 + "/" + m2 + "/" + y2 + "' AND LastGivenDate < '" + d3 + "/" + m3 + "/" + y3 + "' AND LastGivenDate < '" + d4 + "/" + m4 + "/" + y4 + "' ";

                        SqlConnection conn = new SqlConnection(myconnstr);
                        SqlDataAdapter sda = new SqlDataAdapter(sql, conn);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        dataGridView1.DataSource = dt;
                    }
                    else
                    {
                        // here bloodGroup = all , institute = specific , availability = marked
                        //MessageBox.Show("here bloodGroup = all , institute = specific , availability = marked");

                        string sql = "SELECT * FROM tblDonor WHERE LastGivenDate < '" + d1 + "/" + m1 + "/" + y1 + "' AND LastGivenDate < '" + d2 + "/" + m2 + "/" + y2 + "' AND LastGivenDate < '" + d3 + "/" + m3 + "/" + y3 + "' AND LastGivenDate < '" + d4 + "/" + m4 + "/" + y4 + "' AND Institute like '%" + institute + "%'";

                        SqlConnection conn = new SqlConnection(myconnstr);
                        SqlDataAdapter sda = new SqlDataAdapter(sql, conn);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        dataGridView1.DataSource = dt;
                    }
                }
                else
                {
                    if (institute == "")
                    {
                        // here bloodGroup = specific , institute = all , availability = marked
                        //MessageBox.Show("here bloodGroup = specific , institute = all , availability = marked");

                        string sql = "SELECT * FROM tblDonor WHERE LastGivenDate < '" + d1 + "/" + m1 + "/" + y1 + "' AND LastGivenDate < '" + d2 + "/" + m2 + "/" + y2 + "' AND LastGivenDate < '" + d3 + "/" + m3 + "/" + y3 + "' AND LastGivenDate < '" + d4 + "/" + m4 + "/" + y4 + "' AND BloodGroup like '%" + bloodGroup + "%'";

                        SqlConnection conn = new SqlConnection(myconnstr);
                        SqlDataAdapter sda = new SqlDataAdapter(sql, conn);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        dataGridView1.DataSource = dt;
                    }
                    else
                    {
                        // here bloodGroup = specific , institute = specific , availability = marked
                        //MessageBox.Show("here bloodGroup = specific , institute = specific , availability = marked");

                        string sql = "SELECT * FROM tblDonor WHERE LastGivenDate < '" + d1 + "/" + m1 + "/" + y1 + "' AND LastGivenDate < '" + d2 + "/" + m2 + "/" + y2 + "' AND LastGivenDate < '" + d3 + "/" + m3 + "/" + y3 + "' AND LastGivenDate < '" + d4 + "/" + m4 + "/" + y4 + "' AND BloodGroup like '%" + bloodGroup + "%' AND Institute like '%" + institute + "%'";

                        SqlConnection conn = new SqlConnection(myconnstr);
                        SqlDataAdapter sda = new SqlDataAdapter(sql, conn);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        dataGridView1.DataSource = dt;
                    }
                }
            }
            else
            {
                if (bloodGroup == "")
                {
                    if (institute == "")
                    {
                        // here bloodGroup = all , institute = all , availability = not marked
                        //MessageBox.Show("here bloodGroup = all , institute = all , availability = not marked");
                        SqlConnection conn = new SqlConnection(myconnstr);
                        SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM tblDonor", conn);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        dataGridView1.DataSource = dt;
                    }
                    else
                    {
                        // here bloodGroup = all , institute = specific , availability = not marked
                        //MessageBox.Show("here bloodGroup = all , institute = specific , availability = not marked");
                        SqlConnection conn = new SqlConnection(myconnstr);
                        SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM tblDonor WHERE Institute like '%" + institute + "%'", conn);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        dataGridView1.DataSource = dt;
                    }
                }
                else
                {
                    if (institute == "")
                    {
                        // here bloodGroup = specific , institute = all , availability = not marked
                        //MessageBox.Show("here bloodGroup = specific , institute = all , availability = not marked");
                        SqlConnection conn = new SqlConnection(myconnstr);
                        SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM tblDonor WHERE BloodGroup like '%" + bloodGroup + "%'", conn);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        dataGridView1.DataSource = dt;
                    }
                    else
                    {
                        // here bloodGroup = specific , institute = specific , availability = not marked
                        //MessageBox.Show("here bloodGroup = specific , institute = specific , availability = not marked");
                        SqlConnection conn = new SqlConnection(myconnstr);
                        SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM tblDonor WHERE BloodGroup like '%" + bloodGroup + "%' AND Institute like '%" + institute + "%'", conn);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        dataGridView1.DataSource = dt;
                    }
                }
            }
        }

        private void checkBoxAvailability_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBoxAvailability.Checked)
            {
                _checked = true;
            }
            else
            {
                _checked = false;
            }
        }

        private void comboBloodGroup_KeyDown(object sender, KeyEventArgs e)
        {

        }
    }
}
