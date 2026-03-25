using PRG1_MAUI_ERP_Activum.Model;
using PRG1_MAUI_ERP_Activum.Services;
using System.Diagnostics;

namespace PRG1_MAUI_ERP_Activum.View;

public partial class CustomersPage : ContentPage
{
    private readonly RegisterService _service = RegisterService.Instance;

    Customer? SelectedCustomer = null;
    Insurance? SelectedInsurance = null;

    public CustomersPage()
	{
		InitializeComponent();

        CustomersCollection.ItemsSource = _service.Customers;
        ChosenCustomerLayout.IsVisible = false;
    }

    private void Customers_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var selected = e.CurrentSelection.FirstOrDefault() as Customer;


        UpdateSelectedCustomer(selected);

        //if (selected == null)
        //{
        //    SelectedCustomer = null;
        //    CustomerInsurances.ItemsSource = null;
        //    ChosenCustomerLayout.IsVisible = false;
        //    return;
        //}

        //SelectedCustomer = selected;
        //CustomerInsurances.ItemsSource = _service.GetInsurancesForCustomer(selected);
        //ChosenCustomerLayout.IsVisible = true;
    }

    private void UpdateSelectedCustomer(Customer? newCustomer)
    {
        if (newCustomer == null)
        {
            CustomerInsurances.ItemsSource = null;
            ChosenCustomerLayout.IsVisible = false;
        }
        else
        {
            CustomerInsurances.ItemsSource = _service.GetInsurancesForCustomer(newCustomer);
            ChosenCustomerLayout.IsVisible = true;
        }
        UpdateSelectedInsurance(null);
        SelectedCustomer = newCustomer;
    }
    private void UpdateSelectedInsurance(Insurance? newInsurance)
    {
        if (newInsurance == null)
        {
            ChosenInsuranceLayout.IsVisible = false;
            CustomerInsurances.SelectedItem = null;
        }
        else
        {
            ChosenInsuranceLayout.IsVisible = true;
            ChosenInsuranceLabel.Text = $"Vald Försäkring: {newInsurance.Type}";

            CostEntry.Text = newInsurance.MonthlyCost.ToString();
            StartDatePicker.Date = newInsurance.StartDate;
            EndDatePicker.Date = newInsurance.EndDate;
        }

        SelectedInsurance = newInsurance;
    }

    private void CustomerInsurances_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var selected = e.CurrentSelection.FirstOrDefault() as Insurance;

        UpdateSelectedInsurance(selected);

        //if (selected == null)
        //{
        //    SelectedInsurance = null;
        //    ChosenInsuranceLayout.IsVisible = false;
        //    return;
        //}

        //SelectedInsurance = selected;
        //ChosenInsuranceLayout.IsVisible = true;

        //SelectedInsuranceLabel.Text = $"Vald Försäkring: {selected.Type}";
        //StartDatePicker.Date = selected.StartDate;
        //EndDatePicker.Date = selected.EndDate;
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

    private void StartDatePicker_DateSelected(object sender, DateChangedEventArgs e)
    {
        if (SelectedInsurance != null && SelectedCustomer != null && e.NewDate.HasValue)
        {
            SelectedInsurance.StartDate = e.NewDate.Value;
            CustomerInsurances.ItemsSource = _service.GetInsurancesForCustomer(SelectedCustomer);
        }
    }

    private void EndDatePicker_DateSelected(object sender, DateChangedEventArgs e)
    {
        if (SelectedInsurance != null && SelectedCustomer != null && e.NewDate.HasValue)
        {
            SelectedInsurance.EndDate = e.NewDate.Value;
            CustomerInsurances.ItemsSource = _service.GetInsurancesForCustomer(SelectedCustomer);
        }
    }

    private void ChosenInsuranceSave_Clicked(object sender, EventArgs e)
    {

    }
}