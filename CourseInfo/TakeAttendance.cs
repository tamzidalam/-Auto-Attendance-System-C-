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
    public partial class TakeAttendance : Form
    {

        private Panel F1 { get; set; }

        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Semester5\Project\Database\DBproject.mdf;Integrated Security=True;Connect Timeout=30");

        public TakeAttendance()
        {
            InitializeComponent();
            if (con != null && con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            FillcomboCourse();

        }
        public TakeAttendance(Panel f1)
        {
            InitializeComponent();
            if (con != null && con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            FillcomboCourse();
            this.F1 = f1;
        }

        internal void insert()
        {
            try
            {
                if (con != null && con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                string sid = txtAttendanceId.Text;
                SqlDataAdapter adp = new SqlDataAdapter(new TakeAttendanceRepo().selectFromCourseInfo(cmbCourse.Text, cmbSection.Text), con);
                DataSet ds = new DataSet();
                adp.Fill(ds);

                DataRow dRow = ds.Tables[0].Rows[0];

                string st = dRow.ItemArray.GetValue(0).ToString();

                int id = Int32.Parse(st);

                SqlCommand cmd2 = new SqlCommand(new TakeAttendanceRepo().insertRowTakeAttendance(sid, id), con); 
                cmd2.ExecuteNonQuery();

            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
            finally
            {
                con.Close();

            }

        }

        internal bool AttendanceValidation()
        {
            SqlDataAdapter adp = new SqlDataAdapter(new TakeAttendanceRepo().selectFromTakeAttendance(txtAttendanceId.Text), con);
            DataSet ds = new DataSet();
            int i = adp.Fill(ds);

            if (i > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        void FillcomboCourse()
        {
            SqlCommand cmd = new SqlCommand(new TakeAttendanceRepo().selectAllCourseInfo(), con);
            SqlDataReader myreader;

            try
            {
                myreader = cmd.ExecuteReader();

                while (myreader.Read())
                {
                    string sName = myreader["courseName"].ToString();
                    cmbCourse.Items.Add(sName);
                }
                myreader.Close();
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message);
            }
        }
        internal bool StudentValidation()
        {
            if (con != null && con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            try
            {
                SqlDataAdapter adp = new SqlDataAdapter(new TakeAttendanceRepo().selectFromCourseInfo(cmbCourse.Text, cmbSection.Text), con);
                DataSet ds = new DataSet();
                adp.Fill(ds);

                DataRow dRow = ds.Tables[0].Rows[0];

                string st = dRow.ItemArray.GetValue(0).ToString();

                int id = Int32.Parse(st);

                adp = new SqlDataAdapter(new TakeAttendanceRepo().selectFromTakeAttendance(txtAttendanceId.Text, id), con);
                ds = new DataSet();
                int i = adp.Fill(ds);

                if (i == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);

            }
            finally
            {
                con.Close();
            }
            return false;
        }
        void attendance()
        {

            DataSet ds = new DataSet();
            string temp;
            int id = 0;
            string studentId = this.txtAttendanceId.Text.ToString();
            string courseName = cmbCourse.Text.ToString();
            string section = cmbSection.Text.ToString();

            SqlCommand cmd2 = new SqlCommand(new TakeAttendanceRepo().selectFromCourseInfo(this.cmbCourse.Text, this.cmbSection.Text), con);
            SqlDataReader myreader2 = cmd2.ExecuteReader();
            if (myreader2.HasRows)
            {
                myreader2.Read();
                temp = myreader2["id"].ToString();
                id = Convert.ToInt32(temp);

            }
            myreader2.Close();
            myreader2.Dispose();

            SqlCommand cmd3 = new SqlCommand(new TakeAttendanceRepo().countRowStudentCourse(id, studentId), con);

            myreader2 = cmd3.ExecuteReader();
            myreader2.Read();

            if (myreader2.HasRows)
            {
                myreader2.Close();
                myreader2.Dispose();
                try
                {
                    SqlCommand cmd4 = new SqlCommand(new TakeAttendanceRepo().selectAllReport(studentId, id), con);
                    SqlDataReader myreader3 = cmd4.ExecuteReader();
                    myreader3.Read();
                    if (!myreader3.HasRows)
                    {
                        cmd4 = new SqlCommand(new TakeAttendanceRepo().insertRowReport(id, studentId), con);
                        MessageBox.Show("Taken");
                    }
                    else
                    {
                        MessageBox.Show(" Already Taken");
                    }
                }
                catch (Exception exc)
                {
                    Console.WriteLine(exc.Message);
                }
            }
            else
            {
                MessageBox.Show(" Student is not enlisted on this course");
            }
            con.Close();
        }

        

        private void cmbCourse_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (con != null && con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            cmbSection.Items.Clear();
            SqlCommand cmd = new SqlCommand(new TakeAttendanceRepo().selectFromCourseInfo(cmbCourse.Text), con);

            SqlDataReader myreader = cmd.ExecuteReader();

            while (myreader.Read())
            {
                string sName = myreader["section"].ToString();
                cmbSection.Items.Add(sName);
            }

            myreader.Close();
            myreader.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FillcomboCourse();
        }

        private void picBoxHome_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            this.F1.Visible = true;
        }

        private void txtAttendanceId_TextChanged(object sender, EventArgs e)
        {
            con.Close();
            string id = txtAttendanceId.Text;
            if (id.Length == 10)
            {
                if (StudentValidation())
                {
                    if (AttendanceValidation())
                    {
                        insert();
                        this.txtAttendanceId.ResetText();
                        MessageBox.Show("Attendance Taken successfully", "Attendance");
                    }
                    else
                    {
                        MessageBox.Show("Attendance Already Taken");
                        this.txtAttendanceId.ResetText();
                    }
                }
                else
                {
                    MessageBox.Show("Student is not enrolled in this course");
                }
            }
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
    }
}