using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SupportSystem.Models.DAL
{

    [MetadataType(typeof(SupportSystemCategoryMeta))]
    public partial class SupportSystemCategory
    {

    }

    public class SupportSystemCategoryMeta
    {
        public string GuidId { get; set; }
        public string StatusName { get; set; }
    }
}