using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace CourseInfo
{
    public partial class SpecificReport : Form
    {
        public Report report { get; set; }
        private DataAccessDB Da { get; set; }
        public delegate void setStudentIdAttendance(string stdDate, string stdId, int course);
      //  public setStudentIdAttendance setStudentDate;
        

        public SpecificReport()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {           
                //if (AttendanceValidation())
                //{
                //    string stdId = dgvSpecific.Rows[0].Cells[0].Value.ToString();

                //    int courseId = Int32.Parse(dgvSpecific.Rows[0].Cells[1].Value.ToString());

                //    Report report = new Report();
                //    report.spReport = this;
                //    this.setStudentDate += new setStudentIdAttendance(report.setStudentDate);
                //    setStudentDate(specificDate.Text.ToString(), stdId, courseId);
                //    MessageBox.Show("Added");
                //}
                //else
                //{
                //    MessageBox.Show("Attendance Already Taken");
                //}                 
        }

       /* internal bool AttendanceValidation()
        {
            //this.Da.Ds = Da.ExecuteQuery(new SpecificReportRepo().selectFromAttendance());

            //int i = Da.Sda.Fill(Da.Ds);

            //if (i > 0)
            //{
            //    return false;
            //}
            //else
            //{
            //    return true;
            //}
        }*/

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
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