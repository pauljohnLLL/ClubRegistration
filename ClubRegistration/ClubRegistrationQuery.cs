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
            connectionString = @"Data Source=LAB-A-PC00";
            sqlConnect = new SqlConnection(connectionString);
            dataTable = new DataTable();
            bindingSource = new BindingSource();
        }

        public bool DisplayList()
        {
            try
            {
                string ViewClubMembers = @"Select StudentID,FirstName,MiddleName,LastName,Age,Gender,Program";
                sqlAdapter = new SqlDataAdapter(ViewClubMembers, sqlConnect);
                dataTable.Clear();
                sqlAdapter.Fill(dataTable);
                return true;
            }
            catch (Exception EX)
            {
                MessageBox.Show("Error loading: " + EX.Message);
                return false;

            }

        }
    }
}
