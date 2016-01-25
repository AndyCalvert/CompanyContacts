using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace Contacts.Models
{
   public class Company
   {
      [Key]
      [Required(ErrorMessage ="{0} must be specified and unique")]
      [DisplayName("Company Name")]
      public string CompanyName { get; set; }

      [Required( ErrorMessage = "{0} must be specified" )]
      [DisplayName("Company Ref")]
      public string CompanyRef { get; set; }

      [DisplayName("Nature of business")]
      public string NatureOfBusiness { get; set; }
   }
}