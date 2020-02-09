using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseInfo
{
    class DataAccessDB
    {
        private SqlConnection sqlcon;
        internal SqlConnection Sqlcon
        {
             get { return sqlcon; }
             set { sqlcon = value; }
        }

        private SqlCommand sqlcom;
        internal SqlCommand Sqlcom
        {
            get { return sqlcom; }
            set { sqlcom = value; }
        }

        private SqlDataReader myreader;
        internal SqlDataReader Myread
        {
            get { return myreader; }
            set { myreader = value; }

            
        }

        private SqlDataAdapter sda;
        internal SqlDataAdapter Sda
        {
            get { return sda; }
            set { sda = value; }
        }

        private DataSet ds;
        internal DataSet Ds
        {
            get { return ds; }
            set { ds = value; }
        }
        private DataRow dr;
        internal DataRow Dr
        {
            get;
            set;
        }

        private DataTable dt;
        internal DataTable Dt
        {
            get; set;
        }

        public DataAccessDB()
        {
            this.Sqlcon = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Semester5\Project\Database\DBproject.mdf;Integrated Security=True;Connect Timeout=30");
            if (Sqlcon != null && Sqlcon.State == ConnectionState.Closed)
            {
                Sqlcon.Open();
            }
        }

        internal void QueryText(string query)
        {
            this.Sqlcom = new SqlCommand(query, this.Sqlcon);
        }

        internal DataSet ExecuteQuery(string sql)
        {
            this.QueryText(sql);
            this.Sda = new SqlDataAdapter(this.Sqlcom);
            this.Ds = new DataSet();
            this.Sda.Fill(this.Ds);
            //this.Myread.Close();
          //  this.Myread.Dispose();
            return Ds;
        }

        internal DataTable ExecuteQuery(SqlCommand sqlcom)
        {
            //this.QueryText(sql);
            this.Sda = new SqlDataAdapter(Sqlcom);
            this.Dt = new DataTable();
            this.Sda.Fill(this.Dt);
            //this.Myread.Close();
            //  this.Myread.Dispose();
            return Dt;
        }

        internal int ExecuteUpdateQuery(string sql)
        {
            this.QueryText(sql);
            int u = this.Sqlcom.ExecuteNonQuery();
            return u;
        }
    }
}
