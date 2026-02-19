using Microsoft.Maui.Controls;
using PRG1_MAUI_ERP_Activum.View;

namespace PRG1_MAUI_ERP_Activum
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            FlyoutBehavior = FlyoutBehavior.Disabled;

            if (Preferences.Get("IsLoggedIn", false))
            {
                FlyoutBehavior = FlyoutBehavior.Flyout;
                FlyoutView.UpdateUsername();
            }
        }

        public void UserLoggedIn()
        {
            EnableFlyout();
            FlyoutView.UpdateUsername();
        }

        public void EnableFlyout()
        {
            FlyoutBehavior = FlyoutBehavior.Flyout;
        }

        public void DisableFlyout()
        {
            FlyoutBehavior = FlyoutBehavior.Disabled;
        }
    }

}
