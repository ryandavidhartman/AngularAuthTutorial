using AuthTestModel.Data;
using ServiceStack;

namespace AuthTest.Services
{
    public class UnsecuredService : Service
    {
        public object Any(Unsecured request)
        {
            return new UnsecuredResponse { Result = request.Data };
        }
    }
}