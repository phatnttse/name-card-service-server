using System.ComponentModel.DataAnnotations;

namespace Demo_Grapesjs.Entities
{
    public class Video : BaseEntity
    {
        public string Poster {  get; set; } = string.Empty;
        public string Source { get; set; } = string.Empty;
        public string Title {  get; set; } = string.Empty;
        public int Width { get; set; }
        public int Height { get; set; } 
    }
}
