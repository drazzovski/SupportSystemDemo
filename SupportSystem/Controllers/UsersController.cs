using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using SupportSystem.Models;
using SupportSystem.Models.BLL;
using System.Web.Security;
using Microsoft.AspNet.Identity.EntityFramework;

namespace SupportSystem.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        private SupportSystemPraksaEntities db;

        // GET: Users
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetUsers()
        {
            try
            {
                MainBLL mainBll = new MainBLL();
                var jsonResult = mainBll.GetUsersList();
                return Json(jsonResult, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                throw;
            }          
        }

        public JsonResult GetNonActiveUsers()
        {
            try
            {
                MainBLL mainBll = new MainBLL();
                var jsonResult = mainBll.GetNonActiveUsersList();
                return Json(jsonResult, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public JsonResult RolesList()
        {

            try
            {
                MainBLL mainBll = new MainBLL();
                var jsonResult = mainBll.RolesList();
                return Json(jsonResult, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                throw;
            }          
        }


        [HttpPost]
        public JsonResult PopulateUsersEdit(Guid id)
        {

            try
            {

                object model;

                using (db = new SupportSystemPraksaEntities())
                {


                    var dbmodel = db.AspNetUsers.Find(id);
                    var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
                    //  var rolesname = userManager.GetRoles(id).FirstOrDefault();
                    var rolesId = userManager.FindById(id).Roles.Select(r => r.RoleId).FirstOrDefault();
                    model = new Models.DAL.AspNetUsersMeta()
                    {
                        Id = dbmodel.Id,
                        Address = dbmodel.Address,
                        City = dbmodel.City,
                        Country = dbmodel.Country,
                        Email = dbmodel.Email,
                        UserName = dbmodel.UserName,
                        FirstName = dbmodel.FirstName,
                        LastName = dbmodel.LastName,
                        PhoneNumber = dbmodel.PhoneNumber,
                        IdRole = rolesId,
                        isActive = dbmodel.isActive
                    };

                }

                return Json(model, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                throw;
            }

        }


        public ActionResult Create()
        {
            return View();
        }

        [StaticBLL.CustomAuthorize(StaticBLL.EditMode.Edit)]
        public ActionResult Edit()
        {
            StaticBLL.lastUrl = HttpContext.Request.Path;
            return View();
        }


        [HttpPost]
        public JsonResult EditModeSet(Guid id)
        {
            using (db = new SupportSystemPraksaEntities())
            {
                var ssUser = db.AspNetUsers.Find(id);

                if (ssUser != null)
                {
                    StaticBLL.emode = StaticBLL.EditMode.Edit;
                }
            }

            StaticBLL.lastUrl = "/Users/Edit/";
            return Json("ok", JsonRequestBehavior.AllowGet);
        }


        //[HttpPost]
        //public JsonResult EditRowOnUsers(Models.DAL.AspNetUsersMeta obj)
        //{

        //    using (db = new SupportSystemPraksaEntities())
        //    {
        //        var rowMain = db.AspNetUsers.FirstOrDefault(x => x.Id == obj.Id);
        //        //obj.Id = null;


        //        if (rowMain != null)
        //        {
        //            MainBLL.CopyNonNullProperties(obj, rowMain);

        //            db.Entry(rowMain).State = EntityState.Modified;
        //            db.SaveChanges();
        //        }
        //    }

        //    var jsonResult = "";
        //    return Json(jsonResult, JsonRequestBehavior.AllowGet);
        //}

        //// GET: Users/Details/5
        //public ActionResult Details(Guid? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    AspNetUsers aspNetUsers = db.AspNetUsers.Find(id);
        //    if (aspNetUsers == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(aspNetUsers);
        //}

        // GET: Users/Create


        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "Id,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName,Address,City,Country,isActive")] AspNetUsers aspNetUsers)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        aspNetUsers.Id = Guid.NewGuid();
        //        db.AspNetUsers.Add(aspNetUsers);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(aspNetUsers);
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async JsonResult SaveUser(Models.DAL.AspNetUsersMeta userModel)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var newUser = new ApplicationUser()
        //        {
        //            Id = Guid.NewGuid(),
        //            Address = userModel.Address,
        //            City = userModel.City,
        //            Country = userModel.Country,
        //            FirstName = userModel.FirstName,
        //            LastName = userModel.LastName,
        //            Email = userModel.Email,
        //            isActive = userModel.isActive,
        //            PhoneNumber = userModel.PhoneNumber
        //        };


        //        var result = await UserManager.CreateAsync(userModel, userModel.Password);
        //        if (result.Succeeded)
        //        {
        //            await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

        //            // For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=320771
        //            // Send an email with this link
        //            // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
        //            // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
        //            // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

        //            return RedirectToAction("Index", "Home");
        //        }
        //        AddErrors(result);







        //    }
        //    var jsonResult = "";
        //    return Json(jsonResult, JsonRequestBehavior.AllowGet);
        //}

        //// GET: Users/Edit/5
        //public ActionResult Edit(Guid? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    AspNetUsers aspNetUsers = db.AspNetUsers.Find(id);
        //    if (aspNetUsers == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(aspNetUsers);
        //}

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "Id,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName,Address,City,Country,isActive")] AspNetUsers aspNetUsers)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(aspNetUsers).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(aspNetUsers);
        //}

        //// GET: Users/Delete/5
        //public ActionResult Delete(Guid? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    AspNetUsers aspNetUsers = db.AspNetUsers.Find(id);
        //    if (aspNetUsers == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(aspNetUsers);
        //}

        // POST: Users/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(Guid id)
        //{
        //    AspNetUsers aspNetUsers = db.AspNetUsers.Find(id);
        //    db.AspNetUsers.Remove(aspNetUsers);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        protected override void Dispose(bool disposing)
        {
            using (db = new SupportSystemPraksaEntities())
            {
                if (disposing)
                {
                    db.Dispose();
                }
                base.Dispose(disposing);
            }         
        }
    }
}
