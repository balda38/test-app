using Newtonsoft.Json;
using System.Data;
using System.Web;
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
            try
            {
                string query = "GetAllSmartphones";

                queryBuilder.SetCommandType("procedure");
                DataTable result = queryBuilder.Execute(query);
                string json = JsonConvert.SerializeObject(result);

                return Json(new { data = json, status = "done" }, JsonRequestBehavior.AllowGet);
            }
            catch (HttpException e)
            {
                return Json(new { data = e.Message, status = "error" }, JsonRequestBehavior.AllowGet);
            }
        }

        // POST: Smartphones/Create
        public JsonResult Create(string json)
        {
            try
            {
                string query = "AddNewSmartphone";
                queryBuilder.SetCommandType("procedure");
                DataTable result = queryBuilder.Execute(query, json);
                string outputJson = JsonConvert.SerializeObject(result);

                return Json(new { data = outputJson, status = "done" }, JsonRequestBehavior.AllowGet);
            }
            catch (HttpException e)
            {
                return Json(new { data = e.Message, status = "error" }, JsonRequestBehavior.AllowGet);
            }
        }


        // POST: Smartphones/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, string json)
        {
            try
            {
                string query = "UpdateSmartphoneById";
                queryBuilder.SetCommandType("procedure");
                DataTable result = queryBuilder.Execute(query, json);
                string outputJson = JsonConvert.SerializeObject(result);

                return Json(new { data = outputJson, status = "done" }, JsonRequestBehavior.AllowGet);
            }
            catch (HttpException e)
            {
                return Json(new { data = e.Message, status = "error" }, JsonRequestBehavior.AllowGet);
            }
        }

        // POST: Smartphones/Delete/5
        [HttpPost]
        public JsonResult Delete(int id, string json)
        {
            try
            {
                string query = "DeleteSmartphoneById";
                queryBuilder.SetCommandType("procedure");
                DataTable result = queryBuilder.Execute(query, json);
                string outputJson = JsonConvert.SerializeObject(result);

                return Json(new { data = outputJson, status = "done" }, JsonRequestBehavior.AllowGet);
            }
            catch (HttpException e)
            {
                return Json(new { data = e.Message, status = "error" }, JsonRequestBehavior.AllowGet);
            }
        }

        // POST: Smartphones/MultipleDelete
        [HttpPost]
        public JsonResult MultipleDelete(string json)
        {
            try
            {
                string query = "MultipleDeleteSmartphones";
                queryBuilder.SetCommandType("procedure");
                DataTable result = queryBuilder.Execute(query, json);
                string outputJson = JsonConvert.SerializeObject(result);

                return Json(new { data = outputJson, status = "done" }, JsonRequestBehavior.AllowGet);
            }
            catch (HttpException e)
            {
                return Json(new { data = e.Message, status = "error" }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
