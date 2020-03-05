namespace Library.Core
{
    public class OperationStatus
    {
        public bool Success { get; set; }
        public string ErrorMessage { get; set; }

        public static OperationStatus CompletedSuccessfully = new OperationStatus()
        {
            Success = true
        };
    }
}
