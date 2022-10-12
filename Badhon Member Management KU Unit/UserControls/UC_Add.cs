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
using System.Configuration;
using System.Data.SqlClient;

namespace Badhon_Member_Management_KU_Unit.UserControls
{
    public partial class UC_Add : UserControl
    {
        public UC_Add()
        {
            InitializeComponent();
        }
        DonorClass d = new DonorClass();
        private void label3_Click(object sender, EventArgs e)
        {

        }
        private void button1_Click(object sender, EventArgs e)
        {
            //get the values from the input fields
            d.Name = textBoxName.Text;
            d.ContactNo = textBoxContactNo.Text;
            d.Address = textBoxAddress.Text;
            d.Institute = textBoxInstitute.Text;
            d.StudentID = textBoxStudentID.Text;
            //d.Disease = textBoxDisease.Text;
            d.BloodGroup = comboBoxBloodGroup.Text;
            d.LastGivenDate = dateTimePickerLastGiven.Text;
            d.Weight = textBoxWeight.Text;

            if(d.Name == "")
            {
                MessageBox.Show("Name is missing in field");
                goto here;
            }
            else if(d.ContactNo == "")
            {
                MessageBox.Show("Contant No. is missing in field");
                goto here;
            }
            else if(d.Address == "")
            {
                MessageBox.Show("Address is missing in field");
                goto here;
            }
            else if(d.BloodGroup == "")
            {
                MessageBox.Show("Blood Group is missing in field");
                goto here;
            }
            //inserting data into database using method that we created
            bool success = d.Insert(d);
            if(success==true)
            {
                //successfully created
                MessageBox.Show("Successful");
                //calling the clear method here
                Clear();
            }
            else
            {
                // failed to insert
                MessageBox.Show("Unsuccessful");
            }
            // Load and update data on datagrid view
            string myconnstr = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;
            SqlConnection conn = new SqlConnection(myconnstr);
            SqlDataAdapter sda = new SqlDataAdapter("SELECT Name,Address, BloodGroup,Weight FROM tblDonor ORDER BY DonorID DESC", conn);
            DataTable dt_for_recently_added = new DataTable();
            sda.Fill(dt_for_recently_added);
            dgvRecent.DataSource = dt_for_recently_added;
        here:;
        }

        private void UC_Add_Load(object sender, EventArgs e)
        {
            // Load data on datagrid view
            //for recently added option to show recently added 
            string myconnstr = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;
            SqlConnection conn = new SqlConnection(myconnstr);
            SqlDataAdapter sda = new SqlDataAdapter("SELECT Name,Address, BloodGroup,Weight FROM tblDonor ORDER BY DonorID DESC", conn);
            DataTable dt_for_recently_added = new DataTable();
            sda.Fill(dt_for_recently_added);
            dgvRecent.DataSource = dt_for_recently_added;

            //hiding these fields
            label4.Hide();
            label3.Hide();
            textBoxInstitute.Hide();
            textBoxStudentID.Hide();
        }
        //Method to clear fields
        public void Clear()
        {
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

        private void buttonClearField_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void checkBoxStudent_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBoxStudent.Checked)
            {
                label4.Show();
                label3.Show();
                textBoxInstitute.Show();
                textBoxStudentID.Show();
            }
            else
            {
                label4.Hide();
                label3.Hide();
                textBoxInstitute.Hide();
                textBoxStudentID.Hide();
            }
        }

        private void dateTimePickerLastGiven_ValueChanged(object sender, EventArgs e)
        {
            // Date time picker format
            dateTimePickerLastGiven.Format = DateTimePickerFormat.Custom;
            dateTimePickerLastGiven.CustomFormat = "dd/MM/yyyy";
        }

        private void dateTimePickerLastGiven_KeyDown(object sender, KeyEventArgs e)
        {
            if((e.KeyCode == Keys.Back) || (e.KeyCode == Keys.Delete))
            {
                dateTimePickerLastGiven.CustomFormat = " ";
            }
        }

        private void textBoxContactNo_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
