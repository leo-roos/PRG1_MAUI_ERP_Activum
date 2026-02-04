using System.Diagnostics;

namespace PRG1_MAUI_ERP_Activum.View;

public partial class CalculatorPage : ContentPage
{
	public CalculatorPage()
	{
		InitializeComponent();
	}

    private double accumulator = 0;
    private string operand = "";
    private string operation = "";
    private bool addComma = false;
    private bool canAddComma = true;

    // hantering för numeriska knappar
    private void NumberButton(object sender, EventArgs e)
    {
        Button button = (Button)sender;

        if (button.Text == "." && addComma == false && canAddComma == true)
        {
            string accumulatorStr = accumulator.ToString();
            if (!accumulatorStr.Contains(","))
            {
                addComma = true;
                EntryCalculations.Text += ",";
                return;
            }
        }

        // Bygg upp operand baserat på knapptexten (t.ex. "1", "2")
        else if (button.Text != ".")
        {
            if (addComma)
            {
                if (button.Text == "0" && canAddComma)
                {
                    operand = $"{operand},0";
                }
                else if (button.Text != "0")
                {
                    operand = operand + (Convert.ToDouble(button.Text) / 10);
                }
                addComma = false;
            }
            else
            {
                operand = $"{operand}" + button.Text;
            }
        } else
        {
            return;
        }

        Debug.WriteLine($"operand: {operand.ToString()}");
        EntryCalculations.Text += button.Text;
        EntryResult.Text = operand.ToString();
    }


    // hantering för operator-knappar (+, -, *, /)
    private void OperatorButton(object sender, EventArgs e)
    {
        if (operation != "") // Utför beräkning om en tidigare operation finns
        {
            Calculate();
        }
        else
        {
            accumulator = double.Parse(operand); // Spara första talet i accumulator
        }

        operand = "";

        Button button = (Button)sender;
        operation = button.Text;

        EntryCalculations.Text += $" {operation} ";
    }


    private void EqualButton(object sender, EventArgs e)
    {
        Calculate();

        EntryResult.Text = accumulator.ToString();
        EntryCalculations.Text = accumulator.ToString();

        operation = "";
        operand = accumulator.ToString();
    }


    private void Calculate()
    {
        double operandValue = double.Parse(operand);
        switch (operation)
        {
            case "+":
                accumulator += operandValue;
                break;
            case "-":
                accumulator -= operandValue;
                break;
            case "*":
                accumulator *= operandValue;
                break;
            case "/":
                if (operandValue == 0) // Hantera division med noll
                {
                    DisplayAlertAsync("Fel!", "Division med noll är ej tillåtet.", "OK");
                    Clear();
                    return;
                }
                accumulator /= operandValue;
                break;
            case "%":
                accumulator %= operand;
                break;
            case "^":
                accumulator = Math.Pow(accumulator, operand);
                break;
        }

        operand = "";
    }

    private void ClearButton(object sender, EventArgs e)
    {
        Clear();
    }

    private void Clear()
    {
        accumulator = 0;
        operand = "";
        operation = "";

        EntryCalculations.Text = "";
        EntryResult.Text = "0";
    }

}