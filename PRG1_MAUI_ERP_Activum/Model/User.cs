namespace PRG1_MAUI_ERP_Activum.Model
{
    class User
    {
        public required string Username { get; set; }
        public required string Password { get; set; }
        public string Role { get; set; } = "Försäljare";
    }
}
