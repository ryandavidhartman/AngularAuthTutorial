using AuthTestModel.Data;
using ServiceStack;

namespace AuthTest.Services
{
    [Authenticate]
    public class SecuredService : Service
    {
        public object Any(Secured request)
        {
            var session = GetSession();
            var userName = session.UserName;
            return new SecuredResponse { Result = request.Data, UserName = userName};
        }
    }
}