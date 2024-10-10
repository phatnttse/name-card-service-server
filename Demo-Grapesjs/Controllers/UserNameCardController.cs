using Demo_Grapesjs.Dtos;
using Demo_Grapesjs.Entities;
using Demo_Grapesjs.Models;
using Demo_Grapesjs.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;


namespace Demo_Grapesjs.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UserNameCardController : ControllerBase
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly INameCardTemplateService _nameCardTemplateService;
        private readonly IUserService _userService;
        private readonly IUserNameCardService _userNameCardService;

        public UserNameCardController(IWebHostEnvironment webHostEnvironment, INameCardTemplateService nameCardTemplateService, IUserService userService, IUserNameCardService userNameCardService)
        {
            _webHostEnvironment = webHostEnvironment;
            _nameCardTemplateService = nameCardTemplateService;
            _userService = userService;
            _userNameCardService = userNameCardService;
        }

        /// <summary>
        /// Thêm hoặc cập nhật name card của người dùng
        /// </summary>
        /// <param name="insertUpdateUserNameCardDto"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<object>), 200)]
        [ProducesResponseType(typeof(ApiResponse<object>), 400)]
        public async Task<IActionResult> UserNameCard_InsertUpdate(InsertUpdateUserNameCardDto insertUpdateUserNameCardDto)
        {
            try
            {              
                if (insertUpdateUserNameCardDto == null) throw new ArgumentNullException(nameof(insertUpdateUserNameCardDto));

                var template = await _nameCardTemplateService.NameCardTemplate_GetById(insertUpdateUserNameCardDto.NameCardId!);

                User newUser = await _userService.User_InsertUpdate(insertUpdateUserNameCardDto.User!);

                UserNameCard newUserNameCard = await _userNameCardService.UserNameCard_InsertUpdate(template, newUser, insertUpdateUserNameCardDto?.Id, insertUpdateUserNameCardDto?.HostUrl!, HttpContext);

                var response = new ApiResponse<object>(newUserNameCard, "OK", "Successfully");
                return Ok(response);
            }
            catch (Exception ex)
            {
                var response = new ApiResponse<object>(null, "Error", ex.Message);
                return BadRequest(response);
            }
        }

        /// <summary>
        /// Lấy thông tin name card của người dùng theo slug
        /// </summary>
        /// <param name="slug"></param>
        /// <returns></returns>
        [HttpGet("{slug}")]
        public async Task<IActionResult> UserNameCard_GetBySlug([FromRoute] string slug)
        {
            try
            {
                var userNameCard = await _userNameCardService.UserNameCard_GetBySlug(slug);

                var response = new ApiResponse<object>(userNameCard, "OK", "Successfully");
                return Ok(response);
            }
            catch (Exception ex)
            {
                var response = new ApiResponse<object>(null, "Error", ex.Message);
                return BadRequest(response);
            }
        }


    }
}
