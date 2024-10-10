namespace Demo_Grapesjs.Dtos
{
    public class InsertUpdateUserDto
    {
        public string? Id { get; set; }
        public string Avatar { get; set; } = string.Empty;
        public string CoverPhoto { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string CompanyName { get; set; } = string.Empty;
        public string Position { get; set; } = string.Empty;
        public string CompanyAddress { get; set; } = string.Empty;
        public string WebsiteUrl { get; set; } = string.Empty;
    }
}
