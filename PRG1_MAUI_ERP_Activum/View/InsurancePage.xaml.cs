using PRG1_MAUI_ERP_Activum.Services;
using PRG1_MAUI_ERP_Activum.Model;
using System.Diagnostics;

namespace PRG1_MAUI_ERP_Activum.View;
public partial class InsurancePage : ContentPage
{
    private readonly RegisterService _service = RegisterService.Instance;
    private readonly SelectedInsuranceService selectedInsuranceService = SelectedInsuranceService.Instance;

    public Insurance? SelectedInsurance = null;

    public InsurancePage()
	{
		InitializeComponent();

        Insurances_CollectionView.ItemsSource = _service.Insurances;
    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();

        selectedInsuranceService.UpdateSelectedInsurace(null);
        Insurances_CollectionView.SelectedItem = null;
    }

    private void Insurances_CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var selectedinsurance = e.CurrentSelection.FirstOrDefault() as Insurance;

        selectedInsuranceService.UpdateSelectedInsurace(selectedinsurance);
    }

    private void AddNewInsurance_Clicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//NewInsurancePage");
    }
}