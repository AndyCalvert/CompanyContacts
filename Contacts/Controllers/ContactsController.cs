using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Contacts.Models;

namespace Contacts.Controllers
{
   [Authorize]
   public class ContactsController : Controller
   {
      // GET: Contacts
      public ActionResult Index()
      {
         List<Contact> view = new List<Contact>();
         using (CompanyContactsContext db = new CompanyContactsContext())
         {
            view = db.Contacts.ToList();
         }
         return View( view );
      }

      // GET: Contacts/_Details/{companyName}
      public ActionResult _Details(string id)
      {
         List<Contact> view = new List<Contact>();
         using (CompanyContactsContext db = new CompanyContactsContext())
         {
            foreach (Contact thisContact in db.Contacts)
            {
               if (thisContact.CompanyName.Equals( id ))
               {
                  view.Add( thisContact );
               }
            }
         }
         return View( view );
      }

      // GET: Contacts/Create
      public ActionResult Create()
      {
         Contact contact      = new Contact();
         contact.Companies    = GetCompanies();
         contact.ContactNames = GetContacts();
         return View( contact );
      }

      // POST: Contacts/Create
      [HttpPost]
      [ValidateAntiForgeryToken]
      public ActionResult Create( [Bind( Include = "CompanyName,ContactId,Title,Name,Surname,Email,Phone,Mobile,Position,ParentContactId,Notes" )] Contact contact )
      {
         contact.Companies    = GetCompanies();
         contact.ContactNames = GetContacts();
         using (CompanyContactsContext db = new CompanyContactsContext())
         {
            if (ModelState.IsValid)
            {
               db.Contacts.Add( contact );
               db.SaveChanges();
               return RedirectToAction( "Index" );
            }
         }
         return View( contact );
      }

      // GET: Contacts/Edit/5
      public ActionResult Edit( int? id )
      {
         if (id == null)
         {
            return new HttpStatusCodeResult( HttpStatusCode.BadRequest );
         }
         using (CompanyContactsContext db = new CompanyContactsContext())
         {
            Contact contact = db.Contacts.Find(id);
            if (contact != null)
            {
               contact.Companies = GetCompanies();
               contact.ContactNames = GetContacts();
               return View( contact );
            }
         }
         return HttpNotFound();
      }

      // POST: Contacts/Edit/5
      [HttpPost]
      [ValidateAntiForgeryToken]
      public ActionResult Edit( [Bind( Include = "CompanyName,CompanyId,ContactId,Title,Name,Surname,Email,Phone,Mobile,Position,ParentContactId,Notes" )] Contact contact )
      {
         contact.Companies = GetCompanies();
         contact.ContactNames = GetContacts();

         if (ModelState.IsValid)
         {
            using (CompanyContactsContext db = new CompanyContactsContext())
            {
               db.Entry( contact ).State = EntityState.Modified;
               db.SaveChanges();
            }
            return RedirectToAction( "Index" );
         }
         return View( contact );
      }

      // GET: Contacts/Delete/5
      public ActionResult Delete( int? id )
      {
         if (id == null)
         {
            return new HttpStatusCodeResult( HttpStatusCode.BadRequest );
         }
         using (CompanyContactsContext db = new CompanyContactsContext())
         {
            Contact contact = db.Contacts.Find(id);
            if (contact != null)
            {
               return View( contact );
            }
         }
         return HttpNotFound();
      }

      // POST: Contacts/Delete/5
      [HttpPost]
      [ActionName( "Delete" )]
      [ValidateAntiForgeryToken]
      public ActionResult DeleteConfirmed( int id )
      {
         using (CompanyContactsContext db = new CompanyContactsContext())
         {
            // Need to null out any references to this contact as being a manger. Note that we do *not* want to implement a cascade delete !
            foreach (Contact staff in db.Contacts)
            {
               if (staff.ParentContactId == id)
               {
                  staff.ParentContactId = null;
                  db.Entry( staff).State = EntityState.Modified;
               }
            }
            db.SaveChanges();

            Contact contact = db.Contacts.Find(id);
            db.Contacts.Remove( contact );
            db.SaveChanges();
         }
         return RedirectToAction( "Index" );
      }

      private IEnumerable<SelectListItem> GetCompanies()
      {
         var selectList = new List<SelectListItem>();
         using (CompanyContactsContext db = new CompanyContactsContext())
         {
            foreach (var company in db.Companies.ToList())
            {
               selectList.Add( new SelectListItem
               {
                  Value = company.CompanyName,
                  Text = company.CompanyName
               } );
            }
         }
         return selectList;
      }

      private IEnumerable<SelectListItem> GetContacts()
      {
         var selectList = new List<SelectListItem>();
         using (CompanyContactsContext db = new CompanyContactsContext())
         {
            selectList.Add( new SelectListItem { Value = "", Text = "Optional - manager" });
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
