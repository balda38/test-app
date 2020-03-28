using Newtonsoft.Json;
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
            return View();
        }

        // GET: Smartphones
        [HttpGet]
        public JsonResult GetAll()
        {
            string query = "GetAllSmartphones";

            queryBuilder.SetCommandType("procedure");
            DataTable result = queryBuilder.Execute(query);
            string json = JsonConvert.SerializeObject(result);

            return Json(json, JsonRequestBehavior.AllowGet);
        }

        // POST: Smartphones/Create
        public JsonResult Create(string json)
        {
            string query = "AddNewSmartphone";
            queryBuilder.SetCommandType("procedure");
            DataTable result = queryBuilder.Execute(query, json);

            ViewBag.smartphones = result;
            JsonResult jsonMsg = Json("");
            return jsonMsg;
        }


        // POST: Smartphones/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, string json)
        {
            string query = "UpdateSmartphoneById";
            queryBuilder.SetCommandType("procedure");
            DataTable result = queryBuilder.Execute(query, json);

            ViewBag.smartphones = result;
            JsonResult jsonMsg = Json("");
            return jsonMsg;
        }

        // POST: Smartphones/Delete/5
        [HttpPost]
        public JsonResult Delete(int id, string json)
        {
            string query = "DeleteSmartphoneById";
            queryBuilder.SetCommandType("procedure");
            DataTable result = queryBuilder.Execute(query, json);

            ViewBag.smartphones = result;
            JsonResult jsonMsg = Json("");
            return jsonMsg;
        }
    }
}
