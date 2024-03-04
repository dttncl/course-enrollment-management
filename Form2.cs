using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Runtime.Remoting.Contexts;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Course_Enrollment_Management
{

    public partial class Form2 : Form
    {
        internal DataGridViewRow receivedRow;
        internal Action receivedAction;
        private bool init = false;

        // reload display enrollments table after modifications
        private void RefreshDisplayEnrollments()
        {
            Data.DataTables.ReloadEnrollments();
        }

        // property for receivedaction from form 1
        internal Action ReceivedAction { 
            get => receivedAction; 
            set => receivedAction = value; 
        }

        // function to determine which type of form to show
        private void ShowForm(Action ac)
        {
            switch (ac)
            {
                case Action.Add:
                    lblFormDescription.Text += " - Add New Student";

                    // populate cbo student
                    cboStId.DataSource = Data.Students.GetStudents();
                    cboStId.DisplayMember = "StId";
                    cboStId.ValueMember = "StName";

                    cboCId.DataSource = Data.Courses.GetCourses();
                    cboCId.DisplayMember = "CId";
                    cboCId.ValueMember = "CName";

                    init = true;

                    cboStId.Text = txtStName.Text = "";
                    cboCId.Text = txtCName.Text = "";

                    cboStId.Enabled = true;
                    cboCId.Enabled = true;

                    break;

                case Action.Modify:
                    lblFormDescription.Text += " - Modify Student Course";

                    // populate cbo student
                    cboStId.DataSource = Data.Students.GetStudents();
                    cboStId.DisplayMember = "StId";
                    cboStId.ValueMember = "StName";

                    // populate cbo course
                    cboCId.DataSource = Data.Courses.GetCourses();
                    cboCId.DisplayMember = "CId";
                    cboCId.ValueMember = "CName";
                    cboCId.Enabled = true;

                    cboStId.Text = receivedRow.Cells[0].Value.ToString();
                    txtStName.Text = receivedRow.Cells[1].Value.ToString();
                    cboCId.Text = receivedRow.Cells[2].Value.ToString();
                    txtCName.Text = receivedRow.Cells[3].Value.ToString();
                    txtFinalGrade.Text = receivedRow.Cells[6].Value.ToString();

                    init = true;
                    break;

                case Action.Delete:
                    lblFormDescription.Text += " - Delete Student Enrollment";

                    btnDelete.Visible = true;
                    btnSave.Visible = false;

                    cboStId.Text = receivedRow.Cells[0].Value.ToString();
                    txtStName.Text = receivedRow.Cells[1].Value.ToString();
                    cboCId.Text = receivedRow.Cells[2].Value.ToString();
                    txtCName.Text = receivedRow.Cells[3].Value.ToString();
                    txtFinalGrade.Text = receivedRow.Cells[6].Value.ToString();
                    break;

                case Action.Manage:
                    lblFormDescription.Text += " - Manage Final Grade";

                    txtFinalGrade.ReadOnly = false;

                    cboStId.Text = receivedRow.Cells[0].Value.ToString();
                    txtStName.Text = receivedRow.Cells[1].Value.ToString();
                    cboCId.Text = receivedRow.Cells[2].Value.ToString();
                    txtCName.Text = receivedRow.Cells[3].Value.ToString();
                    txtFinalGrade.Text = receivedRow.Cells[6].Value.ToString();
                    break;

                default:
                    break;
            }
        }

        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            ShowForm(ReceivedAction);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to discard changes? ", "Cancel ?", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            switch (ReceivedAction)
            {
                case Action.Add:
                    try
                    {
                        // get enrollments table
                        DataTable dtEnrollments = Data.Enrollments.GetEnrollments();

                        // create a new row
                        DataRow newRow = dtEnrollments.NewRow();

                        // add contents to row
                        newRow["StId"] = cboStId.Text;
                        newRow["CId"] = cboCId.Text;

                        // append the new row to enrollments table rows
                        dtEnrollments.Rows.Add(newRow);

                        // if database update is successful, refresh display enrollments table
                        if (Data.Enrollments.UpdateEnrollments() > 0)
                        {
                            MessageBox.Show("New Enrollment Added Successfully.", "Successful");
                            RefreshDisplayEnrollments();
                        }

                    } catch (Exception)
                    {
                        msgDataError();
                    }

                    Close();
                    break;

                case Action.Modify:
                    try
                    { 
                        string stid = receivedRow.Cells[0].Value.ToString();
                        string cid = receivedRow.Cells[2].Value.ToString();

                        // get enrollments table
                        DataTable dtEnrollments = Data.Enrollments.GetEnrollments();

                        // loop thru the rows of enrollments table
                        for (int i = 0; i < dtEnrollments.Rows.Count; i++)
                        {   
                            // if student id and course id are found,
                            if (dtEnrollments.Rows[i]["StId"].ToString() == stid && dtEnrollments.Rows[i]["CId"].ToString() == cid)
                            {
                                // modify enrollment
                                BusinessLayer.Enrollments.ModifyAnEnrollment(dtEnrollments.Rows[i], cboCId.Text);
                            }
                        }

                        if (Data.Enrollments.UpdateEnrollments() > 0)
                        {
                            MessageBox.Show("Course Updated Successfully.", "Successful");
                            RefreshDisplayEnrollments();
                        }

                    }
                    catch (Exception)
                    {
                        msgDataError();
                    }

                    Close();
                    break;

                case Action.Manage:
                    try
                    {
                        string stid = receivedRow.Cells[0].Value.ToString();
                        string cid = receivedRow.Cells[2].Value.ToString();

                        DataTable dtEnrollments = Data.Enrollments.GetEnrollments();
                        for (int i = 0; i < dtEnrollments.Rows.Count; i++)
                        {
                            if (dtEnrollments.Rows[i]["StId"].ToString() == stid && dtEnrollments.Rows[i]["CId"].ToString() == cid)
                            {
                                if (txtFinalGrade.Text == "")
                                {
                                    // if empty, assign null
                                    dtEnrollments.Rows[i]["FinalGrade"] = DBNull.Value;
                                } else
                                {
                                    dtEnrollments.Rows[i]["FinalGrade"] = txtFinalGrade.Text;
                                }
                                
                            }
                        }

                        if (BusinessLayer.Enrollments.UpdateEnrollments() > 0)
                        {
                            MessageBox.Show("Final Grade Updated Successfully.", "Successful");
                            RefreshDisplayEnrollments();
                        }

                    }
                    catch (Exception)
                    {
                        msgDataError();
                    }

                    Close();
                    break;

                default:
                    break;
            }

        }


        private void btnDelete_Click(object sender, EventArgs e)
        {
            string stid = receivedRow.Cells[0].Value.ToString();
            string cid = receivedRow.Cells[2].Value.ToString();
            bool found = false;

            DataTable dtEnrollments = Data.Enrollments.GetEnrollments();
            for (int i = 0; i < dtEnrollments.Rows.Count && found == false; i++)
            {
                if (dtEnrollments.Rows[i]["StId"].ToString() == stid && dtEnrollments.Rows[i]["CId"].ToString() == cid)
                {
                    if (MessageBox.Show("Are you sure you want to delete? ", "Cancel ?", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        BusinessLayer.Enrollments.DeleteAnEnrollment(dtEnrollments.Rows[i]);
                    }

                    found = true;                    
                }
            }
            

            if (Data.Enrollments.UpdateEnrollments() > 0)
            {
                MessageBox.Show("Row Deleted Successfully.", "Successful");
                RefreshDisplayEnrollments();
            }

            Close();
        }

        // data rule violation message
        internal static void msgDataError()
        {
            MessageBox.Show("Data Not Saved.\nPlease insert a valid entry to insert, update, or delete.", "Data Entry Error");
        }

        private void cboStId_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtStName.Text = cboStId.SelectedValue.ToString();
            if (init && cboCId.Text != "")
            {
                // verify the course selected
                int status = BusinessLayer.Enrollments.VerifyCourseProgram(cboCId.Text, cboStId.Text);

                // clear inputs if selected invalid course
                if (status < 0)
                {
                    cboStId.SelectedText = "";
                    txtStName.Text = "";
                }
            }
        }

        private void cboCId_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtCName.Text = cboCId.SelectedValue.ToString();
            if (init && cboStId.Text != "")
            {
                int status = BusinessLayer.Enrollments.VerifyCourseProgram(cboCId.Text, cboStId.Text);

                if (status < 0)
                {
                    cboCId.SelectedText = "";
                    txtCName.Text = "";
                }
            }

        }
    }
}
