using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Contacts.Models;

namespace Contacts.Controllers
{
   [Authorize]
   public class CompaniesController : Controller
   {
      // GET: Companies
      public ActionResult Index()
      {
         List<Company> companyList = new List<Company>();
         using (CompanyContactsContext db = new CompanyContactsContext())
         {
            companyList = db.Companies.ToList();
         }
         return View( companyList );
      }

      // GET: Companies/Create
      public ActionResult Create()
      {
         return View();
      }

      // POST: Companies/Create/{name}
      [HttpPost]
      [ValidateAntiForgeryToken]
      public ActionResult Create( [Bind( Include = "CompanyName,CompanyRef" )] Company id )
      {
         if (ModelState.IsValid)
         {
            using (CompanyContactsContext db = new CompanyContactsContext())
            {
               db.Companies.Add( id );
               db.SaveChanges();
            }
            return RedirectToAction( "Index" );
         }

         return View( id );
      }

      // GET: Companies/Edit/{name}
      public ActionResult Edit( string id )
      {
         if (string.IsNullOrEmpty( id ))
         {
            return new HttpStatusCodeResult( HttpStatusCode.BadRequest );
         }
         using (CompanyContactsContext db = new CompanyContactsContext())
         {
            Company company = db.Companies.Find(id);
            if (company != null)
            {
               int? keyContactId = null;
               int? emergencyContactId = null;
               bool onSite = false;
               foreach (Contact contact in db.Contacts)
               {
                  if (contact.CompanyName.Equals( company.CompanyName ))
                  {
                     if (contact.KeyContact)
                     {
                        keyContactId = contact.ContactId;
                        onSite = contact.OnSite;
                     }
                     if (contact.EmergencyContact)
                     {
                        emergencyContactId = contact.ContactId;
                     }
                  }
               }
               var viewModel = new CompanyContactDetailsViewModel
               {
                  CompanyName = company.CompanyName,
                  CompanyRef = company.CompanyRef,
                  NatureOfBusiness = company.NatureOfBusiness,
                  ContactNames = GetContacts(),
                  KeyContact = keyContactId,
                  OnSite = onSite,
                  EmergencyContact = emergencyContactId
               };
               return View( viewModel );
            }
         }
         return HttpNotFound();
      }

      // POST: Companies/Edit/{name}
      [HttpPost]
      [ValidateAntiForgeryToken]
      public ActionResult Edit( [Bind( Include = "CompanyName,CompanyRef,NatureOfBusiness,KeyContact,OnSite,EmergencyContact" )] CompanyContactDetailsViewModel companyView )
      {
         Company company = new Company();
         company.CompanyName = companyView.CompanyName;
         company.CompanyRef = companyView.CompanyRef;
         company.NatureOfBusiness = companyView.NatureOfBusiness;
         companyView.ContactNames = GetContacts();
         if (ModelState.IsValid)
         {
            using (CompanyContactsContext db = new CompanyContactsContext())
            {
               // We may have just made a contact the key or emergency contact. If so, ensure that only one
               // person is the specified contact for this company.
               foreach ( Contact contact in db.Contacts)
               {
                  if (contact.CompanyName.Equals( company.CompanyName ))
                  {
                     contact.KeyContact = false;
                     contact.OnSite = false;
                     if (contact.ContactId == companyView.KeyContact)
                     {
                        contact.KeyContact = true;
                        contact.OnSite = companyView.OnSite;
                     }

                     contact.EmergencyContact = false;
                     if (contact.ContactId == companyView.EmergencyContact)
                     {
                        contact.EmergencyContact = true;
                     }

                     db.Entry( contact ).State = EntityState.Modified;
                  }
               }

               db.Entry( company ).State = EntityState.Modified;
               db.SaveChanges();
            }
            return RedirectToAction( "Index" );
         }
         return View( companyView );
      }

      // GET: Companies/Delete/{name}
      public ActionResult Delete( string id )
      {
         if (string.IsNullOrEmpty( id ))
         {
            return new HttpStatusCodeResult( HttpStatusCode.BadRequest );
         }
         using (CompanyContactsContext db = new CompanyContactsContext())
         {
            Company company = db.Companies.Find(id);
            if (company != null)
            {
               return View( company );
            }
         }
         return HttpNotFound();
      }

      // POST: Companies/Delete/{name}
      [HttpPost]
      [ActionName( "Delete" )]
      [ValidateAntiForgeryToken]
      public ActionResult DeleteConfirmed( string id )
      {
         using (CompanyContactsContext db = new CompanyContactsContext())
         {
            foreach (Contact staff in db.Contacts)
            {
               if (staff.CompanyName.Equals( id ))
               {
                  db.Contacts.Remove( staff );
               }
            }
            db.SaveChanges();

            Company company = db.Companies.Find(id);
            db.Companies.Remove( company );
            db.SaveChanges();
         }
         return RedirectToAction( "Index" );
      }

      private IEnumerable<SelectListItem> GetContacts()
      {
         var selectList = new List<SelectListItem>();
         using (CompanyContactsContext db = new CompanyContactsContext())
         {
            selectList.Add( new SelectListItem { Value = "", Text = "Optional - contact" } );
            foreach (var contact in db.Contacts.ToList())
            {
               selectList.Add( new SelectListItem
               {
                  Value = contact.ContactId.ToString(),
                  Text = contact.Name
               } );
            }
         }
         return selectList;
      }

      protected override void Dispose( bool disposing )
      {
         if (disposing)
         {
         }
         base.Dispose( disposing );
      }
   }
}
