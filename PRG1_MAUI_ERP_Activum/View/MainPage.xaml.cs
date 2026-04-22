using PRG1_MAUI_ERP_Activum.Services;

namespace PRG1_MAUI_ERP_Activum.View
{
    public partial class MainPage : ContentPage
    {

        private readonly RegisterService _service = RegisterService.Instance;
        public MainPage()
        {
            InitializeComponent();
        }

        private void OnSearchCompleted(object sender, EventArgs e)
        {
            PerformSearch();
        }

        private void OnSearchClicked(object sender, EventArgs e)
        {
            PerformSearch();
           
        }


        // TODO Sökfunktionen på startsidan är inte implementerad
         


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

        }
    }
}
