using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SupportSystem.Models.DAL
{
    [MetadataType(typeof(SupportSystemCommentsMeta))]
    public partial class SupportSystemComments
    {

    }


    public class SupportSystemCommentsMeta
    {
        public Guid Id { get; set; }
        public Nullable<System.Guid> IdUser { get; set; }
        public string User { get; set; }
        public string OnDate { get; set; }
        public string Comment { get; set; }
        public string IdReply { get; set; }

        public DateTime OnDateDT { get; set; }
    }
}