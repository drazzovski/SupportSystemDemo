using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SupportSystem.Models.DAL
{
    [MetadataType(typeof(AspNetRolesMeta))]
    public partial class AspNetRoles
    {

    } 

    public class AspNetRolesMeta
    {
        public string GuidId { get; set; }
        public string Name { get; set; }
    }
}