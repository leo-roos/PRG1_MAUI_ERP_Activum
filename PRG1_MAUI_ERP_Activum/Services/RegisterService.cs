
using PRG1_MAUI_ERP_Activum.Model;
using System.Collections.ObjectModel;

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
            Insurance insurance1 = new Insurance("Bilförsäkring", 249);
            Insurance insurance2 = new Insurance("Hemförsäkring", 129);
            Insurance insurance3 = new Insurance("Reseförsäkring", 39);

            Insurances.Add(insurance1);
            Insurances.Add(insurance2);
            Insurances.Add(insurance3);

            Customer customer1 = new Customer("Pelle", "Svanslös", "pelle@kattstugan.se", "0707 77 00 77");
            Customer customer2 = new Customer("Maja", "Gräddnos", "maja@katthotel.se", "0708 88 11 22");
            Customer customer3 = new Customer("Erik", "Kattlös", "erik.kattlos@gmail.com", "0707 12 34 56");
            Customer customer4 = new Customer("Lisa", "Kattälskare", "lisa@kattälskare.se", "0709 99 88 77");
            Customer customer5 = new Customer("Olle", "Kattvän", "olle@kattvän.se", "0706 66 55 44");

            customer1.Insurances.Add(new Insurance(insurance1));
            customer1.Insurances.Add(new Insurance(insurance2));
            customer1.Insurances.Add(new Insurance(insurance3));

            customer2.Insurances.Add(new Insurance(insurance1));

            customer3.Insurances.Add(new Insurance(insurance2));
            customer3.Insurances.Add(new Insurance(insurance3));

            customer4.Insurances.Add(new Insurance(insurance1));
            customer4.Insurances.Add(new Insurance(insurance2));

            customer5.Insurances.Add(new Insurance(insurance3));

            Customers.Add(customer1);
            Customers.Add(customer2);
            Customers.Add(customer3);
            Customers.Add(customer4);
            Customers.Add(customer5);
        }

        public Customer? GetCustomer(Guid id)
        {
            return Customers.FirstOrDefault(c => c.Id == id);
        }

        public Insurance? GetInsurance(Guid id) {
            return Insurances.FirstOrDefault(i => i.Id == id);
        }

        public List<Insurance> GetInsurancesForCustomer(Customer customer) {
            return customer.Insurances;
        }

        public static void LinkInsurance(Customer customer, Insurance insurance)
        {
            if (!customer.Insurances.Contains(insurance))
                customer.Insurances.Add(new Insurance(insurance));
        }

        public static void UnlinkInsurance(Customer customer, Insurance insurance)
        {
            customer.Insurances.Remove(insurance);
        }
    }
}