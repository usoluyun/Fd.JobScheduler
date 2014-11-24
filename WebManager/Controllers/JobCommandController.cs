using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HubRoute;
using System.Web;
using HubRoute.Domain;
using Common.Jobs;

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
            string name = HttpContext.Current.Request["name"];
            string group = HttpContext.Current.Request["group"];
            JobCommand.Current.PauseTrigger(name, group);
            return "success";
        }


        public string RescheduleJob()
        {
            string name = HttpContext.Current.Request["name"];
            string group = HttpContext.Current.Request["group"];
            string cron = HttpContext.Current.Request["cron"];

            JobCommand.Current.RescheduleJob(name, group, cron);
            return "success";
        }


        public string UnscheduleJob()
        {
            string name = HttpContext.Current.Request["name"];
            string group = HttpContext.Current.Request["group"];
            string cron = HttpContext.Current.Request["cron"];
            JobContent jd = new JobContent();
            jd.Url = HttpContext.Current.Request["url"];

            JobCommand.Current.AddJob(name, group, cron, jd);
            return "success";
        }
    }
}
