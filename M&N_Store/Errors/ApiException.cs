namespace M_N_Store.Errors
{
    public class ApiException : BaseCommonResponse
    {
        public ApiException(int statuesCode, string message = null, string details = null) : base(statuesCode, message)
        {
            Details = details;
        }
        public string Details { get; set; }
    }
}
