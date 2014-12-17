using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ApiGeoMoniker.Controllers
{
    public class MapViewController : Controller
    {
        //
        // GET: /MapView/

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /MapView/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /MapView/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /MapView/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /MapView/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /MapView/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /MapView/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /MapView/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
