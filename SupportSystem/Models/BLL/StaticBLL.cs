using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace SupportSystem.Models.BLL
{
    public static class StaticBLL
    {
        public static Guid UserId { get; set; }

        public static EditMode emode { get; set; }

        //lastuUrl in custom auth (line: 46)
        public static string lastUrl;

        public enum EditMode
        {
            Disabled,
            Edit
        }

        public class CustomAuthorize : AuthorizeAttribute
        {

            public CustomAuthorize(params EditMode[] roles)
            {

            }

            protected override bool AuthorizeCore(HttpContextBase httpContext)
            {
                var isAuthorized = base.AuthorizeCore(httpContext);

                if (!isAuthorized)
                {
                    return false;
                }

                if (lastUrl != httpContext.Request.Path)
                {

                    emode = EditMode.Disabled;

                }

                if (emode == EditMode.Edit)
                {
                    return true;

                }
                else
                {
                    httpContext.Response.Redirect("/Home/DisabledEdit");
                    return false;
                }
            }
        }
        
        //Temporary lista komentara
        public static List<SupportSystemComments> komentari = new List<SupportSystemComments>();

        //Clear list 
        public static void ClearTempComments()
        {
            komentari.Clear();
        }
        
        public static void CopyNonNullProperties(object source, object target) // a b
        {

            Type typeB = target.GetType();
            foreach (PropertyInfo property in source.GetType().GetProperties())
            {
                if (!property.CanRead || (property.GetIndexParameters().Length > 0))
                    continue;

                PropertyInfo other = typeB.GetProperty(property.Name);

                var newProp = property.GetValue(source, null);

                if ((other != null) && (other.CanWrite) && newProp != null)
                    other.SetValue(target, newProp, null);
            }
        }


        //public static List<SupportSystemCommentsMeta> PokupiKomentare(Guid id)
        //{
        //    using (db = new SupportSystemPraksaEntities())
        //    {
        //        var lista = db.SupportSystemComments.Where(x => x.IdMain == id).OrderBy(x => x.OnDate).ToList();

        //        List<SupportSystemCommentsMeta> listaMainKomentara = new List<SupportSystemCommentsMeta>();
        //        List<SupportSystemCommentsMeta> listaReplyKomentara = new List<SupportSystemCommentsMeta>();

        //        foreach (var item in lista)
        //        {
        //            var commentsMeta = new SupportSystemCommentsMeta()
        //            {
        //                Id = item.Id,
        //                Comment = item.Comment,
        //                IdReply = item.IdReply,
        //                IdUser = item.IdUser,
        //                OnDate = String.Format("{0:G}", item.OnDate),
        //                User = item.AspNetUsers.UserName
        //            };


        //            if (item.IdReply == "0")
        //            {
        //                listaMainKomentara.Add(commentsMeta);

        //            }
        //            else
        //            {
        //                listaReplyKomentara.Add(commentsMeta);
        //            }
        //        }

        //        listaReplyKomentara = listaReplyKomentara.OrderByDescending(x => x.OnDate).ToList();

        //        foreach (var item in listaReplyKomentara)
        //        {
        //            listaMainKomentara.AddAfterMainComment(x => x.Id.ToString() == item.IdReply, item);
        //        }

        //        return listaMainKomentara;
        //    }

        //}

        public static void AddAfterMainComment<T>(this List<T> list, Func<T, Boolean> condition, T commentToAdd)
        {
            foreach (var item in list.Select((o, i) => new { Value = o, Index = i }).Where(p => condition(p.Value)).OrderBy(p => p.Index))
            {
                if (item.Index + 1 == list.Count) list.Add(commentToAdd);
                else list.Insert(item.Index + 1, commentToAdd);
            }
        }

    }
}