using System.Diagnostics;
using System.Globalization;
using PRG1_MAUI_ERP_Activum.Model;


namespace PRG1_MAUI_ERP_Activum.View;

public partial class SalaryPage : ContentPage
{
    private Salary mySalary;
    private int baseSalary;
    private int provisionRate;
    private int salaryTax;

    public SalaryPage()
    {
        InitializeComponent();

        mySalary = new Salary();
        baseSalary = mySalary.BaseSalary;
        provisionRate = mySalary.ProvisionRate;
        salaryTax = mySalary.SalaryTax;
    }

    private int GetCalculatedSalary(int mySales, bool withTax = true, bool withRate = true)
    {
        double mySalesProvision = mySales;

        if (withRate)
        {
            mySalesProvision = mySalesProvision * (provisionRate / 100.0);
        }

        double totalSalary = baseSalary + mySalesProvision;

        if (!withTax)
        {
            return (int)totalSalary;
        }

        double taxedSalary = totalSalary - (totalSalary * (salaryTax / 100.0));

        return (int)taxedSalary;
    }

    private void CalculateSalary_Clicked(object sender, EventArgs e)
    {
        int mySales = 0;

        if (string.IsNullOrEmpty(MySalesEntry.Text))
        {
            MySalesEntry.Focus();
            DisplayAlertAsync("Error", "För att beräkna din Månadslön måste du ange din totala försäljning denna månad!", "Ok");
            return;
        }

        if (int.TryParse(MySalesEntry.Text, out int result))
        {
            mySales = result;
        }
        else
        {
            MySalesEntry.Focus();
            DisplayAlertAsync("Error", "För att beräkna din Månadslön måste du ange ett tal i \"din totala försäljning denna månad\"!", "Ok");
            return;
        }

        int CalculatedSalary = GetCalculatedSalary(mySales);
        int CalculatedSalaryWithoutTax = GetCalculatedSalary(mySales, false, false);

        string FormattedCalculatedSalary = CalculatedSalary.ToString("N0", new CultureInfo("en-US"));
        string FormattedCalculatedSalaryWithoutTax = CalculatedSalaryWithoutTax.ToString("N0", new CultureInfo("en-US"));
        ExpectedSalaryResult.Text = $"{FormattedCalculatedSalary} kr ({FormattedCalculatedSalaryWithoutTax}kr brutto)";
        ExpectedSalaryBorder.IsVisible = true;
    }

    private void CalculateSemesterSalary_Clicked(object sender, EventArgs e)
    {
        int myAverageSales = 0;

        if (string.IsNullOrEmpty(MyAverageSalesEntry.Text))
        {
            MyAverageSalesEntry.Focus();
            DisplayAlertAsync("Error", "För att beräkna din Semesterlön måste du ange din totala försäljning de senaste 11 månaderna", "Ok");
            return;
        }

        if (int.TryParse(MyAverageSalesEntry.Text, out int result))
        {
            myAverageSales = result;
        }
        else
        {
            MyAverageSalesEntry.Focus();
            DisplayAlertAsync("Error", "För att beräkna din Semesterlön måste du ange ett tal i \"Genomsnittlig försäljning (11 månader)\"!", "Ok");
            return;
        }

        int CalculatedSalary = GetCalculatedSalary(myAverageSales);
        int CalculatedSalaryWithoutTax = GetCalculatedSalary(myAverageSales, false, false);

        string FormattedCalculatedSalary = CalculatedSalary.ToString("N0", new CultureInfo("en-US"));
        string FormattedCalculatedSalaryWithoutTax = CalculatedSalaryWithoutTax.ToString("N0", new CultureInfo("en-US"));
        ExpectedSemesterSalaryResult.Text = $"{FormattedCalculatedSalary} kr ({FormattedCalculatedSalaryWithoutTax}kr brutto)";
        ExpectedSemesterSalaryBorder.IsVisible = true;
    }
}