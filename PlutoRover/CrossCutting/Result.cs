namespace PlutoRover.CrossCutting
{
    // Result object to aggregate a T object and the Error class
    public class Result<T>
    {
        public T Data { get; }

        public Error Error { get; }

        public bool HasError => Error is not null;

        public Result(T data) => Data = data;

        public Result(Error error) => Error = error;
    }
}