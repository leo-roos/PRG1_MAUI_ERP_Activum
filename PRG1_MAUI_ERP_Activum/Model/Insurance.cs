namespace PRG1_MAUI_ERP_Activum.Model
{
    public class Insurance
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Type { get; set; } = "";
        public int MonthlyCost { get; set; } = 0;
        public DateTime StartDate { get; set; } = DateTime.Now;
        public DateTime EndDate { get; set; } = DateTime.Now.AddYears(1);
        public bool IsActive { get; set; } = true;


        public Insurance(string type, int monthlyCost)
        {
            Type = type;
            MonthlyCost = monthlyCost;
            StartDate = DateTime.Now;
            EndDate = DateTime.Now.AddYears(1);
            IsActive = true;
        }

        public Insurance(Insurance insurance) // to copy an insurance when linking to a customer, so that the same insurance can be linked to multiple customers without sharing the same start and end date
        {
            Id = Guid.NewGuid();
            Type = insurance.Type;
            MonthlyCost = insurance.MonthlyCost;
            StartDate = insurance.StartDate;
            EndDate = insurance.EndDate;
            IsActive = insurance.IsActive;
        }

        public Insurance() { }
    }
}
