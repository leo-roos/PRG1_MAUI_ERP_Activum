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

    Dictionary<string, List<Dictionary<string, object>>> keyValuePairs = new()
    {
        ["numbers"] = new()
        {
            new() { ["key"] = '0' },
            new() { ["key"] = '1' },
            new() { ["key"] = '2' },
            new() { ["key"] = '3' },
            new() { ["key"] = '4' },
            new() { ["key"] = '5' },
            new() { ["key"] = '6' },
            new() { ["key"] = '7' },
            new() { ["key"] = '8' },
            new() { ["key"] = '9' }
        },

        ["operators"] = new()
        {
            new() { ["modifier"] = KeyboardModifiers.None, ["key"] = '-', ["value"] = '-' },
            new() { ["modifier"] = KeyboardModifiers.None, ["key"] = '+', ["value"] = '+' },
            new() { ["modifier"] = KeyboardModifiers.Shift, ["key"] = '5', ["value"] = '%' },
            new() { ["modifier"] = KeyboardModifiers.Shift, ["key"] = '7', ["value"] = '/' },
            new() { ["modifier"] = KeyboardModifiers.Shift, ["key"] = char.Parse("'"), ["value"] = '*' },
            new() { ["modifier"] = KeyboardModifiers.Shift, ["key"] = '¨', ["value"] = '^' },
        }
    };


    void OnKeyUp(object sender, KeyPressedEventArgs args)
    {
        foreach (var entry in keyValuePairs["numbers"])
        {
            char key = (char)entry["key"];

            KeyboardModifiers modifier = KeyboardModifiers.None;
            if (entry.ContainsKey("modifier"))
            {
                modifier = (KeyboardModifiers)entry["modifier"];
            }

            if (args.KeyChar == key && args.Modifiers == modifier)
            {
                char value = key;
                if (entry.ContainsKey("value"))
                {
                    value = (char)entry["value"];
                }

                AddNumber(value.ToString());
                return;
            }
        }
        foreach (var entry in keyValuePairs["operators"])
        {
            char key = (char)entry["key"];

            KeyboardModifiers modifier = args.Modifiers;
            if (entry.ContainsKey("modifier"))
            {
                modifier = (KeyboardModifiers)entry["modifier"];
            }

            if (args.KeyChar == key && args.Modifiers == modifier)
            {
                char value = key;
                if (entry.TryGetValue("value", out object? value1))
                {
                    value = (char)value1;
                }

                AddOperator(value.ToString());
                return;
            }
        }

        if (args.Keys == KeyboardKeys.Return || args.Keys == KeyboardKeys.Enter || args.Keys == KeyboardKeys.NumPadEnter)
        {
            HandleEqauls();
        } else if (args.Keys == KeyboardKeys.Comma)
        {
            AddDecimal();
        } else if (args.Keys == KeyboardKeys.C)
        {
            Clear();
        }
    }


    private double accumulator = 0;
    private string operand = "";
    private string operation = "";
    private bool addComma = false;

    void AddNumber(string number)
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

    void AddDecimal()
    {
        if (addComma == false)
        {
            addComma = true;
            return;
        }
    }

    void AddOperator(string op)
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

        operation = op;

        EntryCalculations.Text += $" {operation} ";
    }

    void HandleEqauls()
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

    // hantering för numeriska knappar
    private void NumberButton(object sender, EventArgs e)
    {
        Button button = (Button)sender;
        AddNumber(button.Text);
    }

    private void CommaButton(object sender, EventArgs e)
    {
        AddDecimal();
    }

    // hantering för operator-knappar (+, -, *, /)
    private void OperatorButton(object sender, EventArgs e)
    {
        Button button = (Button)sender;
        AddOperator(button.Text);
    }

    private void EqualButton(object sender, EventArgs e)
    {
        HandleEqauls();
    }

    private void ClearButton(object sender, EventArgs e)
    {
        Clear();
    }
}