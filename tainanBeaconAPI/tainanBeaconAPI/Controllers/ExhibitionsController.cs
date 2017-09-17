using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using tainanBeaconAPI.Models;

namespace tainanBeaconAPI.Controllers
{
    public class ExhibitionsController : Controller
    {
        private TainanBeaconApp db = new TainanBeaconApp();

        // GET: Exhibitions
        public ActionResult Index()
        {
            var exhibitions = db.Exhibitions.Include(e => e.Site);
            return View(exhibitions.ToList());
        }

        // GET: Exhibitions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Exhibition exhibition = db.Exhibitions.Find(id);
            if (exhibition == null)
            {
                return HttpNotFound();
            }
            return View(exhibition);
        }

        // GET: Exhibitions/Create
        public ActionResult Create()
        {
            ViewBag.SiteId = new SelectList(db.Sites, "Id", "Name");
            return View();
        }

        // POST: Exhibitions/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,SiteId,Name,Introduction,StartDate,EndDate,OpenTime,CloseTime,TicketInfo,Note,Icon,Photo")] Exhibition exhibition, HttpPostedFileBase icon, HttpPostedFileBase photo)
        {
            if (ModelState.IsValid)
            {
                if (icon != null && icon.ContentLength > 0)
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        icon.InputStream.CopyTo(ms);
                        exhibition.Icon = System.Convert.ToBase64String(ms.GetBuffer());
                    }
                }
                if (photo != null && photo.ContentLength > 0)
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        photo.InputStream.CopyTo(ms);
                        exhibition.Photo = System.Convert.ToBase64String(ms.GetBuffer());
                    }
                }
                db.Exhibitions.Add(exhibition);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SiteId = new SelectList(db.Sites, "Id", "Name", exhibition.SiteId);
            return View(exhibition);
        }

        // GET: Exhibitions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Exhibition exhibition = db.Exhibitions.Find(id);
            if (exhibition == null)
            {
                return HttpNotFound();
            }
            ViewBag.SiteId = new SelectList(db.Sites, "Id", "Name", exhibition.SiteId);
            return View(exhibition);
        }

        // POST: Exhibitions/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,SiteId,Name,Introduction,StartDate,EndDate,OpenTime,CloseTime,TicketInfo,Note,Icon,Photo")] Exhibition exhibition, HttpPostedFileBase icon, HttpPostedFileBase photo)
        {
            if (ModelState.IsValid)
            {
                if (icon != null && icon.ContentLength > 0)
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        icon.InputStream.CopyTo(ms);
                        exhibition.Icon = System.Convert.ToBase64String(ms.GetBuffer());
                    }
                }
                if (photo != null && photo.ContentLength > 0)
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        photo.InputStream.CopyTo(ms);
                        exhibition.Photo = System.Convert.ToBase64String(ms.GetBuffer());
                    }
                }
                db.Entry(exhibition).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SiteId = new SelectList(db.Sites, "Id", "Name", exhibition.SiteId);
            return View(exhibition);
        }

        // GET: Exhibitions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Exhibition exhibition = db.Exhibitions.Find(id);
            if (exhibition == null)
            {
                return HttpNotFound();
            }
            return View(exhibition);
        }

        // POST: Exhibitions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Exhibition exhibition = db.Exhibitions.Find(id);
            db.Exhibitions.Remove(exhibition);
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
