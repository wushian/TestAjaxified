using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using TestAjax.Models;

namespace TestAjax.Controllers
{
    public class AddressesController : Controller
    {
        private DataDb db = new DataDb();


        public ActionResult Index(int id)
        {
            ViewBag.PersonID = id;
            var addresses = db.Addresses.Where(a => a.PersonID == id).OrderBy(a => a.City);

            return PartialView("_Index", addresses.ToList());
        }

        [ChildActionOnly]
        public ActionResult List(int id)
        {
            ViewBag.PersonID = id;
            var addresses = db.Addresses.Where(a => a.PersonID == id);

            return PartialView("_List", addresses.ToList());
        }

        public ActionResult Create(int PersonID)
        {
            Address address = new Address();
            address.PersonID = PersonID;

            return PartialView("_Create", address);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,City,Street,Phone,PersonID")] Address address)
        {
            if (ModelState.IsValid)
            {
                db.Addresses.Add(address);
                db.SaveChanges();

                string url = Url.Action("Index", "Addresses", new { id = address.PersonID });
                return Json(new { success = true, url = url });
            }

            return PartialView("_Create", address);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Address address = db.Addresses.Find(id);
            if (address == null)
            {
                return HttpNotFound();
            }

            return PartialView("_Edit", address);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,City,Street,Phone,PersonID")] Address address)
        {
            if (ModelState.IsValid)
            {
                db.Entry(address).State = EntityState.Modified;
                db.SaveChanges();

                string url = Url.Action("Index", "Addresses", new { id = address.PersonID });
                return Json(new { success = true, url = url });
            }


            return PartialView("_Edit", address);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Address address = db.Addresses.Find(id);
            if (address == null)
            {
                return HttpNotFound();
            }
            return PartialView("_Delete", address);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Address address = db.Addresses.Find(id);
            db.Addresses.Remove(address);
            db.SaveChanges();

            string url = Url.Action("Index", "Addresses", new { id = address.PersonID });
            return Json(new { success = true, url = url });

        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
