using Demo_Grapesjs.Dtos;
using Demo_Grapesjs.Entities;

namespace Demo_Grapesjs.Services.Interfaces
{
    public interface IUserService
    {
        Task<User> User_InsertUpdate(InsertUpdateUserDto createUserDto);
    }
}
