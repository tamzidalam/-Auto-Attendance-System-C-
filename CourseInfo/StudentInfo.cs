﻿using System;
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
    public partial class StudentInfo : Form
    {
        public static int idGenarated = 0;
        private Panel F1 { get; set; }
        private DataAccessDB Da { get; set; }
      
        public StudentInfo()
        {
            InitializeComponent();
            this.Da = new DataAccessDB();
            this.PopulateGridView();
            this.FillcomboCourse();
        }

        public StudentInfo(Panel f1)
        {
            InitializeComponent();
            this.Da = new DataAccessDB();
            this.F1 = f1;
            this.PopulateGridView();
            FillcomboCourse();
        }

        public void PopulateGridView()
        {
                this.Da.Ds= this.Da.ExecuteQuery(new StudentInfoRepo().selectAllStudentInfo());
                this.dgvStudentInfo.AutoGenerateColumns = false;
                this.dgvStudentInfo.DataSource = Da.Ds.Tables[0];
        }

        public void PopulateGridView(string sql)
        {
                Da.Ds= this.Da.ExecuteQuery(sql);
                this.dgvStudentInfo.AutoGenerateColumns = false;
                dgvStudentInfo.DataSource = this.Da.Ds.Tables[0];
        }

        internal int autoId()
        {
            Da.Ds = this.Da.ExecuteQuery(new StudentInfoRepo().countRowStudentInfo());
            this.Da.Dr = Da.Ds.Tables[0].Rows[0];

            string st = this.Da.Dr.ItemArray.GetValue(0).ToString();

            int i = Int32.Parse(st);
            return i;

        }

        internal void InsertData()
        {
            try
            {
                int sid = autoId();

               string autoGeneratedId = "19-" + sid.ToString("D5") + "-1";
               this.Da.QueryText(new StudentInfoRepo().insertRowStudentInfo(autoGeneratedId.ToString(), txtFirstName.Text, txtLastName.Text));
               this.Da.Sqlcom.ExecuteNonQuery();

                Da.Ds = this.Da.ExecuteQuery(new StudentInfoRepo().selectFromCourseInfo(cmbCourse.Text, cmbSection.Text));

                this.Da.Dr = Da.Ds.Tables[0].Rows[0];

                string st = this.Da.Dr.ItemArray.GetValue(0).ToString();

                int id = Int32.Parse(st);

                this.Da.QueryText(new StudentInfoRepo().insertRowStudentCourse(autoGeneratedId, id));
                this.Da.Sqlcom.ExecuteNonQuery();

                MessageBox.Show("Inserted successfully","Inserted");

                refresh();

            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
            
        }


        internal void refresh()
        {
            this.txtId.Text = "";
            this.txtFirstName.Text = "";
            this.txtLastName.Text = "";
            this.cmbCourse.ResetText();
            this.cmbSection.ResetText();
        }

        internal void UpdateData()
        {
            try
            {
                string firstName = txtFirstName.Text.ToString();
                string lastName = txtLastName.Text.ToString();
                string id = txtId.Text.ToString();

                this.Da.QueryText(new StudentInfoRepo().updateRowStudentInfo(firstName, lastName, id));
                this.Da.Sqlcom.ExecuteNonQuery();
                Da.Ds = this.Da.ExecuteQuery(new StudentInfoRepo().selectFromCourseInfo(cmbCourse.Text, cmbSection.Text));

                Da.Dr= this.Da.Ds.Tables[0].Rows[0];

                string st = Da.Dr.ItemArray.GetValue(0).ToString();

                int cid = Int32.Parse(st);

                this.Da.QueryText(new StudentInfoRepo().updateStudentCourse(cid, id));

                this.Da.Sqlcom.ExecuteNonQuery();

                MessageBox.Show("Updated successfully","Update");
                PopulateGridView();
                refresh();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);

            }

        }


        internal void delete()
        {
            try
            {


                string firstName = txtFirstName.Text.ToString();
                string lastName = txtLastName.Text.ToString();
                string id = txtId.Text.ToString();
                Da.Ds = this.Da.ExecuteQuery(new StudentInfoRepo().selectFromCourseInfo(cmbCourse.Text, cmbSection.Text));

                Da.Dr = Da.Ds.Tables[0].Rows[0];

                string st = this.Da.Dr.ItemArray.GetValue(0).ToString();

                int cid = Int32.Parse(st);

                Da.Ds = this.Da.ExecuteQuery(new StudentInfoRepo().deleteFromStudentCourse(cid, id));
                this.Da.Sqlcom.ExecuteNonQuery();

                Da.Ds = this.Da.ExecuteQuery(new StudentInfoRepo().deleteRowStudentInfo(id));
                this.Da.Sqlcom.ExecuteNonQuery();


                refresh();
                MessageBox.Show("Deleted successfully","Delete");
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
           
            PopulateGridView();
        }
       
        internal bool validation()
        {
            try
            {
                Da.Ds = this.Da.ExecuteQuery(new StudentInfoRepo().selectFromCourseInfo(cmbCourse.Text, cmbSection.Text));
               this.Da.Dr= Da.Ds.Tables[0].Rows[0];

                string st = Da.Dr.ItemArray.GetValue(0).ToString();

                int id = Int32.Parse(st);

                Da.Ds= this.Da.ExecuteQuery(new StudentInfoRepo().selectAllStudentInfo2(txtId.Text.ToString(), id, txtFirstName.Text, txtLastName.Text));
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
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);

            }
            

            return false;
        }

        void FillcomboCourse()
        {
           Da.QueryText(new StudentInfoRepo().selectAllCourseInfo());

            try
            {

                Da.Myread = this.Da.Sqlcom.ExecuteReader();

                while (this.Da.Myread.Read())
                {
                    string sName = this.Da.Myread["courseName"].ToString();
                    cmbCourse.Items.Add(sName);

                }
                Da.Myread.Close();
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message);
            }
        }
        
                  

        private void btnAdd_Click(object sender, EventArgs e)
        {
            idGenarated++;
            try
            {
                if (validation())
                {
                    InsertData();
                }
                else
                {
                    MessageBox.Show("Already Enrolled");
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);

            }
           
            PopulateGridView();
        }

       


        private void dgvStudentInfo_DoubleClick(object sender, EventArgs e)
        {
            this.txtFirstName.Text = this.dgvStudentInfo.CurrentRow.Cells["firstName"].Value.ToString();
            this.txtLastName.Text = this.dgvStudentInfo.CurrentRow.Cells["lastName"].Value.ToString();
            this.txtId.Text = this.dgvStudentInfo.CurrentRow.Cells["studentId"].Value.ToString();
            this.cmbCourse.Text = this.dgvStudentInfo.CurrentRow.Cells["courseName"].Value.ToString();
            this.cmbSection.Text = this.dgvStudentInfo.CurrentRow.Cells["section"].Value.ToString();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try

            {
                delete();
            }

            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);

            }

        }

        private void btnUpdate_Click_1(object sender, EventArgs e)
        {
            try
            {

                if (validation())
                {
                    UpdateData();
                }
                else
                {
                    MessageBox.Show("Already Enrolled");
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);

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

        private void picBoxHome_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            this.F1.Visible = true;
        }

        private void cmbCourse_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbSection.Items.Clear();
            this.Da.QueryText(new StudentInfoRepo().selectFromCourseInfo(cmbCourse.Text));

           Da.Myread = Da.Sqlcom.ExecuteReader();

            while (Da.Myread.Read())
            {
                string sName = Da.Myread["section"].ToString();
                cmbSection.Items.Add(sName);
            }

           Da.Myread.Close();
           Da.Myread.Dispose();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
          this.PopulateGridView(new StudentInfoRepo().selectAllStudentInfo3(txtSearch.Text, txtSearch.Text, txtSearch.Text));

        }

       

        private void button1_Click(object sender, EventArgs e)
        {
            Da.Ds = this.Da.ExecuteQuery(new StudentInfoRepo().selectFromCourseInfo(cmbCourse.Text, cmbSection.Text));
            Da.Dr = Da.Ds.Tables[0].Rows[0];

            string st = Da.Dr.ItemArray.GetValue(0).ToString();

            int id = Int32.Parse(st);
            PopulateGridView(new StudentInfoRepo().selectAllStudentInfo2(id));
        }
    }


}
