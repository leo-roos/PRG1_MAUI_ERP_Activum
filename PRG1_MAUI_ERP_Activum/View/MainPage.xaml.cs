using PRG1_MAUI_ERP_Activum.Model;
using PRG1_MAUI_ERP_Activum.Services;

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
            CustomersCollection.ItemsSource = _service.Customers;
        }

        private void OnSearchCompleted(object sender, EventArgs e)
        {
            PerformSearch();
        }

        private void OnSearchClicked(object sender, EventArgs e)
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
            CustomersCollection.ItemsSource = _service.Customers.Where(c => customersFound.Contains(c.Id));
            CustomersCollection.IsVisible = true;
        }

        

       // private bool LookupCustomer(string input)
       
       

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
            
            SelectedCustomer = newCustomer;
            ChosenCustomerLayout.IsVisible = true;
        }

        private void CustomerInsurances_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
