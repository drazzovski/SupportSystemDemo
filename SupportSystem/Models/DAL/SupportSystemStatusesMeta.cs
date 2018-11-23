using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SupportSystem.Models.DAL
{
    [MetadataType(typeof(SupportSystemStatusesMeta))]
    public partial class SupportSystemStatuses
    {

    }


    public class SupportSystemStatusesMeta
    {
        public string GuidId { get; set; }
        public Guid Id { get; set; }

        [Required]
        [MinLength(3)]
        public string StatusName { get; set; }

        public string Description { get; set; }
        public bool? isDeleted { get; set; }
    }
}