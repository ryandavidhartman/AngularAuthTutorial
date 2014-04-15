using ServiceStack;

namespace AuthTestModel.Data
{
    public class CustomUserSession : AuthUserSession
    {
        public string CustomProperty { get; set; }
    }
}
