using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseInfo
{
    class StudentInfoRepo
    {
        string select, insert, delete, update, count;

        internal string selectAllStudentInfo()
        {
            return select = "Select StudentInfo.studentId,firstName,lastName,courseName,section from StudentInfo,CourseInfo,StudentCourse where StudentInfo.studentId = StudentCourse.studentId and CourseInfo.id = StudentCourse.courseId;";
        }
        
        internal string selectAllStudentInfo2(string id, int id2, string fN, string lN)
        {
            return select = "Select StudentInfo.studentId,firstName,lastName,courseName,section from StudentInfo,CourseInfo,StudentCourse where StudentInfo.studentId = StudentCourse.studentId and CourseInfo.id = StudentCourse.courseId and studentInfo.studentId= '" + id + "' and StudentCourse.courseId= '" + id2 + "' and firstName='" + fN + "' and lastName='" + lN + "';";
        
        }

        internal string selectAllStudentInfo2(int id2)
        {
            return select = "Select StudentInfo.studentId,firstName,lastName,courseName,section from StudentInfo,CourseInfo,StudentCourse where StudentInfo.studentId = StudentCourse.studentId and CourseInfo.id = StudentCourse.courseId and CourseInfo.id = '" + id2 + "'";
        }

        internal string selectAllStudentInfo3(string fN, string lN, string id)
        {
            return select = "select StudentInfo.studentId,firstName,lastName,courseName,section from studentInfo,courseInfo,StudentCourse where  StudentInfo.studentId = StudentCourse.studentId and CourseInfo.id = StudentCourse.courseId and (firstName like '" + fN + "%' or lastName like '" + lN + "%'or StudentInfo.studentId like'" + id+ "%');";
        }

        internal string selectAllCourseInfo()
        {
            return select = "Select * from CourseInfo;";
        }

        internal string deleteRowStudentInfo(string id)
        {
            return delete = "Delete from StudentInfo where studentId='" + id + "';";
        }

        internal string updateRowStudentInfo(string fN, string lN, string id)
        {
            return update = "Update  StudentInfo set firstName='" + fN + "',lastName='" + lN + "' where studentId='" + id + "';";
        }

        internal string insertRowStudentInfo(string autoId, string fN, string lN)
        {
            return insert = "Insert into StudentInfo values ('" + autoId + "','" + fN + "','" + lN + "');";
        }

        internal string insertRowStudentCourse(string autoId, int id)
        {
            return insert = "Insert into StudentCourse(studentId,courseId) values ('" + autoId + "','" + id + "');";
        }

        internal string countRowStudentInfo()
        {
            return count = "Select count(*) from StudentInfo;";
        }

        internal string updateStudentCourse(int cid, string id)
        {
            return update = "Update StudentCourse set courseId ='" + cid + "' where studentId='" + id + "';";
        }

        internal string selectFromCourseInfo(string cN, string sec)
        {
            return select = "SELECT * FROM CourseInfo where courseName='" + cN + "'and section='" + sec + "';";
        }

        internal string selectFromCourseInfo(string cN)
        {
            return select = "SELECT * FROM CourseInfo where courseName='" + cN + "';";
        }

        internal string deleteFromStudentCourse(int cid, string id)
        {
            return delete = "Delete from StudentCourse where courseId ='" + cid + "' and studentId='" + id + "';";
        }
        
    }
}
