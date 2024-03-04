using System;
using System.CodeDom.Compiler;
using System.Data;
using System.Windows.Forms;

namespace Course_Enrollment_Management
{
    
    enum Action
    {
        Add,
        Modify,
        Manage,
        Delete,
        Default
    }

    enum Tables
    {
        Programs,
        Courses,
        Students,
        DisplayEnrollments,
        Unknown
    }

    public partial class Form1 : Form
    {
        private bool OkToChange = false;
        public static DataGridViewRow SelectedRow { get; set; }

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.Dock = DockStyle.Fill;
        }

        // function to load datagridview
        private void LoadDataGridView(Tables selectedTable)
        {
            switch (selectedTable)
            {
                case Tables.Programs:
                    bindingSource1.DataSource = Data.Programs.GetPrograms();
                    bindingSource1.Sort = "ProgId";
                    dataGridView1.DataSource = bindingSource1;
                    break;
                case Tables.Courses:
                    bindingSource2.DataSource = Data.Courses.GetCourses();
                    bindingSource2.Sort = "CId";
                    dataGridView1.DataSource = bindingSource2;
                    break;
                case Tables.Students:
                    bindingSource3.DataSource = Data.Students.GetStudents();
                    bindingSource3.Sort = "StId";
                    dataGridView1.DataSource = bindingSource3;
                    break;
                case Tables.DisplayEnrollments:
                    bindingSource4.DataSource = Data.Enrollments.GetDisplayEnrollments();
                    bindingSource4.Sort = "StId";
                    dataGridView1.DataSource = bindingSource4;
                    break;
                case Tables.Unknown:
                    MessageBox.Show("Table Doesn't Exist");
                    break;
            }
        }

        // function to setup datagridview
        private void SetupDataGridView()
        {
            dataGridView1.ReadOnly = false;
            dataGridView1.AllowUserToAddRows = true;
            dataGridView1.AllowUserToDeleteRows = true;
            dataGridView1.RowHeadersVisible = true;
            dataGridView1.Dock = DockStyle.Fill;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void programsToolStripMenuItem_Click(object sender, EventArgs e)
        {            
            if (OkToChange)
            {

                SetupDataGridView();
                LoadDataGridView(Tables.Programs);

                dataGridView1.Columns["ProgId"].HeaderText = "Program ID";
                dataGridView1.Columns["ProgId"].DisplayIndex = 0;

                dataGridView1.Columns["ProgName"].HeaderText = "Program Name";
                dataGridView1.Columns["ProgName"].DisplayIndex = 1;
            }

        }

        private void coursesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (OkToChange)
            {
                SetupDataGridView();
                LoadDataGridView(Tables.Courses);

                dataGridView1.Columns["CId"].HeaderText = "Course ID";
                dataGridView1.Columns["CId"].DisplayIndex = 0;

                dataGridView1.Columns["CName"].HeaderText = "Course Name";
                dataGridView1.Columns["CName"].DisplayIndex = 1;

                dataGridView1.Columns["ProgId"].HeaderText = "Program Name";
                dataGridView1.Columns["ProgId"].DisplayIndex = 2; 
            }        
        }

