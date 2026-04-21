using System.Collections.ObjectModel;

namespace PRG1_MAUI_ERP_Activum.Model
{
    public class Customer
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public string Email { get; set; } = "";
        public string Phone { get; set; } = "";
        public ObservableCollection<Note> Notes { get; set; } = [];
        public ObservableCollection<Insurance> Insurances { get; set; } = [];

        public Customer(string firstName, string lastName, string email, string phone)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Phone = phone;
        }
    }
}
