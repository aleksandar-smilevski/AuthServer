namespace AuthServer.API.Helpers
{
    public class ResponseObject<T>
    {
        public T Data { get; set; }
        public string Error { get; set; }
        public ResponseType ResponseType { get; set; }
    }

}