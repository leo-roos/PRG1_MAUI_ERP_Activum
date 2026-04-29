using PRG1_MAUI_ERP_Activum.Model;
using PRG1_MAUI_ERP_Activum.Services;

namespace PRG1_MAUI_ERP_Activum.View;

public partial class SelectedCustomerInformationPage : ContentPage
{
    private readonly SelectedCustomerService selectedCustomerService = SelectedCustomerService.Instance;
    private readonly RegisterService _service = RegisterService.Instance;

    public SelectedCustomerInformationPage()
	{
		InitializeComponent();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        if (selectedCustomerService.SelectedCustomer != null)
        {
            Title.Text = $"{selectedCustomerService.SelectedCustomer.FirstName} {selectedCustomerService.SelectedCustomer.LastName} - Kund Information";

            FirstNameEntry.Text = selectedCustomerService.SelectedCustomer.FirstName;
            LastNameEntry.Text = selectedCustomerService.SelectedCustomer.LastName;
            EmailEntry.Text = selectedCustomerService.SelectedCustomer.Email;
            PhoneNumberEntry.Text = selectedCustomerService.SelectedCustomer.Phone;
        }
    }

    private void SaveButton_Clicked(object sender, EventArgs e)
    {
        if (!string.IsNullOrWhiteSpace(FirstNameEntry.Text) &&
            !string.IsNullOrWhiteSpace(LastNameEntry.Text) &&
            !string.IsNullOrWhiteSpace(EmailEntry.Text) &&
            !string.IsNullOrWhiteSpace(PhoneNumberEntry.Text))
        {

            var updatedCustomer = new Customer
            {
                Id = selectedCustomerService.SelectedCustomer.Id,
                FirstName = FirstNameEntry.Text,
                LastName = LastNameEntry.Text,
                Email = EmailEntry.Text,
                Phone = PhoneNumberEntry.Text
            };
            _service.UpdateCustomer(updatedCustomer);
            Title.Text = $"{updatedCustomer.FirstName} {updatedCustomer.LastName} - Kund Information";
            DisplayAlertAsync("Kund Uppdaterad", "Kundinformationen har uppdaterats.", "Ok");
        }
        else
        {
            DisplayAlertAsync("Fel", "Vänligen fyll i alla fält.", "Ok");
        }
    }

    private void GoBack_Clicked(object sender, EventArgs e)
    {
        selectedCustomerService.GotoPage();
    }
}