using Demo_Grapesjs.Dtos;
using Demo_Grapesjs.Entities;
using Demo_Grapesjs.Responses;


namespace Demo_Grapesjs.Services.Interfaces
{
    public interface IImageService
    {
        Task<PagedResponse<ImageDto>> Image_GetAll(int pageNumber, int pageSize);
        Task<Image> Image_InsertUpdate(ImageDto image);
        Task<Image> Image_Delete(string id);
    }
}
