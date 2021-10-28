namespace PlutoRover.CrossCutting
{
    // Error class to aggregate both an error code and default message
    public class Error
    {
        public int Code { get; set; }
        public string Msg { get; set; }

        public Error(int code, string msg) => (Code, Msg) = (code, msg);
    }
}