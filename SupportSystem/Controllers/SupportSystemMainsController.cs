using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SupportSystem.Models;

namespace SupportSystem.Controllers
{
    public class SupportSystemMainsController : Controller
    {
    //    private SupportSystemPraksaEntities db = new SupportSystemPraksaEntities();

    //    // GET: SupportSystemMains
    //    public ActionResult Index()
    //    {
    //        var supportSystemMain = db.SupportSystemMain.Include(s => s.AspNetUsers).Include(s => s.SupportSystemCategory).Include(s => s.SupportSystemPriority).Include(s => s.SupportSystemSection).Include(s => s.SupportSystemSeverity).Include(s => s.SupportSystemStatuses);
    //        return View(supportSystemMain.ToList());
    //    }

    //    // GET: SupportSystemMains/Details/5
    //    public ActionResult Details(Guid? id)
    //    {
    //        if (id == null)
    //        {
    //            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
    //        }
    //        SupportSystemMain supportSystemMain = db.SupportSystemMain.Find(id);
    //        if (supportSystemMain == null)
    //        {
    //            return HttpNotFound();
    //        }
    //        return View(supportSystemMain);
    //    }

    //    // GET: SupportSystemMains/Create
    //    public ActionResult Create()
    //    {
    //        ViewBag.IdUser = new SelectList(db.AspNetUsers, "Id", "Email");
    //        ViewBag.IdKategorija = new SelectList(db.SupportSystemCategory, "Id", "CategoryName");
    //        ViewBag.IdComment = new SelectList(db.SupportSystemComments, "Id", "Comment");
    //        ViewBag.IdPriority = new SelectList(db.SupportSystemPriority, "Id", "PriorityName");
    //        ViewBag.IdSeverity = new SelectList(db.SupportSystemSection, "Id", "Name");
    //        ViewBag.IdSeverity = new SelectList(db.SupportSystemSeverity, "Id", "SeverityName");
    //        ViewBag.IdStatus = new SelectList(db.SupportSystemStatuses, "Id", "StatusName");
    //        return View();
    //    }

    //    // POST: SupportSystemMains/Create
    //    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    //    // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
    //    [HttpPost]
    //    [ValidateAntiForgeryToken]
    //    public ActionResult Create([Bind(Include = "Id,Number,Title,IdStatus,IdKategorija,IdSeverity,IdComment,IdUser,CreatedOn,AccceptedOn,DueOn,ResolvedOn,IdPriority,Steps,Notes,IdSystemSection")] SupportSystemMain supportSystemMain)
    //    {
    //        if (ModelState.IsValid)
    //        {
    //            supportSystemMain.Id = Guid.NewGuid();
    //            db.SupportSystemMain.Add(supportSystemMain);
    //            db.SaveChanges();
    //            return RedirectToAction("Index");
    //        }

    //        ViewBag.IdUser = new SelectList(db.AspNetUsers, "Id", "Email", supportSystemMain.IdUser);
    //        ViewBag.IdKategorija = new SelectList(db.SupportSystemCategory, "Id", "CategoryName", supportSystemMain.IdKategorija);
    //        ViewBag.IdPriority = new SelectList(db.SupportSystemPriority, "Id", "PriorityName", supportSystemMain.IdPriority);
    //        ViewBag.IdSeverity = new SelectList(db.SupportSystemSection, "Id", "Name", supportSystemMain.IdSeverity);
    //        ViewBag.IdSeverity = new SelectList(db.SupportSystemSeverity, "Id", "SeverityName", supportSystemMain.IdSeverity);
    //        ViewBag.IdStatus = new SelectList(db.SupportSystemStatuses, "Id", "StatusName", supportSystemMain.IdStatus);
    //        return View(supportSystemMain);
    //    }

    //    // GET: SupportSystemMains/Edit/5
    //    public ActionResult Edit(Guid? id)
    //    {
           
    //        SupportSystemMain supportSystemMain = db.SupportSystemMain.Find(id);
    //        if (supportSystemMain == null)
    //        {
    //            return HttpNotFound();
    //        }
    //        ViewBag.IdUser = new SelectList(db.AspNetUsers, "Id", "Email", supportSystemMain.IdUser);
    //        ViewBag.IdKategorija = new SelectList(db.SupportSystemCategory, "Id", "CategoryName", supportSystemMain.IdKategorija);
    //        ViewBag.IdPriority = new SelectList(db.SupportSystemPriority, "Id", "PriorityName", supportSystemMain.IdPriority);
    //        ViewBag.IdSeverity = new SelectList(db.SupportSystemSection, "Id", "Name", supportSystemMain.IdSeverity);
    //        ViewBag.IdSeverity = new SelectList(db.SupportSystemSeverity, "Id", "SeverityName", supportSystemMain.IdSeverity);
    //        ViewBag.IdStatus = new SelectList(db.SupportSystemStatuses, "Id", "StatusName", supportSystemMain.IdStatus);
    //        return View(supportSystemMain);
    //    }

    //    // POST: SupportSystemMains/Edit/5
    //    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    //    // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
    //    [HttpPost]
    //    [ValidateAntiForgeryToken]
    //    public ActionResult Edit([Bind(Include = "Id,Number,Title,IdStatus,IdKategorija,IdSeverity,IdComment,IdUser,CreatedOn,AccceptedOn,DueOn,ResolvedOn,IdPriority,Steps,Notes,IdSystemSection")] SupportSystemMain supportSystemMain)
    //    {
    //        if (ModelState.IsValid)
    //        {
    //            db.Entry(supportSystemMain).State = EntityState.Modified;
    //            db.SaveChanges();
    //            return RedirectToAction("Index");
    //        }
    //        ViewBag.IdUser = new SelectList(db.AspNetUsers, "Id", "Email", supportSystemMain.IdUser);
    //        ViewBag.IdKategorija = new SelectList(db.SupportSystemCategory, "Id", "CategoryName", supportSystemMain.IdKategorija);
    //        ViewBag.IdPriority = new SelectList(db.SupportSystemPriority, "Id", "PriorityName", supportSystemMain.IdPriority);
    //        ViewBag.IdSeverity = new SelectList(db.SupportSystemSection, "Id", "Name", supportSystemMain.IdSeverity);
    //        ViewBag.IdSeverity = new SelectList(db.SupportSystemSeverity, "Id", "SeverityName", supportSystemMain.IdSeverity);
    //        ViewBag.IdStatus = new SelectList(db.SupportSystemStatuses, "Id", "StatusName", supportSystemMain.IdStatus);
    //        return View(supportSystemMain);
    //    }

    //    // GET: SupportSystemMains/Delete/5
    //    public ActionResult Delete(Guid? id)
    //    {
    //        if (id == null)
    //        {
    //            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
    //        }
    //        var vt = db.SupportSystemMain.Find(id);
    //        if (vt == null)
    //        {
    //            return HttpNotFound();
    //        }
    //        return View(vt);
    //    }

    //    // POST: SupportSystemMains/Delete/5
    //    [HttpPost, ActionName("Delete")]
    //   // [ValidateAntiForgeryToken]
    //    public ActionResult DeleteConfirmed(Guid id)
    //    {
    //        var vt = db.SupportSystemMain.Find(id);
    //        db.SupportSystemMain.Remove(vt);
    //        db.SaveChanges();
    //        return RedirectToAction("Index");
    //    }

    //    protected override void Dispose(bool disposing)
    //    {
    //        if (disposing)
    //        {
    //            db.Dispose();
    //        }
    //        base.Dispose(disposing);
    //    }
    }
}
