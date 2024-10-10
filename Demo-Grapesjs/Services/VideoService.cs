using AutoMapper;
using Demo_Grapesjs.Entities;
using Demo_Grapesjs.Repositories;
using Demo_Grapesjs.Services.Interfaces;

namespace Demo_Grapesjs.Services
{
    public class VideoService : IVideoService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public VideoService(IUnitOfWork unitOfWork, IMapper mapper, IWebHostEnvironment hostingEnvironment)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        // Thêm hoặc cập nhật video
        public async Task<Video> Video_InsertUpdate(Video video)
        {
            await _unitOfWork.GetRepository<Video>().CreateAsync(video);
            await _unitOfWork.SaveChangesAsync();
            return video;
        }

        // Lấy tất cả video
        public Task<IEnumerable<Video>> Video_GetAll()
        {
            var response = _unitOfWork.GetRepository<Video>().GetAllAsync();
            return response;
        }

        // Xóa video
        public async Task<Video> Video_Delete(string id)
        {
            Video existingVideo = await _unitOfWork.GetRepository<Video>().GetByIdAsync(id) ?? throw new Exception("Video not found");

            // Trích xuất tên tệp từ URL
            var videos = Path.GetFileName(new Uri(existingVideo.Source).LocalPath);

            // Kết hợp tên tệp với thư mục "wwwroot/videos"
            var videoPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "videos", videos);

            // Kiểm tra nếu tệp tồn tại trong thư mục wwwroot/videos
            if (File.Exists(videoPath))
            {
                // Xóa tệp ảnh từ thư mục wwwroot/videos
                File.Delete(videoPath);
            }

            await _unitOfWork.GetRepository<Video>().DeleteAsync(id);

            await _unitOfWork.SaveChangesAsync();

            return existingVideo;
        }
    }
}
