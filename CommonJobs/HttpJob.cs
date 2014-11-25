using Common.Jobs.Utils;
using Quartz;
using System.Web.Script.Serialization;

namespace Common.Jobs
{
    /// <summary>
    /// 执行Http请求的Job
    /// </summary>
    public class HttpJobs : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            JobDataMap dataMap = context.JobDetail.JobDataMap;

            string content = dataMap.GetString("jobData");

            var jd = new JavaScriptSerializer().Deserialize<HttpJobData>(content);

            var result = RequestHelper.Post(jd.Url);

        }
    }
}
