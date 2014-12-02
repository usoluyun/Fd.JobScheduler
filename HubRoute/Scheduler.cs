using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using Quartz;
using Quartz.Impl;
using Quartz.Impl.Matchers;
using HubRoute.Utils;
using HubRoute.Domain;
namespace HubRoute
{
    public class Scheduler
    {
        public static Scheduler CurrentInstance;

        public  readonly IScheduler Instance;
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

                //if (!Instance.IsStarted)
                //    Instance.Start();
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
                JobGroups = GetJobGroups(Instance),
                TriggerGroups = GetTriggerGroups(Instance),
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
      

         private static IList<TriggerGroupData> GetTriggerGroups(IScheduler scheduler)
         {
             var result = new List<TriggerGroupData>();
             if (!scheduler.IsShutdown)
             {
                 foreach (var groupName in scheduler.GetTriggerGroupNames())
                 {
                     var data = new TriggerGroupData(groupName);
                     data.Init();
                     result.Add(data);
                 }
             }

             return result;
         }

         private static IList<JobGroupData> GetJobGroups(IScheduler scheduler)
         {
             var result = new List<JobGroupData>();

             if (!scheduler.IsShutdown)
             {
                 foreach (var groupName in scheduler.GetJobGroupNames())
                 {
                     var groupData = new JobGroupData(
                         groupName,
                         GetJobs(scheduler, groupName));
                     groupData.Init();
                     result.Add(groupData);
                 }
             }

             return result;
         }

         private static IList<JobData> GetJobs(IScheduler scheduler, string groupName)
         {
             var result = new List<JobData>();

             foreach (var jobKey in scheduler.GetJobKeys(GroupMatcher<JobKey>.GroupEquals(groupName)))
             {
                 result.Add(GetJobData(scheduler, jobKey.Name, groupName));
             }

             return result;
         }

         private static JobData GetJobData(IScheduler scheduler, string jobName, string group)
         {
             var jobData = new JobData(jobName, group, GetTriggers(scheduler, jobName, group));
             jobData.Init();
             return jobData;
         }

         private static IList<TriggerData> GetTriggers(IScheduler scheduler, string jobName, string group)
         {
             return scheduler
                 .GetTriggersOfJob(new JobKey(jobName, @group))
                 .Select(trigger => GetTriggerData(scheduler, trigger))
                 .ToList();
         }


         private readonly static TriggerTypeExtractor TriggerTypeExtractor = new TriggerTypeExtractor();
         //获取详细时间
         private static TriggerData GetTriggerData(IScheduler scheduler, ITrigger trigger)
         {
             return new TriggerData(trigger.Key.Name, GetTriggerStatus(trigger, scheduler))
             {
                 GroupName = trigger.Key.Group,
                 StartDate = trigger.StartTimeUtc.DateTime.ToLocalTime(),
                 EndDate = trigger.StartTimeUtc.DateTime.ToLocalTime(),
                 NextFireDate = trigger.GetNextFireTimeUtc().ToDateTime(),
                 PreviousFireDate = trigger.GetPreviousFireTimeUtc().ToDateTime(),
                 TriggerType = TriggerTypeExtractor.GetFor(trigger)
             };
         }
         private static ActivityStatus GetTriggerStatus(ITrigger trigger, IScheduler scheduler)
         {
             return GetTriggerStatus(trigger.Key.Name, trigger.Key.Group, scheduler);
         }
         private static ActivityStatus GetTriggerStatus(string triggerName, string triggerGroup, IScheduler scheduler)
         {
             var state = scheduler.GetTriggerState(new TriggerKey(triggerName, triggerGroup));
             switch (state)
             {
                 case TriggerState.Paused:
                     return ActivityStatus.Paused;
                 case TriggerState.Complete:
                     return ActivityStatus.Complete;
                 default:
                     return ActivityStatus.Active;
             }
         }

         public static string ToGMTFormat(DateTime dt)
         {
             return dt.ToString("r") + dt.ToString("zzz").Replace(":", "");
         }

         /// <summary>   
         /// GMT时间转成本地时间   
         /// </summary>   
         /// <param name="gmt">字符串形式的GMT时间</param>   
         /// <returns></returns>   
         public static DateTime GMT2Local(string gmt)
         {
             DateTime dt = DateTime.MinValue;
             try
             {
                 string pattern = "";
                 if (gmt.IndexOf("+0") != -1)
                 {
                     gmt = gmt.Replace("GMT", "");
                     pattern = "ddd, dd MMM yyyy HH':'mm':'ss zzz";
                 }
                 if (gmt.ToUpper().IndexOf("GMT") != -1)
                 {
                     pattern = "ddd, dd MMM yyyy HH':'mm':'ss 'GMT'";
                 }
                 if (pattern != "")
                 {
                     dt = DateTime.ParseExact(gmt, pattern, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.AdjustToUniversal);
                     dt = dt.ToLocalTime();
                 }
                 else
                 {
                     dt = Convert.ToDateTime(gmt);
                 }
             }
             catch
             {
             }
             return dt;
         } 
    }
}