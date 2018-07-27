using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SupportSystem.Models.DAL
{

    [MetadataType(typeof(AspNetUsersMeta))]
    public partial class AspNetUsers
    {

    }

    public class AspNetUsersMeta
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public bool isActive { get; set; }
        public string FirstName { get; set; }
        public string PhoneNumber { get; set; }
        public string LastName { get; set; }
        public string RoleName { get; set; }
        public Guid IdRole { get; set; }

    }
}