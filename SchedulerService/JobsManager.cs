using Quartz;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulerService
{
    public class JobsManager
    {
        public static NameValueCollection GetProperties()
        {
            var properties = new NameValueCollection();
            properties["quartz.scheduler.instanceName"] = "作业调度系统";

            // set thread pool info
            properties["quartz.threadPool.type"] = "Quartz.Simpl.SimpleThreadPool, Quartz";
            properties["quartz.threadPool.threadCount"] = "5";
            properties["quartz.threadPool.threadPriority"] = "Normal";

            // set remoting expoter
            properties["quartz.scheduler.exporter.type"] = "Quartz.Simpl.RemotingSchedulerExporter, Quartz";
            properties["quartz.scheduler.exporter.port"] = "5555";
            properties["quartz.scheduler.exporter.bindName"] = "QuartzScheduler";
            properties["quartz.scheduler.exporter.channelType"] = "tcp";
            properties["quartz.scheduler.exporter.channelName"] = "httpQuartz";

            //cluster
            //properties["quartz.scheduler.instanceId"] = "AUTO";
            //properties["quartz.jobStore.useProperties"] = "true";
            //properties["quartz.jobStore.clustered"] = "true";
            //properties["quartz.jobStore.misfireThreshold"] = "60000";
            //properties["quartz.jobStore.type"] = "Quartz.Impl.AdoJobStore.JobStoreTX, Quartz";
            //properties["quartz.jobStore.tablePrefix"] = "QRTZ_";
            //properties["quartz.jobStore.driverDelegateType"] = "Quartz.Impl.AdoJobStore.SqlServerDelegate, Quartz";
            //properties["quartz.jobStore.dataSource"] = "myDS";
            //properties["quartz.dataSource.myDS.connectionString"] = @"Data Source=10.1.249.44;Initial Catalog=CheckInTaskScheduler;User ID=sa;Password=hanting@2010";
            //properties["quartz.dataSource.myDS.provider"] = "SqlServer-20";

            ////plugin
            //properties["quartz.plugin.myPlugin.type"] = "TaskSchedulerManager.Plugin.History.LoggingTriggerHistoryPlugin,TaskSchedulerManager";

            return properties;
        }
        public static void Test(IScheduler scheduler)
        {
            var job = JobBuilder.Create<HelloJob>()
               .WithIdentity("testJob", "testJobs")
               .Build();
            var trigger = TriggerBuilder.Create()
                .WithIdentity("testTrigger", "testTriggers")
                .WithCronSchedule("0/2 * * ? * *")
                .Build();
            scheduler.ScheduleJob(job, trigger);
        }
    }
}
