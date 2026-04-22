using PRG1_MAUI_ERP_Activum.Model;
using PRG1_MAUI_ERP_Activum.Services;

namespace PRG1_MAUI_ERP_Activum.View;

public partial class SelectedCustomerNotesPage : ContentPage
{
    private readonly SelectedCustomerService selectedCustomerService = SelectedCustomerService.Instance;
    private readonly RegisterService _service = RegisterService.Instance;

    public SelectedCustomerNotesPage()
	{
		InitializeComponent();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        if (selectedCustomerService.SelectedCustomer != null)
        {
            Title.Text = $"{selectedCustomerService.SelectedCustomer.FirstName} {selectedCustomerService.SelectedCustomer.LastName} - Anteckningar";

            CustomerNotes.ItemsSource = null;
            CustomerNotes.ItemsSource = _service.GetNotesForCustomer(selectedCustomerService.SelectedCustomer.Id);
        }
    }

    private void Save_Clicked(object sender, EventArgs e)
    {
        Button button = (Button)sender;
        Note note = (Note)button.BindingContext;
        if (note != null)
        {
            Entry entry = (Entry)button.Parent.FindByName("MessageEntry");
            note.Message = entry.Text;
            note.EditDate = DateTime.Now;
        }
    }

    private void GoBack_Clicked(object sender, EventArgs e)
    {
        selectedCustomerService.GotoPage();
    }
}