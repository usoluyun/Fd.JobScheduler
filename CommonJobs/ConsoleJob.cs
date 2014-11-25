using Quartz;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;

namespace Common.Jobs
{
    /// <summary>
    /// 执行控制台的job
    /// </summary>
    public class ConsoleJob:IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            JobDataMap dataMap = context.JobDetail.JobDataMap;
            string content = dataMap.GetString("jobData");
            var jd = new JavaScriptSerializer().Deserialize<ConsoleJobData>(content);

            System.Diagnostics.Process p = new System.Diagnostics.Process();
            p.StartInfo.UseShellExecute = true;
            p.StartInfo.FileName = jd.Path;
            p.StartInfo.Arguments = jd.Parameters;   //空格分割
            p.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
            p.Start();
        }
    }
}
