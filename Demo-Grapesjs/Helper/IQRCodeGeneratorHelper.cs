namespace Demo_Grapesjs.Helper
{
    public interface IQRCodeGeneratorHelper
    {
        byte[] GeneratorQRCode(string text);
        string SaveQRCode(string text, string fileName);
    } 
}
