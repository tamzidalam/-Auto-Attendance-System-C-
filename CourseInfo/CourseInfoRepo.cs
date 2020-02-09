using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseInfo
{
    class CourseInfoRepo
    {
        string select, insert, delete, update, validation;

        internal string selectAllCourseInfo()
        {
            return select = "Select * from CourseInfo;";
        }

        internal string deleteRowCourseInfo(string cN)
        {
            return delete = "Delete from CourseInfo where courseName= '" + cN + "';";
        }

        internal string searchCourseInfo(string search)
        {
            return select = "Select * from CourseInfo where courseName like '" +search+ "%' or courseCode like '" + search + "%' or section like '" + search + "%' ;";
        }

        internal string updateRowCourseInfo(string cC, string cN, string sec)
        {
            return update = "update CourseInfo set courseName= '" + cN + "', section='" + sec + "' where courseCode='" + cC + "';";
        }

        internal string insertRowCourseInfo(string cC, string cN, string sec)
        {
            return insert = "Insert into CourseInfo (courseCode, CourseName, section) values('" + cC + "', '" + cN + "', '" + sec + "');";
        }

        internal string validationSelectCourseInfo(string cC, string cN, string sec)
        {
            return validation = "Select * from CourseInfo where courseCode ='" + cC + "' and courseName = '" + cN + "' and section ='" + sec + "';";
        }

        internal string selectByCourseId(int id)
        {
            return select = "Select * from CourseInfo where id ='" + id + "';";
        }
    }
}
