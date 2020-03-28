using System.Data;
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
            DataTable result = queryBuilder.Execute(query);

            ViewBag.smartphones = result;
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

        // POST: Smartphones/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            string json = "{ \"id\": \"" + id + "\" }"; 
            string query = "DeleteSmartphoneById";
            queryBuilder.SetCommandType("procedure");
            DataTable result = queryBuilder.Execute(query, json);

            ViewBag.smartphones = result;
            JsonResult jsonMsg = Json("");
            return jsonMsg;
            //return View();
        }
    }
}
