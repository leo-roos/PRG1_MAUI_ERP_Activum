using System;
using System.Collections.Generic;
using System.Text;

namespace PRG1_MAUI_ERP_Activum.Model
{
    public class Insurance
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string InsuranceType { get; set; } = "";
        public decimal MonthlyCost { get; set; }
        public DateTime StartDate { get; set; }
        public bool IsActive { get; set; }

        public Insurance(string insuranceType, decimal monthlyCost)
        {
            InsuranceType = insuranceType;
            MonthlyCost = monthlyCost;
            StartDate = DateTime.Now;
            IsActive = true;
        }
    }
}
