using System.Diagnostics;

namespace PRG1_MAUI_ERP_Activum.View;

public partial class PerDiemPage : ContentPage
{
	public PerDiemPage()
	{
		InitializeComponent();
	}

	int WholeDayPayment = 240;
	int HalfDayPayment = 120;


    private void CalculatePerDiem(object sender, EventArgs e)
    {

        int AmountOfWholeDays = 0;
        int AmountOfHalfDays = 0;
        int ExtraExpensesAmount = 0;

        try
		{
			AmountOfWholeDays = int.Parse(WholeDays.Text);
			AmountOfHalfDays = int.Parse(HalfDays.Text);
			ExtraExpensesAmount = int.Parse(ExtraExpenses.Text);
		}
		catch
		{
			DisplayAlertAsync("Error", "Please enter a valid number", "Ok");
		}

		int WholeDayAmountResult = WholeDayPayment * AmountOfWholeDays;
		int HalfDayAmountResult = HalfDayPayment * AmountOfHalfDays;
		int TotalResult = HalfDayAmountResult + WholeDayAmountResult + ExtraExpensesAmount;

		AllowanceResultLabel.Text = TotalResult + "kr";

        ResultBorder.IsVisible = true;
		
    }
}