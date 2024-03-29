using OOPClassLibrary.Fiscal;

namespace Fiscal.WinForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            cmbGender.Items.Add(Gender.Female.ToString());
            cmbGender.Items.Add(Gender.Male.ToString());
            cmbGender.SelectedIndex = 0;
        }

        private void btnCreatePerson_Click(object sender, EventArgs e)
        {
            try
            {

                Gender gender = Enum.Parse<Gender>((cmbGender.SelectedItem as string)!);
                Person person =
                    new Person
                    (
                        txtFirstName.Text,
                        txtLastName.Text,
                        txtPlaceOfBirth.Text,
                        string.Empty,
                        DateOnly.FromDateTime(dtpDateOfBirth.Value),
                        gender,
                        MaritalStatus.Unmarried
                    );

                lstPeople.Items.Add( person );
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }
    }
}
