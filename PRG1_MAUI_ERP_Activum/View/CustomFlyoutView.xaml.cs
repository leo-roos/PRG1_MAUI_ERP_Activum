using System.Diagnostics;
using System.Windows.Input;

namespace PRG1_MAUI_ERP_Activum.View;

public partial class CustomFlyoutView : ContentView
{
    public ICommand NavigateCommand { get; }
    public ICommand ToggleToolsCommand { get; }

    private bool _isToolsOpen;
    public bool IsToolsOpen
    {
        get => _isToolsOpen;
        set
        {
            _isToolsOpen = value;
            OnPropertyChanged();
        }
    }

    public void UpdateUsername()
    {
        string LoggedInUser = Preferences.Get("LoggedInUser", "");
        if (LoggedInUser != "")
        {
            LoggedInAsLabel.Text = $"Inloggad som: \"{LoggedInUser}\"";
            LoggedInAsLabel.IsVisible = true;
        }
        else
        {
            LoggedInAsLabel.Text = "";
            LoggedInAsLabel.IsVisible = false;
        }
    }

    public CustomFlyoutView()
    {
        NavigateCommand = new Command<string>(async (page) =>
        {
            Debug.WriteLine($"[CustomFlyoutView] NavigateCommand: {page}");
            await Shell.Current.GoToAsync($"//{page}");
            Shell.Current.FlyoutIsPresented = false;
        });

        ToggleToolsCommand = new Command(() =>
        {
            Debug.WriteLine("[CustomFlyoutView] ToggleToolsCommand executed");
            IsToolsOpen = !IsToolsOpen;
            ToolsSubMenu.IsVisible = IsToolsOpen;
        });

        InitializeComponent();

        BindingContext = this;

        UpdateUsername();
    }

    private async void Logout_Clicked(object sender, EventArgs e)
    {
        Preferences.Remove("IsLoggedIn");
        Preferences.Remove("LoggedInUser");
        UpdateUsername();

        if (Shell.Current is AppShell shell)
        {
            shell.DisableFlyout();
            shell.FlyoutIsPresented = false;
        }

        await Shell.Current.GoToAsync("//LoginPage");
    }
}

