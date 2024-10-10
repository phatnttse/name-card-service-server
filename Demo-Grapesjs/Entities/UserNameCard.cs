using System.ComponentModel.DataAnnotations;

namespace Demo_Grapesjs.Entities
{
    public class UserNameCard : BaseEntity
    {
        public string Slug { get; set; } = string.Empty;
        public string LinkUrl { get; set; } = string.Empty;
        public string QRCodeUrl { get; set; } = string.Empty;
        public string UserId { get; set; }
        public User? User { get; set; }
        public string NameCardTemplateId { get; set; }
        public NameCardTemplate? NameCardTemplate { get; set; }

    }
}
