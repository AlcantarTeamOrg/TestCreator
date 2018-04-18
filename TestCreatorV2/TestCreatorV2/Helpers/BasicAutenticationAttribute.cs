using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using TestCreatorV2.Models;

namespace TestCreatorV2.Helpers
{
    public class BasicAutenticationAttribute: AuthorizationFilterAttribute
    {

        public override void OnAuthorization(HttpActionContext actionContext)
        {
            if(actionContext.Request.Headers.Authorization == null)
            {
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
            }
            else
            {
                string authoToken = actionContext.Request.Headers.Authorization.Parameter;
                string decodedToken =Encoding.UTF8.GetString( Convert.FromBase64String(authoToken));

                string[] loginData =decodedToken.Split(':');

                string username = loginData[0];
                string password = loginData[1];

                Uzytkownik us = new Uzytkownik()
                {
                    Login = username,
                    Haslo = password
                };

                if (UserSecurity.Login(us))
                {
                    IPrincipal principal = new GenericPrincipal(new GenericIdentity(username), null);
                    Thread.CurrentPrincipal = principal;
                    HttpContext.Current.User = principal;
                }
                else
                {
                    actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
                }

            }
        }

    }
}