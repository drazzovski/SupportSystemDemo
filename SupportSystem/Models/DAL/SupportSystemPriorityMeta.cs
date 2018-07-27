using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SupportSystem.Models.DAL
{
    [MetadataType(typeof(SupportSystemPriorityMeta))]
    public partial class SupportSystemPriority
    {

    }

    public class SupportSystemPriorityMeta
    {
        public string GuidId { get; set; }
        public string StatusName { get; set; }
    }
}