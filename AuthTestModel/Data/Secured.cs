using ServiceStack;

namespace AuthTestModel.Data
{
    [Route("/Secured", "POST")]
    [Route("/Secured/{Data}", "GET")]
    public class Secured : IReturn<SecuredResponse>
    {
        public string Data { get; set; }
    }

    public class SecuredResponse
    {
        public string Result { get; set; }
        public string UserName { get; set; }
        public ResponseStatus ResponseStatus { get; set; }
    }
}