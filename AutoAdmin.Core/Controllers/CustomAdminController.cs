using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using AutoAdmin.Mvc.Core.Extensions;
using AutoAdmin.Mvc.Core.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AutoAdmin.Core.Controllers
{
    public class CustomAdminController : Controller
    {
        // GET: CustomAdmin
        public ActionResult Index(string table)
        {
            var list = QueryHelper.GetMultiple(table, HttpUtility.ParseQueryString(Request.QueryString.ToString()));
            return View(list);
        }

        // GET: CustomAdmin/Details/5
        public ActionResult Details(string table, object id)
        {
            var model = QueryHelper.Get(table, id);
            return View();
        }

        // GET: CustomAdmin/Create
        public ActionResult Create(string table)
        {
            var instance = QueryHelper.GetInstance(table);

            foreach (var property in QueryHelper.GetRelationProperties(table))
                ViewData[property.Name] = QueryHelper.GetMultiple(property.PropertyType);

            return View(instance);
        }

        // POST: CustomAdmin/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(string table, IFormCollection collection)
        {
            try
            {
#if DEBUG
                Debug.WriteLine("________________\n________________\n________________\n");
                foreach (var item in collection)
                {
                    Debug.WriteLine($"[{item.Key}] = {item.Value} ");
                }
                Debug.WriteLine("________________\n________________\n________________\n");
#endif

                var instance = QueryHelper.GetInstance(table);

                instance.CopyFrom(collection, table);

                QueryHelper.Add(table, instance);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CustomAdmin/Edit/5
        public ActionResult Edit(string table, int id)
        {
            var instance = QueryHelper.GetInstance(table);

            foreach (var property in QueryHelper.GetRelationProperties(table))
                ViewData.Add(property.Name, QueryHelper.GetMultiple(property.PropertyType));

            return View(instance);
        }

        // POST: CustomAdmin/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string table, int id, IFormCollection collection)
        {
            try
            {
                var edited = QueryHelper.Get(table, id);

                edited.CopyFrom(collection, table);

                QueryHelper.Update(table, edited, id);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CustomAdmin/Delete/5
        public ActionResult Delete(string table, int id)
        {
            var entity = QueryHelper.Get(table, id);
            return View(entity);
        }

        // POST: CustomAdmin/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string table, int id, IFormCollection collection)
        {
            try
            {
                QueryHelper.Delete(table, id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}