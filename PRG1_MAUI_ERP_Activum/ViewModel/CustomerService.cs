using PRG1_MAUI_ERP_Activum.Model;
using System.Collections.ObjectModel;

namespace PRG1_MAUI_ERP_Activum.ViewModel
{
    internal class CustomerService
    {
        private static CustomerService _instance;

        public static CustomerService Instance => _instance ?? (_instance = new CustomerService()); // detta garanterar att det enbart finns en instance
        public ObservableCollection<Customer> CustomersList { get; set; }


        private CustomerService()
        {
            CustomersList = new ObservableCollection<Customer>
            {
                new Customer("Pelle", "Svanslös", "mail@kattstugan.se", "0707 77 00 77"),
                new Customer("Maja", "Gräddnos", "maja@katthotell.se", "0708 88 11 22")
            };
        }
    } 
}
