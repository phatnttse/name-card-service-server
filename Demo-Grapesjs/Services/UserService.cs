using AutoMapper;
using Demo_Grapesjs.Dtos;
using Demo_Grapesjs.Entities;
using Demo_Grapesjs.Repositories;
using Demo_Grapesjs.Services.Interfaces;

namespace Demo_Grapesjs.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        // Thêm hoặc cập nhật thông tin người dùng
        public async Task<User> User_InsertUpdate(InsertUpdateUserDto insertUpdateUserDto)
        {
            User userResponse = null;

            if (insertUpdateUserDto.Id == null)
            {
                var user = _mapper.Map<User>(insertUpdateUserDto);
                userResponse = await _unitOfWork.GetRepository<User>().CreateAsync(user);
                await _unitOfWork.SaveChangesAsync();
            }else
            {
                var user = _mapper.Map<User>(insertUpdateUserDto);
                user.Id = insertUpdateUserDto.Id;
                userResponse = await _unitOfWork.GetRepository<User>().UpdateAsync(user);
                await _unitOfWork.SaveChangesAsync();
            }

            return userResponse;

        }
    }
}
