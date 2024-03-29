namespace Fiscal.WinForm
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            lblFirstNAme = new Label();
            txtFirstName = new TextBox();
            txtLastName = new TextBox();
            lblLastName = new Label();
            txtPlaceOfBirth = new TextBox();
            lblPlaceOfBirth = new Label();
            dtpDateOfBirth = new DateTimePicker();
            lblDateOfBirth = new Label();
            btnCreatePerson = new Button();
            cmbGender = new ComboBox();
            lblGender = new Label();
            lstPeople = new ListBox();
            SuspendLayout();
            // 
            // lblFirstNAme
            // 
            lblFirstNAme.AutoSize = true;
            lblFirstNAme.Location = new Point(37, 50);
            lblFirstNAme.Name = "lblFirstNAme";
            lblFirstNAme.Size = new Size(77, 20);
            lblFirstNAme.TabIndex = 0;
            lblFirstNAme.Text = "First name";
            // 
            // txtFirstName
            // 
            txtFirstName.Location = new Point(120, 43);
            txtFirstName.Name = "txtFirstName";
            txtFirstName.Size = new Size(257, 27);
            txtFirstName.TabIndex = 1;
            // 
            // txtLastName
            // 
            txtLastName.Location = new Point(495, 43);
            txtLastName.Name = "txtLastName";
            txtLastName.Size = new Size(257, 27);
            txtLastName.TabIndex = 3;
            // 
            // lblLastName
            // 
            lblLastName.AutoSize = true;
            lblLastName.Location = new Point(412, 50);
            lblLastName.Name = "lblLastName";
            lblLastName.Size = new Size(76, 20);
            lblLastName.TabIndex = 2;
            lblLastName.Text = "Last name";
            // 
            // txtPlaceOfBirth
            // 
            txtPlaceOfBirth.Location = new Point(140, 96);
            txtPlaceOfBirth.Name = "txtPlaceOfBirth";
            txtPlaceOfBirth.Size = new Size(237, 27);
            txtPlaceOfBirth.TabIndex = 5;
            // 
            // lblPlaceOfBirth
            // 
            lblPlaceOfBirth.AutoSize = true;
            lblPlaceOfBirth.Location = new Point(37, 103);
            lblPlaceOfBirth.Name = "lblPlaceOfBirth";
            lblPlaceOfBirth.Size = new Size(97, 20);
            lblPlaceOfBirth.TabIndex = 4;
            lblPlaceOfBirth.Text = "Place of birth";
            // 
            // dtpDateOfBirth
            // 
            dtpDateOfBirth.Location = new Point(510, 94);
            dtpDateOfBirth.Name = "dtpDateOfBirth";
            dtpDateOfBirth.Size = new Size(242, 27);
            dtpDateOfBirth.TabIndex = 6;
            // 
            // lblDateOfBirth
            // 
            lblDateOfBirth.AutoSize = true;
            lblDateOfBirth.Location = new Point(412, 99);
            lblDateOfBirth.Name = "lblDateOfBirth";
            lblDateOfBirth.Size = new Size(94, 20);
            lblDateOfBirth.TabIndex = 7;
            lblDateOfBirth.Text = "Date of birth";
            // 
            // btnCreatePerson
            // 
            btnCreatePerson.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnCreatePerson.Location = new Point(658, 581);
            btnCreatePerson.Name = "btnCreatePerson";
            btnCreatePerson.Size = new Size(94, 59);
            btnCreatePerson.TabIndex = 8;
            btnCreatePerson.Text = "Create Person";
            btnCreatePerson.UseVisualStyleBackColor = true;
            btnCreatePerson.Click += btnCreatePerson_Click;
            // 
            // cmbGender
            // 
            cmbGender.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbGender.FormattingEnabled = true;
            cmbGender.Location = new Point(120, 166);
            cmbGender.Name = "cmbGender";
            cmbGender.Size = new Size(151, 28);
            cmbGender.TabIndex = 9;
            // 
            // lblGender
            // 
            lblGender.AutoSize = true;
            lblGender.Location = new Point(37, 169);
            lblGender.Name = "lblGender";
            lblGender.Size = new Size(57, 20);
            lblGender.TabIndex = 10;
            lblGender.Text = "Gender";
            // 
            // lstPeople
            // 
            lstPeople.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            lstPeople.FormattingEnabled = true;
            lstPeople.Location = new Point(37, 260);
            lstPeople.Name = "lstPeople";
            lstPeople.Size = new Size(727, 264);
            lstPeople.TabIndex = 11;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 664);
            Controls.Add(lstPeople);
            Controls.Add(lblGender);
            Controls.Add(cmbGender);
            Controls.Add(btnCreatePerson);
            Controls.Add(lblDateOfBirth);
            Controls.Add(dtpDateOfBirth);
            Controls.Add(txtPlaceOfBirth);
            Controls.Add(lblPlaceOfBirth);
            Controls.Add(txtLastName);
            Controls.Add(lblLastName);
            Controls.Add(txtFirstName);
            Controls.Add(lblFirstNAme);
            Name = "Form1";
            Text = "Person Form";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblFirstNAme;
        private TextBox txtFirstName;
        private TextBox txtLastName;
        private Label lblLastName;
        private TextBox txtPlaceOfBirth;
        private Label lblPlaceOfBirth;
        private DateTimePicker dtpDateOfBirth;
        private Label lblDateOfBirth;
        private Button btnCreatePerson;
        private ComboBox cmbGender;
        private Label lblGender;
        private ListBox lstPeople;
    }
}
