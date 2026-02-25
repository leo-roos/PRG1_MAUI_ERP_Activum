using PRG1_MAUI_ERP_Activum.Model;
using PRG1_MAUI_ERP_Activum.Services;

namespace PRG1_MAUI_ERP_Activum.View
{

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

        private void AddCustomer_Clicked(object sender, EventArgs e)
        {
            _service.Customers.Add(new Customer("", "", "", ""));
        }

        private void DeleteCustomer_Clicked(object sender, EventArgs e)
        {
            var selected = CustomersCollection.SelectedItem as Customer;
            if (selected != null)
                _service.Customers.Remove(selected);
        }
    }
}