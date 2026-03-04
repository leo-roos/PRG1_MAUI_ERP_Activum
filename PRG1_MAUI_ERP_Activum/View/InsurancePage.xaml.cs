using PRG1_MAUI_ERP_Activum.Services;
using PRG1_MAUI_ERP_Activum.Model;
using System.Diagnostics;


namespace PRG1_MAUI_ERP_Activum.View;
public partial class InsurancePage : ContentPage
{
    private readonly RegisterService _service = RegisterService.Instance;

    public InsurancePage()
	{
		InitializeComponent();

		Debug.WriteLine(_service.chosenCustomer);
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
            .Where(i => _service.chosenCustomer.InsuranceId.Contains(i.Id))
            .Select(i => $"{i.Type} ({i.MonthlyCost} kr / månad)")
            .ToList();

        CustomerInsurances.ItemsSource = _service.GetInsurancesForCustomer(_service.chosenCustomer).ToList();
    }

    private void Button_Clicked_1(object sender, EventArgs e)
    {

    }

    private void CustomerInsurances_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {

    }
}