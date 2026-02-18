using Plugin.Maui.KeyListener;
using System.Diagnostics;

namespace PRG1_MAUI_ERP_Activum.View;

public partial class CalculatorPage : ContentPage
{
	public CalculatorPage()
	{
		InitializeComponent();
	}

    KeyboardBehavior keyboardBehavior = new();

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        //keyboardBehavior.KeyDown += OnKeyDown;
        keyboardBehavior.KeyUp += OnKeyUp;
        this.Behaviors.Add(keyboardBehavior);

        base.OnNavigatedTo(args);
    }

    protected override void OnNavigatedFrom(NavigatedFromEventArgs args)
    {
        //keyboardBehavior.KeyDown -= OnKeyDown;
        keyboardBehavior.KeyUp -= OnKeyUp;
        this.Behaviors.Remove(keyboardBehavior);

        base.OnNavigatedFrom(args);
    }

    void OnKeyUp(object sender, KeyPressedEventArgs args)
    {

    }

    private double accumulator = 0;
    private string operand = "";
    private string operation = "";
    private bool addComma = false;

    private void addNumber(string number)
    {
        // Bygg upp operand baserat på knapptexten (t.ex. "1", "2")
        //else if (number != ".")
        if (number != ".")
        {
            if (addComma)
            {
                if (!operand.Contains(','))
                {
                    if (operand == "")
                    {
                        EntryCalculations.Text += "0";
                        operand += "0";
                    }
                    EntryCalculations.Text += ",";
                    operand += ',';
                }
                operand += number;
            }
            else
            {
                operand = $"{operand}" + number;
            }
        }
        else
        {
            return;
        }

        Debug.WriteLine($"operand: {operand.ToString()}");
        EntryCalculations.Text += number;
        EntryResult.Text = operand.ToString();
    }

    private void addDecimal()
    {
        if (addComma == false)
        {
            addComma = true;
            return;
        }
    }

    // hantering för numeriska knappar
    private void NumberButton(object sender, EventArgs e)
    {
        Button button = (Button)sender;
        addNumber(button.Text);
    }

    private void CommaButton(object sender, EventArgs e)
    {
        addDecimal();
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

        addComma = false;
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
                accumulator %= operandValue;
                break;
            case "^":
                accumulator = Math.Pow(accumulator, operandValue);
                break;
        }

        operand = "";
    }

    private void Clear()
    {
        accumulator = 0;
        operand = "";
        operation = "";

        EntryCalculations.Text = "";
        EntryResult.Text = "0";
    }

    private void ClearButton(object sender, EventArgs e)
    {
        Clear();
    }
}