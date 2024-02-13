namespace ToadaySolnBE.DTO
{
    public class ResponseDTO<T>
    {
        public bool Succeeded { get; set; }

        public T? Entity { get; set; }

        public string Message { get; set; }
        public ResponseDTO(bool succeeded, T? result, string message) { 
            Succeeded = succeeded;
            Message = message;
            Entity = result;
        }
        public ResponseDTO(bool succeeded, string message)
        {
            Succeeded = succeeded;
            Message = message;
        }
        public static ResponseDTO<T> Success(T entity, string message = "") {
            return new ResponseDTO<T>(true, entity, message);
        }
        public static ResponseDTO<T> Failure(T? entity, string message)
        {
            return new ResponseDTO<T>(false, entity, message);
        }

        public static ResponseDTO<T> Failure(Exception e)
        {
            return new ResponseDTO<T>(false, e.Message);
        }
    }
}
