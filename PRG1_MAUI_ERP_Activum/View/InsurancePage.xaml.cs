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

        Insurances_CollectionView.ItemsSource = _service.Insurances;



    }

    //private void Change_Price()
    private void Button_Clicked(object sender, EventArgs e)
    {
        if(!string.IsNullOrWhiteSpace(TypeInsurancesEntry.Text) && !string.IsNullOrWhiteSpace(PriceInsurancesEntry.Text)) 
        {
            if(int.TryParse(PriceInsurancesEntry.Text, out int priceinsurance))
            {
                Insurance insurance = new Insurance(TypeInsurancesEntry.Text, priceinsurance);
                _service.Insurances.Add(insurance);
                Insurances_CollectionView.ItemsSource = _service.Insurances;
            }
            else
            {
                DisplayAlertAsync("Error", "Se till att skriva ett nummer i kostnadsfältet!", "Ok");
            }
       
        }


    }

    private void UpdateSelectedCustomerInsurance(Insurance? newInsurance)
    {
        if (newInsurance == null)
        {
            ChosenInsuranceLayout.IsVisible = false;
            Insurances_CollectionView.SelectedItem = null;
        }
        else
        {
            ChosenInsuranceLayout.IsVisible = true;
            ChosenInsuranceLabel.Text = $"Vald Försäkring: {newInsurance.Type}";

            PriceEntry.Text = newInsurance.MonthlyCost.ToString();
           
        }

        SelectedInsurance = newInsurance;

    }

    private void Insurances_CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var selectedinsurance = e.CurrentSelection.FirstOrDefault() as Insurance;

        UpdateSelectedCustomerInsurance(selectedinsurance);
    }

    private void Save_ChangeOfPrice(object sender, EventArgs e)
    {

    }
}