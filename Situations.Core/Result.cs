namespace Situations.Core
{
    public class Result<T>
    {
        public T? Data { get; }
        public Exception? Exception { get; }
        public bool IsSuccess { get; }
        public bool IsFailure => !IsSuccess;

        private Result(T data)
        {
            Data = data;
            IsSuccess = true;
        }

        private Result(Exception exception)
        {
            Exception = exception;
        }

        public static Result<T> Success(T data)
        {
            return new Result<T>(data);
        }

        public static Result<T> Failure(Exception exception)
        {
            return new Result<T>(exception);
        }
    }
}
