using PRG1_MAUI_ERP_Activum.Services;
using PRG1_MAUI_ERP_Activum.Model;

namespace PRG1_MAUI_ERP_Activum.View;
public partial class SelectedInsurancePage : ContentPage
{
    private readonly SelectedInsuranceService selectedInsuranceService = SelectedInsuranceService.Instance;
    private readonly RegisterService _service = RegisterService.Instance;

    public SelectedInsurancePage()
	{
		InitializeComponent();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        if (selectedInsuranceService.SelectedInsurance != null)
        {
            Title.Text = $"Försäkringar - {selectedInsuranceService.SelectedInsurance.Type}";

            TypeEntry.Text = selectedInsuranceService.SelectedInsurance.Type;
            CostEntry.Text = selectedInsuranceService.SelectedInsurance.MonthlyCost.ToString();
        }
    }

    private void Save_Clicked(object sender, EventArgs e)
    {
        if (selectedInsuranceService.SelectedInsurance == null)
        {
            return;
        }

        if (!string.IsNullOrWhiteSpace(TypeEntry.Text) && int.TryParse(CostEntry.Text, out int newCost))
        {
            selectedInsuranceService.SelectedInsurance.Type = TypeEntry.Text;
            selectedInsuranceService.SelectedInsurance.MonthlyCost = newCost;
            DisplayAlertAsync("Uppdatera Försäkringen", "Försäkringen har uppdaterats", "Ok");
        } else
        {
            DisplayAlertAsync("Fel", "Vänligen fyll i alla fält.", "OK");
        }

    }

    private void GoBack_Clicked(object sender, EventArgs e)
    {
        selectedInsuranceService.UpdateSelectedInsurace(null);
    }
}