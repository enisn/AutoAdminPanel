using AutoAdmin.Extensions;
using AutoAdmin.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AutoAdmin.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index(string table)
        {
            //var list = new Models.NORTHWNDEntities().Categories.ToList();

            var list = QueryHelper.GetMultiple(table);
            return View(list);
        }

        // GET: Admin/Details/5
        public ActionResult Details(string table, object id)
        {
            var model = QueryHelper.Get(table, id);
            return View(model);
        }

        // GET: Admin/Create
        public ActionResult Create(string table)
        {
            var model = QueryHelper.GetInstance(table);

            foreach (var name in QueryHelper.GetRelationsNames(table))
            {
                ViewData.Add(name, QueryHelper.GetMultiple(name));
            }

            return View(model);
        }

        // POST: Admin/Create
        [HttpPost]
        public ActionResult Create(string table, FormCollection collection)
        {
            try
            {
                var model = QueryHelper.GetInstance(table);

                model.CopyFrom(collection);

                QueryHelper.Add(table,model);
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
                return View();
            }
        }

        // GET: Admin/Edit/5
        public ActionResult Edit(string table, object id)
        {
            var model = QueryHelper.Get(table, id);

            foreach (var name in QueryHelper.GetRelationsNames(table))
            {
                ViewData.Add(name, QueryHelper.GetMultiple(name));
            }

            return View(model);
        }

        // POST: Admin/Edit/5
        [HttpPost]
        public ActionResult Edit(string table, object id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                var edited = QueryHelper.Get(table, id);

                edited.CopyFrom(collection);

                QueryHelper.Update(table, edited, id);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/Delete/5
        public ActionResult Delete(string table, object id)
        {
            var model = QueryHelper.Get(table, id);
            return View(model);
        }

        // POST: Admin/Delete/5
        [HttpPost]
        public ActionResult Delete(string table, object id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                QueryHelper.Delete(table, id);

                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
                return View();
            }
        }
    }
}
