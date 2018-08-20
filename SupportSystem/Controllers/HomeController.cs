using SupportSystem.Models;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Data.Entity;
using SupportSystem.Models.BLL;
using Microsoft.AspNet.Identity;

namespace SupportSystem.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private SupportSystemPraksaEntities db;

        public ActionResult Index()
        {           

            return View();
        }

        public ActionResult DisabledEdit()
        {
            return View();
        }

        public JsonResult GetList()
        {
            try
            {
                MainBLL mainBll = new MainBLL();
                var jsonResult = mainBll.GetMainList();

                return Json(jsonResult, JsonRequestBehavior.AllowGet);                
            }
            catch (Exception)
            {

                throw;
            }
        }

        public JsonResult ShowClosed()
        {
            try
            {
                MainBLL mainBll = new MainBLL();
                var jsonResult = mainBll.ShowClosed();
                return Json(jsonResult, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                throw;
            }          
        }

        public JsonResult SuggestionNumber()
        {
            try
            {
                MainBLL mainBll = new MainBLL();
                var jsonResult = mainBll.SuggestionNumber();
                return Json(jsonResult, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                throw;
            }

        }
               

        public JsonResult UserName()
        {
            try
            {
                var userName = HttpContext.User.Identity.Name;
                StaticBLL.UserId = Guid.Parse(HttpContext.User.Identity.GetUserId());
                //var jsonResult = MainBLL.CurrentUser();
                return Json(userName, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                throw;
            }
        
        }

        public JsonResult StatusList()
        {
            try
            {
                MainBLL mainBll = new MainBLL();
                var jsonResult = mainBll.StatusList();
                return Json(jsonResult, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                throw;
            }       
        }

        public JsonResult CategoriesList()
        {
            try
            {
                MainBLL mainBll = new MainBLL();
                var jsonResult = mainBll.CategoriesList();
                return Json(jsonResult, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                throw;
            }        
        }

        public JsonResult PrioritiesList()
        {
            try
            {
                MainBLL mainBll = new MainBLL();
                var jsonResult = mainBll.PrioritiesList();
                return Json(jsonResult, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                throw;
            }
           
        }

        public JsonResult SeveritiesList()
        {
            try
            {
                MainBLL mainBll = new MainBLL();
                var jsonResult = mainBll.SeveritiesList();
                return Json(jsonResult, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                throw;
            }
          
        }

        public JsonResult SectionList()
        {
            try
            {
                MainBLL mainBll = new MainBLL();
                var jsonResult = mainBll.SectionsList();
                return Json(jsonResult, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                throw;
            }
           
        }


        public JsonResult CommentsByRowId(Guid id)
        {
            try
            {
                MainBLL mainBll = new MainBLL();
                var jsonResult = mainBll.PokupiKomentare(id);
                return Json(jsonResult, JsonRequestBehavior.AllowGet);
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
                var ssMain = db.SupportSystemMain.Find(id);

                if (ssMain != null)
                {
                    StaticBLL.emode = StaticBLL.EditMode.Edit;
                } 
            }

            StaticBLL.lastUrl = "/Home/Edit/";
            return Json("ok", JsonRequestBehavior.AllowGet);
        }
        

        //SAVE COMMENT IN TEMP LIST
        [HttpPost]
        public JsonResult SaveComment(Models.DAL.SupportSystemCommentsMeta model)
        {
            SupportSystemComments newComment = new SupportSystemComments()
            {
                Id = model.Id,
                Comment = model.Comment,                
                IdUser = StaticBLL.UserId,
                IdReply = model.IdReply, 
                OnDate = DateTime.ParseExact(model.OnDate, "yyyy-MM-dd HH:mm:ss.FFF", null)
            };
            StaticBLL.komentari.Add(newComment);
            var jsonResult = StaticBLL.komentari.Count();
            return Json(jsonResult, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult PopulateHomeEdit(Guid id)
        {

            StaticBLL.UserId = Guid.Parse(HttpContext.User.Identity.GetUserId());

            object model;

            using (db = new SupportSystemPraksaEntities())
            {
                var dbmodel = db.SupportSystemMain.Find(id);

                model = new Models.DAL.SupportSystemMainMeta()
                {
                    Id = dbmodel.Id,
                    AcceptedOn = dbmodel.AcceptedOn,                   
                    strCreatedOn = String.Format("{0:yyyy-MM-dd}", dbmodel.CreatedOn),
                    DueOn = dbmodel.DueOn,
                    IdKategorija = dbmodel.IdKategorija,
                    IdPriority = dbmodel.IdPriority,
                    IdSeverity = dbmodel.IdSeverity,
                    IdStatus = dbmodel.IdStatus,
                    IdSystemSection = dbmodel.IdSystemSection,
                    IdUser = dbmodel.IdStatus,
                    Kategorija = dbmodel.SupportSystemCategory?.CategoryName,
                    Notes = dbmodel.Notes,
                    Number = dbmodel.Number,
                    Priority = dbmodel.SupportSystemPriority?.PriorityName,
                    ResolvedOn = dbmodel.ResolvedOn,
                    Severity = dbmodel.SupportSystemSeverity?.SeverityName,
                    Status = dbmodel.SupportSystemStatuses?.StatusName,
                    Steps = dbmodel.Steps,
                    Title = dbmodel.Title,
                    SystemSection = dbmodel.SupportSystemSection?.Name,
                    User = dbmodel.AspNetUsers.UserName,
                    strAcceptedOn = String.Format("{0:yyyy-MM-dd}", dbmodel.AcceptedOn),
                    strDueOn = String.Format("{0:yyyy-MM-dd}", dbmodel.DueOn),
                    strResolvedOn = String.Format("{0:yyyy-MM-dd}", dbmodel.ResolvedOn),
                    CreatedOn = dbmodel.CreatedOn

                };

            }
                                        
            return Json(model, JsonRequestBehavior.AllowGet);
        }


        //[HttpPost]
        //public JsonResult EditRowOnMain(Models.DAL.SupportSystemMainMeta obj)
        //{

        //    using (db = new SupportSystemPraksaEntities())
        //    {
        //        var rowMain = db.SupportSystemMain.FirstOrDefault(x => x.Id == obj.Id);
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
            

        public JsonResult ClearTempComments()
        {
            StaticBLL.ClearTempComments();
            var jsonResult = "lista obrisana";
            return Json(jsonResult, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult SaveSuggestion(Models.DAL.SupportSystemMainMeta model)
        {
            var id= Guid.NewGuid();
                        
            SupportSystemMain newObj = new SupportSystemMain()
            {
                Id = id,
                CreatedOn = model.CreatedOn,     
                AcceptedOn = model.AcceptedOn, 
                DueOn = model.DueOn,
                ResolvedOn = model.ResolvedOn,
                IdKategorija = model.IdKategorija,
                IdPriority = model.IdPriority,
                IdSeverity = model.IdSeverity,
                IdStatus = model.IdStatus,
                IdUser = StaticBLL.UserId,
                IdSystemSection = model.IdSystemSection,
                Notes = model.Notes,
                Steps = model.Steps, 
                Number = model.Number,
                Title = model.Title                
            };            

            using (db = new SupportSystemPraksaEntities())
            {
                
                 db.SupportSystemMain.Add(newObj);

                if (StaticBLL.komentari.Count > 0)
                {
                    StaticBLL.komentari.Select(x => { x.IdMain = id; return x; }).ToList();

                    foreach (var item in StaticBLL.komentari)
                    {
                        db.SupportSystemComments.Add(item);
                    }

                }

                 db.SaveChanges();
                
                 var jsonResult = "main i comment ok!";
                 return Json(jsonResult, JsonRequestBehavior.AllowGet);

            }
        }


        [HttpPost]
        public JsonResult SaveEditedSuggestion(Models.DAL.SupportSystemMainMeta obj)
        {

            using (db = new SupportSystemPraksaEntities())
            {
                var rowMain = db.SupportSystemMain.FirstOrDefault(x => x.Id == obj.Id);
                //obj.Id = null;

                
                if (rowMain != null)
                {
                    StaticBLL.CopyNonNullProperties(obj, rowMain);
                    db.Entry(rowMain).State = EntityState.Modified;                   
                }

                if (StaticBLL.komentari.Count > 0)
                {

                    StaticBLL.komentari.Select(x => { x.IdMain = obj.Id; return x; }).ToList();

                    foreach (var item in StaticBLL.komentari)
                    {
                        db.SupportSystemComments.Add(item);
                    }
                }

                db.SaveChanges();
            }

            var jsonResult = "";
            return Json(jsonResult, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult SaveSingleCommentInDb(Guid id)
        {
            using(db = new SupportSystemPraksaEntities())
            {

                if (StaticBLL.komentari.Count > 0)
                {
                    StaticBLL.komentari.Select(x => { x.IdMain = id; return x; }).ToList();

                    foreach (var item in StaticBLL.komentari)
                    {
                        db.SupportSystemComments.Add(item);
                    }

                }

                db.SaveChanges();

            }

            StaticBLL.ClearTempComments();
            return Json("Comment applied successfully.", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SendMailWithComments(string toEmail, string subject, string msg)
        {
            MainBLL mainBll = new MainBLL();
            mainBll.SendMail(toEmail, subject, msg);

            return Json("ok", JsonRequestBehavior.AllowGet);
        }

        public ActionResult Game()
        {

            return View();
        }

        //protected override void Dispose(bool disposing)
        //{
        //    using (db = new SupportSystemPraksaEntities())
        //    {
        //        if (disposing)
        //        {
        //            db.Dispose();
        //        }
        //        base.Dispose(disposing);
        //    }
        //}

    }
}