using System;
using System.Collections.Generic;
using System.Linq;
using SupportSystem.Models.DAL;
using MimeKit;
using MailKit.Security;

namespace SupportSystem.Models.BLL
{
    public class MainBLL
    {
        private SupportSystemPraksaEntities db;

        public List<SupportSystemMainMeta> GetMainList() 
        {

            List<SupportSystemMainMeta> lista = new List<SupportSystemMainMeta>();


            using (db = new SupportSystemPraksaEntities())
            {
                var ssMain = db.SupportSystemMain.AsEnumerable().OrderBy(x => int.Parse(x.Number)).ToList();

                foreach (var item in ssMain)
                {
                    var result = new SupportSystemMainMeta
                    {
                        Id = item.Id,
                        Number = item.Number,
                        Title = item.Title,
                        IdStatus = item.IdStatus,
                        IdKategorija = item.IdKategorija,
                        IdPriority = item.IdPriority,
                        IdSeverity = item.IdSeverity,
                        Status = item.SupportSystemStatuses?.StatusName,
                        Kategorija = item.SupportSystemCategory?.CategoryName,
                        Severity = item.SupportSystemSeverity?.SeverityName,
                        User = item.AspNetUsers.UserName,
                        strCreatedOn = String.Format("{0:dd/MM/yyyy}", item.CreatedOn),
                        strAcceptedOn = String.Format("{0:dd/MM/yyyy}", item.AcceptedOn),
                        strDueOn = String.Format("{0:dd/MM/yyyy}", item.DueOn),
                        strResolvedOn = String.Format("{0:dd/MM/yyyy}", item.ResolvedOn),
                        AcceptedOn = item.AcceptedOn,
                        DueOn = item.DueOn,
                        ResolvedOn = item.ResolvedOn,
                        Priority = item.SupportSystemPriority?.PriorityName,
                        Steps = item.Steps,
                        Notes = item.Notes,
                        SystemSection = item.SupportSystemSection?.Name
                    };


                    lista.Add(result);
                }
            }   

            return lista;
        }

        public List<SupportSystemMainMeta> ShowClosed()
        {
            List<SupportSystemMainMeta> lista = new List<SupportSystemMainMeta>();

            using (db = new SupportSystemPraksaEntities())
            {
                var ssMain = db.SupportSystemMain.Where(x => x.SupportSystemStatuses.StatusName.Trim() == "Closed").AsEnumerable().OrderBy(x => int.Parse(x.Number)).ToList();


                foreach (var item in ssMain)
                {
                    var result = new SupportSystemMainMeta
                    {
                        Id = item.Id,
                        Number = item.Number,
                        Title = item.Title,
                        IdStatus = item.IdStatus,
                        IdKategorija = item.IdKategorija,
                        IdPriority = item.IdPriority,
                        IdSeverity = item.IdSeverity,
                        Status = item.SupportSystemStatuses?.StatusName,
                        Kategorija = item.SupportSystemCategory?.CategoryName,
                        Severity = item.SupportSystemSeverity?.SeverityName,
                        User = item.AspNetUsers.UserName,
                        strCreatedOn = String.Format("{0:dd/MM/yyyy}", item.CreatedOn),
                        strAcceptedOn = String.Format("{0:dd/MM/yyyy}", item.AcceptedOn),
                        strDueOn = String.Format("{0:dd/MM/yyyy}", item.DueOn),
                        strResolvedOn = String.Format("{0:dd/MM/yyyy}", item.ResolvedOn),
                        AcceptedOn = item.AcceptedOn,
                        DueOn = item.DueOn,
                        ResolvedOn = item.ResolvedOn,
                        Priority = item.SupportSystemPriority?.PriorityName,
                        Steps = item.Steps,
                        Notes = item.Notes,
                        SystemSection = item.SupportSystemSection?.Name
                    };


                    lista.Add(result);
                }
            }           

            return lista;
        }


        public string SuggestionNumber()
        {
            using (db = new SupportSystemPraksaEntities())
            {
                var numlist = db.SupportSystemMain.Select(x => x.Number).ToList().Select(s => int.Parse(s)).OrderByDescending(x => x).ToList();                 

                return (numlist[0] + 1).ToString().PadLeft(3, '0');
            }           
        }

        public class MyDictEntry
        {
            public string key { get; set; }
            public string value { get; set; }
        };

        public List<MyDictEntry> StatusList()
        {
           using (db = new SupportSystemPraksaEntities())
           {
                var dict = db.SupportSystemStatuses.Where(x => x.isDeleted == false).Select(x => new SupportSystemStatusesMeta { GuidId = x.Id.ToString(), StatusName = x.StatusName }).ToDictionary(t => t.GuidId, t => t.StatusName);
                var list = dict.Keys.AsEnumerable().Select(x => new MyDictEntry { key = x, value = dict[x] }).ToList();
                return list;
            }          
        }

