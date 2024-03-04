namespace Course_Enrollment_Management
{
    partial class Form2
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.cboStId = new System.Windows.Forms.ComboBox();
            this.txtStName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cboCId = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtCName = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtFinalGrade = new System.Windows.Forms.TextBox();
            this.lblFormDescription = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 168);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Student Id:";
            // 
            // cboStId
            // 
            this.cboStId.Enabled = false;
            this.cboStId.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cboStId.FormattingEnabled = true;
            this.cboStId.Location = new System.Drawing.Point(32, 201);
            this.cboStId.Name = "cboStId";
            this.cboStId.Size = new System.Drawing.Size(193, 28);
            this.cboStId.TabIndex = 1;
            this.cboStId.SelectedIndexChanged += new System.EventHandler(this.cboStId_SelectedIndexChanged);
            // 
            // txtStName
            // 
            this.txtStName.Location = new System.Drawing.Point(32, 119);
            this.txtStName.Name = "txtStName";
            this.txtStName.ReadOnly = true;
            this.txtStName.Size = new System.Drawing.Size(250, 26);
            this.txtStName.TabIndex = 2;
            this.txtStName.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(28, 86);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(116, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "Student Name:";
            // 
            // cboCId
            // 
            this.cboCId.Enabled = false;
            this.cboCId.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cboCId.FormattingEnabled = true;
            this.cboCId.Location = new System.Drawing.Point(426, 201);
            this.cboCId.Name = "cboCId";
            this.cboCId.Size = new System.Drawing.Size(193, 28);
            this.cboCId.TabIndex = 2;
            this.cboCId.SelectedIndexChanged += new System.EventHandler(this.cboCId_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(422, 168);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 20);
            this.label3.TabIndex = 4;
            this.label3.Text = "Course Id:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(422, 86);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(110, 20);
            this.label4.TabIndex = 7;
            this.label4.Text = "Course Name:";
            // 
            // txtCName
            // 
            this.txtCName.Location = new System.Drawing.Point(426, 119);
            this.txtCName.Name = "txtCName";
            this.txtCName.ReadOnly = true;
            this.txtCName.Size = new System.Drawing.Size(283, 26);
            this.txtCName.TabIndex = 6;
            this.txtCName.TabStop = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(815, 86);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(96, 20);
            this.label5.TabIndex = 9;
            this.label5.Text = "Final Grade:";
            // 
            // txtFinalGrade
            // 
            this.txtFinalGrade.Location = new System.Drawing.Point(797, 119);
            this.txtFinalGrade.Name = "txtFinalGrade";
            this.txtFinalGrade.ReadOnly = true;
            this.txtFinalGrade.Size = new System.Drawing.Size(128, 26);
            this.txtFinalGrade.TabIndex = 3;
            // 
            // lblFormDescription
            // 
            this.lblFormDescription.AutoSize = true;
            this.lblFormDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFormDescription.Location = new System.Drawing.Point(27, 21);
            this.lblFormDescription.Name = "lblFormDescription";
            this.lblFormDescription.Size = new System.Drawing.Size(304, 29);
            this.lblFormDescription.TabIndex = 10;
            this.lblFormDescription.Text = "Student Management Form";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(783, 269);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(162, 45);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.Transparent;
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnCancel.Location = new System.Drawing.Point(783, 330);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(162, 45);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.SystemColors.Control;
            this.btnDelete.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.btnDelete.Location = new System.Drawing.Point(783, 269);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(162, 45);
            this.btnDelete.TabIndex = 4;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Visible = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(985, 415);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.lblFormDescription);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtFinalGrade);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtCName);
            this.Controls.Add(this.cboCId);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtStName);
            this.Controls.Add(this.cboStId);
            this.Controls.Add(this.label1);
            this.Name = "Form2";
            this.Text = "Student Management Form";
            this.Load += new System.EventHandler(this.Form2_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboStId;
        private System.Windows.Forms.TextBox txtStName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cboCId;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtCName;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtFinalGrade;
        private System.Windows.Forms.Label lblFormDescription;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnDelete;
    }
}