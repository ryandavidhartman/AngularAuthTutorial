using System;
using System.Collections.Generic;
using System.Configuration;
using ServiceStack;
using ServiceStack.Auth;
using ServiceStack.OrmLite;

namespace DbSetup
{
    public class Program
    {
        private static OrmLiteAuthRepository _authRepository;

        public static void Main()
        {

            const string adminName = "Administrator";
            const string adminPassword = "bobafet12bobafet12";
            const string userName = "user";
            const string userPassword = "p@55word";

            var recreateAuthTables = ConfigurationManager.AppSettings["RecreateAuthTables"];
            var dbConnectionFactory = new OrmLiteConnectionFactory(ConfigurationManager.ConnectionStrings["AuthDB"].ConnectionString, SqlServerDialect.Provider);

            _authRepository = new OrmLiteAuthRepository(dbConnectionFactory);


            if ("true".Equals(recreateAuthTables, StringComparison.CurrentCultureIgnoreCase))
            {
                _authRepository.DropAndReCreateTables(); //Drop and re-create all Auth and registration tables
                CreateUser(1, adminName, "rhartman@omnisite.com", adminPassword, new List<string> {"Admin"}, new List<string> {"Administrator"});
                CreateUser(2, userName, null, userPassword, new List<string> { "TheRole" }, new List<string> { "ThePermission" });
            }
            else
            {
                _authRepository.InitSchema(); //Create only the missing tables
            }
        }

       

        private static void CreateUser(int id, string username, string email, string password, List<string> roles = null, List<string> permissions = null)
        {
            string hash;
            string salt;
            new SaltedHash().GetHashAndSaltString(password, out hash, out salt);

            _authRepository.CreateUserAuth(new UserAuth
            {
                Id = id,
                DisplayName = "DisplayName",
                Email = email ?? "as@if{0}.com".Fmt(id),
                UserName = username,
                FirstName = "FirstName",
                LastName = "LastName",
                PasswordHash = hash,
                Salt = salt,
                Roles = roles,
                Permissions = permissions

            }, password);



        }
    }
}