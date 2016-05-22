using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.Azure.Devices;
using TapBoxCommon;
using TapBoxWebApplication.Models;
using Device = TapBoxCommon.Models.Device;

namespace TapBoxWebApplication.Controllers
{
    public class DevicesController : Controller
    {
        private readonly TapBoxContext _db = new TapBoxContext();
        readonly DeviceManager _deviceManager = new DeviceManager(ConfigurationManager.AppSettings["AzureIoTHub.ConnectionString"]);

        // GET: Devices
        public ActionResult Index()
        {
            return View(_db.Devices.ToList().Select(device => new DeviceViewModel(device)));
        }

        // GET: Devices/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Device device = _db.Devices.Find(id);
            if (device == null)
            {
                return HttpNotFound();
            }
            return View(new DeviceViewModel(device));
        }

        // GET: Devices/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Devices/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DeviceViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var newDevice = new Device();
                newDevice.DeviceName = vm.DeviceName;
                vm.UpdateModel(newDevice);
                _db.Devices.Add(newDevice);

                _deviceManager.AddDeviceAsync(newDevice.DeviceName);

                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(vm);
        }

        // GET: Devices/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Device device = _db.Devices.Find(id);
            if (device == null)
            {
                return HttpNotFound();
            }
            return View(new DeviceViewModel(device));
        }

        // POST: Devices/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(DeviceViewModel vm)
        {
            if (ModelState.IsValid) {
                Device device = _db.Devices.Find(vm.DeviceName);
                if (device == null) {
                    return HttpNotFound();
                }
                vm.UpdateModel(device);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(vm);
        }

        // GET: Devices/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Device device = _db.Devices.Find(id);
            if (device == null)
            {
                return HttpNotFound();
            }
            return View(new DeviceViewModel(device));
        }

        // POST: Devices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Device device = _db.Devices.Find(id);
            _db.Devices.Remove(device);
            _deviceManager.RemoveDeviceAsync(device.DeviceName);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Sends a request to unlock to the device.
        /// </summary>
        /// <param name="id">Device which should be unlocked.</param>
        public async Task<ActionResult> Unlock(string id)
        {
            // TODO why does this need to be async? Couldn't we wait until the task finishes?
            var command = new Commands(ConfigurationManager.AppSettings["AzureIoTHub.ConnectionString"]);
            await command.SendUnlockAsync(id);
            return View(new UnlockDeviceViewModel() {DeviceName = id});
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
