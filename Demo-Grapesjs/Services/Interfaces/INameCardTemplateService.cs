using Demo_Grapesjs.Dtos;
using Demo_Grapesjs.Entities;

namespace Demo_Grapesjs.Services.Interfaces
{
    public interface INameCardTemplateService
    {
        Task<NameCardTemplate> NameCardTemplate_InsertUpdate(InsertUpdateNameCardTemplateDto nameCardTemplateDto, HttpContext httpContext);
        Task<IEnumerable<NameCardTemplate>> NameCardTemplate_GetAll();
        Task<NameCardTemplate> NameCardTemplate_GetById(string id);
        Task NameCardTemplate_Delete (string id);
    }
}
