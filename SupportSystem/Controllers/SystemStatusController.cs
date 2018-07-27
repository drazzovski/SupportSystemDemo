using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SupportSystem.Models;
using SupportSystem.Models.BLL;

namespace SupportSystem.Controllers
{

    [Authorize(Roles = "Admin")]
    public class SystemStatusController : Controller
    {
        private SupportSystemPraksaEntities db = new SupportSystemPraksaEntities();

        // GET: SystemStatus
        public ActionResult Index()
        {
            return View();
        }


        public JsonResult GetStatusList()
        {
            MainBLL mainBll = new MainBLL();
            var jsonResult = mainBll.GetStatusList();
            return Json(jsonResult, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetDeletedStatusList()
        {
            MainBLL mainBll = new MainBLL();
            var jsonResult = mainBll.GetDeletedStatusList();
            return Json(jsonResult, JsonRequestBehavior.AllowGet);
        }
        // GET: SystemStatus/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SupportSystemStatuses supportSystemStatuses = db.SupportSystemStatuses.Find(id);
            if (supportSystemStatuses == null)
            {
                return HttpNotFound();
            }
            return View(supportSystemStatuses);
        }

        // GET: SystemStatus/Create
        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public JsonResult SaveStatus(Models.DAL.SupportSystemStatusesMeta model)
        {
            SupportSystemStatuses newObj = new SupportSystemStatuses()
            {
                Id = Guid.NewGuid(),
                Description = model.Description,
                StatusName = model.StatusName,
                isDeleted = false
            };

            db.SupportSystemStatuses.Add(newObj);
            db.SaveChanges();

            var jsonResult = "section uspješno kreiran!";
            return Json(jsonResult, JsonRequestBehavior.AllowGet);
        }

        // POST: SystemStatus/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,StatusName")] SupportSystemStatuses supportSystemStatuses)
        {
            if (ModelState.IsValid)
            {
                supportSystemStatuses.Id = Guid.NewGuid();
                db.SupportSystemStatuses.Add(supportSystemStatuses);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(supportSystemStatuses);
        }

        // GET: SystemStatus/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SupportSystemStatuses supportSystemStatuses = db.SupportSystemStatuses.Find(id);
            if (supportSystemStatuses == null)
            {
                return HttpNotFound();
            }
            return View(supportSystemStatuses);
        }

        // POST: SystemStatus/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,StatusName")] SupportSystemStatuses supportSystemStatuses)
        {
            if (ModelState.IsValid)
            {
                db.Entry(supportSystemStatuses).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(supportSystemStatuses);
        }

        // GET: SystemStatus/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SupportSystemStatuses supportSystemStatuses = db.SupportSystemStatuses.Find(id);
            if (supportSystemStatuses == null)
            {
                return HttpNotFound();
            }
            return View(supportSystemStatuses);
        }

        // POST: SystemStatus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            SupportSystemStatuses supportSystemStatuses = db.SupportSystemStatuses.Find(id);
            db.SupportSystemStatuses.Remove(supportSystemStatuses);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
