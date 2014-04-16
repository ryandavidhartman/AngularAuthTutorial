using System.Configuration;
using System.Reflection;
using Funq;
using ServiceStack;
using ServiceStack.Auth;
using ServiceStack.Configuration;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using AuthTestModel.Data;

namespace AuthTest
{
    public class AuthTestAppHost : AppHostBase
    {
        public AuthTestAppHost() : base("Auth Test Service", typeof (AuthTestAppHost).Assembly)
        {
        }

        public AuthTestAppHost(string serviceName, params Assembly[] assembliesWithServices) : base(serviceName, assembliesWithServices)
        {
        }

        public override void Configure(Container container)
        {
            var appSettings = new AppSettings();

            //Default route: /auth/{provider}
            Plugins.Add(new AuthFeature(() => new CustomUserSession(), new IAuthProvider[] { new BasicAuthProvider(appSettings) }));

            //Default route: /register
            Plugins.Add(new RegistrationFeature());


            var dbConnectionFactory = new OrmLiteConnectionFactory(ConfigurationManager.ConnectionStrings["AuthDB"].ConnectionString, SqlServerDialect.Provider);
            container.Register<IDbConnectionFactory>(dbConnectionFactory);

            //Store User Data into the referenced SqlServer database
            container.Register<IUserAuthRepository>(c => new OrmLiteAuthRepository(c.Resolve<IDbConnectionFactory>()));
            
            Plugins.Add(new CorsFeature());
        }
    }
}