using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SupportSystem.Models.DAL
{

    [MetadataType(typeof(SupportSystemMainMeta))]
    public partial class SupportSystemMain //class u edmxu
    {

    }



    public class SupportSystemMainMeta // viewmodel
    {
        
        public System.Guid? Id { get; set; }

        [Required]
        public string Number { get; set; }
        
        [Required]
        [MinLength(3)]
        public string Title { get; set; }

        public string Status { get; set; }
        public string Kategorija { get; set; }
        public string Severity { get; set; }
        //public string Comment { get; set; }
        public string User { get; set; }
        public string strCreatedOn { get; set; }
        public string strAcceptedOn { get; set; }
        public string strDueOn { get; set; }
        public string strResolvedOn { get; set; }

        [Required]
        public Nullable<System.Guid> IdStatus { get; set; }

        [Required]
        public Nullable<System.Guid> IdKategorija { get; set; }

        [Required]
        public Nullable<System.Guid> IdSeverity { get; set; } 

        [Required]
        public Nullable<System.Guid> IdUser { get; set; }

        [Required]
        public Nullable<System.Guid> IdPriority { get; set; }

        [Required]
        public Nullable<System.Guid> IdSystemSection { get; set; }

        [Required]
        public Nullable<System.DateTime> AcceptedOn { get; set; }

        [Required]
        public Nullable<System.DateTime> DueOn { get; set; }
        public Nullable<System.DateTime> ResolvedOn { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public string Priority { get; set; }
        public string Steps { get; set; }
        public string Notes { get; set; }
        public string SystemSection { get; set; }
    }
}