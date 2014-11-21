using HubRoute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebManagerPanel
{
    public partial class Default : System.Web.UI.Page
    {
        public Scheduler SchedulerInfo { get; private set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Scheduler.CurrentInstance != null)
            {
                SchedulerInfo = Scheduler.CurrentInstance;
            }
            else
            {
                SchedulerInfo = Scheduler.CurrentInstance = new Scheduler("10.1.200.30", 5555);
            }
        }
    }
}