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

namespace TestCreatorV2.Helpers
{
    public class RegistryAutenticationAttribute : AuthorizationFilterAttribute
    {


        public override void OnAuthorization(HttpActionContext actionContext)
        {
            if (actionContext.Request.Headers.Authorization == null)
            {
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
            }
            else
            {
                string authoToken = actionContext.Request.Headers.Authorization.Parameter;
                string decodedToken = Encoding.UTF8.GetString(Convert.FromBase64String(authoToken));




                IPrincipal principal = new GenericPrincipal(new GenericIdentity(decodedToken), null);
                Thread.CurrentPrincipal = principal;
                HttpContext.Current.User = principal;

            }
        }
    }
}