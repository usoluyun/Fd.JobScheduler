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
    public class JobCommandController : BaseApiController
    {
        public string PauseTrigger()
        {
            JobCommand.Current.PauseTrigger(JobName, JobGroup);
            return "success";
        }

        public string ResumeTrigger()
        {
            JobCommand.Current.ResumeTrigger(JobName, JobGroup);
            return "success";
        }
       
        public string RescheduleJob()
        {
            string cron = System.Web.HttpContext.Current.Request["cron"];

            JobCommand.Current.RescheduleJob(JobName, JobGroup, cron);
            return "success";
        }

        public string TriggerJob()
        {
            JobCommand.Current.TriggerJob(JobName, JobGroup);
            return "success";
        }
        public string AddHttpJob()
        {
            string cron = System.Web.HttpContext.Current.Request["cron"];
            HttpJobData jd = new HttpJobData();
            jd.Url = System.Web.HttpContext.Current.Request["url"];

            JobCommand.Current.AddHttpJob(JobName, JobGroup, cron, jd);
            return "success";
        }
        public string AddConsoleJob()
        {
            string cron = System.Web.HttpContext.Current.Request["cron"];

            ConsoleJobData jd = new ConsoleJobData();
            jd.Path = System.Web.HttpContext.Current.Request["path"];
            jd.Parameters = System.Web.HttpContext.Current.Request["parameters"];

            JobCommand.Current.AddConsoleJob(JobName, JobGroup, cron, jd);
            return "success";
        }
    }
}
