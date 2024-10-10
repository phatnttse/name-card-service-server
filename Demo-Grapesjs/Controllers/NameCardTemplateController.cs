using Demo_Grapesjs.Dtos;
using Demo_Grapesjs.Models;
using Demo_Grapesjs.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Demo_Grapesjs.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Microsoft.AspNetCore.Cors.EnableCors("CorsPolicy")]
    public class NameCardTemplateController : ControllerBase
    {
        private readonly INameCardTemplateService _nameCardTemplateService;
        public NameCardTemplateController(INameCardTemplateService nameCardTemplateService) { _nameCardTemplateService = nameCardTemplateService; }

        /// <summary>
        /// Lấy tất cả các template
        /// </summary>
        /// <returns></returns>
        [HttpGet("")]
        [AllowAnonymous]
        [ProducesResponseType( typeof(ApiResponse<object>),200)]
        [ProducesResponseType(typeof(ApiResponse<object>), 400)]
        public async Task<IActionResult> NameCardTemplate_GetAll()
        {
            try
            {
                var nameCardTemplates = await _nameCardTemplateService.NameCardTemplate_GetAll();
                var response = new ApiResponse<object>(nameCardTemplates, "OK", "Successfully");
                return Ok(response);
            }
            catch (Exception ex)
            {
                var response = new ApiResponse<object>(null, "ERROR", ex.Message);
                return BadRequest(response);

            }
        }


        /// <summary>
        /// Lấy template theo id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ApiResponse<object>), 200)]
        [ProducesResponseType(typeof(ApiResponse<object>), 400)]
        public async Task<IActionResult> NameCardTemplate_GetById([FromRoute] string id)
        {
            try
            {
                var nameCardTemplate = await _nameCardTemplateService.NameCardTemplate_GetById(id);
                var response = new ApiResponse<object>(nameCardTemplate, "OK", "Successfully");
                return Ok(response);

            }
            catch (Exception ex)
            {
                var response = new ApiResponse<object>(null, "ERROR", ex.Message);
                return BadRequest(response);
            }
        }


        /// <summary>
        /// Thêm hoặc cập nhật template
        /// </summary>
        /// <param name="insertUpdateNameCardTemplate"></param>
        /// <returns></returns>
        [HttpPost("")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ApiResponse<object>), 200)]
        [ProducesResponseType(typeof(ApiResponse<object>), 400)]
        public async Task<IActionResult> NameCardTemplate_InsertUpdate(InsertUpdateNameCardTemplateDto insertUpdateNameCardTemplate)
        {
            try
            {
                var nameCardTemplate = await _nameCardTemplateService.NameCardTemplate_InsertUpdate(insertUpdateNameCardTemplate, HttpContext);
                var response = new ApiResponse<object>(nameCardTemplate, "OK", "Successfully");
                return Ok(response);
            }
            catch (Exception ex)
            {
                var response = new ApiResponse<object>(null, "ERROR", ex.Message);
                return BadRequest(response);

            }
        }
    }
}
