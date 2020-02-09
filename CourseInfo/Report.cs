using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;
using System.Data.SqlClient;

namespace CourseInfo
{
    public partial class Report : Form
    {
        private Panel F1 { get; set; }
        public SpecificReport spReport { get; set; }
      
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Semester5\Project\Database\DBproject.mdf;Integrated Security=True;Connect Timeout=30");
        public Report()
        {
            InitializeComponent();
            FillcomboCourse();
           
        }
        public Report(Panel f1)
        {
            InitializeComponent();
            FillcomboCourse();
            this.F1 = f1;

        }
      
        void FillcomboCourse()
        {
            if (con != null && con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            SqlCommand cmd = new SqlCommand(new ReportRepo().selectAllCourseInfo(), con);

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
            finally
            {
                con.Close();
            }
        }

        private void cmbCourse_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (con != null && con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            cmbSection.Items.Clear();
            SqlCommand cmd = new SqlCommand(new ReportRepo().selectFromCourseInfo(cmbCourse.Text), con);

            SqlDataReader myreader = cmd.ExecuteReader();

            while (myreader.Read())
            {
                string sName = myreader["section"].ToString();
                cmbSection.Items.Add(sName);
            }
            myreader.Close();
            myreader.Dispose();
        }
        

        private void btnReport_Click_1(object sender, EventArgs e)
        {

            if (con != null && con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            SqlDataAdapter adp = new SqlDataAdapter(new ReportRepo().selectIdFromCourseInfo(cmbCourse.Text, cmbSection.Text), con);
            DataSet ds = new DataSet();
            adp.Fill(ds);

            DataRow dRow = ds.Tables[0].Rows[0];

            string st = dRow.ItemArray.GetValue(0).ToString();

            int id = Int32.Parse(st);

            SqlCommand cmd = new SqlCommand(new ReportRepo().attendanceQuery(id), con);
            cmd.ExecuteNonQuery();

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dgvReport.DataSource = dt;
            con.Close();
        }

        private void dgvReport_CellDoubleClick_1(object sender, DataGridViewCellEventArgs e)
        {
            SpecificReport spReport = new SpecificReport();
            spReport.report = this;
            string stdId = this.dgvReport.CurrentRow.Cells[0].Value.ToString();
            int courseId = Int32.Parse(this.dgvReport.CurrentRow.Cells[3].Value.ToString());

            con.Open();
            SqlCommand cmd = new SqlCommand(new ReportRepo().selectAllAttendance(stdId, courseId), con);
            cmd.ExecuteNonQuery();

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            spReport.dgvSpecific.DataSource = dt;
            spReport.Show();
            con.Close();
        }

       /* public void setStudentDate(string stdDate, string stdid, int course)
        {
            DateTime dtm = Convert.ToDateTime(stdDate);

            con.Open();
            SqlCommand cmd = new SqlCommand(new ReportRepo().insertRowAttendance(stdid, course, stdDate), con);
            cmd.ExecuteNonQuery();
            con.Close();

            int id = course;
            con.Open();

            SqlCommand cmd2 = new SqlCommand(new ReportRepo().attendanceQuery(id), con);
            cmd2.ExecuteNonQuery();

            SqlDataAdapter da = new SqlDataAdapter(cmd2);
            DataTable dt = new DataTable();
            da.Fill(dt);

            dgvReport.DataSource = dt;
            dgvReport.Update();
            dgvReport.Refresh();
            con.Close();
        }
        */
        private void dgvReport_SelectionChanged(object sender, EventArgs e)
        {

        }

        private void picBoxHome_Click(object sender, EventArgs e)
        {
            this.Visible = false;
             this.F1.Visible = true;
            
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

        private void BtnExcel_Click(object sender, EventArgs e)
        {
            Microsoft.Office.Interop.Excel.Application application= new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Workbook workbook = application.Workbooks.Add(Type.Missing);
            Microsoft.Office.Interop.Excel.Worksheet worksheet = null;
            worksheet = workbook.Sheets["Sheet1"];
            worksheet = workbook.ActiveSheet;
            worksheet.Name = "StudentReports";

            for (int i = 1; i <dgvReport.Columns.Count+1; i++)
            {
                worksheet.Cells[1,i] = dgvReport.Columns[i-1].HeaderText;

            }
            for (int i =0; i<dgvReport.Rows.Count;i++)
            {
                for (int j = 0; j < dgvReport.Columns.Count; j++)
                {
                    worksheet.Cells[i + 2, j + 1] = dgvReport.Rows[i].Cells[j].Value.ToString();

                }
            }
            var SaveFileDialoge = new SaveFileDialog();
            SaveFileDialoge.FileName = "StudentReports";
            SaveFileDialoge.DefaultExt = ".xlsx";
            if(SaveFileDialoge.ShowDialog()==DialogResult.OK)
            {
                workbook.SaveAs(SaveFileDialoge.FileName, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            }
            application.Quit();

        }

    }
}