using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HubRoute;
using System.Web;

namespace WebManager.Controllers
{
    public class JobCommandController : ApiController
    {
        // GET api/jobcommand
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/jobcommand/5

        public string PauseTrigger()
        {
            string name =HttpContext.Current.Request["name"];
            string group = HttpContext.Current.Request["group"];
            JobCommand.Current.PauseTrigger(name, group);
            return "success";
        }

    }
}