        public List<MyDictEntry> CategoriesList()
        {
            using (db = new SupportSystemPraksaEntities())
            {
                var dict = db.SupportSystemCategory.Select(x => new SupportSystemCategoryMeta { GuidId = x.Id.ToString(), StatusName = x.CategoryName }).ToDictionary(t => t.GuidId, t => t.StatusName);
                var list = dict.Keys.AsEnumerable().Select(x => new MyDictEntry { key = x, value = dict[x] }).ToList();
                return list;
            }           
        }

        public List<MyDictEntry> PrioritiesList()
        {
            using (db = new SupportSystemPraksaEntities())
            {
                var dict = db.SupportSystemPriority.Select(x => new SupportSystemPriorityMeta { GuidId = x.Id.ToString(), StatusName = x.PriorityName }).ToDictionary(t => t.GuidId, t => t.StatusName);
                var list = dict.Keys.AsEnumerable().Select(x => new MyDictEntry { key = x, value = dict[x] }).ToList();
                return list;
            }
           
        }

        public List<MyDictEntry> SeveritiesList()
        {
            using (db = new SupportSystemPraksaEntities())
            {
                var dict = db.SupportSystemSeverity.Select(x => new SupportSystemSeverityMeta { GuidId = x.Id.ToString(), StatusName = x.SeverityName }).ToDictionary(t => t.GuidId, t => t.StatusName);
                var list = dict.Keys.AsEnumerable().Select(x => new MyDictEntry { key = x, value = dict[x] }).ToList();
                return list;
            }
          
        }

        public List<MyDictEntry> RolesList()
        {
            using (db = new SupportSystemPraksaEntities())
            {
                var dict = db.AspNetRoles.Select(x => new AspNetRolesMeta { GuidId = x.Id.ToString(), Name = x.Name }).ToDictionary(t => t.GuidId, t => t.Name);
                var list = dict.Keys.AsEnumerable().Select(x => new MyDictEntry { key = x, value = dict[x] }).ToList();
                return list;
            }
           
        }

        public List<MyDictEntry> SectionsList()
        {
            using (db = new SupportSystemPraksaEntities())
            {
                var dict = db.SupportSystemSection.Where(x => x.isDeleted == false).Select(x => new SupportSystemSectionMeta { GuidId = x.Id.ToString(), Name = x.Name }).ToDictionary(t => t.GuidId, t => t.Name);
                var list = dict.Keys.AsEnumerable().Select(x => new MyDictEntry { key = x, value = dict[x] }).ToList();
                return list;
            }
           
        }


        public List<AspNetUsersMeta> GetUsersList()
        {
            using (db = new SupportSystemPraksaEntities())
            {
                var ssMain = db.AspNetUsers.ToList();
                List<AspNetUsersMeta> lista = new List<AspNetUsersMeta>();

                foreach (var item in ssMain)
                {
                    var result = new AspNetUsersMeta
                    {
                        Id = item.Id,
                        Address = item.Address,
                        City = item.City,
                        Country = item.Country,
                        Email = item.Email,
                        isActive = item.isActive,
                        UserName = item.UserName
                    };


                    lista.Add(result);
                }

                return lista;
            }
          
        }

        public List<AspNetUsersMeta> GetNonActiveUsersList()
        {
            using (db = new SupportSystemPraksaEntities())
            {
                var ssMain = db.AspNetUsers.Where(x => x.isActive == false).ToList();
                List<AspNetUsersMeta> lista = new List<AspNetUsersMeta>();

                foreach (var item in ssMain)
                {
                    var result = new AspNetUsersMeta
                    {
                        Id = item.Id,
                        Address = item.Address,
                        City = item.City,
                        Country = item.Country,
                        Email = item.Email,
                        isActive = item.isActive,
                        UserName = item.UserName
                    };


                    lista.Add(result);
                }

                return lista;
            }

        }


        public List<SupportSystemSectionMeta> GetSectionList()
        {
            using (db = new SupportSystemPraksaEntities())
            {
                var ssMain = db.SupportSystemSection.Where(x => x.isDeleted == false).ToList();
                List<SupportSystemSectionMeta> lista = new List<SupportSystemSectionMeta>();

                foreach (var item in ssMain)
                {
                    var result = new SupportSystemSectionMeta
                    {
                        Id = item.Id,
                        Name = item.Name,
                        Description = item.Description,
                        isDeleted = item.isDeleted
                    };


                    lista.Add(result);
                }

                return lista;
            }
          
        }

