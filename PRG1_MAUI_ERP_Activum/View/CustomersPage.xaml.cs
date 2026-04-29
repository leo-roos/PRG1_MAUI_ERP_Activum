using PRG1_MAUI_ERP_Activum.Model;
using PRG1_MAUI_ERP_Activum.Services;

namespace PRG1_MAUI_ERP_Activum.View;

public partial class CustomersPage : ContentPage
{
    private readonly SelectedCustomerService selectedCustomerService = SelectedCustomerService.Instance;
    private readonly RegisterService _service = RegisterService.Instance;

    public CustomersPage()
	{
		InitializeComponent();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        selectedCustomerService.UpdateSelectedCustomer(null);
        selectedCustomerService.UpdateSelectedInsurace(null);
        CustomersCollection.SelectedItem = null;
        CustomersCollection.ItemsSource = null;
        CustomersCollection.ItemsSource = _service.Customers;
    }

    private void Customers_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var selected = e.CurrentSelection.FirstOrDefault() as Customer;

        selectedCustomerService.UpdateSelectedCustomer(selected);
        selectedCustomerService.GotoPage();
    }

    private void SearchCustomers_TextChanged(object sender, TextChangedEventArgs e)
    {
        string search = SearchCustomers.Text;
        List<Guid> customersFound = _service.Customers.Where(c =>
                ($"{c.FirstName} {c.LastName}".ToLower().Contains(search.ToLower())) ||
                c.Email.ToLower().Contains(search.ToLower()) ||
                c.Phone.Contains(search)
            ).Select(c => c.Id).ToList();

        CustomersCollection.ItemsSource = _service.Customers.Where(c => customersFound.Contains(c.Id));
    }

    private void AddNewCustomer_Clicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//NewCustomerPage");
    }
}