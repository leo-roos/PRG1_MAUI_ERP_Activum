using PRG1_MAUI_ERP_Activum.Model;
using PRG1_MAUI_ERP_Activum.Services;

namespace PRG1_MAUI_ERP_Activum.View;

public partial class CustomersPage : ContentPage
{
    private readonly RegisterService _service = RegisterService.Instance;

    public CustomersPage()
	{
		InitializeComponent();

        CustomersCollection.ItemsSource = _service.Customers;
        ChosenCustomerLayout.IsVisible = false;
    }

    private void Customers_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var selected = e.CurrentSelection.FirstOrDefault() as Customer;
        if (selected == null)
        {
            _service.chosenCustomer = null;
            CustomerInsurances.ItemsSource = null;
            ChosenCustomerLayout.IsVisible = false;
            return;
        }

        _service.chosenCustomer = selected;
        CustomerInsurances.ItemsSource = _service.GetInsurancesForCustomer(selected).ToList();
        ChosenCustomerLayout.IsVisible = true;
    }

    private void SearchCustomers_TextChanged(object sender, TextChangedEventArgs e)
    {
        string search = SearchCustomers.Text;
        List<Guid> customersFound = new List<Guid>();
        foreach (var customer in _service.Customers)
        {
            if (customer.FirstName.ToLower().Contains(search.ToLower()))
            {
                if (!customersFound.Contains(customer.Id))
                {
                    customersFound.Add(customer.Id);
                }
            }
        }
        CustomersCollection.ItemsSource = _service.Customers.Where(c => customersFound.Contains(c.Id)).ToList();
    }

    private async void ChangeInsurances_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync($"//InsurancePage");
    }
}