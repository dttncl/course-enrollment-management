using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Data
{
    // create and access connection string
    internal class Connect
    {
        private static String cliComConnectionString = GetConnectString();

        private static String GetConnectString()
        {
            SqlConnectionStringBuilder cs = new SqlConnectionStringBuilder();
            cs.DataSource = "(local)";
            cs.InitialCatalog = "College1en";
            cs.UserID = "sa";
            cs.Password = "sysadm";
            return cs.ConnectionString;
        }

        internal static String ConnectionString
        {
            get => cliComConnectionString;
        }
    }

    // manage data tables
    internal class DataTables
    {
        // load adapters
        private static SqlDataAdapter adapterPrograms = InitAdapterPrograms();
        private static SqlDataAdapter adapterCourses = InitAdapterCourses();
        private static SqlDataAdapter adapterStudents = InitAdapterStudents();
        private static SqlDataAdapter adapterEnrollments = InitAdapterEnrollments();
        private static SqlDataAdapter adapterDisplayEnrollments = InitAdapterDisplayEnrollments();


        // fill up the tables and load dataset
        private static DataSet ds = InitDataSet();

        private static SqlDataAdapter InitAdapterPrograms()
        {
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM PROGRAMS ORDER BY ProgId", Connect.ConnectionString);

            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
            adapter.UpdateCommand = builder.GetUpdateCommand();

            return adapter;
        }

        private static SqlDataAdapter InitAdapterCourses()
        {
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM COURSES ORDER BY CId", Connect.ConnectionString);

            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
            builder.ConflictOption = ConflictOption.OverwriteChanges;
            adapter.UpdateCommand = builder.GetUpdateCommand();

            return adapter;
        }

        private static SqlDataAdapter InitAdapterStudents()
        {
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM STUDENTS ORDER BY StId", Connect.ConnectionString);

            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
            builder.ConflictOption = ConflictOption.OverwriteChanges;
            adapter.UpdateCommand = builder.GetUpdateCommand();

            return adapter;
        }

        private static SqlDataAdapter InitAdapterEnrollments()
        {
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM ENROLLMENTS ORDER BY StId, CId", Connect.ConnectionString);

            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
            builder.ConflictOption = ConflictOption.OverwriteChanges;
            adapter.UpdateCommand = builder.GetUpdateCommand();
            
            return adapter;
        }

        private static SqlDataAdapter InitAdapterDisplayEnrollments()
        {
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT Enrollments.StId, Students.StName,Enrollments.CId, Courses.CName, Programs.ProgId, Programs.ProgName, FinalGrade " +
                "FROM ENROLLMENTS " +
                "INNER JOIN STUDENTS ON Enrollments.StId = Students.StId " +
                "INNER JOIN COURSES ON Enrollments.CId = Courses.CId " +
                "INNER JOIN PROGRAMS ON Courses.ProgId = Programs.ProgId;"
                , Connect.ConnectionString);

            return adapter;
        }

        private static DataSet InitDataSet()
        {
            DataSet ds = new DataSet();

            loadPrograms(ds);
            loadCourses(ds);
            loadStudents(ds);
            loadEnrollments(ds);
            loadDisplayEnrollments(ds);

            return ds;
        }
        
        private static void loadPrograms(DataSet ds)
        {
            adapterPrograms.MissingSchemaAction = MissingSchemaAction.AddWithKey;
            adapterPrograms.Fill(ds, "Programs");
        }

        private static void loadCourses(DataSet ds)
        {
            adapterCourses.MissingSchemaAction = MissingSchemaAction.AddWithKey;
            adapterCourses.Fill(ds, "Courses");

            ForeignKeyConstraint cFK = new ForeignKeyConstraint("CoursesFK",
                new DataColumn[]
                {
                    ds.Tables["Programs"].Columns["ProgId"]
                },
                new DataColumn[]
                {
                    ds.Tables["Courses"].Columns["ProgId"]
                }
            );

            cFK.DeleteRule = Rule.Cascade;
            cFK.UpdateRule = Rule.Cascade;

            ds.Tables["Courses"].Constraints.Add(cFK);
        }

        private static void loadStudents(DataSet ds)
        {
            adapterStudents.MissingSchemaAction = MissingSchemaAction.AddWithKey;
            adapterStudents.Fill(ds, "Students");

            ForeignKeyConstraint stFK = new ForeignKeyConstraint("StudentsFK",
                new DataColumn[]
                {
                    ds.Tables["Programs"].Columns["ProgId"]
                },
                new DataColumn[]
                {
                    ds.Tables["Students"].Columns["ProgId"]
                }
            );

            stFK.DeleteRule = Rule.None;
            stFK.UpdateRule = Rule.Cascade;

            ds.Tables["Students"].Constraints.Add(stFK);
        }

        private static void loadEnrollments(DataSet ds)
        {
            adapterEnrollments.MissingSchemaAction = MissingSchemaAction.AddWithKey;
            adapterEnrollments.Fill(ds, "Enrollments");

            // FK for Students
            ForeignKeyConstraint enrollStFK = new ForeignKeyConstraint("EnrollmentsFK_1",
                new DataColumn[]
                {
                    ds.Tables["Students"].Columns["StId"]
                },
                new DataColumn[]
                {
                    ds.Tables["Enrollments"].Columns["StId"]
                }
            );

            enrollStFK.DeleteRule = Rule.Cascade;
            enrollStFK.UpdateRule = Rule.Cascade;

            ds.Tables["Enrollments"].Constraints.Add(enrollStFK);


            // FK for Courses
            ForeignKeyConstraint enrollCourseFK = new ForeignKeyConstraint("EnrollmentsFK_2",
                new DataColumn[]
                {
                    ds.Tables["Courses"].Columns["CId"]
                },
                new DataColumn[]
                {
                    ds.Tables["Enrollments"].Columns["CId"]
                }
            );

            enrollCourseFK.DeleteRule = Rule.None;
            enrollCourseFK.UpdateRule = Rule.None;

            ds.Tables["Enrollments"].Constraints.Add(enrollCourseFK);
            
        }

        private static void loadDisplayEnrollments(DataSet ds)
        {
            // adapterEnrollments.MissingSchemaAction = MissingSchemaAction.AddWithKey;
            adapterDisplayEnrollments.Fill(ds, "DisplayEnrollments");
            
            // FK for Students
            ForeignKeyConstraint myFK1 = new ForeignKeyConstraint("EnrollmentsFK_3",
                new DataColumn[]
                {
                    ds.Tables["Students"].Columns["StId"]
                },
                new DataColumn[]
                {
                    ds.Tables["DisplayEnrollments"].Columns["StId"]
                }
            );

            myFK1.DeleteRule = Rule.Cascade;
            myFK1.UpdateRule = Rule.Cascade;

            ds.Tables["DisplayEnrollments"].Constraints.Add(myFK1);


            // FK for Courses
            ForeignKeyConstraint myFK2 = new ForeignKeyConstraint("EnrollmentsFK_4",
                new DataColumn[]
                {
                    ds.Tables["Courses"].Columns["CId"]
                },
                new DataColumn[]
                {
                    ds.Tables["DisplayEnrollments"].Columns["CId"]
                }
            );

            myFK2.DeleteRule = Rule.None;
            myFK2.UpdateRule = Rule.None;

            ds.Tables["DisplayEnrollments"].Constraints.Add(myFK2);
            
        }


        // provide access to adapters
        internal static SqlDataAdapter getAdapterPrograms()
        {
            return adapterPrograms;
        }

        internal static SqlDataAdapter getAdapterCourses()
        {
            return adapterCourses;
        }

        internal static SqlDataAdapter getAdapterStudents()
        {
            return adapterStudents;
        }

        internal static SqlDataAdapter getAdapterEnrollments()
        {
            return adapterEnrollments;
        }

        internal static SqlDataAdapter getAdapterDisplayEnrollments()
        {
            return adapterDisplayEnrollments;
        }


        // provide access to dataset
        internal static DataSet getDataSet()
        {
            return ds;
        }

        // reload enrollments table
        internal static void ReloadEnrollments()
        {
            // clear data table
            ds.Tables["DisplayEnrollments"].Clear();

            // reload data table with data from database
            adapterDisplayEnrollments.Fill(ds, "DisplayEnrollments");  

        }

    }


    internal class Programs
    {
        private static SqlDataAdapter adapter = DataTables.getAdapterPrograms();
        private static DataSet ds = DataTables.getDataSet();

        internal static DataTable GetPrograms()
        {
            return ds.Tables["Programs"];
        }

        internal static int UpdatePrograms()
        {

            return (!ds.Tables["Programs"].HasErrors) ? adapter.Update(ds.Tables["Programs"]) : -1;
        }
    }

    internal class Courses
    {
        private static SqlDataAdapter adapter = DataTables.getAdapterCourses();
        private static DataSet ds = DataTables.getDataSet();

        internal static DataTable GetCourses()
        {
            return ds.Tables["Courses"];
        }

        internal static int UpdateCourses()
        {

            return (!ds.Tables["Courses"].HasErrors) ? adapter.Update(ds.Tables["Courses"]) : -1;
        }

        // filter courses based on student id
        internal static DataTable GetFilteredCourses(string sid)
        {

            // get customized courses based on student ID
            var query = (
                   from COURSES in ds.Tables["Courses"].AsEnumerable()
                   from PROGRAMS in ds.Tables["Programs"].AsEnumerable()
                   from STUDENTS in ds.Tables["Students"].AsEnumerable()
                   where COURSES.Field<string>("ProgId") == PROGRAMS.Field<string>("ProgId")
                   where COURSES.Field<string>("ProgId") == STUDENTS.Field<string>("ProgId")
                   where STUDENTS.Field<string>("StId") == sid
                   select new
                   {
                       CId = COURSES.Field<string>("CId"),
                       CName = COURSES.Field<string>("CName"),
                   });

            // create new data table with two columns
            DataTable FilteredCourses = new DataTable();
            FilteredCourses.Columns.Add("CId");
            FilteredCourses.Columns.Add("CName");

            // for each row in query
            foreach (var q in query)
            {
                // create an array of 'q' properties [CId,CName]
                object[] allFields = { q.CId, q.CName };

                // append to table row
                FilteredCourses.Rows.Add(allFields);
            }

            return FilteredCourses;

            /*
            // using adapter
            DataSet ds = new DataSet();
            SqlConnectionStringBuilder cs = new SqlConnectionStringBuilder();
            cs.DataSource = "(local)";
            cs.InitialCatalog = "College1en";
            cs.UserID = "sa";
            cs.Password = "sysadm";

            SqlDataAdapter adapter = new SqlDataAdapter("SELECT COURSES.CId, COURSES.CName FROM COURSES " +
                "INNER JOIN PROGRAMS ON COURSES.PROGID = PROGRAMS.PROGID " +
                "INNER JOIN STUDENTS ON STUDENTS.PROGID = PROGRAMS.PROGID " +
                "WHERE STID = '" + sid + "'", cs.ConnectionString);

            adapter.Fill(ds, "FilteredCourses");

            // populate cbo course
            return ds.Tables["FilteredCourses"];
            */
        }

    }

    internal class Students
    {
        private static SqlDataAdapter adapter = DataTables.getAdapterStudents();
        private static DataSet ds = DataTables.getDataSet();

        internal static DataTable GetStudents()
        {
            return ds.Tables["Students"];
        }

        internal static int UpdateStudents()
        {
            return (!ds.Tables["Students"].HasErrors) ? adapter.Update(ds.Tables["Students"]) : -1;
        }
    }

    internal class Enrollments
    {
        private static SqlDataAdapter adapter = DataTables.getAdapterEnrollments();
        private static DataSet ds = DataTables.getDataSet();

        internal static DataTable GetEnrollments()
        {
            return ds.Tables["Enrollments"];
        }

        internal static DataTable GetDisplayEnrollments()
        {
            return ds.Tables["DisplayEnrollments"];
        }

        internal static int UpdateEnrollments()
        {
            return (!ds.Tables["Enrollments"].HasErrors) ? adapter.Update(ds.Tables["Enrollments"]) : -1;
        }

    }



}
