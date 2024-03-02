
namespace M_N_Store.Errors
{
    public class BaseCommonResponse
    {
        public BaseCommonResponse(int statuesCode, string message= null)
        {
            StatuesCode = statuesCode;
            Message = message?? DefaultMessageForStatusCode(statuesCode);
        }



        private string DefaultMessageForStatusCode(int statuesCode)
        {
            return statuesCode switch
            {
                400 => "bad request",
                401 => "not authorize",
                404 => "resource not found",
                500 => "server error",
                _ => null
            };
        }
        public int StatuesCode { get; set; }
        public string Message { get; set; }
    }
}
