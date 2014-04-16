using System.Web.UI;
using AuthTestModel.Data;
using ServiceStack;
using ServiceStack.Caching;

namespace AuthTest
{
    public class PageBase : Page
    {
        /// <summary>
        /// Typed UserSession
        /// </summary>
        private object _userSession;
        protected virtual TUserSession SessionAs<TUserSession>()
        {
            return (TUserSession)(_userSession ?? (_userSession = Cache.SessionAs<TUserSession>()));
        }

        protected CustomUserSession UserSession
        {
            get
            {
                return SessionAs<CustomUserSession>();
            }
        }

        public new ICacheClient Cache
        {
            get { return HostContext.Resolve<ICacheClient>(); }
        }

        private ISessionFactory _sessionFactory;
        public virtual ISessionFactory SessionFactory
        {
            get { return _sessionFactory ?? (_sessionFactory = HostContext.Resolve<ISessionFactory>()) ?? new SessionFactory(Cache); }
        }

        /// <summary>
        /// Dynamic SessionBag Bag
        /// </summary>
        private ISession _sessionBag;
        public new ISession SessionBag
        {
            get
            {
                return _sessionBag ?? (_sessionBag = SessionFactory.GetOrCreateSession());
            }
        }

        public void ClearSession()
        {
            _userSession = null;
            Cache.Remove(SessionFeature.GetSessionKey());
        }
    }
}