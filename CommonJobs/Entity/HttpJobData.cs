using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Jobs
{

    public class HttpJobData : IJobData
    {
        public string Url { get; set; }
    }
}
