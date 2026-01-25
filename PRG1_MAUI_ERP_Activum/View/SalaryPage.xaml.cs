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

    private int GetCalculatedSalary(int mySales)
    {
        double mySalesProvision = mySales * (provisionRate / 100.0);
        double totalSalary = baseSalary + mySalesProvision;
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
        ExpectedSalaryResult.Text = CalculatedSalary.ToString("N0", new CultureInfo("en-US")) + " kr";
        ExpectedSalaryBorder.IsVisible = true;
    }

    private void CalculateSemesterSalary_Clicked(object sender, EventArgs e)
    {
        int myAverageSales = 0;

        if (string.IsNullOrEmpty(MyAverageSalesEntry.Text))
        {
            MyAverageSalesEntry.Focus();
            DisplayAlertAsync("Error", "För att beräkna din Månadslön måste du ange din totala försäljning denna månad!", "Ok");
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
        ExpectedSemesterSalaryResult.Text = CalculatedSalary.ToString("N0", new CultureInfo("en-US")) + " kr";
        ExpectedSemesterSalaryBorder.IsVisible = true;
    }
}