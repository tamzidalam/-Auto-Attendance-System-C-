using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CourseInfo
{
    public partial class Panel : Form
    {
       
        public Panel(string user)
        {
            InitializeComponent();
            lblstatus.Text = "User : " + user;

           
        }

        private void btnCourse_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            CourseInfo courseInfo = new CourseInfo(this);
            courseInfo.Visible = true;
           
        }

        private void btnStudent_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            StudentInfo student = new StudentInfo(this);
            student.Visible = true;
        }

        private void btnAttendance_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            TakeAttendance attendance = new TakeAttendance(this);
            attendance.Visible = true;
        }

        

        private void btnClose_Click(object sender, EventArgs e)
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

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            Login login = new Login();
            login.Visible = true;
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            Report report = new Report(this);
            report.Visible = true;
        }
    }
}
