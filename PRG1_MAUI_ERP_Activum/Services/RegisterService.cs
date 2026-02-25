
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

        private RegisterService()
        {
            Customers = new ObservableCollection<Customer>();
            Insurances = new ObservableCollection<Insurance>();

            TestData();
        }

        private void TestData()
        {
            var insurance1 = new Insurance("Bilförsäkring", 249);
            var insurance2 = new Insurance("Hemförsäkring", 129);
            var insurance3 = new Insurance("Reseförsäkring", 39);

            Insurances.Add(insurance1);
            Insurances.Add(insurance2);
            Insurances.Add(insurance3);

            var customer1 = new Customer("Pelle", "Svanslös", "pelle@kattstugan.se", "0707 77 00 77");
            var customer2 = new Customer("Maja", "Gräddnos", "maja@katthotel.se", "0708 88 11 22");

            customer1.InsuranceId.Add(insurance1.Id);
            customer1.InsuranceId.Add(insurance2.Id);
            customer1.InsuranceId.Add(insurance3.Id);

            customer2.InsuranceId.Add(insurance3.Id);

            Customers.Add(customer1);
            Customers.Add(customer2);
        }

        public Customer? GetCustomer(Guid id) => Customers.FirstOrDefault(c => c.Id == id);
        public Insurance? GetInsurance(Guid id) => Insurances.FirstOrDefault(i => i.Id == id);

        public IEnumerable<Insurance> GetInsurancesForCustomer(Customer customer) =>
            Insurances.Where(i => customer.InsuranceId.Contains(i.Id));

        public void LinkInsurance(Customer customer, Insurance insurance)
        {
            if (!customer.InsuranceId.Contains(insurance.Id))
                customer.InsuranceId.Add(insurance.Id);
        }

        public void UnlinkInsurance(Customer customer, Insurance insurance)
        {
            customer.InsuranceId.Remove(insurance.Id);
        }
    }
}