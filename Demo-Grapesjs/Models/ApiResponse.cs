namespace Demo_Grapesjs.Models
{
    public class ApiResponse<T>
{
    public T Result { get; set; }
    public string ResultCode { get; set; }
    public string Msg { get; set; }

    public ApiResponse(T result, string resultCode, string msg)
    {
        Result = result;
        ResultCode = resultCode;
        Msg = msg;
    }
}

}
