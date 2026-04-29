using PRG1_MAUI_ERP_Activum.Model;
using PRG1_MAUI_ERP_Activum.Services;
using System.Diagnostics;

namespace PRG1_MAUI_ERP_Activum.View
{
    public partial class MainPage : ContentPage
    {

        private readonly RegisterService _service = RegisterService.Instance;
        private readonly SelectedCustomerService selectedCustomerService = SelectedCustomerService.Instance;

        Customer? SelectedCustomer = null;

        public MainPage()
        {
            InitializeComponent();

            CustomersCollection.ItemsSource = _service.Customers;
            CustomersCollection.IsVisible = false;
            ChosenCustomerLayout.IsVisible = false;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            CustomerInsurances.SelectedItem = null;
        }

        private void CustomerIdEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            PerformSearch();
        }

        private void PerformSearch()
        {
            string search = CustomerIdEntry.Text;
            List<Guid> customersFound = _service.Customers.Where(c =>
                ($"{c.FirstName} {c.LastName}".ToLower().Contains(search.ToLower())) ||
                c.Email.ToLower().Contains(search.ToLower()) ||
                c.Phone.Contains(search)
            ).Select(c => c.Id).ToList();

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
                await DisplayAlertAsync("Fel", "Anteckningen är tom.", "Ok");
                return;
            }
            else
            {
                Note newNote = new Note(notes);
                if (SelectedCustomer != null)
                {
                    newNote.CreatedDate = (DateTime)date;
                    SelectedCustomer.Notes.Add(newNote);
                    await DisplayAlertAsync("Sparat", $"Anteckning sparad för datum: {date:d}", "Ok");
                }
                else
                {
                    await DisplayAlertAsync("Fel", "Ingen kund vald.", "Ok");
                    return;

                }
            }
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
                CustomerInsurances.ItemsSource = _service.GetInsurancesForCustomer(newCustomer.Id);
            }

            SelectedCustomer = newCustomer;
            ChosenCustomerLayout.IsVisible = true;
        }

        private void CustomerInsurances_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var newInsurance = e.CurrentSelection.FirstOrDefault() as Insurance;
            
            if (newInsurance == null)
            {
                selectedCustomerService.UpdateSelectedCustomer(null);
                return;
            }
            selectedCustomerService.UpdateSelectedCustomer(SelectedCustomer);
            selectedCustomerService.UpdateSelectedInsurace(newInsurance);
            selectedCustomerService.GotoPage();
        }
    }
}
