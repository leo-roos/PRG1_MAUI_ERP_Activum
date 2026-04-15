using PRG1_MAUI_ERP_Activum.Model;
using PRG1_MAUI_ERP_Activum.View;

namespace PRG1_MAUI_ERP_Activum.Services
{
    public class SelectedInsuranceService
    {
        private static SelectedInsuranceService? _instance;
        public static SelectedInsuranceService Instance => _instance ??= new SelectedInsuranceService();

        public Insurance? SelectedInsurance = null;

        private SelectedInsuranceService()
        {
            SelectedInsurance = null;
        }

        public void GotoPage()
        {
            if (SelectedInsurance != null)
            {
                Shell.Current.GoToAsync("//SelectedInsurancePage");
            }
            else
            {
                Shell.Current.GoToAsync("//InsurancePage");
            }
        }

        public void UpdateSelectedInsurace(Insurance? newInsurance)
        {
            SelectedInsurance = newInsurance;

            GotoPage();
        }
    }
}
