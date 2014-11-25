using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Topshelf;

namespace SchedulerService
{
    class Program
    {
        static void Main(string[] args)
        {
            HostFactory.Run(x =>
            {
                x.Service<Jobs>(s =>
                {
                    s.ConstructUsing(name => new Jobs());
                    s.WhenStarted(tc => tc.OnStart());
                    s.WhenStopped(tc => tc.OnStop());
                });
                x.RunAsLocalSystem();
                x.SetDescription("作业调度系统");
                x.SetDisplayName("HuzhuJobScheduler");
                x.SetServiceName("HuzhuJobScheduler");
            });
        }
    }
}
