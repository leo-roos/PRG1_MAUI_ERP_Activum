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

    private void Save_Clicked(object sender, EventArgs e)
    {
        if (selectedCustomerService.SelectedInsurance == null)
        {
            return;
        }

        bool isCostChanged = false;
        bool isStartDateChanged = false;
        bool isEndDateChanged = false;

        if (int.TryParse(CostEntry.Text, out int newCost))
        {
            if (newCost != selectedCustomerService.SelectedInsurance.MonthlyCost)
            {
                selectedCustomerService.SelectedInsurance.MonthlyCost = newCost;
                isCostChanged = true;
            }
        }

        if (StartDatePicker.Date.HasValue)
        {
            string currentDate = selectedCustomerService.SelectedInsurance.StartDate.ToString("yyyy-MM-dd");
            string newDate = StartDatePicker.Date.Value.ToString("yyyy-MM-dd");
            if (currentDate != newDate)
            {
                selectedCustomerService.SelectedInsurance.StartDate = StartDatePicker.Date.Value;
                isStartDateChanged = true;
            }
        }

        if (EndDatePicker.Date.HasValue)
        {
            string currentDate = selectedCustomerService.SelectedInsurance.EndDate.ToString("yyyy-MM-dd");
            string newDate = EndDatePicker.Date.Value.ToString("yyyy-MM-dd");
            if (currentDate != newDate)
            {
                selectedCustomerService.SelectedInsurance.StartDate = EndDatePicker.Date.Value;
                isEndDateChanged = true;
            }
        }


        string updateMessage = "Uppdaterade följande: ";
        if (isCostChanged)
        {
            updateMessage += "Månads Pris, ";
        }
        if (isStartDateChanged)
        {
            updateMessage += "Start Datum, ";
        }
        if (isEndDateChanged)
        {
            updateMessage += "Slut Datum, ";
        }
        if (!isCostChanged && !isStartDateChanged && !isStartDateChanged)
        {
            updateMessage += "Inget";
        }

        DisplayAlertAsync("Uppdatera Försäkringen", updateMessage, "Ok");
    }

    private void GoBack_Clicked(object sender, EventArgs e)
    {
        selectedCustomerService.UpdateSelectedInsurace(null);
        selectedCustomerService.GotoPage();
    }
}