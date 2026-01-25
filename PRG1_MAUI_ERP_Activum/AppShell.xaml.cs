namespace PRG1_MAUI_ERP_Activum
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();


            FlyoutBehavior = FlyoutBehavior.Disabled;
        }

        public bool IsLoggedIn = false;
        public string LoggedInUser;

        public void EnableFlyout()
        {
            FlyoutBehavior = FlyoutBehavior.Flyout;
        }
    }
}
