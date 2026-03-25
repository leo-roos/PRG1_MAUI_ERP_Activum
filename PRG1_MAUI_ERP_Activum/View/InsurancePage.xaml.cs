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

        CustomerInsurances.ItemsSource = _service.Insurances;



    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        if(!string.IsNullOrWhiteSpace(TypeInsurancesEntry.Text) && !string.IsNullOrWhiteSpace(PriceInsurancesEntry.Text)) 
        {
            if(int.TryParse(PriceInsurancesEntry.Text, out int priceinsurance))
            {
                Insurance insurance = new Insurance(TypeInsurancesEntry.Text, priceinsurance);
                _service.Insurances.Add(insurance);
                CustomerInsurances.ItemsSource = _service.Insurances;
            }
            else
            {
                DisplayAlertAsync("Error", "Se till att skriva ett nummer i kostnadsfältet!", "Ok");
            }
       
        }


    }
}