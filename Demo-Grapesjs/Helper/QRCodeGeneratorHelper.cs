using Microsoft.AspNetCore.Hosting;
using QRCoder;

namespace Demo_Grapesjs.Helper
{
    public class QRCodeGeneratorHelper : IQRCodeGeneratorHelper
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public QRCodeGeneratorHelper(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public byte[] GeneratorQRCode(string text)
        {
            byte[] QRCode = new byte[0];
            if (!string.IsNullOrEmpty(text))
            {
                QRCodeGenerator qrGenerator = new QRCodeGenerator();
                QRCodeData qrCodeData = qrGenerator.CreateQrCode(text, QRCodeGenerator.ECCLevel.Q);
                BitmapByteQRCode bitmapByteQRCode = new BitmapByteQRCode(qrCodeData);
                QRCode = bitmapByteQRCode.GetGraphic(20);
            }
            return QRCode;
        }

        public string SaveQRCode(string text, string fileName)
        {
            if (string.IsNullOrEmpty(text))
            {
                throw new ArgumentException("Text cannot be null or empty", nameof(text));
            }

            // Generate QR code
            byte[] qrCodeBytes = GeneratorQRCode(text);

            // Define the path to save the QR code image
            var qrCodeFolderPath = Path.Combine(_webHostEnvironment.WebRootPath, "qrcodes");
            if (!Directory.Exists(qrCodeFolderPath))
            {
                Directory.CreateDirectory(qrCodeFolderPath);
            }

            var filePath = Path.Combine(qrCodeFolderPath, fileName);

            // Save the QR code image as a PNG file
            using (var stream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                stream.Write(qrCodeBytes, 0, qrCodeBytes.Length);
            }

            // Return the relative path of the saved QR code image
            return filePath;
        }
    }
}
