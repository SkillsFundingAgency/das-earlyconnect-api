namespace SFA.DAS.EarlyConnect.Application.Responses
{
    public abstract class BaseResponse
    {
        public ResponseCode ResultCode { get; set; }
        public List<object> ValidationErrors { get; set; } = new List<object>();
    }

    public enum ResponseCode
    {
        Success,
        InvalidRequest,
        NotFound,
        Created
    }
}
