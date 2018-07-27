using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SupportSystem.Models.DAL
{

    [MetadataType(typeof(SupportSystemSeverityMeta))]
    public partial class SupportSystemSeverity
    {

    }

    public class SupportSystemSeverityMeta
    {
        public string GuidId { get; set; }
        public string StatusName { get; set; }
    }
}