using Microsoft.AspNet.Identity;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Web.Mvc;
using Paho.Models;
using System.Data.Entity.Infrastructure;
using System.Net;

namespace Paho.Controllers
{
    public class CatReasonNotSamplingController : ControllerBase
    {
        private int _pageSize = 10;

        // GET: CatReasonNotSampling
        [Authorize(Roles = "Admin")]
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.IDSortParm = sortOrder == "id" ? "id_desc" : "id";
            ViewBag.SpaSortParm = string.IsNullOrEmpty(sortOrder) ? "spa_desc" : "";
            ViewBag.EngSortParm = sortOrder == "eng" ? "eng_desc" : "eng";
            ViewBag.OrdenSortParm = sortOrder == "orden" ? "orden_desc" : "orden";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var catalogo = from c in db.CatReasonNotSampling select c;
            if (!string.IsNullOrEmpty(searchString))
            {
                catalogo = catalogo.Where(s => s.SPA.Contains(searchString) || s.ENG.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "spa_desc":
                    catalogo = catalogo.OrderByDescending(s => s.SPA);
                    break;
                case "eng":
                    catalogo = catalogo.OrderBy(s => s.ENG);
                    break;
                case "eng_desc":
                    catalogo = catalogo.OrderByDescending(s => s.ENG);
                    break;
                default:
                    catalogo = catalogo.OrderBy(s => s.SPA);
                    break;
            }

            //**** Link Dashboard
            var user = UserManager.FindById(User.Identity.GetUserId());

            string dashbUrl = "", dashbTitle = "";
            List<Models.CatDashboardLink> lista = (from tg in db.CatDashboarLinks
                                                   where tg.id_country == user.Institution.CountryID
                                                   select tg).ToList();
            if (lista.Count > 0)
            {
                dashbUrl = lista[0].link;
                dashbTitle = lista[0].title;
            }

            ViewBag.DashbUrl = dashbUrl;
            ViewBag.DashbTitle = dashbTitle;

            //****
            int pageSize = _pageSize;
            int pageNumber = (page ?? 1);

            //****
            return View(catalogo.ToPagedList(pageNumber, pageSize));
        }

        // GET: CatReasonNotSampling/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CatReasonNotSampling/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CatReasonNotSampling/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SPA, ENG")]CatReasonNotSampling catalog)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.CatReasonNotSampling.Add(catalog);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (RetryLimitExceededException /* dex */)
            {
                ModelState.AddModelError("", "No es posible guardar los datos. Intente de nuevo, si el problema persiste contacte al administrador.");
            }
            return View(catalog);
        }

        // GET: CatReasonNotSampling/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var catalogo = db.CatReasonNotSampling.Find(id);
            if (catalogo == null)
            {
                return HttpNotFound();
            }
            return View(catalogo);
        }

        // POST: CatReasonNotSampling/Edit/5
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(int? id)
        {
            //try
            //{
            //    // TODO: Add update logic here

            //    return RedirectToAction("Index");
            //}
            //catch
            //{
            //    return View();
            //}
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var studentToUpdate = db.CatReasonNotSampling.Find(id);
            if (TryUpdateModel(studentToUpdate, "",
               new string[] { "SPA", "ENG" }))
            {
                try
                {
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch (RetryLimitExceededException /* dex */)
                {
                    ModelState.AddModelError("", "No es posible guardar los datos. Intente de nuevo, si el problema persiste contacte al administrador.");
                }
            }

            return View(studentToUpdate);
        }

        // GET: CatReasonNotSampling/Delete/5
        public ActionResult Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Delete failed. Try again, and if the problem persists see your system administrator.";
            }
            var catalogo = db.CatReasonNotSampling.Find(id);
            if (catalogo == null)
            {
                return HttpNotFound();
            }
            return View(catalogo);
        }

        // POST: CatReasonNotSampling/Delete/5
        //[HttpPost]
        //public ActionResult Delete(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add delete logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                var student = db.CatReasonNotSampling.Find(id);
                db.CatReasonNotSampling.Remove(student);
                db.SaveChanges();
            }
            catch (RetryLimitExceededException/* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                return RedirectToAction("Delete", new { id = id, saveChangesError = true });
            }
            return RedirectToAction("Index");
        }
    }
}

