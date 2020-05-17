namespace Store.Core
{
    public class RequestResult<T>
    {
        public T RequestData { get; set; }
        public bool IsOk { get; set; }
        public string ExMessage { get; set; }
    }
}
