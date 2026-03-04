using PRG1_MAUI_ERP_Activum.Services;
using PRG1_MAUI_ERP_Activum.Model;
using System.Diagnostics;

namespace PRG1_MAUI_ERP_Activum.View;
public partial class InsurancePage : ContentPage
{
    private readonly RegisterService _service = RegisterService.Instance;

    public Insurance? SelectedInsurance = null;

    public InsurancePage()
	{
		InitializeComponent();
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();

        if (_service.chosenCustomer == null)
        {
            CustomerInsurances.ItemsSource = null;
            return;
        }

        InsurancesPicker.ItemsSource = _service.Insurances
            .Where(i => _service.chosenCustomer.Insurances.All(ci => ci.Type != i.Type))
            .Select(i => $"{i.Type} ({i.MonthlyCost} kr / månad)")
            .ToList();

        CustomerInsurances.ItemsSource = _service.GetInsurancesForCustomer(_service.chosenCustomer).ToList();
    }

    private void Button_Clicked_1(object sender, EventArgs e)
    {

    }

    private void CustomerInsurances_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var selected = e.CurrentSelection.FirstOrDefault() as Insurance;
        if (selected == null)
        {
            SelectedInsurance = null;
            return;
        }

        SelectedInsurance = selected;

        SelectedInsuranceLabel.Text = $"Vald Försäkring: {selected.Type}";
        StartDatePicker.Date = selected.StartDate;
        EndDatePicker.Date = selected.EndDate;
    }

    private void StartDatePicker_DateSelected(object sender, DateChangedEventArgs e)
    {
        if (SelectedInsurance != null && e.NewDate.HasValue)
        {
            SelectedInsurance.StartDate = e.NewDate.Value;
            CustomerInsurances.ItemsSource = _service.GetInsurancesForCustomer(_service.chosenCustomer).ToList();
        }
    }

    private void EndDatePicker_DateSelected(object sender, DateChangedEventArgs e)
    {
        if (SelectedInsurance != null && e.NewDate.HasValue)
        {
            SelectedInsurance.EndDate = e.NewDate.Value;
            CustomerInsurances.ItemsSource = _service.GetInsurancesForCustomer(_service.chosenCustomer).ToList();
        }
    }
}