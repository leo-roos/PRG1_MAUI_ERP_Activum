using PRG1_MAUI_ERP_Activum.Model;
using PRG1_MAUI_ERP_Activum.Services;
using System.Diagnostics;

namespace PRG1_MAUI_ERP_Activum.View
{
    public partial class MainPage : ContentPage
    {

        private readonly RegisterService _service = RegisterService.Instance;

        Customer? SelectedCustomer = null;
        Insurance? SelectedInsurance = null;

        public MainPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            CustomersCollection.ItemsSource = _service.Customers;
            CustomersCollection.IsVisible = false;
            ChosenCustomerLayout.IsVisible = false;
        }

        private void CustomerIdEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            PerformSearch();
        }

        private void PerformSearch()
        {
            string search = CustomerIdEntry.Text;
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
                else if (customer.Phone.Contains(search))
                {
                    customersFound.Add(customer.Id);
                }
            }

            if (string.IsNullOrWhiteSpace(search) && SelectedCustomer != null) {
                customersFound.Clear();
                customersFound.Add(SelectedCustomer.Id);
            } else if (string.IsNullOrWhiteSpace(search) && SelectedCustomer == null) {
                CustomersCollection.ItemsSource = _service.Customers;
                CustomersCollection.IsVisible = false;
                return;
            }

            CustomersCollection.ItemsSource = _service.Customers.Where(c => customersFound.Contains(c.Id));
            CustomersCollection.IsVisible = true;
        }

        private async void OnSaveNotesClicked(object sender, EventArgs e)
        {
            string notes = NotesEditor.Text?.Trim();
            DateTime? date = DatePickerField.Date;

            if (string.IsNullOrWhiteSpace(notes))
            {
                await DisplayAlertAsync("Fel", "Anteckningen är tom.", "OK");
                return;
            }

            // TODO Skadeanmälan sparas inte just nu.
            await DisplayAlertAsync("Sparat", $"Anteckning sparad för datum: {date:d}", "OK");
        }

        private void CustomersCollection_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var newCustomer = e.CurrentSelection.FirstOrDefault() as Customer;
            CustomerInsurances.SelectedItem = null;
            if (newCustomer == null)
            {
                CustomerInsurances.ItemsSource = null;
            }
            else
            {
                CustomerInsurances.ItemsSource = _service.GetInsurancesForCustomer(newCustomer);
            }

            SelectedCustomer = newCustomer;
            ChosenCustomerLayout.IsVisible = true;
        }

        private void CustomerInsurances_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
