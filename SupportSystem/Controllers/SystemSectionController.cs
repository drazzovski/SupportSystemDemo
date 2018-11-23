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
    public class SystemSectionController : Controller
    {
        private SupportSystemPraksaEntities db = new SupportSystemPraksaEntities();

        // GET: SystemSection
        public ActionResult Index()
        {
            return View();
        }


        public JsonResult GetSectionList()
        {
            MainBLL mainBll = new MainBLL();
            var jsonResult = mainBll.GetSectionList();
            return Json(jsonResult, JsonRequestBehavior.AllowGet);
        }


        public JsonResult GetDeletedSectionList()
        {
            MainBLL mainBll = new MainBLL();
            var jsonResult = mainBll.GetSectionDeletedList();
            return Json(jsonResult, JsonRequestBehavior.AllowGet);
        }

        // GET: SystemSection/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SupportSystemSection supportSystemSection = db.SupportSystemSection.Find(id);
            if (supportSystemSection == null)
            {
                return HttpNotFound();
            }
            return View(supportSystemSection);
        }

        // GET: SystemSection/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public JsonResult SaveSection(Models.DAL.SupportSystemSectionMeta model)
        {
            SupportSystemSection newObj = new SupportSystemSection()
            {
                Id = Guid.NewGuid(),
                Description = model.Description,
                Name = model.Name,
                isDeleted = false
            };

            db.SupportSystemSection.Add(newObj);
            db.SaveChanges();

            var jsonResult = "section uspješno kreiran!";
            return Json(jsonResult, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult EditSectionRow(Models.DAL.SupportSystemSectionMeta obj)
        {

            using (db = new SupportSystemPraksaEntities())
            {
                var rowMain = db.SupportSystemSection.FirstOrDefault(x => x.Id == obj.Id);
                //obj.Id = null;


                if (rowMain != null)
                {
                    StaticBLL.CopyNonNullProperties(obj, rowMain);

                    db.Entry(rowMain).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }

            var jsonResult = "";
            return Json(jsonResult, JsonRequestBehavior.AllowGet);
        }

        //// POST: SystemSection/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "Id,Name,Description")] SupportSystemSection supportSystemSection)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        supportSystemSection.Id = Guid.NewGuid();
        //        db.SupportSystemSection.Add(supportSystemSection);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(supportSystemSection);
        //}

        //// GET: SystemSection/Edit/5
        //public ActionResult Edit(Guid? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    SupportSystemSection supportSystemSection = db.SupportSystemSection.Find(id);
        //    if (supportSystemSection == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(supportSystemSection);
        //}

        //// POST: SystemSection/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "Id,Name,Description")] SupportSystemSection supportSystemSection)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(supportSystemSection).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(supportSystemSection);
        //}

        //// GET: SystemSection/Delete/5
        //public ActionResult Delete(Guid? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    SupportSystemSection supportSystemSection = db.SupportSystemSection.Find(id);
        //    if (supportSystemSection == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(supportSystemSection);
        //}

        //// POST: SystemSection/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(Guid id)
        //{
        //    SupportSystemSection supportSystemSection = db.SupportSystemSection.Find(id);
        //    db.SupportSystemSection.Remove(supportSystemSection);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}


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
