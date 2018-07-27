using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SupportSystem.Models.DAL
{
    [MetadataType(typeof(SupportSystemSectionMeta))]
    public partial class SupportSystemSection
    {


    }

    public class SupportSystemSectionMeta
    {
        public Guid Id { get; set; }
        public string GuidId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool? isDeleted { get; set; }
    }
}