        public List<SupportSystemSectionMeta> GetSectionDeletedList()
        {
            using (db = new SupportSystemPraksaEntities())
            {
                var ssMain = db.SupportSystemSection.Where(x => x.isDeleted == true).ToList();
                List<SupportSystemSectionMeta> lista = new List<SupportSystemSectionMeta>();

                foreach (var item in ssMain)
                {
                    var result = new SupportSystemSectionMeta
                    {
                        Id = item.Id,
                        Name = item.Name,
                        Description = item.Description,
                        isDeleted = item.isDeleted
                    };


                    lista.Add(result);
                }

                return lista;
            }
            
        }

        public List<SupportSystemStatusesMeta> GetStatusList()
        {
            using (db = new SupportSystemPraksaEntities())
            {
                var ssMain = db.SupportSystemStatuses.Where(x => x.isDeleted == false).ToList();
                List<SupportSystemStatusesMeta> lista = new List<SupportSystemStatusesMeta>();

                foreach (var item in ssMain)
                {
                    var result = new SupportSystemStatusesMeta
                    {
                        Id = item.Id,
                        StatusName = item.StatusName,
                        Description = item.Description,
                        isDeleted = item.isDeleted
                    };


                    lista.Add(result);
                }

                return lista;
            }
           
        }

        public List<SupportSystemStatusesMeta> GetDeletedStatusList()
        {
            using (db = new SupportSystemPraksaEntities())
            {
                var ssMain = db.SupportSystemStatuses.Where(x => x.isDeleted == true).ToList();
                List<SupportSystemStatusesMeta> lista = new List<SupportSystemStatusesMeta>();

                foreach (var item in ssMain)
                {
                    var result = new SupportSystemStatusesMeta
                    {
                        Id = item.Id,
                        StatusName = item.StatusName,
                        Description = item.Description,
                        isDeleted = item.isDeleted
                    };


                    lista.Add(result);
                }

                return lista;
            }
          
        }
        
        public List<SupportSystemCommentsMeta> PokupiKomentare(Guid id)
        {
            using (db = new SupportSystemPraksaEntities())
            {
                var lista = db.SupportSystemComments.Where(x => x.IdMain == id).OrderBy(x => x.OnDate).ToList();

                List<SupportSystemCommentsMeta> listaMainKomentara = new List<SupportSystemCommentsMeta>();
                List<SupportSystemCommentsMeta> listaReplyKomentara = new List<SupportSystemCommentsMeta>();

                foreach (var item in lista)
                {
                    var commentsMeta = new SupportSystemCommentsMeta()
                    {
                        Id = item.Id,
                        Comment = item.Comment,
                        IdReply = item.IdReply,
                        IdUser = item.IdUser,
                        OnDate = String.Format("{0:G}", item.OnDate),
                        OnDateDT = item.OnDate.Value,
                        User = item.AspNetUsers.UserName
                    };


                    if (item.IdReply == "0")
                    {
                        listaMainKomentara.Add(commentsMeta);

                    }
                    else
                    {
                        listaReplyKomentara.Add(commentsMeta);
                    }
                }

                listaReplyKomentara = listaReplyKomentara.OrderByDescending(x => x.OnDateDT).ToList();

                foreach(var item in listaReplyKomentara)
                {
                    listaMainKomentara.AddAfterMainComment(x => x.Id.ToString() == item.IdReply, item);
                }

                return listaMainKomentara;
            }
           
        }
        
        //public class RefreshDetectFilter : ActionFilterAttribute, IActionFilter
        //{
        //    public override void OnActionExecuting(ActionExecutingContext filterContext)
        //    {
        //        var cookie = filterContext.HttpContext.Request.Cookies["RefreshFilter"];
        //        filterContext.RouteData.Values["IsRefreshed"] = cookie != null &&
        //                                                        cookie.Value == filterContext.HttpContext.Request.Url.ToString();
        //    }
        //    public override void OnActionExecuted(ActionExecutedContext filterContext)
        //    {
        //        filterContext.HttpContext.Response.SetCookie(new HttpCookie("RefreshFilter", filterContext.HttpContext.Request.Url.ToString()));
        //    }
        //}    

        public void SendMail(string toEmail, string subject, string msg)
        {
            var emailBody = new BodyBuilder();
            emailBody.HtmlBody = msg;
            
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("testsuppp123@gmail.com"));
            message.To.Add(new MailboxAddress(toEmail));
            message.Subject = subject;
            message.Body = emailBody.ToMessageBody();

            using (var client = new MailKit.Net.Smtp.SmtpClient())
            {
                client.ServerCertificateValidationCallback = (s, c, ch, e) => true;
                client.Connect("smtp.gmail.com", 465, SecureSocketOptions.SslOnConnect);
                client.Authenticate("testsuppp123@gmail.com", "Asdfg123!");
                client.Send(message);
                client.Disconnect(false);
            }
        }
    }
}