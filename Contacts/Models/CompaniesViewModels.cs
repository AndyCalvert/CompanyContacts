using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Contacts.Models
{
   public class CompanyContactDetailsViewModel
   {
      [Key]
      [Required( ErrorMessage = "{0} must be specified and unique" )]
      [DisplayName( "Company Name" )]
      public string CompanyName { get; set; }

      [Required( ErrorMessage = "{0} must be specified" )]
      [DisplayName( "Company Ref" )]
      public string CompanyRef { get; set; }

      [DisplayName( "Nature of business" )]
      public string NatureOfBusiness { get; set; }

      [DisplayName( "Key contact")]
      public int? KeyContact { get; set; }

      [DisplayName( "Key contact on site")]
      public bool OnSite { get; set; }

      [DisplayName( "Emergency contact" )]
      public int? EmergencyContact { get; set; }

      public IEnumerable<SelectListItem> ContactNames { get; set; }
   }
}