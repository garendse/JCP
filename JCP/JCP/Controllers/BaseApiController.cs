using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace JCP.Controllers
{
    public class BaseApiController: ApiController
    {
        // Workaround because .NET for some reason does not know about CORS complicance. This makes the debugging process better as we can now use hot reloading for the frontend.
        public HttpResponseMessage Options()
        {
            return new HttpResponseMessage { StatusCode = HttpStatusCode.OK };
        }
    }
}