        private void studentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (OkToChange)
            {
                SetupDataGridView();
                LoadDataGridView(Tables.Students);

                dataGridView1.Columns["StId"].HeaderText = "Student ID";
                dataGridView1.Columns["StId"].DisplayIndex = 0;

                dataGridView1.Columns["StName"].HeaderText = "Student Name";
                dataGridView1.Columns["StName"].DisplayIndex = 1;

                dataGridView1.Columns["ProgId"].HeaderText = "Program Name";
                dataGridView1.Columns["ProgId"].DisplayIndex = 2;
            }
        }

        private void enrollmentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (OkToChange)
            {
                dataGridView1.ReadOnly = true;
                dataGridView1.AllowUserToAddRows = false;
                dataGridView1.AllowUserToDeleteRows = false;
                dataGridView1.RowHeadersVisible = true;
                dataGridView1.Dock = DockStyle.Fill;
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                LoadDataGridView(Tables.DisplayEnrollments);

                dataGridView1.Columns["StId"].HeaderText = "Student ID";
                dataGridView1.Columns["StId"].DisplayIndex = 0;

                dataGridView1.Columns["StName"].HeaderText = "Student Name";
                dataGridView1.Columns["StName"].DisplayIndex = 1;

                dataGridView1.Columns["CId"].HeaderText = "Course ID";
                dataGridView1.Columns["CId"].DisplayIndex = 2;

                dataGridView1.Columns["CName"].HeaderText = "Course Name";
                dataGridView1.Columns["CName"].DisplayIndex = 3;

                dataGridView1.Columns["ProgId"].HeaderText = "Program ID";
                dataGridView1.Columns["ProgId"].DisplayIndex = 4;

                dataGridView1.Columns["ProgName"].HeaderText = "Program Name";
                dataGridView1.Columns["ProgName"].DisplayIndex = 5;

                dataGridView1.Columns["FinalGrade"].HeaderText = "Final Grade";
                dataGridView1.Columns["FinalGrade"].DisplayIndex = 6;
            }
        }

        private void bindingSource1_CurrentChanged(object sender, EventArgs e)
        {

            if (BusinessLayer.Programs.UpdatePrograms() == -1)
            {
                bindingSource1.ResetBindings(false);
            }
        }

        private void bindingSource2_CurrentChanged(object sender, EventArgs e)
        {
            if (BusinessLayer.Courses.UpdateCourses() == -1)
            {
                bindingSource2.ResetBindings(false);
            }
        }

        private void bindingSource3_CurrentChanged(object sender, EventArgs e)
        {
            if (BusinessLayer.Students.UpdateStudents() == -1)
            {
                bindingSource3.ResetBindings(false);
            }
        }

        private void bindingSource4_CurrentChanged(object sender, EventArgs e)
        {
            if (BusinessLayer.Enrollments.UpdateEnrollments() == -1)
            {
                bindingSource4.ResetBindings(false);
            }
        }

        private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            msgDataError();
            e.Cancel = false;
            OkToChange = false;
        }

        private void menuStrip1_Click(object sender, EventArgs e)
        {
            OkToChange = true;
            BindingSource temp = (BindingSource)dataGridView1.DataSource;
            Validate();

            if (temp == bindingSource1)
            {
                if (BusinessLayer.Programs.UpdatePrograms() == -1)
                {
                    OkToChange = false;
                }

            }
            else if (temp == bindingSource2)
            {
                if (BusinessLayer.Courses.UpdateCourses() == -1)
                {
                    Validate();
                    OkToChange = false;
                }
            }
            else if (temp == bindingSource3)
            {
                if (BusinessLayer.Students.UpdateStudents() == -1)
                {
                    Validate();
                    OkToChange = false;
                }
            }
        }
        
        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            Form2 addForm = new Form2();
            addForm.ReceivedAction = Action.Add;
            addForm.ShowDialog();
            LoadDataGridView(Tables.DisplayEnrollments);
        }


        private void modifyToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (SelectedRow != null)
            {
                Form2 modifyForm = new Form2();
                modifyForm.ReceivedAction = Action.Modify;
                modifyForm.receivedRow = SelectedRow;
                modifyForm.ShowDialog();

            } else
            {
                MessageBox.Show("No student selected. Please select a row to proceed.","Warning!");
            }

        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SelectedRow != null)
            {
                Form2 deleteForm = new Form2();
                deleteForm.ReceivedAction = Action.Delete;
                deleteForm.receivedRow = SelectedRow;
                deleteForm.ShowDialog();
            }
            else
            {
                MessageBox.Show("No student selected. Please select a row to proceed.", "Warning!");
            }
        }

        private void manageFinalGradeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SelectedRow != null)
            {
                Form2 manageStudForm = new Form2();
                manageStudForm.ReceivedAction = Action.Manage;
                manageStudForm.receivedRow = SelectedRow;
                manageStudForm.ShowDialog();
            }
            else
            {
                MessageBox.Show("No student selected. Please select a row to proceed.", "Warning!");
            }
        }


        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            // save the selected row
            SelectedRow = dataGridView1.Rows[e.RowIndex];
        }

        // data rule violation message
        internal static void msgDataError()
        {
            MessageBox.Show("Invalid Data Action.\nPlease select a valid entry to insert, update, or delete.", "Data Modification Error");
        }

        // business rule violation messages
        internal static void msgInvalidCourse()
        {
            MessageBox.Show("Student cannot enroll to this course.\nPlease select courses available in program.","New Enrollment Error");
        }

        internal static void msgInvalidGrade()
        {
            MessageBox.Show("Invalid Final Grade Input.\nPlease enter a valid grade from 0 to 100.", "Final Grade Error");
        }

        internal static void msgRestrictDelete()
        {
            MessageBox.Show("This enrollment cannot be removed.\nPlease remove Final Grade before confirming deletion.", "Deletion Error");
        }

        internal static void msgRestrictModify()
        {
            MessageBox.Show("This enrollment cannot be modified.\nPlease remove Final Grade before confirming modification.", "Modification Error");
        }

        internal static void msgInvalidKeyFormat()
        {
            MessageBox.Show("Please follow proper primary key format.\n(P0000 - Programs, C000000 - Courses, S000000000 - Student)", "Invalid Primary Key Format");
        }

    }
}
