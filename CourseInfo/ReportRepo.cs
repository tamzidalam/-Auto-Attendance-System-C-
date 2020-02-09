using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseInfo
{
    class ReportRepo
    {
        string select, insert;

        internal string selectAllCourseInfo()
        {
            return select = "Select * from courseInfo;";
        }

        internal string selectAllAttendance(string stdId, int courseId)
        {
            return select = "Select studentId, courseId, aDate from Attendance where studentId = '" + stdId + "' and courseId = '" + courseId + "' Order by 1 ;";
        }

        internal string selectFromCourseInfo(string cN)
        {
            return select = "SELECT * FROM CourseInfo where courseName='" + cN + "';";
        }

        internal string selectIdFromCourseInfo(string cN, string sec)
        {
            return select = "SELECT id FROM CourseInfo where courseName='" + cN + "'and section='" + sec + "';";
        }

        internal string attendanceQuery(int id)
        {
            return select = "Select x.studentId,firstName,lastName, x.courseId,courseName, section, Count(*) daysCount from Attendance x inner join CourseInfo y on x.courseId=y.id inner join studentinfo z on x.studentId = z.studentId where x.courseId = '" + id + "' group by x.studentId, firstName, lastName, x.courseId,courseName, section";
        }

        internal string attendanceSearch(string fname, string lname)                                                                                                          //  firstName like '" + fN + "%' or lastName like '" + lN + "%'or StudentInfo.studentId like'" + id+ "%'
        {
            return select = "Select x.studentId,firstName,lastName, x.courseId,courseName, section, Count(*) daysCount from Attendance x inner join CourseInfo y on x.courseId=y.id inner join studentinfo z on x.studentId = z.studentId where (x.firstName like '"+ fname + "%' or x.lastName like '" + lname + "%')group by x.studentId, firstName, lastName, x.courseId,courseName, section";
        }

        internal string insertRowAttendance(string stdid, int course, string stdDate)
        {
            return insert = "Insert into Attendance (studentId, courseId, aDate) values ('" + stdid + "','" + course + "','" + stdDate + "');";
        }
    }
}
