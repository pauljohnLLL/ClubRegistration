using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ClubRegistration
{
    public partial class FrmClubRegistration : Form
    {
        private ClubRegistrationQuery clubRegistrationQuery;

        int ID, Age, count = 0;
        string FirstName, MiddleName, LastName, Gender, Program;
        long StudentId;


        private void button3_Click(object sender, EventArgs e)
        {
            RefreshListOfClubMembers();
        }

        public FrmClubRegistration()
        {
            InitializeComponent();
        }

        private void FrmClubRegistration_Load(object sender, EventArgs e)
        {
            string[] ListOfProgram = new string[]
       {
            "BS Information Technology",
            "BS Computer Science",
            "BS Information Systems",
            "BS in Accountancy",
            "BS in Hospitality Management",
            "BS in Tourism Management"
       };

            for (int i = 0; i < 6; i++)
            {
                cbPrograms.Items.Add(ListOfProgram[i].ToString());
            }
            string[] ListOfGender = new string[]
       {
            "Male",
            "Female",

       };

            for (int i = 0; i < ListOfGender.Length; i++)
            {
                cbGender.Items.Add(ListOfGender[i].ToString());
            }

            clubRegistrationQuery = new ClubRegistrationQuery();
            RefreshListOfClubMembers();

        }
        private int RegistrationID()
        {
            count++;
            return count;
        }
        private void RefreshListOfClubMembers()
        {
            clubRegistrationQuery.DisplayList();
            dataGridView1.DataSource = clubRegistrationQuery.bindingSource;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                ID = RegistrationID();
                StudentId = long.Parse(txtStudentID.Text);
                FirstName = txtFirstName.Text;
                MiddleName = txtMiddleName.Text;
                LastName = txtLastName.Text;
                Age = int.Parse(txtAge.Text);
                Gender = cbGender.Text;
                Program = cbPrograms.Text;

                bool ok = clubRegistrationQuery.RegisterStudent(ID, StudentId, FirstName,
                                                                MiddleName, LastName,
                                                                Age, Gender, Program);

                if (ok)
                {
                    MessageBox.Show("Member Registered Successfully");
                    RefreshListOfClubMembers();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Select a record first before updating.");
                return;
            }

            try
            {
                long studentId = long.Parse(
                    dataGridView1.SelectedRows[0].Cells["StudentId"].Value.ToString()
                );

               FrmUpdateMember updateForm = new FrmUpdateMember();
               updateForm.ShowDialog();

                RefreshListOfClubMembers();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error selecting student ID for update: " + ex.Message);
            }
        }
    }
}

