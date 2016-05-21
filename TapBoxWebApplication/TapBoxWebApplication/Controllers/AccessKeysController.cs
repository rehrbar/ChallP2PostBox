using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TapBoxCommon;
using TapBoxCommon.Models;
using TapBoxWebApplication.Models;

namespace TapBoxWebApplication.Controllers
{
    public class AccessKeysController : Controller
    {
        private TapBoxContext db = new TapBoxContext();

        // GET: AccessKeys
        public ActionResult Index()
        {
            return View(db.AccessKeys.ToList().Select(key => new AccessKeyViewModel(key)));
        }

        // GET: AccessKeys/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccessKey accessKey = db.AccessKeys.Find(id);
            if (accessKey == null)
            {
                return HttpNotFound();
            }
            return View(accessKey);
        }

        // GET: AccessKeys/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AccessKeys/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AccessKeyViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var accessKey = new AccessKey();
                accessKey.Id = Guid.NewGuid();
                vm.UpdateModel(accessKey);
                db.AccessKeys.Add(accessKey);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(vm);
        }

        // GET: AccessKeys/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccessKey accessKey = db.AccessKeys.Find(id);
            if (accessKey == null)
            {
                return HttpNotFound();
            }
            return View(new AccessKeyViewModel(accessKey));
        }

        // POST: AccessKeys/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AccessKeyViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var accessKey = db.AccessKeys.Find(vm.Id);
                vm.UpdateModel(accessKey);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(vm);
        }

        // GET: AccessKeys/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccessKey accessKey = db.AccessKeys.Find(id);
            if (accessKey == null)
            {
                return HttpNotFound();
            }
            return View(new AccessKeyViewModel(accessKey));
        }

        // POST: AccessKeys/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            AccessKey accessKey = db.AccessKeys.Find(id);
            db.AccessKeys.Remove(accessKey);
            db.SaveChanges();
            return RedirectToAction("Index");
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
