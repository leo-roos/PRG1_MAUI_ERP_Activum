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
            Title.Text = $"{selectedInsuranceService.SelectedInsurance.Type}";

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

        bool isTypeChanged = false;
        bool isCostChanged = false;

        if (selectedInsuranceService.SelectedInsurance.Type != TypeEntry.Text)
        {
            selectedInsuranceService.SelectedInsurance.Type = TypeEntry.Text;
            isTypeChanged = true;
        }

        if (int.TryParse(CostEntry.Text, out int newCost))
        {
            if (newCost != selectedInsuranceService.SelectedInsurance.MonthlyCost)
            {
                selectedInsuranceService.SelectedInsurance.MonthlyCost = newCost;
                isCostChanged = true;
            }
        }

        string updateMessage = "Uppdaterade följande: ";
        if (isTypeChanged)
        {
            updateMessage += "Typ, ";
        }
        if (isCostChanged)
        {
            updateMessage += "Månads Pris, ";
        }

        DisplayAlertAsync("Uppdatera Försäkringen", updateMessage, "Ok");

        _service.UpdateInsurance(selectedInsuranceService.SelectedInsurance);
    }

    private void GoBack_Clicked(object sender, EventArgs e)
    {
        selectedInsuranceService.UpdateSelectedInsurace(null);
    }
}