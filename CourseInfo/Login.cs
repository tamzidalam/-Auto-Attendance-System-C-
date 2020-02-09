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

namespace CourseInfo
{
    public partial class Login : Form
    {
        private DataAccessDB Da { get; set; }

        public Login()
        {
            InitializeComponent();
            this.Da = new DataAccessDB();
        }
        
        private void btnLogin_Click(object sender, EventArgs e)
        {
            log();
        }

        private void log()
        {
            string userName = txtboxUsername.Text.ToString();
            string password = txtboxPassword.Text.ToString();
            string cmbType = cmbUsers.Text.ToString();
            this.Da.Ds = this.Da.ExecuteQuery(new LoginRepo().countRowLogin(userName, password, cmbType));
            if (this.Da.Ds.Tables[0].Rows[0][0].ToString() == "1")
            {
                this.Da.Ds = this.Da.ExecuteQuery(new LoginRepo().selectLoginType(userName,password));

                if (this.Da.Ds.Tables[0].Rows[0][0].ToString() == "Admin")
                {
                    this.Visible = false;
                    Panel p = new Panel(cmbUsers.Text);

                    p.Show();
                }
                if (this.Da.Ds.Tables[0].Rows[0][0].ToString() == "Faculty")
                {
                    this.Visible = false;
                    Panel p = new Panel(cmbUsers.Text);
                    p.btnCourse.Hide();
                    p.btnStudent.Hide();
                    p.Show();
                }
                if (this.Da.Ds.Tables[0].Rows[0][0].ToString() == "Student")
                {
                    this.Visible = false;
                    Panel p = new Panel(cmbUsers.Text);
                    p.btnCourse.Hide();
                    p.btnStudent.Hide();
                    p.btnAttendance.Hide();

                    p.Show();
                }
            }
            else
            {
                MessageBox.Show("Please Check your UserName and Password", "Error");
            }



        }

      

        private void btnClose_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnMini_Click(object sender, EventArgs e)
        {

            if (this.WindowState != FormWindowState.Minimized)
            {
                this.WindowState = FormWindowState.Minimized;
            }
        }

      
    }
}
