using PRG1_MAUI_ERP_Activum.Services;
using PRG1_MAUI_ERP_Activum.Model;

namespace PRG1_MAUI_ERP_Activum.View;
public partial class SelectedInsurancePage : ContentPage
{
    private readonly SelectedInsuranceService selectedInsuranceService = SelectedInsuranceService.Instance;

    public Insurance? SelectedInsurance = null;

    public SelectedInsurancePage()
	{
		InitializeComponent();
    }

    private void Save_ChangeOfPrice(object sender, EventArgs e)
    {

    }

    private void GoBack_Clicked(object sender, EventArgs e)
    {
        selectedInsuranceService.UpdateSelectedInsurace(null);
    }
}