using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ClubRegistration
{
    public partial class FrmUpdateMember : Form
    {
        private SqlConnection sqlConnect;
        private SqlCommand sqlCommand;
        private SqlDataReader sqlReader;
        private string connectionString =
    @"Data Source=(LocalDB)\MSSQLLocalDB;
      AttachDbFilename=C:\Users\Administrator\Documents\ClubDB.mdf;
      Integrated Security=True;
      Connect Timeout=30";


        private ClubRegistrationQuery clubQuery;

        public FrmUpdateMember()
        {
            InitializeComponent();
            clubQuery = new ClubRegistrationQuery();
        }

        private void FrmUpdateMember_Load(object sender, EventArgs e)
        {
            sqlConnect = new SqlConnection(connectionString);
            LoadStudentIds();

            cbGender.Items.AddRange(new string[] { "Male", "Female" });
            cbProgram.Items.AddRange(new string[]
            {
                "BS Information Technology", "BS Computer Science",
                "BS Information Systems", "BS in Accountancy",
                "BS in Hospitality Management", "BS in Tourism Management"
            });

            txtStudentID.SelectedIndexChanged += txtStudentID_SelectedIndexChanged;
        }

        private void LoadStudentIds()
        {
            txtStudentID.Items.Clear();
            sqlConnect.Open();
            sqlCommand = new SqlCommand("SELECT StudentId FROM ClubMembers", sqlConnect);
            sqlReader = sqlCommand.ExecuteReader();

            while (sqlReader.Read())
            {
                txtStudentID.Items.Add(sqlReader["StudentId"].ToString());
            }

            sqlReader.Close();
            sqlConnect.Close();
        }

        private void txtStudentID_SelectedIndexChanged(object sender, EventArgs e)
        {
            sqlConnect.Open();
            sqlCommand = new SqlCommand(
                "SELECT * FROM ClubMembers WHERE StudentId=@StudentId", sqlConnect);
            sqlCommand.Parameters.AddWithValue("@StudentId", txtStudentID.Text);

            sqlReader = sqlCommand.ExecuteReader();

            if (sqlReader.Read())
            {
                txtFirstName.Text = sqlReader["FirstName"].ToString();
                txtMiddleName.Text = sqlReader["MiddleName"].ToString();
                txtLastName.Text = sqlReader["LastName"].ToString();
                txtAge.Text = sqlReader["Age"].ToString();
                cbGender.Text = sqlReader["Gender"].ToString();
                cbProgram.Text = sqlReader["Program"].ToString();
            }

            sqlReader.Close();
            sqlConnect.Close();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            bool ok = clubQuery.UpdateStudent(
               long.Parse(txtStudentID.Text),
               txtFirstName.Text,
               txtMiddleName.Text,
               txtLastName.Text,
               int.Parse(txtAge.Text),
               cbGender.Text,
               cbProgram.Text
           );

            if (ok)
            {
                MessageBox.Show("Member updated successfully!");
                this.Close();
            }
        }
    }
}



