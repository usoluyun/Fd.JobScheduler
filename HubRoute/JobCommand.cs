using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Quartz.Impl.Matchers;

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

        public void PauseTrigger(string name,string group)
        {
            Instance.PauseTrigger(new TriggerKey(name, group));
        }
        public void PauseTriggers(string group)
        {
            Instance.PauseTriggers(GroupMatcher<TriggerKey>.GroupEquals(group));
        }
        public void PauseJob(string name, string group)
        {
            Instance.PauseJob(new JobKey(name, group));
        }
        public void PauseJobs(string group)
        {
            Instance.PauseJobs(GroupMatcher<JobKey>.GroupEquals(group));
        }
    }
}
