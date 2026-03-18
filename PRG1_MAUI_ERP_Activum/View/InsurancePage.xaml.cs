using PRG1_MAUI_ERP_Activum.Services;
using PRG1_MAUI_ERP_Activum.Model;
using System.Diagnostics;

namespace PRG1_MAUI_ERP_Activum.View;
public partial class InsurancePage : ContentPage
{
    private readonly RegisterService _service = RegisterService.Instance;

    public Insurance? SelectedInsurance = null;

    public InsurancePage()
	{
		InitializeComponent();
	}
}