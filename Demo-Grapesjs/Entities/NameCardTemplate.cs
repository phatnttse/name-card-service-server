using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Demo_Grapesjs.Entities
{
    public class NameCardTemplate : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Thumbnail { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
        [JsonIgnore]
        public ICollection<UserNameCard>? UserNameCards { get; set; }

    }
}
