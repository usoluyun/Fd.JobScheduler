using System;
using System.Collections.Generic;


namespace HubRoute
{
   
    public class SchedulerData
    {
        public string SchedulerName { get; set; }

        public string InstanceId { get; set; }

        public bool IsStarted
        {
            get
            {
                return Status == SchedulerStatus.Started;
            }
        }

        public bool CanStart
        {
            get
            {
                return Status == SchedulerStatus.Ready;
            }
        }

        public bool CanShutdown
        {
            get
            {
                return Status != SchedulerStatus.Shutdown;
            }
        }

        public SchedulerStatus Status { get; set; }

        public int JobsTotal { get; set; }

        public int JobsExecuted { get; set; }

        public bool IsRemote { get; set; }

        public Type SchedulerType { get; set; }

        public DateTime RunningSince { get; set; }

        public string Version { get; set; }

        public int ThreadPoolSize { get; set; }

        public string JobStoreTypeName { get; set; }
        public bool  IsPersistence { get; set; }
        public bool IsClustered { get; set; }

    }
    public enum SchedulerStatus
    {
        Empty,
        Ready,
        Started,
        Shutdown
    }
}