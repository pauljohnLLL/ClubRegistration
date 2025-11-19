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
        private long _studentId;
        private ClubRegistrationQuery clubRegistrationQuery;

        public FrmUpdateMember(long studentId)
        {
            InitializeComponent();

            _studentId = studentId;
            clubRegistrationQuery = new ClubRegistrationQuery();
        }

        private void FrmUpdateMember_Load(object sender, EventArgs e)
        {
            LoadStudentData();
        }

        private void LoadStudentData()
        {
            var student = clubRegistrationQuery.GetStudentById(_studentId);

            if (student != null)
            {
                txtStudentID.Text = student.StudentId.ToString();
                txtLastName.Text = student.LastName;
                txtFirstName.Text = student.FirstName;
                txtMiddleName.Text = student.MiddleName;
                txtAge.Text = student.Age.ToString();
                cbGender.Text = student.Gender;
                cbProgram.Text = student.Program;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            bool ok = clubRegistrationQuery.UpdateStudent(
                _studentId,                   
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


