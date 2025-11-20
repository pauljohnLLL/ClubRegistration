using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;

namespace ClubRegistration
{
    internal class ClubRegistrationQuery
    {
        private SqlConnection sqlConnect;
        private SqlCommand sqlCommand;
        private SqlDataAdapter sqlAdapter;
        private SqlDataReader sqlReader;
        public DataTable dataTable;
        public BindingSource bindingSource;
        private string connectionString;
        public string _FirstName, _MiddleName, _LastName, _Gender, _Program;
        int _Age;

        public ClubRegistrationQuery()
        {
            connectionString =
    @"Data Source=(LocalDB)\MSSQLLocalDB;
      AttachDbFilename=C:\Users\Administrator\Documents\ClubDB.mdf;
      Integrated Security=True;
      Connect Timeout=30";

            sqlConnect = new SqlConnection(connectionString);
            dataTable = new DataTable();
            bindingSource = new BindingSource();
        }

        public bool DisplayList()
        {
            try
            {
                string ViewClubMembers = @"Select StudentID,FirstName,MiddleName,LastName,Age,Gender,Program FROM ClubMembers";
                sqlAdapter = new SqlDataAdapter(ViewClubMembers, sqlConnect);
                dataTable.Clear();
                sqlAdapter.Fill(dataTable);
                bindingSource.DataSource = dataTable;
                return true;
            }
            catch (Exception EX)
            {
                MessageBox.Show("Error loading: " + EX.Message);
                return false;

            }

        }
        public bool RegisterStudent(int ID, long StudentID, string FirstName,
                                string MiddleName, string LastName,
                                int Age, string Gender, string Program)
        {
            try
            {
                sqlCommand = new SqlCommand(
                    "INSERT INTO ClubMembers (ID, StudentId, FirstName, MiddleName, LastName, Age, Gender, Program) " +
                    "VALUES(@ID, @StudentID, @FirstName, @MiddleName, @LastName, @Age, @Gender, @Program)",
                    sqlConnect);

                sqlCommand.Parameters.Add("@ID", SqlDbType.Int).Value = ID;
                sqlCommand.Parameters.Add("@StudentID", SqlDbType.BigInt).Value = StudentID;
                sqlCommand.Parameters.Add("@FirstName", SqlDbType.VarChar).Value = FirstName;
                sqlCommand.Parameters.Add("@MiddleName", SqlDbType.VarChar).Value = MiddleName;
                sqlCommand.Parameters.Add("@LastName", SqlDbType.VarChar).Value = LastName;
                sqlCommand.Parameters.Add("@Age", SqlDbType.Int).Value = Age;
                sqlCommand.Parameters.Add("@Gender", SqlDbType.VarChar).Value = Gender;
                sqlCommand.Parameters.Add("@Program", SqlDbType.VarChar).Value = Program;

                sqlConnect.Open();
                sqlCommand.ExecuteNonQuery();
                sqlConnect.Close();

                return true;
            }
            catch (Exception ex)
            {
                if (sqlConnect.State == ConnectionState.Open)
                    sqlConnect.Close();
                MessageBox.Show("Error inserting student: " + ex.Message);
                return false;
            }
        }
        public bool UpdateStudent(long StudentID, string FirstName, string MiddleName,
                          string LastName, int Age, string Gender, string Program)
        {
            try
            {
                sqlCommand = new SqlCommand(
                    "UPDATE ClubMembers SET FirstName=@FirstName, MiddleName=@MiddleName, " +
                    "LastName=@LastName, Age=@Age, Gender=@Gender, Program=@Program " +
                    "WHERE StudentId=@StudentID",
                    sqlConnect);

                sqlCommand.Parameters.Add("@FirstName", SqlDbType.VarChar).Value = FirstName;
                sqlCommand.Parameters.Add("@MiddleName", SqlDbType.VarChar).Value = MiddleName;
                sqlCommand.Parameters.Add("@LastName", SqlDbType.VarChar).Value = LastName;
                sqlCommand.Parameters.Add("@Age", SqlDbType.Int).Value = Age;
                sqlCommand.Parameters.Add("@Gender", SqlDbType.VarChar).Value = Gender;
                sqlCommand.Parameters.Add("@Program", SqlDbType.VarChar).Value = Program;
                sqlCommand.Parameters.Add("@StudentID", SqlDbType.BigInt).Value = StudentID;

                sqlConnect.Open();
                sqlCommand.ExecuteNonQuery();
                sqlConnect.Close();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating student: " + ex.Message);
                return false;
            }
        }

        public Student GetStudentById(long studentId)
        {
            try
            {
                string query = "SELECT ID, StudentId, FirstName, MiddleName, LastName, Age, Gender, Program " +
                               "FROM ClubMembers WHERE StudentId = @StudentId";

                sqlCommand = new SqlCommand(query, sqlConnect);
                sqlCommand.Parameters.Add("@StudentId", SqlDbType.BigInt).Value = studentId;

                sqlConnect.Open();
                sqlReader = sqlCommand.ExecuteReader();

                if (sqlReader.Read())
                {
                    Student s = new Student
                    {
                        ID = Convert.ToInt32(sqlReader["ID"]),
                        StudentId = Convert.ToInt64(sqlReader["StudentId"]),
                        FirstName = sqlReader["FirstName"].ToString(),
                        MiddleName = sqlReader["MiddleName"].ToString(),
                        LastName = sqlReader["LastName"].ToString(),
                        Age = Convert.ToInt32(sqlReader["Age"]),
                        Gender = sqlReader["Gender"].ToString(),
                        Program = sqlReader["Program"].ToString()
                    };

                    sqlReader.Close();
                    sqlConnect.Close();
                    return s;
                }

                sqlReader.Close();
                sqlConnect.Close();
                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading student: " + ex.Message);
                if (sqlConnect.State == ConnectionState.Open)
                    sqlConnect.Close();
                return null;
            }
        }
    }
}



