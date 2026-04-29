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

    private async void Remove_Clicked(object sender, EventArgs e)
    {
        if (selectedInsuranceService.SelectedInsurance == null)
        {
            return;
        }

        bool answer = await DisplayAlertAsync("Ta bort försäkringen", "Är du säker på att du vill ta bort denna försäkring?", "Ja", "Nej");
        if (answer)
        {
            _service.RemoveInsurance(selectedInsuranceService.SelectedInsurance);
            selectedInsuranceService.UpdateSelectedInsurace(null);
            selectedInsuranceService.GotoPage();
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
            DisplayAlertAsync("Fel", "Vänligen fyll i alla fält.", "Ok");
        }

    }

    private void GoBack_Clicked(object sender, EventArgs e)
    {
        selectedInsuranceService.UpdateSelectedInsurace(null);
    }
}