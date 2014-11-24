using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Quartz.Impl.Matchers;
using Common.Jobs;
using System.Web.Script.Serialization;


namespace HubRoute
{
    /// <summary>
    /// 作业操作命令
    /// </summary>
    public class JobCommand
    {
        private static Lazy<JobCommand> current { get; set; }

        static JobCommand()
        {
            current = new Lazy<JobCommand>(System.Threading.LazyThreadSafetyMode.ExecutionAndPublication);
        }
        public static JobCommand Current { get { return current.Value; } }



        private IScheduler Instance = Scheduler.CurrentInstance.Instance;

        public void PauseTrigger(string name, string group)
        {
            Instance.PauseTrigger(new TriggerKey(name, group));
        }
        public void ResumeTrigger(string name, string group)
        {
            Instance.ResumeTrigger(new TriggerKey(name, group));
        }


        public void PauseTriggers(string group)
        {
            Instance.PauseTriggers(GroupMatcher<TriggerKey>.GroupEquals(group));
        }
        public void ResumeTriggers(string group)
        {
            Instance.ResumeTriggers(GroupMatcher<TriggerKey>.GroupEquals(group));
        }


        public void PauseJob(string name, string group)
        {
            Instance.PauseJob(new JobKey(name, group));
        }
        public void ResumeJob(string name, string group)
        {
            Instance.ResumeJob(new JobKey(name, group));
        }

        public void ResumeJobs(string group)
        {
            Instance.ResumeJobs(GroupMatcher<JobKey>.GroupEquals(group));
        }
        public void PauseJobs(string group)
        {
            Instance.PauseJobs(GroupMatcher<JobKey>.GroupEquals(group));
        }


        public bool UnscheduleJob(string jobName, string jobGroup)
        {
            var jobKey = new JobKey(jobName, jobGroup);

            if (Instance.CheckExists(jobKey))
            {
                return Instance.UnscheduleJob(new TriggerKey(jobName, jobGroup));
            }
            return false;
        }

        public void RescheduleJob(string jobName, string jobGroup, string cronExpression)
        {
            var trigger = (ICronTrigger)TriggerBuilder.Create()
                .WithIdentity(jobName, jobGroup)
                .WithCronSchedule(cronExpression)
                .Build();
            Instance.RescheduleJob(new TriggerKey(jobName, jobGroup), trigger);
        }

        public void AddJob(string jobName, string jobGroup, string cronExpression, JobContent jobData)
        {
            var job = JobBuilder.Create<HttpJobs>()
            .WithIdentity(jobName, jobGroup)
            .Build();

            var trigger = (ICronTrigger)TriggerBuilder.Create()
                .WithIdentity(jobName, jobGroup)
                .UsingJobData("jobData", new JavaScriptSerializer().Serialize(jobData))
                .WithCronSchedule(cronExpression)
                .Build();
            Instance.ScheduleJob(job, trigger);
        }

    }
}
