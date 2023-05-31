using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Hosting;
using System.Web.Http;
//using System.Web.Mvc;


namespace G4SScheduler.Controllers
{
   // [RoutePrefix("SecureApis/SFTPPath")] 
    public class SFTPPathController : ApiController
    {
     //   [Route("GetSFTPath")]
        [HttpGet]
        public HttpResponseMessage GetSFTPath()
        {
            var response = new HttpResponseMessage(HttpStatusCode.OK);
            string path = HostingEnvironment.MapPath("~/Docs/TaskFileDownload");
            response.Content = new StringContent(path);
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("text/plain");
            return response;
        }
    }
}

