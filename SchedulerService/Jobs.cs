using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulerService
{
    public class Jobs
    {
        IScheduler scheduler = null;
        public void OnStart()
        {

            var properties = JobsManager.GetProperties();
            var schedulerFactory = new StdSchedulerFactory(properties);

            scheduler = schedulerFactory.GetScheduler();

            JobsManager.Test(scheduler);

            scheduler.Start();

        }
        public void OnStop()
        {
            scheduler.Shutdown(true);
        }
    }
}
