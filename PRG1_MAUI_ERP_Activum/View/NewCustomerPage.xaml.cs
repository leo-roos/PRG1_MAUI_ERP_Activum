using PRG1_MAUI_ERP_Activum.Model;
using PRG1_MAUI_ERP_Activum.Services;

namespace PRG1_MAUI_ERP_Activum.View;

public partial class NewCustomerPage : ContentPage
{
    private readonly RegisterService _service = RegisterService.Instance;

    public NewCustomerPage()
	{
		InitializeComponent();
    }

    private void SaveButton_Clicked(object sender, EventArgs e)
    {
        if (!string.IsNullOrWhiteSpace(FirstNameEntry.Text) &&
            !string.IsNullOrWhiteSpace(LastNameEntry.Text) &&
            !string.IsNullOrWhiteSpace(EmailEntry.Text) &&
            !string.IsNullOrWhiteSpace(PhoneNumberEntry.Text))
        {

            var newCustomer = new Customer
            {
                FirstName = FirstNameEntry.Text,
                LastName = LastNameEntry.Text,
                Email = EmailEntry.Text,
                Phone = PhoneNumberEntry.Text
            };
            _service.AddCustomer(newCustomer);
            DisplayAlertAsync("Kund Tillagd", "Kunden har lagts till.", "Ok");

            FirstNameEntry.Text = string.Empty;
            LastNameEntry.Text = string.Empty;
            EmailEntry.Text = string.Empty;
            PhoneNumberEntry.Text = string.Empty;
        }
        else
        {
            DisplayAlertAsync("Fel", "Vänligen fyll i alla fält.", "Ok");
        }
    }

    private void GoBack_Clicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//CustomersPage");
    }
}