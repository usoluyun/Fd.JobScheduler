using HubRoute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebManager
{
    public partial class _Default : System.Web.UI.Page
    {
        public Scheduler sd = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            sd = new Scheduler("10.1.200.30",5555);
        }
    }
}
