using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseInfo
{
    class TakeAttendanceRepo
    {
        string select, insert;

        internal string selectFromTakeAttendance(string attnid)
        {
            return select = "Select * from Attendance where studentId = '" + attnid + "' and aDate = cast(getdate() as Date);";
        }

        internal string selectFromTakeAttendance(string id, int id2)
        {
            return select = "Select StudentInfo.studentId,firstName,lastName,courseName,section from StudentInfo,CourseInfo,StudentCourse where StudentInfo.studentId = StudentCourse.studentId and CourseInfo.id = StudentCourse.courseId and studentInfo.studentId= '" + id + "' and StudentCourse.courseId= '" + id2 + "';";
        }

        internal string selectAllCourseInfo()
        {
            return select = "Select * from CourseInfo;";
        }

        internal string selectFromCourseInfo(string cN, string sec)
        {
            return select = "SELECT * FROM CourseInfo where courseName='" + cN + "'and section='" + sec + "';";
        }

        internal string selectFromCourseInfo(string cN)
        {
            return select = "SELECT * FROM CourseInfo where courseName='" + cN + "';";
        }

        internal string selectAllStudentInfo(string id)
        {
            return select = "Select * from StudentInfo where studentId = '" + id + "';";
        }

        internal string countRowStudentCourse(int id, string stdId)
        {
            return select = "Select count(*) from StudentCourse where courseId='" + id + "' and studentId='" + stdId + "';";
        }

        internal string selectAllReport(string stdId, int id)
        {
            return select = "Select * from report where studentId = '" + stdId + "' and  Id ='" + id + "' and date = CURDATE();";
        }

        internal string insertRowTakeAttendance(string sid, int cid)
        {
            return insert = "Insert into Attendance(studentId,courseId,aDate) values ('" + sid + "','" + cid + "',GETDATE());";
        }

        internal string insertRowReport(int id, string stdId)
        {
            return insert = "INSERT INTO report(courseId, studentId, date) VALUES ('" + id + "','" + stdId + "',CURDATE());";
        }
    }
}
