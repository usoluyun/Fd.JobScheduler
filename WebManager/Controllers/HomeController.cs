using HubRoute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebManager.Models;
using System.Configuration;
namespace WebManager.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            Scheduler SchedulerInfo = null;
            if (Scheduler.CurrentInstance != null)
            {
                SchedulerInfo = Scheduler.CurrentInstance;
            }
            else
            {
               var host= ConfigurationManager.AppSettings["host"];
               var port = ConfigurationManager.AppSettings["port"];
               SchedulerInfo = Scheduler.CurrentInstance = new Scheduler(host, Convert.ToInt32(port));
            }
            SchedulerModel sm = new SchedulerModel();
            sm.SchedulerInfo = SchedulerInfo;
            return View(sm);
        }
        
         //<header class="cq-job-header clearfix">
         //       <a class="cq-job-header-title loadDetails name" href="#"></a>

         //       <button class="btn btn-primary" onclick="jbox.addHttpJob()">添加http作业</button>
         //   </header>
        //
        // GET: /Home/Details/5

        public ActionResult JobDetail()
        {

            return View();
        }
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Home/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Home/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Home/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Home/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Home/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Home/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
