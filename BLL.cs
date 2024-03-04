using Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BusinessLayer
{
    internal class Programs
    {
        internal static int UpdatePrograms()
        {
            DataTable dt = Data.Programs.GetPrograms().GetChanges(DataRowState.Added | DataRowState.Modified);
            
            if (dt != null)
            {
                // check the primary key format
                Regex pkRegex = new Regex(@"^P\d{4}$");
                string new_pk = dt.Rows[0]["ProgId"].ToString();

                if (!(pkRegex.IsMatch(new_pk)))
                {
                    Course_Enrollment_Management.Form1.msgInvalidKeyFormat(); 
                    Data.Programs.GetPrograms().RejectChanges();
                    return -1;
                }
            }
            
            return Data.Programs.UpdatePrograms();
        }
    }

    internal class Courses
    {
        internal static int UpdateCourses()
        {
            DataTable dt = Data.Courses.GetCourses().GetChanges(DataRowState.Added | DataRowState.Modified);

            if (dt != null)
            {
                // check the primary key format
                Regex pkRegex = new Regex(@"^C\d{6}$");
                string new_pk = dt.Rows[0]["CId"].ToString();

                if (!(pkRegex.IsMatch(new_pk)))
                {
                    Course_Enrollment_Management.Form1.msgInvalidKeyFormat();
                    Data.Courses.GetCourses().RejectChanges();
                    return -1;
                }
            }

            return Data.Courses.UpdateCourses();
        }
    }

    internal class Students
    {
        internal static int UpdateStudents()
        {
            DataTable dt = Data.Students.GetStudents().GetChanges(DataRowState.Added | DataRowState.Modified);

            if (dt != null)
            {
                // check the primary key format
                Regex pkRegex = new Regex(@"^S\d{9}$");
                string new_pk = dt.Rows[0]["StId"].ToString();

                if (!(pkRegex.IsMatch(new_pk)))
                {
                    Course_Enrollment_Management.Form1.msgInvalidKeyFormat();
                    Data.Students.GetStudents().RejectChanges();
                    return -1;
                }
            }

            return Data.Students.UpdateStudents();
        }
    }

    internal class Enrollments
    {
        internal static int UpdateEnrollments()
        {

            DataTable dt = Data.Enrollments.GetEnrollments().GetChanges(DataRowState.Added | DataRowState.Modified);

            // check if final grade value is valid
            if (dt != null && dt.Select("FinalGrade < 0 OR FinalGrade > 100").Length > 0)
            {
                Course_Enrollment_Management.Form1.msgInvalidGrade();
                Data.Enrollments.GetEnrollments().RejectChanges();
                return -1;
            }

            return Data.Enrollments.UpdateEnrollments();
        }

        internal static void DeleteAnEnrollment(DataRow row)
        {
            // check if value of final grade is NOT null
            if (row["FinalGrade"] != DBNull.Value)
            {
                Course_Enrollment_Management.Form1.msgRestrictDelete();

            } else
            {
                row.Delete();
            }
        }


        internal static void ModifyAnEnrollment(DataRow row,string cid)
        {
            // check if value of final grade is NOT null
            if (row["FinalGrade"] != DBNull.Value)
            {
                Course_Enrollment_Management.Form1.msgRestrictModify();

            }
            else
            {
                row["CId"] = cid;
            }
        }

        // function that verifies the valid courses for enrolled student - program
        internal static int VerifyCourseProgram(string cid,string sid)
        {
            bool found = false;

            // get table of filtered courses by student id
            DataTable dtFiltered = Data.Courses.GetFilteredCourses(sid);

            // go thru all the rows
            for (int i = 0; i < dtFiltered.Rows.Count; i++)
            {
                if (cid == dtFiltered.Rows[i]["CId"].ToString())
                {
                    found = true;
                }
            }

            if (!found)
            {
                Course_Enrollment_Management.Form1.msgInvalidCourse();
                return -1;
            }

            return 1;

        }
    }

}
