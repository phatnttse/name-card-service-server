namespace Demo_Grapesjs.Dtos
{
    public class InsertUpdateUserNameCardDto
    {
        public string? Id { get; set; }
        public string? NameCardId { get; set; }
        public string? HostUrl { get; set; }
        public InsertUpdateUserDto? User { get; set; }

    }
}
