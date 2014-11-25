using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace WebManager.Controllers
{
    public class BaseApiController : ApiController
    {
        public string JobName { get { return System.Web.HttpContext.Current.Request["name"]; } }
        public string JobGroup { get { return System.Web.HttpContext.Current.Request["group"]; } }


    }
}