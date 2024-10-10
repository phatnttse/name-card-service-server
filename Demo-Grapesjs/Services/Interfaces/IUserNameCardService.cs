using Demo_Grapesjs.Entities;

namespace Demo_Grapesjs.Services.Interfaces
{
    public interface IUserNameCardService
    {
        Task<UserNameCard> UserNameCard_InsertUpdate(NameCardTemplate nameCardTemplate, User user, string id, string hostUrl, HttpContext httpContext);

        Task<UserNameCard> UserNameCard_GetBySlug(string slug);

    }
}
