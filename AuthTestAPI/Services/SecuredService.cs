using AuthTestModel.Data;
using ServiceStack;

namespace AuthTest.Services
{
    [Authenticate]
    public class SecuredService : Service
    {
        public object Any(Secured request)
        {
            return new SecuredResponse { Result = request.Data };
        }
    }
}