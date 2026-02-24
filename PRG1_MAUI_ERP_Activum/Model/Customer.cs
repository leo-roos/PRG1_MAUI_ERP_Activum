using System;
using System.Collections.Generic;
using System.Text;

namespace PRG1_MAUI_ERP_Activum.Model
{
    public class Customer
    {
        public Guid Id { get; set; } = Guid.NewGuid(); // Guid garanterar att man genererar ett unikt Id. Ex: 550e8400-e29b-31d4-a716-44c655440370
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public string Email { get; set; } = "";
        public string Phone { get; set; } = "";
        public List<Guid> InsuranceId { get; set; } = new(); // vilka försäkringar kunden har (en "relation")

        public Customer(string firstName, string lastName, string email, string phone)
        {
            FirstName = firstName;
            LastName = lastname;
            Email = email;
            Phone = phone;
        }
    }
}
