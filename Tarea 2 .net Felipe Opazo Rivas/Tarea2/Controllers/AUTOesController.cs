﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Tarea2.Models;

namespace Tarea2.Controllers
{
    public class AUTOesController : Controller
    {
      
        private testEntities db = new testEntities();

        // GET: AUTOes
        public ActionResult Index()
        {
            var aUTO = db.AUTO.Include(a => a.MODELO);
            return View(aUTO.ToList());
        }

        // GET: AUTOes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AUTO aUTO = db.AUTO.Find(id);
            if (aUTO == null)
            {
                return HttpNotFound();
            }
            return View(aUTO);
        }

        // GET: AUTOes/Create
        public ActionResult Create()
        {
            
            if (TempData["mensajeError"] != null)
            {
                ViewBag.mensaje = TempData["mensajeError"].ToString();
                ViewBag.modelo = TempData["modelo"].ToString();
                ViewBag.patente = TempData["patente"].ToString();
                ViewBag.ano = TempData["año"];
                ViewBag.color = TempData["color"].ToString();
                ViewBag.ID_MODELO = new SelectList(db.MODELO, "ID_MODELO", "DESCRIPCION_MODELO", TempData["modelo"].ToString());
            } else
            {
                ViewBag.ID_MODELO = new SelectList(db.MODELO, "ID_MODELO", "DESCRIPCION_MODELO");
            }
 
            return View();
        }

        // POST: AUTOes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_AUTO,ID_MODELO,PATENTE,ANO,COLOR,OBSERVACION")] AUTO aUTO)
        {
            String patenteAuto = aUTO.PATENTE;
            int year = DateTime.Now.Year;
            year += 2;

            // Validar que solo exista una patente por auto 
            if (db.AUTO.Any(o => o.PATENTE == patenteAuto))
            {
                TempData["mensajeError"] = "La patente ingresada ya existe, intente con otra";
                TempData["modelo"] = aUTO.ID_MODELO;
                TempData["patente"] = aUTO.PATENTE;
                TempData["año"] = aUTO.ANO;
                TempData["color"] = aUTO.COLOR;
                return RedirectToAction("Create");
            }

            // Validar que el año ingresado no sea superior al actual + 2
            if (aUTO.ANO > year)
            {
                TempData["mensajeError"] = "No puede ingresar autos con año superior a "+year;
                TempData["modelo"] = aUTO.ID_MODELO;
                TempData["patente"] = aUTO.PATENTE;
                TempData["año"] = aUTO.ANO;
                TempData["color"] = aUTO.COLOR;
                return RedirectToAction("Create");
            }

            aUTO.OBSERVACION = "SIN OBSERVACIONES";

            if(aUTO.ANO < 1990)
            {
                aUTO.OBSERVACION = "ANTIGUO";
            }

            if (ModelState.IsValid)
            {
                db.AUTO.Add(aUTO);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_MODELO = new SelectList(db.MODELO, "ID_MODELO", "DESCRIPCION_MODELO", aUTO.ID_MODELO);
            return View(aUTO);
        }

        // GET: AUTOes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AUTO aUTO = db.AUTO.Find(id);
            if (aUTO == null)
            {
                return HttpNotFound();
            }
            if (TempData["mensajeError"] != null)
            {
                ViewBag.mensaje = TempData["mensajeError"].ToString();
                ViewBag.modelo = TempData["modelo"].ToString();
                ViewBag.patente = TempData["patente"].ToString();
                ViewBag.ano = TempData["año"];
                ViewBag.color = TempData["color"].ToString();
                ViewBag.ID_MODELO = new SelectList(db.MODELO, "ID_MODELO", "DESCRIPCION_MODELO", TempData["modelo"].ToString());
            }
            else
            {
                ViewBag.ID_MODELO = new SelectList(db.MODELO, "ID_MODELO", "DESCRIPCION_MODELO", aUTO.ID_MODELO);
            }

            return View(aUTO);
        }

        // POST: AUTOes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_AUTO,ID_MODELO,PATENTE,ANO,COLOR,OBSERVACION")] AUTO aUTO)
        {
            String patenteAuto = aUTO.PATENTE;
            int year = DateTime.Now.Year;
            year += 2;


            // Validar que solo exista una patente por auto 
         
            if (!db.AUTO.Any(o => o.ID_AUTO == aUTO.ID_AUTO && o.PATENTE == patenteAuto))
            {
                if (db.AUTO.Any(o => o.PATENTE == patenteAuto))
                {
                    TempData["mensajeError"] = "La patente ingresada ya existe, intente con otra";
                    TempData["modelo"] = aUTO.ID_MODELO;
                    TempData["patente"] = aUTO.PATENTE;
                    TempData["año"] = aUTO.ANO;
                    TempData["color"] = aUTO.COLOR;
                    return RedirectToAction("EDIT", aUTO.ID_AUTO);
                }
            } else

            // Validar que el año ingresado no sea superior al actual + 2
            if (aUTO.ANO > year)
            {
                TempData["mensajeError"] = "No puede ingresar autos con año superior a " + year;
                TempData["modelo"] = aUTO.ID_MODELO;
                TempData["patente"] = aUTO.PATENTE;
                TempData["año"] = aUTO.ANO;
                TempData["color"] = aUTO.COLOR;
                return RedirectToAction("EDIT", aUTO.ID_AUTO);
            }

            aUTO.OBSERVACION = "SIN OBSERVACIONES";

            if (aUTO.ANO < 1990)
            {
                aUTO.OBSERVACION = "ANTIGUO";
            }


            if (ModelState.IsValid)
            {
                db.Entry(aUTO).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_MODELO = new SelectList(db.MODELO, "ID_MODELO", "DESCRIPCION_MODELO", aUTO.ID_MODELO);
            return View(aUTO);
        }

        // GET: AUTOes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AUTO aUTO = db.AUTO.Find(id);
            if (aUTO == null)
            {
                return HttpNotFound();
            }
            return View(aUTO);
        }

        // POST: AUTOes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AUTO aUTO = db.AUTO.Find(id);
            db.AUTO.Remove(aUTO);
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
