using OOPClassLibrary.Fiscal;

namespace Fiscal.WinForm
{
    public partial class Form1 : Form
    {
        private readonly Dictionary<PlaceOfBirthMethods, AbstractFiscalCodeBuilder> _fiscalCodeBuilders;

        public Form1()
        {
            InitializeComponent();
            PopulateComboboxes();
            
            _fiscalCodeBuilders =
                new Dictionary<PlaceOfBirthMethods, AbstractFiscalCodeBuilder>
                {
                    { PlaceOfBirthMethods.Dictionary, new FiscalCodeBuilderByDictionary() },
                    { PlaceOfBirthMethods.If, new FiscalCodeBuilderByIf() },
                    { PlaceOfBirthMethods.Database, new FiscalCodeBuilderByDatabase() }
                };
    }

        private void PopulateComboboxes()
        {
            PopulateGenderCombobox();
            PopulatePlaceOfBirthMethodCombobox();
        }

        private void PopulateGenderCombobox()
        {
            cmbGender.Items.Add(Gender.Female.ToString());
            cmbGender.Items.Add(Gender.Male.ToString());
            cmbGender.SelectedIndex = 0;
        }



        private void PopulatePlaceOfBirthMethodCombobox()
        {
            string[] values = Enum.GetNames<PlaceOfBirthMethods>();

            cmbPlaceOfBirthMethod.Items.AddRange(values);
            //cmbGender.Items.Add(Gender.Female.ToString());
            //cmbGender.Items.Add(Gender.Male.ToString());
            cmbPlaceOfBirthMethod.SelectedIndex = 0;
        }


        private void btnCreatePerson_Click(object sender, EventArgs e)
        {
            try
            {
                var person = BuildPersonFromControls();
                lstPeople.Items.Add(person);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private Person BuildPersonFromControls()
        {
            Gender gender = Enum.Parse<Gender>((cmbGender.SelectedItem as string)!);
            Person person =
                new Person
                (
                    txtFirstName.Text,
                    txtLastName.Text,
                    txtPlaceOfBirth.Text,
                    DateOnly.FromDateTime(dtpDateOfBirth.Value),
                    gender,
                    MaritalStatus.Unmarried
                );

            return person;
        }

        private void btnBuildFiscalCode_Click(object sender, EventArgs e)
        {
            try
            {
                PlaceOfBirthMethods p = Enum.Parse<PlaceOfBirthMethods>((cmbPlaceOfBirthMethod.SelectedItem as string)!);
                var person = BuildPersonFromControls();

                AbstractFiscalCodeBuilder fiscalCodeBuilder =
                    _fiscalCodeBuilders[p];

                string fiscalCode = fiscalCodeBuilder.Build(person);
                MessageBox.Show(fiscalCode);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
