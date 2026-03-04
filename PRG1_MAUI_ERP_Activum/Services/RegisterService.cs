
using PRG1_MAUI_ERP_Activum.Model;
using System.Collections.ObjectModel;
using System.Linq;

namespace PRG1_MAUI_ERP_Activum.Services
{
    public class RegisterService
    {
        private static RegisterService? _instance;
        public static RegisterService Instance => _instance ??= new RegisterService();

        public ObservableCollection<Customer> Customers { get; }
        public ObservableCollection<Insurance> Insurances { get; }
        public Customer? chosenCustomer { get; set; }

        private RegisterService()
        {
            Customers = new ObservableCollection<Customer>();
            Insurances = new ObservableCollection<Insurance>();
            chosenCustomer = null;

            TestData();
        }

        private void TestData()
        {
            Insurance insurance1 = new Insurance("Bilförsäkring", 249);
            Insurance insurance2 = new Insurance("Hemförsäkring", 129);
            Insurance insurance3 = new Insurance("Reseförsäkring", 39);

            Insurances.Add(insurance1);
            Insurances.Add(insurance2);
            Insurances.Add(insurance3);

            Customer customer1 = new Customer("Pelle", "Svanslös", "pelle@kattstugan.se", "0707 77 00 77");
            Customer customer2 = new Customer("Maja", "Gräddnos", "maja@katthotel.se", "0708 88 11 22");

            customer1.Insurances.Add(new Insurance(insurance1));
            customer1.Insurances.Add(new Insurance(insurance2));
            customer1.Insurances.Add(new Insurance(insurance3));

            customer2.Insurances.Add(new Insurance(insurance1));

            Customers.Add(customer1);
            Customers.Add(customer2);
        }

        public Customer? GetCustomer(Guid id) => Customers.FirstOrDefault(c => c.Id == id);
        public Insurance? GetInsurance(Guid id) => Insurances.FirstOrDefault(i => i.Id == id);

        public IEnumerable<Insurance> GetInsurancesForCustomer(Customer customer) => customer.Insurances;

        public void LinkInsurance(Customer customer, Insurance insurance)
        {
            if (!customer.Insurances.Contains(insurance))
                customer.Insurances.Add(new Insurance(insurance));
        }

        public void UnlinkInsurance(Customer customer, Insurance insurance)
        {
            customer.Insurances.Remove(insurance);
        }
    }
}