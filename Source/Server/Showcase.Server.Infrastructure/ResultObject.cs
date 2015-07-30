namespace Showcase.Server.Infrastructure
{
    public class ResultObject<T> where T : class
    {
        public ResultObject(T data)
        {
            this.Success = true;
            this.ErrorMessage = null;
            this.Data = data;
        }

        public ResultObject(bool success, string errorMessage, T data = null)
        {
            this.Success = success;
            this.ErrorMessage = errorMessage;
            this.Data = data;
        }

        public bool Success { get; set; }

        public string ErrorMessage { get; set; }

        public T Data { get; set; }
    }
}