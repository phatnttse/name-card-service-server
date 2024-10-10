
using System.ComponentModel.DataAnnotations;

namespace Demo_Grapesjs.Entities
{
    public abstract class BaseEntity
    {

        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public DateTime UpdatedAt { get; set; }

        public DateTime CreatedAt { get; set; }

    }
}
