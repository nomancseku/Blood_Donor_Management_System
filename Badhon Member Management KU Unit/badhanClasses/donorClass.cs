using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Badhon_Member_Management_KU_Unit.badhanClasses
{
    class DonorClass
    {
        //Getter setter properties, that acts as data carrier in our application
        public int DonorID { get; set; }
        public string Name { get; set; }
        public string Institute { get; set; }
        public string StudentID { get; set; }
        public string ContactNo { get; set; }
        public string Address { get; set; }
        public string BloodGroup { get; set; }
        public string Weight { get; set; }
        //public string Disease { get; set; }
        public string LastGivenDate { get; set; }

        static string myconnstrng = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;

        //selecting data from database
        public DataTable Select()
        {
            //step1 : database connection
            SqlConnection conn = new SqlConnection(myconnstrng);
            DataTable dt = new DataTable();
            try
            {
                //step2: writing sql query
                string sql = "SELECT * FROM tblDonor";
                //creating command using sql and conn
                SqlCommand cmd = new SqlCommand(sql, conn);
                //creating sql data adapter using cmd
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                conn.Open();
                adapter.Fill(dt);
            }
            catch (Exception)
            {

            }
            finally
            {
                conn.Close();
            }
            return dt;
        }

        //inserting data into database
        public bool Insert(DonorClass d)
        {
            //creating a default return type and setting its value to false
            bool isSuccess = false;
            //step1 : connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            try
            {
                //Step 2 : Create a sql query to insert data
                string sql = "INSERT into tblDonor(Name,ContactNo,Address,Institute," +
                    "StudentID,BloodGroup,LastGivenDate,Weight)" +
                    " VALUES(@Name,@ContactNo,@Address,@Institute," +
                    "@StudentID,@BloodGroup,@LastGivenDate,@Weight);";
                //creating command using sql and conn
                SqlCommand cmd = new SqlCommand(sql, conn);
                //creating parameter to add data
                cmd.Parameters.AddWithValue("@Name", d.Name);
                cmd.Parameters.AddWithValue("@ContactNo", d.ContactNo);
                cmd.Parameters.AddWithValue("@Address", d.Address);
                cmd.Parameters.AddWithValue("@Institute", d.Institute);
                cmd.Parameters.AddWithValue("@StudentID", d.StudentID);
                //cmd.Parameters.AddWithValue("@Disease", d.Disease);
                cmd.Parameters.AddWithValue("@BloodGroup", d.BloodGroup);
                cmd.Parameters.AddWithValue("@LastGivenDate", d.LastGivenDate);
                cmd.Parameters.AddWithValue("@Weight", d.Weight);

                //connection open here
                conn.Open();
                int rows = cmd.ExecuteNonQuery();
                // if the query runs successfully then the value of rows will be greater than zero else its value will be 0
                if (rows > 0)
                {
                    isSuccess = true;
                }
                else
                {
                    isSuccess = false;
                }
            }
            catch (Exception)
            {

            }
            finally
            {
                conn.Close();
            }
            return isSuccess;
        }

        //method to update data in database from our application
        public bool Update(DonorClass d)
        {
            //create a default return type and set its value to false
            bool isSuccess = false;
            //step1 : connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            try
            {
                //Step 2 : Create a sql query to update data
                //sql to update data in our database
                string sql = "UPDATE tblDonor SET Name = @Name, " +
                    "ContactNo = @ContactNo, Address = @Address, " +
                    "Institute = @Institute, StudentID = @StudentID, " +
                    "BloodGroup = @BloodGroup, " +
                    "LastGivenDate = @LastGivenDate, Weight = @Weight " +
                    "WHERE DonorID = @DonorID;";

                //creating sql command
                SqlCommand cmd = new SqlCommand(sql, conn);
                //create parameters to add value
                cmd.Parameters.AddWithValue("@DonorID", d.DonorID);
                cmd.Parameters.AddWithValue("@Name", d.Name);
                cmd.Parameters.AddWithValue("@ContactNo", d.ContactNo);
                cmd.Parameters.AddWithValue("@Address", d.Address);
                cmd.Parameters.AddWithValue("@Institute", d.Institute);
                cmd.Parameters.AddWithValue("@StudentID", d.StudentID);
                //cmd.Parameters.AddWithValue("@Disease", d.Disease);
                cmd.Parameters.AddWithValue("@BloodGroup", d.BloodGroup);
                cmd.Parameters.AddWithValue("@LastGivenDate", d.LastGivenDate);
                cmd.Parameters.AddWithValue("@Weight", d.Weight);
                //open database connection
                conn.Open();

                int rows = cmd.ExecuteNonQuery();
                // if the query runs successfully then the value of rows will be greater than zero else its value will be 0
                if (rows > 0)
                {
                    isSuccess = true;
                }
                else
                {
                    isSuccess = false;
                }
            }
            catch (Exception)
            {

            }
            finally
            {
                conn.Close();
            }
            return isSuccess;
        }
        //method to delete data from database
        public bool Delete(DonorClass d)
        {
            //create a default return type and set its value to false
            bool isSuccess = false;
            //connect to database
            SqlConnection conn = new SqlConnection(myconnstrng);
            try
            {
                //sql to delete data
                string sql = "DELETE FROM tblDonor WHERE DonorID = @DonorID";

                //creating sql command
                SqlCommand cmd = new SqlCommand(sql, conn);
                //create parameters to add value
                cmd.Parameters.AddWithValue("@DonorID", d.DonorID);
                //Open connection
                conn.Open();
                int rows = cmd.ExecuteNonQuery();
                // if the query runs successfully then the value of rows will be greater than zero else its value will be 0
                if (rows > 0)
                {
                    isSuccess = true;
                }
                else
                {
                    isSuccess = false;
                }
            }
            catch (Exception)
            {

            }
            finally
            {
                conn.Close();
            }

            return isSuccess;
        }

    }
}
