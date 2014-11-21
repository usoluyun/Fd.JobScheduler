using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HubRoute;
namespace WebManager.Controllers
{
    public class JobCommandController : ApiController
    {
        // GET api/jobcommand
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/jobcommand/5
        public string PauseTrigger(string name, string group)
        {
            JobCommand.Current.PauseTrigger(name, group);
            return "success";
        }

        // POST api/jobcommand
        public void Post([FromBody]string value)
        {
        }

        // PUT api/jobcommand/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/jobcommand/5
        public void Delete(int id)
        {
        }
    }
}
