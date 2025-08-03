namespace Tareas.API.Services.Implementaciones
{
    public class ServiceResponse<T>
    {
        public T Data { get; set; }
        public string Message { get; set; }
        public bool Success { get; set; }

        public ServiceResponse() { }
        public ServiceResponse(T data, string message = "", bool success = true)
        {
            Data = data;
            Message = message;
            Success = success;
        }

        public static ServiceResponse<T> Ok(T data, string message = "")
        {
            return new ServiceResponse<T>(data, message, true);
        }

        public static ServiceResponse<T> Error(string message)
        {
            return new ServiceResponse<T>(default, message, false);
        }
    }
}