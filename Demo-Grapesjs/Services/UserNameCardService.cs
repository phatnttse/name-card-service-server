using AutoMapper;
using Demo_Grapesjs.Entities;
using Demo_Grapesjs.Repositories;
using Demo_Grapesjs.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Text;
using Demo_Grapesjs.Helper;


namespace Demo_Grapesjs.Services
{
    public class UserNameCardService : IUserNameCardService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IQRCodeGeneratorHelper _qRCodeGeneratorHelper;


        public UserNameCardService(IUnitOfWork unitOfWork, IMapper mapper, IWebHostEnvironment webHostEnvironment,
            IQRCodeGeneratorHelper qRCodeGeneratorHelper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
            _qRCodeGeneratorHelper = qRCodeGeneratorHelper;
        }

        // Thêm hoặc cập nhật name card của người dùng
        public async Task<UserNameCard> UserNameCard_InsertUpdate(NameCardTemplate nameCardTemplate, User user, string id, string frHostUrl, HttpContext httpContext)
        {
            if (string.IsNullOrEmpty(id))
            {
                // Normalize the name and phone number to create the slug
                string normalizedSlug = GenerateSlug(user.FullName);

                var fileName = Path.GetFileName(nameCardTemplate.Url);
                var filePath = Path.Combine(_webHostEnvironment.WebRootPath, "files", fileName);

                // Check if the file exists
                if (!System.IO.File.Exists(filePath))
                {
                    throw new Exception("File not found");
                }

                // Read the file content
                var htmlContent = await System.IO.File.ReadAllTextAsync(filePath, Encoding.UTF8);

                // Replace placeholders with user data (use string.Replace or other methods)
                var resultHtml = htmlContent
                    .Replace("{{fullName}}", user.FullName)
                    .Replace("{{email}}", user.Email)
                    .Replace("{{phoneNumber}}", user.PhoneNumber)
                    .Replace("{{companyAddress}}", user.CompanyAddress)
                    .Replace("{{avatar}}", user.Avatar)
                    .Replace("{{coverPhoto}}", user.CoverPhoto)
                    .Replace("{{companyName}}", user.CompanyName)
                    .Replace("{{websiteUrl}}", user.WebsiteUrl)
                    .Replace("{{linkedInUrl}}", user.LinkedInUrl)
                    .Replace("{{position}}", user.Position);


                // Generate the file name for the new HTML file
                var newFileName = $"{normalizedSlug}.html";
                var newFilePath = Path.Combine(_webHostEnvironment.WebRootPath, "files", newFileName);

                // Write the modified HTML content to the new file
                await System.IO.File.WriteAllTextAsync(newFilePath, resultHtml, Encoding.UTF8);

                // Get the current hosting URL (e.g., https://localhost:7191)
                var hostUrl = $"{httpContext.Request.Scheme}://{httpContext.Request.Host}";

                // Generate and save the QR code image
                var qrCodeFileName = $"{normalizedSlug}.png";
                var qrCodeFilePath = _qRCodeGeneratorHelper.SaveQRCode($"{frHostUrl}/{normalizedSlug}", qrCodeFileName);
                var qrCodeUrl = $"{hostUrl}/qrcodes/{qrCodeFileName}";

                UserNameCard userNameCard = new UserNameCard
                {
                    UserId = user.Id,
                    NameCardTemplateId = nameCardTemplate.Id,
                    User = user,
                    NameCardTemplate = nameCardTemplate,
                    Slug = normalizedSlug,
                    LinkUrl = $"{hostUrl}/files/{newFileName}",
                    QRCodeUrl = qrCodeUrl

                };

                UserNameCard newUserNameCard = await _unitOfWork.GetRepository<UserNameCard>().CreateAsync(userNameCard);
                await _unitOfWork.SaveChangesAsync();

                return newUserNameCard;
            }else
            {
                // Fetch the existing UserNameCard record
                var existingUserNameCard = await _unitOfWork.GetRepository<UserNameCard>().GetByIdAsync(id);

                if (existingUserNameCard == null)
                {
                    throw new Exception("UserNameCard not found");
                }

                // Update the HTML template
                var fileNameTempate = Path.GetFileName(nameCardTemplate.Url);
                var filePathTemplate = Path.Combine(_webHostEnvironment.WebRootPath, "files", fileNameTempate);

                if (!System.IO.File.Exists(filePathTemplate))
                {
                    throw new Exception("File not found");
                }

                var htmlContent = await System.IO.File.ReadAllTextAsync(filePathTemplate, Encoding.UTF8);
                var resultHtml = htmlContent
                    .Replace("{{fullName}}", user.FullName)
                    .Replace("{{email}}", user.Email)
                    .Replace("{{phoneNumber}}", user.PhoneNumber)
                    .Replace("{{companyAddress}}", user.CompanyAddress)
                    .Replace("{{avatar}}", user.Avatar)
                    .Replace("{{coverPhoto}}", user.CoverPhoto)
                    .Replace("{{companyName}}", user.CompanyName)
                    .Replace("{{websiteUrl}}", user.WebsiteUrl)
                    .Replace("{{linkedInUrl}}", user.LinkedInUrl)
                    .Replace("{{position}}", user.Position);

                var fileNameUserCard = Path.GetFileName(existingUserNameCard.LinkUrl);
                var filePathUserCard = Path.Combine(_webHostEnvironment.WebRootPath, "files", fileNameUserCard);

                if (!System.IO.File.Exists(filePathTemplate))
                {
                    throw new Exception("File not found");
                }

                // Ghi đè nội dung mới vào file
                await File.WriteAllTextAsync(filePathUserCard, resultHtml, Encoding.UTF8);

                return existingUserNameCard;

            }
          
        }

        // Lấy name card của người dùng theo slug
        public async Task<UserNameCard> UserNameCard_GetBySlug(string slug)
        {
            var repository = _unitOfWork.GetRepository<UserNameCard>();
            var userNameCard = await repository.GetQueryable()
                .Include(unc => unc.User)
                .Include(unc => unc.NameCardTemplate)
                .FirstOrDefaultAsync(unc => unc.Slug == slug);

            if (userNameCard == null)
            {
                throw new Exception("User name card not found.");
            }

            return userNameCard;
        }

        // Helper method to generate slug
        private string GenerateSlug(string name)
        {
            // Remove diacritics (accents) and convert to lowercase
            string normalizedString = RemoveDiacritics(name.ToLower());

            // Replace spaces with hyphens
            normalizedString = normalizedString.Replace(" ", "-");

            // Append the phone number to the slug
            return $"{normalizedString}-{Guid.NewGuid().ToString()}";
        }

        // Method to remove diacritics (accents) from the name
        private string RemoveDiacritics(string text)
        {
            var normalizedString = text.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();

            foreach (var c in normalizedString)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }
        }
}
