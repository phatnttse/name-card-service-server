using System.ComponentModel.DataAnnotations;

namespace Demo_Grapesjs.Entities
{
    public class Image : BaseEntity
    {
        public string Src { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public int Width { get; set; }
        public int Height { get; set; }
    }
}
