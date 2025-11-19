using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClubRegistration
{
    public partial class FrmClubRegistration : Form
    {
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
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
        }
    }
}
