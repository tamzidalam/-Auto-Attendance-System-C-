using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseInfo
{
    class SpecificReportRepo
    {
        string select;

        internal string selectFromAttendance()
        {
            return select = "Select * from Attendance where adate = cast(getdate()) as Date)";
        }
    }
}
