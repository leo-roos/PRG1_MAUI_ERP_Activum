using PRG1_MAUI_ERP_Activum.Model;
using PRG1_MAUI_ERP_Activum.Services;

namespace PRG1_MAUI_ERP_Activum.View;

public partial class SelectedCustomerAddInsurancePage : ContentPage
{
    private readonly SelectedCustomerService selectedCustomerService = SelectedCustomerService.Instance;
    private readonly RegisterService _service = RegisterService.Instance;

    public SelectedCustomerAddInsurancePage()
	{
		InitializeComponent();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        if (selectedCustomerService.SelectedCustomer != null)
        {
            Title.Text = $"{selectedCustomerService.SelectedCustomer.FirstName} {selectedCustomerService.SelectedCustomer.LastName} - Lägg till Försäkring";
            TemplatePicker.ItemsSource = _service.Insurances;
        }
    }

    private void GoBack_Clicked(object sender, EventArgs e)
    {
        selectedCustomerService.GotoPage();
    }

    private void TemplatePicker_SelectedIndexChanged(object sender, EventArgs e)
    {
        var selectedInsurance = TemplatePicker.SelectedItem as Insurance;
        if (selectedInsurance != null)
        {
            TypeEntry.Text = selectedInsurance.Type;
            MonthlyCostEntry.Text = selectedInsurance.MonthlyCost.ToString();
            StartDatePicker.Date = selectedInsurance.StartDate;
            EndDatePicker.Date = selectedInsurance.EndDate;
        }
    }

    private void AddInsurance_Clicked(object sender, EventArgs e)
    {
        if (selectedCustomerService.SelectedCustomer != null)
        {
            if (!string.IsNullOrWhiteSpace(TypeEntry.Text) && int.TryParse(MonthlyCostEntry.Text, out int monthlyCost))
            {
                if (EndDatePicker.Date < StartDatePicker.Date)
                {
                    DisplayAlertAsync("Fel", "Slutdatum kan inte vara före startdatum.", "Ok");
                    return;
                }

                Insurance newInsurance = new Insurance
                {
                    Type = TypeEntry.Text,
                    MonthlyCost = monthlyCost,
                    StartDate = (DateTime)StartDatePicker.Date,
                    EndDate = (DateTime)EndDatePicker.Date
                };
                selectedCustomerService.SelectedCustomer.Insurances.Add(newInsurance);
                selectedCustomerService.GotoPage();
            }
            else
            {
                DisplayAlertAsync("Fel", "Ange en giltig typ och månadskostnad.", "Ok");
            }
        }
    }
}