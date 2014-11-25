using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SchedulerService
{
    public class HelloJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            Console.WriteLine("c" + DateTime.Now);
        }
    }
}
