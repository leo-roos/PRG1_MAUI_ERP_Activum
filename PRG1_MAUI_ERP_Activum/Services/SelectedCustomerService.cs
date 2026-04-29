using PRG1_MAUI_ERP_Activum.Model;

namespace PRG1_MAUI_ERP_Activum.Services
{
    public class SelectedCustomerService
    {
        private static SelectedCustomerService? _instance;
        public static SelectedCustomerService Instance => _instance ??= new SelectedCustomerService();

        public Customer? SelectedCustomer = null;
        public Insurance? SelectedInsurance = null;

        private SelectedCustomerService()
        {
            SelectedCustomer = null;
            SelectedInsurance = null;
        }

        public void GotoPage()
        {
            if (SelectedInsurance != null)
            {
                Shell.Current.GoToAsync("//SelectedCustomerInsurancePage");
            }
            else if (SelectedCustomer != null)
            {
                Shell.Current.GoToAsync("//SelectedCustomerPage");
            }
            else
            {
                Shell.Current.GoToAsync("//CustomersPage");
            }
        }

        public void UpdateSelectedCustomer(Customer? newCustomer)
        {
            SelectedCustomer = newCustomer;
            SelectedInsurance = null;

            GotoPage();
        }

        public void UpdateSelectedInsurace(Insurance? newInsurance)
        {
            if (SelectedCustomer != null)
            {
                SelectedInsurance = newInsurance;

                GotoPage();
            }
        }

        public void ViewNotes()
        {
            if (SelectedCustomer != null)
            {
                Shell.Current.GoToAsync("//SelectedCustomerNotesPage");
            }
        }

        public void ViewInformation()
        {
            if (SelectedCustomer != null)
            {
                Shell.Current.GoToAsync("//SelectedCustomerInformationPage");
            }
        }

        public void ViewAddInsurance()
        {
            if (SelectedCustomer != null)
            {
                Shell.Current.GoToAsync("//SelectedCustomerAddInsurancePage");
            }
        }
    }
}
