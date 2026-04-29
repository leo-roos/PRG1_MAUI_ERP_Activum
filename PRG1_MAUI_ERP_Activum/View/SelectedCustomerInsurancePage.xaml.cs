using PRG1_MAUI_ERP_Activum.Model;
using PRG1_MAUI_ERP_Activum.Services;
using System.Diagnostics;

namespace PRG1_MAUI_ERP_Activum.View;

public partial class SelectedCustomerInsurancePage : ContentPage
{
    private readonly SelectedCustomerService selectedCustomerService = SelectedCustomerService.Instance;
    private readonly RegisterService _service = RegisterService.Instance;

    public SelectedCustomerInsurancePage()
	{
		InitializeComponent();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        if (selectedCustomerService.SelectedCustomer != null && selectedCustomerService.SelectedInsurance != null)
        {
            Title.Text = $"{selectedCustomerService.SelectedCustomer.FirstName} {selectedCustomerService.SelectedCustomer.LastName} - {selectedCustomerService.SelectedInsurance.Type}";

            TypeEntry.Text = selectedCustomerService.SelectedInsurance.Type;
            CostEntry.Text = selectedCustomerService.SelectedInsurance.MonthlyCost.ToString();
            StartDatePicker.Date = selectedCustomerService.SelectedInsurance.StartDate;
            EndDatePicker.Date = selectedCustomerService.SelectedInsurance.EndDate;
        }
    }
    private async void Remove_Clicked(object sender, EventArgs e)
    {
        if (selectedCustomerService.SelectedCustomer == null || selectedCustomerService.SelectedInsurance == null)
        {
            return;
        }

        bool answer = await DisplayAlertAsync("Ta bort försäkringen", "Är du säker på att du vill ta bort denna försäkring?", "Ja", "Nej");
        if (answer)
        {
            selectedCustomerService.SelectedCustomer.Insurances.Remove(selectedCustomerService.SelectedInsurance);
            selectedCustomerService.UpdateSelectedInsurace(null);
            selectedCustomerService.GotoPage();
        }
    }

    private void Save_Clicked(object sender, EventArgs e)
    {
        if (selectedCustomerService.SelectedCustomer == null || selectedCustomerService.SelectedInsurance == null)
        {
            return;
        }

        if (!string.IsNullOrWhiteSpace(TypeEntry.Text)
            && int.TryParse(CostEntry.Text, out int newCost)
            && StartDatePicker.Date.HasValue
            && StartDatePicker.Date.HasValue)
        {
            selectedCustomerService.SelectedInsurance.Type = TypeEntry.Text;
            selectedCustomerService.SelectedInsurance.MonthlyCost = newCost;
            selectedCustomerService.SelectedInsurance.StartDate = StartDatePicker.Date.Value;
            selectedCustomerService.SelectedInsurance.EndDate = EndDatePicker.Date.Value;
            DisplayAlertAsync("Uppdatera Försäkringen", "Kundens försäkring har uppdaterats", "Ok");
        }
        else
        {
            DisplayAlertAsync("Fel", "Vänligen fyll i alla fält.", "Ok");
        }
    }

    private void GoBack_Clicked(object sender, EventArgs e)
    {
        selectedCustomerService.UpdateSelectedInsurace(null);
        selectedCustomerService.GotoPage();
    }

}