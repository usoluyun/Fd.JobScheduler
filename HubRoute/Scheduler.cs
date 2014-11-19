using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using Quartz;
using Quartz.Impl;
using Quartz.Impl.Matchers;
using HubRoute.Utils;
namespace HubRoute
{
    public class Scheduler
    {
        public readonly IScheduler Instance;
        public string Address { get; private set; }

        private readonly ISchedulerFactory _schedulerFactory;


        public SchedulerData Data
        { 
            get { return GetSchedulerData(); }
        }

   
        public Scheduler(string server, int port, string scheduler = "QuartzScheduler")
        {
            Address = string.Format("tcp://{0}:{1}/{2}", server, port, scheduler);
            _schedulerFactory = new StdSchedulerFactory(GetProperties(Address));

            try
            {
                Instance = _schedulerFactory.GetScheduler();

                if (!Instance.IsStarted)
                    Instance.Start();
            }
            catch (SchedulerException ex)
            {
                throw new Exception(string.Format("Failed: {0}", ex.Message));
            }
        }

        private static NameValueCollection GetProperties(string address)
        {
            var properties = new NameValueCollection();
            properties["quartz.scheduler.proxy"] = "true";
            properties["quartz.scheduler.proxy.address"] = address;
            return properties;
        }

        public IScheduler GetScheduler()
        {
            return Instance;
        }

        public SchedulerData GetSchedulerData()
        {
            var metaData = Instance.GetMetaData();

            return new SchedulerData()
            {
                SchedulerName = Instance.SchedulerName,
                InstanceId = Instance.SchedulerInstanceId,
                Status = GetSchedulerStatus(),
                IsRemote = metaData.SchedulerRemote,
                JobsExecuted = metaData.NumberOfJobsExecuted,
                SchedulerType = metaData.SchedulerType,
                Version = metaData.Version,
                JobStoreTypeName = metaData.JobStoreType.Name,
                IsPersistence = metaData.JobStoreSupportsPersistence,
                IsClustered = metaData.JobStoreClustered,
                RunningSince = metaData.RunningSince == null ? metaData.RunningSince.Value.DateTime : DateTime.Now
            };
        }
         public SchedulerStatus GetSchedulerStatus()
        {
             
            if (Instance.IsShutdown)
            {
                return SchedulerStatus.Shutdown;
            }

            var jobGroupNames = Instance.GetJobGroupNames();
            if (jobGroupNames == null || jobGroupNames.Count == 0)
            {
                return SchedulerStatus.Empty;
            }
            if (Instance.IsStarted)
            {
                return SchedulerStatus.Started;
            }
            return SchedulerStatus.Ready;
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


         //private static IList<TriggerGroupData> GetTriggerGroups(IScheduler scheduler)
         //{
         //    var result = new List<TriggerGroupData>();
         //    if (!scheduler.IsShutdown)
         //    {
         //        foreach (var groupName in scheduler.GetTriggerGroupNames())
         //        {
         //            var data = new TriggerGroupData(groupName);
         //            data.Init();
         //            result.Add(data);
         //        }
         //    }

         //    return result;
         //}

         //private static IList<JobGroupData> GetJobGroups(IScheduler scheduler)
         //{
         //    var result = new List<JobGroupData>();

         //    if (!scheduler.IsShutdown)
         //    {
         //        foreach (var groupName in scheduler.GetJobGroupNames())
         //        {
         //            var groupData = new JobGroupData(
         //                groupName,
         //                GetJobs(scheduler, groupName));
         //            groupData.Init();
         //            result.Add(groupData);
         //        }
         //    }

         //    return result;
         //}

         //private static IList<JobData> GetJobs(IScheduler scheduler, string groupName)
         //{
         //    var result = new List<JobData>();

         //    foreach (var jobKey in scheduler.GetJobKeys(GroupMatcher<JobKey>.GroupEquals(groupName)))
         //    {
         //        result.Add(GetJobData(scheduler, jobKey.Name, groupName));
         //    }

         //    return result;
         //}

         //private static JobData GetJobData(IScheduler scheduler, string jobName, string group)
         //{
         //    var jobData = new JobData(jobName, group, GetTriggers(scheduler, jobName, group));
         //    jobData.Init();
         //    return jobData;
         //}

         //private static IList<TriggerData> GetTriggers(IScheduler scheduler, string jobName, string group)
         //{
         //    return scheduler
         //        .GetTriggersOfJob(new JobKey(jobName, @group))
         //        .Select(trigger => GetTriggerData(scheduler, trigger))
         //        .ToList();
         //}

         ////获取详细时间
         //private static TriggerData GetTriggerData(IScheduler scheduler, ITrigger trigger)
         //{
         //    return new TriggerData(trigger.Key.Name, GetTriggerStatus(trigger, scheduler))
         //    {
         //        GroupName = trigger.Key.Group,
         //        StartDate = trigger.StartTimeUtc.DateTime,
         //        EndDate = trigger.EndTimeUtc.ToDateTime(),
         //        NextFireDate = trigger.GetNextFireTimeUtc().ToDateTime(),
         //        PreviousFireDate = trigger.GetPreviousFireTimeUtc().ToDateTime(),
         //    };
         //}
    }
}