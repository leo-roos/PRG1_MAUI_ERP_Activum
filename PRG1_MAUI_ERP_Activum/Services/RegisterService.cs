
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

            Note note1 = new Note("Kunden ringde och ville ändra adress.");
            Note note2 = new Note("Kunden skickade ett e-postmeddelande.");
            Note note3 = new Note("Kunden skickade ett meddelande via chat.");
            note2.CreatedDate = DateTime.Now.AddDays(-1);
            note2.EditDate = DateTime.Now.AddDays(+1);
            customer1.Notes.Add(note1);
            customer1.Notes.Add(note2);
            customer1.Notes.Add(note3);

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

        public void UpdateInsurance(Insurance insurance)
        {
            Insurance? existingInsurance = GetInsurance(insurance.Id);
            if (existingInsurance != null)
            {
                existingInsurance.Type = insurance.Type;
                existingInsurance.MonthlyCost = insurance.MonthlyCost;
                existingInsurance.StartDate = insurance.StartDate;
                existingInsurance.EndDate = insurance.EndDate;
                existingInsurance.IsActive = insurance.IsActive;
            }
        }
        public void UpdateCustomer(Customer customer)
        {
            Customer? existingCustomer = GetCustomer(customer.Id);
            if (existingCustomer != null)
            {
                existingCustomer.FirstName = customer.FirstName;
                existingCustomer.LastName = customer.LastName;
                existingCustomer.Email = customer.Email;
                existingCustomer.Phone = customer.Phone;
            }
        }

        public void AddCustomer(Customer customer)
        {
            Customers.Add(customer);
        }

        public ObservableCollection<Insurance> GetInsurancesForCustomer(Guid id) {
            return Customers.FirstOrDefault(i => i.Id == id).Insurances;
        }

        public ObservableCollection<Note> GetNotesForCustomer(Guid id) {
            return new ObservableCollection<Note>(Customers.FirstOrDefault(i => i.Id == id).Notes.OrderByDescending(n => n.EditDate));
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