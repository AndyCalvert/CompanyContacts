using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Contacts.Models
{
   public class Contact
   {
      [Key]
      public int ContactId { get; set; }

      [Required( ErrorMessage = "{0} must be specified" )]
      [DisplayName( "Title" )]
      public string Title { get; set; }

      [Required( ErrorMessage = "{0} must be specified" )]
      [DisplayName( "Name" )]
      public string Name { get; set; }

      [Required( ErrorMessage = "{0} must be specified" )]
      [DisplayName( "Surname" )]
      public string Surname { get; set; }

      [DataType(DataType.EmailAddress)]
      [DisplayName( "Email" )]
      public string Email { get; set; }

      [DataType(DataType.PhoneNumber)]
      [DisplayName( "Phone number" )]
      public string Phone { get; set; }

      [DataType( DataType.PhoneNumber )]
      [DisplayName( "Mobile number" )]
      public string Mobile { get; set; }

      [DisplayName( "Position" )]
      public string Position { get; set; }

      [DataType(DataType.MultilineText)]
      [DisplayName( "Notes" )]
      public string Notes { get; set; }

      [DisplayName( "Reports to" )]
      public int? ParentContactId { get; set; }

      [Required( ErrorMessage = "{0} must be specified" )]
      [DisplayName( "Company Name" )]
      public string CompanyName { get; set; }

      public bool EmergencyContact { get; set; }
      public bool KeyContact { get; set; }
      public bool OnSite { get; set; }

      public virtual Contact Parent { get; set; }
      public virtual Company Company { get; set; }
      public IEnumerable<SelectListItem> Companies { get; set; }
      public IEnumerable<SelectListItem> ContactNames { get; set; }
   }
}