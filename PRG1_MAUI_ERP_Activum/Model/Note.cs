namespace PRG1_MAUI_ERP_Activum.Model
{
    public class Note
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Message { get; set; } = "";
        public DateTime Date { get; set; } = DateTime.Now;
        public DateTime EditDate { get; set; } = DateTime.Now;
    }
}
