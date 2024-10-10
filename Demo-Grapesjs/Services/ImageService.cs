using AutoMapper;
using Demo_Grapesjs.Dtos;
using Demo_Grapesjs.Entities;
using Demo_Grapesjs.Repositories;
using Demo_Grapesjs.Responses;
using Demo_Grapesjs.Services.Interfaces;

namespace Demo_Grapesjs.Services
{
    public class ImageService : IImageService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ImageService(IUnitOfWork unitOfWork, IMapper mapper) 
        {  
            _unitOfWork = unitOfWork; 
            _mapper = mapper; 
        }

        // Hàm thêm hoặc cập nhật ảnh
        public async Task<Image> Image_InsertUpdate(ImageDto imageDto)
        {
            if (imageDto == null) throw new ArgumentNullException(nameof(imageDto));
            
            Image image = _mapper.Map<Image>(imageDto);
            image.Id = Guid.NewGuid().ToString();
            Image newImage = await _unitOfWork.GetRepository<Image>().CreateAsync(image);

            await _unitOfWork.SaveChangesAsync();

            return newImage;
        }

        // Hàm lấy tất cả ảnh
        public async Task<PagedResponse<ImageDto>> Image_GetAll(int pageNumber, int pageSize)
        {
            var repository = _unitOfWork.GetRepository<Image>();

            var totalCount = await repository.CountAsync();
            var images = await repository.GetPagedAsync(pageNumber, pageSize);

            var imageDtos = _mapper.Map<IEnumerable<ImageDto>>(images);

            return new PagedResponse<ImageDto>
            {
                Items = imageDtos,
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }

        // Hàm xóa ảnh
        public async Task<Image> Image_Delete(string id)
        {
            // Tìm kiếm ảnh trong cơ sở dữ liệu
            var existingImage = await _unitOfWork.GetRepository<Image>().GetByIdAsync(id) ?? throw new Exception("Image not found");

            // Giả sử thuộc tính src chứa URL đầy đủ (ví dụ: "http://localhost:5000/images/icon-logo.svg")
            var imageUrl = existingImage.Src;

            // Trích xuất tên tệp từ URL
            var imageName = Path.GetFileName(new Uri(imageUrl).LocalPath);

            // Kết hợp tên tệp với thư mục "wwwroot/images"
            var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", imageName);

            // Kiểm tra nếu tệp tồn tại trong thư mục wwwroot/images
            if (File.Exists(imagePath))
            {
                // Xóa tệp ảnh từ thư mục wwwroot/images
                File.Delete(imagePath);
            }

            // Xóa bản ghi trong cơ sở dữ liệu
            await _unitOfWork.GetRepository<Image>().DeleteAsync(id);

            // Lưu thay đổi vào cơ sở dữ liệu
            await _unitOfWork.SaveChangesAsync();

            // Trả về thông tin ảnh đã bị xóa
            return existingImage;
        }

    }
}
