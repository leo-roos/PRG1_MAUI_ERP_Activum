using PRG1_MAUI_ERP_Activum.Services;
using PRG1_MAUI_ERP_Activum.Model;
using System.Diagnostics;

namespace PRG1_MAUI_ERP_Activum.View;
public partial class NewInsurancePage : ContentPage
{
    private readonly RegisterService _service = RegisterService.Instance;

    public NewInsurancePage()
	{
		InitializeComponent();
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        if (!string.IsNullOrWhiteSpace(TypeInsurancesEntry.Text) && !string.IsNullOrWhiteSpace(PriceInsurancesEntry.Text))
        {
            if (int.TryParse(PriceInsurancesEntry.Text, out int priceinsurance))
            {
                Insurance insurance = new Insurance(TypeInsurancesEntry.Text, priceinsurance);
                _service.Insurances.Add(insurance);
            }
            else
            {
                DisplayAlertAsync("Error", "Se till att skriva ett nummer i kostnadsfältet!", "Ok");
            }
        }
    }

    private void GoBack_Clicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//InsurancePage");
    }
}