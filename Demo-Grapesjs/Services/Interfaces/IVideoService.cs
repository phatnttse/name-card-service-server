using Demo_Grapesjs.Entities;

namespace Demo_Grapesjs.Services.Interfaces
{
    public interface IVideoService
    {
        Task<Video> Video_InsertUpdate(Video video);
        Task<IEnumerable<Video>> Video_GetAll();
        Task<Video> Video_Delete(string id);

    }
}
