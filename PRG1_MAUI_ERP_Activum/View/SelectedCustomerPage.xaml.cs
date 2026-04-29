using PRG1_MAUI_ERP_Activum.Model;
using PRG1_MAUI_ERP_Activum.Services;

namespace PRG1_MAUI_ERP_Activum.View;

public partial class SelectedCustomerPage : ContentPage
{
    private readonly SelectedCustomerService selectedCustomerService = SelectedCustomerService.Instance;
    private readonly RegisterService _service = RegisterService.Instance;

    public SelectedCustomerPage()
	{
		InitializeComponent();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        selectedCustomerService.UpdateSelectedInsurace(null);
        if (selectedCustomerService.SelectedCustomer != null)
        {
            Title.Text = $"{selectedCustomerService.SelectedCustomer.FirstName} {selectedCustomerService.SelectedCustomer.LastName}";

            CustomerInsurances.SelectedItem = null;
            CustomerInsurances.ItemsSource = null;
            CustomerInsurances.ItemsSource = _service.GetInsurancesForCustomer(selectedCustomerService.SelectedCustomer.Id);
        }
    }

    private void CustomerInsurances_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var selected = e.CurrentSelection.FirstOrDefault() as Insurance;

        selectedCustomerService.UpdateSelectedInsurace(selected);
    }

    private void ViewNotes_Clicked(object sender, EventArgs e)
    {
        selectedCustomerService.ViewNotes();
    }

    private void EditCustomerInfo_Clicked(object sender, EventArgs e)
    {
        selectedCustomerService.ViewInformation();
    }

    private void AddInsurance_Clicked(object sender, EventArgs e)
    {
        selectedCustomerService.ViewAddInsurance();
    }

    private void GoBack_Clicked(object sender, EventArgs e)
    {
        selectedCustomerService.UpdateSelectedCustomer(null);
        selectedCustomerService.GotoPage();
    }
}