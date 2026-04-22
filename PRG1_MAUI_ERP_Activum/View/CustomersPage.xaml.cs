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

        CustomersCollection.ItemsSource = _service.Customers;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        selectedCustomerService.UpdateSelectedCustomer(null);
        selectedCustomerService.UpdateSelectedInsurace(null);
        CustomersCollection.SelectedItem = null;
    }

    private void Customers_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var selected = e.CurrentSelection.FirstOrDefault() as Customer;

        selectedCustomerService.UpdateSelectedCustomer(selected);
    }

    private void SearchCustomers_TextChanged(object sender, TextChangedEventArgs e)
    {
        string search = SearchCustomers.Text;
        List<Guid> customersFound = new List<Guid>();
        foreach (var customer in _service.Customers)
        {
            if (customer.FirstName.ToLower().Contains(search.ToLower()))
            {
                customersFound.Add(customer.Id);
            }
            else if (customer.LastName.ToLower().Contains(search.ToLower()))
            {
                customersFound.Add(customer.Id);
            }
            else if(customer.Phone.Contains(search))
            {
                customersFound.Add(customer.Id);
            }
        }
        CustomersCollection.ItemsSource = _service.Customers.Where(c => customersFound.Contains(c.Id));
    }
}