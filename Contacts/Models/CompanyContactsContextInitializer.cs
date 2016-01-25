using System.Data.Entity;


namespace Contacts.Models
{
   public class CompanyContactsContextInitializer : DropCreateDatabaseAlways<CompanyContactsContext>
   {
      protected override void Seed( CompanyContactsContext context )
      {
         base.Seed( context );
         context.Companies.Add(
            new Company()
            {
               CompanyName = "RippleEffect",
               CompanyRef = "RE",
               NatureOfBusiness = "Digital agency"
            } );
         context.SaveChanges();
      }
   }
}