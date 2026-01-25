using PRG1_MAUI_ERP_Activum.Model;

namespace PRG1_MAUI_ERP_Activum.View
{
    public partial class LoginPage : ContentPage
    {
        private readonly List<User> users = new List<User>
        {
            new User { Username = "admin", Password = "admin123" },
            new User { Username = "user", Password = "user123" }
        };

        public LoginPage()
        {
            InitializeComponent();
        }

        private async Task<string> GetUserName()
        {
            string userNameValue = EmailEntry.Text;
            if (string.IsNullOrWhiteSpace(userNameValue))
            {
                await DisplayAlertAsync("Ogiltigt Användarnamn", "För att logga in så bör du ange ett Användarnamn / E-Mail.", "Ok");
                EmailEntry.Focus();
                return null;
            }

            return userNameValue;
        }
        private async Task<string> GetPassword()
        {
            string passwordValue = PasswordEntry.Text;
            if (string.IsNullOrWhiteSpace(passwordValue))
            {
                await DisplayAlertAsync("Ogiltigt Lösenord", "För att logga in så bör du ange ett Lösenord.", "Ok");
                PasswordEntry.Focus();
                return null;
            }

            return passwordValue;
        }

        private bool isUserValid(string username, string password)
        {
            foreach (User user in users)
            {
                if (user.Username == username && user.Password == password)
                {
                    return true;
                }
            }
            return false;
        }

        private async void Login()
        {
            string userNameValue;
            string passwordValue;

            userNameValue = await GetUserName();
            if (userNameValue == null)
            {
                return;
            }
            passwordValue = await GetPassword();
            if (passwordValue == null)
            {
                return;
            }

            bool isValid = isUserValid(userNameValue, passwordValue);
            if (!isValid)
            {
                await DisplayAlertAsync("Inloggning Misslyckades", "Användarnamnet eller lösenordet är felaktigt. Försök igen.", "Ok");
                return;
            }

            await DisplayAlertAsync("Inloggad", $"Du är nu inloggad som \"{userNameValue}\"!", "Ok");
            if (Shell.Current is AppShell shell)
            {
                shell.IsLoggedIn = true;
                shell.LoggedInUser = userNameValue;

                shell.EnableFlyout();
                await shell.GoToAsync($"//MainPage");
            }
            return;
        }

        private void Login_Clicked(object sender, EventArgs e)
        {
            Login();
        }
    }
}
