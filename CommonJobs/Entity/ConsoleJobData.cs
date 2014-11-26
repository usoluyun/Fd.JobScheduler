using Quartz;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Common.Jobs
{
    public class ConsoleJobData : IJobData
    {
        public string Path { get; set; }

       
    }
}
