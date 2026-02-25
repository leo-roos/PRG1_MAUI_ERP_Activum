using System.Globalization;
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
    }

    private void Customers_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var selected = e.CurrentSelection.FirstOrDefault() as Customer;
        if (selected == null)
        {
            CustomerInsurances.ItemsSource = null;
            return;
        }

        CustomerInsurances.ItemsSource = _service.GetInsurancesForCustomer(selected).ToList();
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
}