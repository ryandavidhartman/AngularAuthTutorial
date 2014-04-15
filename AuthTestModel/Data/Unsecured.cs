using ServiceStack;

namespace AuthTestModel.Data
{
    [Route("/Unsecured", "POST")]
    [Route("/Unsecured/{Data}", "GET")]
    public class Unsecured : IReturn<UnsecuredResponse>
    {
        public string Data { get; set; }
    }

    public class UnsecuredResponse
    {
        public string Result { get; set; }
        public ResponseStatus ResponseStatus { get; set; }
    }
}
