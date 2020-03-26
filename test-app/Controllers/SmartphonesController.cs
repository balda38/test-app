﻿using System.Collections.Generic;
using System.Web.Mvc;
using test_app.Core;

namespace test_app.Controllers
{
    public class SmartphonesController : Controller
    {
        private QueryBuilder queryBuilder;

        public SmartphonesController()
        {
            queryBuilder = new QueryBuilder();
        }

        // GET: Smartphones
        public ActionResult Index()
        {
            string query = "GetAllSmartphones";

            queryBuilder.SetCommandType("procedure");
            List<string> result = queryBuilder.Execute(query);

            ViewData["smartphones"] = result;
            return View();
        }

        // GET: Smartphones/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Smartphones/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Smartphones/Create
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

        // GET: Smartphones/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Smartphones/Edit/5
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

        // GET: Smartphones/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Smartphones/Delete/5
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
