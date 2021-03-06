﻿using AutoAdmin.Mvc.Extensions;
using AutoAdmin.Mvc.Helpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace AutoAdmin.Mvc.Controllers
{
    public class AdminController : Controller
    {
        public virtual ActionResult Index(string table)
        {
            var list = QueryHelper.GetMultiple(table, Request.QueryString);

            return View(list);
        }
        // GET: Admin/Details/5
        public virtual ActionResult Details(string table, object id)
        {
            var model = QueryHelper.Get(table, id);
            return View(model);
        }

        // GET: Admin/Create
        public virtual ActionResult Create(string table)
        {
            var model = QueryHelper.GetInstance(table);

            foreach (var property in QueryHelper.GetRelationProperties(table))
            {
                ViewData.Add(property.Name, QueryHelper.GetMultiple(property.PropertyType).ToSelectList());
            }

            return View(model);
        }

        // POST: Admin/Create
        [HttpPost]
        public virtual ActionResult Create(string table, FormCollection collection)
        {
            try
            {
#if DEBUG
                Debug.WriteLine("________________\n________________\n________________\n");
                foreach (string key in collection)
                {
                    Debug.WriteLine($"[{key}] = {collection[key]} ");
                }
                Debug.WriteLine("________________\n________________\n________________\n");
#endif
                var model = QueryHelper.GetInstance(table);

                model.CopyFrom(collection,table);

                QueryHelper.Add(table, model);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
                return View();
            }
        }

        // GET: Admin/Edit/5
        public virtual ActionResult Edit(string table, object id)
        {
            var model = QueryHelper.Get(table, id);

            foreach (var property in QueryHelper.GetRelationProperties(table))
                ViewData.Add(property.Name, QueryHelper.GetMultiple(property.PropertyType).ToSelectList(property.GetValue(model)?.GetPrimaryKeyValue()));

            return View(model);
        }

        // POST: Admin/Edit/5
        [HttpPost]
        public virtual ActionResult Edit(string table, dynamic id, FormCollection collection)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(collection);

                object edited = QueryHelper.Get(table, id);
                // TODO: Add update logic here

                edited.CopyFrom(collection,table);

                QueryHelper.Update(table, edited, id);


                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {

                var edited = QueryHelper.GetInstance(table);
                //edited.TryCopyFrom(collection);

                foreach (var property in QueryHelper.GetRelationProperties(table))
                    ViewData.Add(property.Name, QueryHelper.GetMultiple(property.PropertyType));

                return View(edited);
            }
        }

        // GET: Admin/Delete/5
        public virtual ActionResult Delete(string table, object id)
        {
            var model = QueryHelper.Get(table, id);
            return View(model);
        }

        // POST: Admin/Delete/5
        [HttpPost]
        public virtual ActionResult Delete(string table, object id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                QueryHelper.Delete(table, id);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
                return View();
            }
        }
    }
}
