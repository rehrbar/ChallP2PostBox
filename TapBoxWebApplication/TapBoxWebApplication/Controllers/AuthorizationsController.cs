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
using Authorization = TapBoxCommon.Models.Authorization;

namespace TapBoxWebApplication.Controllers
{
    public class AuthorizationsController : Controller
    {
        private TapBoxContext db = new TapBoxContext();

        // GET: Authorizations
        public ActionResult Index()
        {
            var authorizations = db.Authorizations.Include(a => a.Device).Include(a => a.Key);
            return View(authorizations.ToList());
        }

        // GET: Authorizations/Details/5
        public ActionResult Details(Guid? keyId, string deviceId) {
            if (deviceId == null || keyId == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Authorization authorization = db.Authorizations.Find(keyId, deviceId);
            if (authorization == null)
            {
                return HttpNotFound();
            }
            return View(authorization);
        }

        // GET: Authorizations/Create
        public ActionResult Create()
        {
            ViewBag.DeviceName = new SelectList(db.Devices, "DeviceName", "DeviceName");
            ViewBag.AccessKeyId = new SelectList(db.AccessKeys, "Id", "CardUID");
            return View();
        }

        // POST: Authorizations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AccessKeyId,DeviceName")] Authorization authorization)
        {
            if (ModelState.IsValid)
            {
                db.Authorizations.Add(authorization);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DeviceName = new SelectList(db.Devices, "DeviceName", "DeviceName", authorization.DeviceName);
            ViewBag.AccessKeyId = new SelectList(db.AccessKeys, "Id", "CardUID", authorization.AccessKeyId);
            return View(authorization);
        }

        // GET: Authorizations/Edit/5
        public ActionResult Edit(Guid? keyId, string deviceId)
        {
            if (deviceId == null || keyId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Authorization authorization = db.Authorizations.Find(keyId, deviceId);
            if (authorization == null)
            {
                return HttpNotFound();
            }
            ViewBag.DeviceName = new SelectList(db.Devices, "DeviceName", "DeviceName", authorization.DeviceName);
            ViewBag.AccessKeyId = new SelectList(db.AccessKeys, "Id", "CardUID", authorization.AccessKeyId);
            return View(authorization);
        }

        // POST: Authorizations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AccessKeyId,DeviceName")] Authorization authorization)
        {
            if (ModelState.IsValid)
            {
                db.Entry(authorization).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DeviceName = new SelectList(db.Devices, "DeviceName", "DeviceName", authorization.DeviceName);
            ViewBag.AccessKeyId = new SelectList(db.AccessKeys, "Id", "CardUID", authorization.AccessKeyId);
            return View(authorization);
        }

        // GET: Authorizations/Delete/5
        public ActionResult Delete(Guid? keyId, string deviceId)
        {
            if (deviceId == null || keyId == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Authorization authorization = db.Authorizations.Find(keyId, deviceId);
            if (authorization == null)
            {
                return HttpNotFound();
            }
            return View(authorization);
        }

        // POST: Authorizations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid? keyId, string deviceId) {
            Authorization authorization = db.Authorizations.Find(keyId, deviceId);
            db.Authorizations.Remove(authorization);
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
