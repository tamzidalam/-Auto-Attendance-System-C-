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
    internal  partial class CourseInfo : Form
    {
        private Panel F1 { get; set; }
        private DataAccessDB Da { get; set; }

        public CourseInfo()
        {
            InitializeComponent();
            this.Da = new DataAccessDB();
            this.PopulateGridView();
        }

        public CourseInfo(Panel f1)
        {
            InitializeComponent();
            this.F1 = f1;
            this.Da = new DataAccessDB();
            this.PopulateGridView();
        }


        public void PopulateGridView()
        {
            Da.Ds = this.Da.ExecuteQuery(new CourseInfoRepo().selectAllCourseInfo());
            this.dgvCourseInfo.AutoGenerateColumns = false;
            this.dgvCourseInfo.DataSource = Da.Ds.Tables[0];
        }

        public void PopulateGridView(string sql)
        {
            Da.Ds = this.Da.ExecuteQuery(sql);
            this.dgvCourseInfo.AutoGenerateColumns = false;
            this.dgvCourseInfo.DataSource = Da.Ds.Tables[0];
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            if (validation())
            {
                InsertData();
            }
            else
            {
                MessageBox.Show("Exists");
            }
        }

        internal void InsertData()
        {
            try
            {
                this.Da.ExecuteUpdateQuery(new CourseInfoRepo().insertRowCourseInfo(txtCourseCode.Text.ToString(), txtCourseName.Text.ToString(), txtCourseSection.Text.ToString()));

                MessageBox.Show("Insertion Done.");
                this.PopulateGridView();
                refresh();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        internal void UpdateData()
        {
            string courseName = txtCourseName.Text.ToString();
            string courseCode = txtCourseCode.Text.ToString();
            string section = txtCourseSection.Text.ToString();

             try
             {

              int i = this.Da.ExecuteUpdateQuery(new CourseInfoRepo().updateRowCourseInfo(courseCode, courseName, section));

                if (i >= 1)
                 {
                     MessageBox.Show(i + " Row Updated successfully : " + courseName);
                 }
                 else
                 {
                     MessageBox.Show("Course Table Updated Fail.");

                 }
            this.PopulateGridView();
            refresh();
            }
            catch (Exception exp)
            {
                Console.WriteLine("Error is : " + exp.Message);
            }
        }

        internal bool validation()
        {
            Da.Ds = this.Da.ExecuteQuery(new CourseInfoRepo().validationSelectCourseInfo(txtCourseCode.Text.ToString(), txtCourseName.Text.ToString(), txtCourseSection.Text.ToString()));
            int i = Da.Sda.Fill(Da.Ds);
            if (i > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        internal void DeleteData()
        {
            string coursename = txtCourseName.Text.ToString();

            coursename = this.dgvCourseInfo.CurrentRow.Cells["courseName"].Value.ToString();

            try
            {
                int i = this.Da.ExecuteUpdateQuery(new CourseInfoRepo().deleteRowCourseInfo(coursename));

                if (i >= 1)
                {
                    MessageBox.Show(i + " Deleted successfully, Name : " + coursename);
                }
                else
                {
                    MessageBox.Show("course Deleted Fail.", "Delete");
                }
                this.PopulateGridView();
                refresh();
            }
            catch (System.Exception exp)
            {
                Console.WriteLine("Error is : " + exp.Message);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (validation())
           {
                UpdateData();

            }
            else
            {
                MessageBox.Show("Already Exists");

            }
        }

            private void btnDelete_Click(object sender, EventArgs e)
        {
            DeleteData();
        }

        private void refresh()
        {
            this.txtCourseName.Text = "";
            this.txtCourseCode.Text = "";
            this.txtCourseSection.Text = "";
            this.txtSearch.Text = "";
        }


        private void dgvCourseInfo_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            this.txtCourseCode.Text = this.dgvCourseInfo.CurrentRow.Cells["courseCode"].Value.ToString();
            this.txtCourseName.Text = this.dgvCourseInfo.CurrentRow.Cells["courseName"].Value.ToString();
            this.txtCourseSection.Text = this.dgvCourseInfo.CurrentRow.Cells["section"].Value.ToString();
        }

        private void picBxHome_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            this.F1.Visible = true;
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            this.PopulateGridView(new CourseInfoRepo().searchCourseInfo(this.txtSearch.Text));
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