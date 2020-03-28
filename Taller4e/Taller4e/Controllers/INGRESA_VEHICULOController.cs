using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Taller4e.Models;

namespace Taller4e.Controllers
{
    public class INGRESA_VEHICULOController : Controller
    {
        private Taller4EEntities db = new Taller4EEntities();

        // GET: INGRESA_VEHICULO
        public ActionResult Index()
        {
            var iNGRESA_VEHICULO = db.INGRESA_VEHICULO.Include(i => i.AUTO);

            List<INGRESA_VEHICULO> ingresaVehiculo = db.INGRESA_VEHICULO.Include(i => i.AUTO).ToList();
            List<valorServicios> resultado = new List<valorServicios>();

            foreach (INGRESA_VEHICULO item in ingresaVehiculo)
            {
                valorServicios valoraux = new valorServicios();
                valoraux.AUTO = item.AUTO;
                valoraux.INGR_FECHA_INGRESO = item.INGR_FECHA_INGRESO;
                valoraux.INGR_ID = item.INGR_ID;
                valoraux.ID_AUTO = item.ID_AUTO;
                valoraux.INGR_RUT_CLIENTE = item.INGR_RUT_CLIENTE;
                valoraux.valorServ = item.SERVICIOS.Sum(x => x.SERVI_VALOR).Value;
                resultado.Add(valoraux);
            }

            ViewData["valoresServicios"] = resultado;
            return View(iNGRESA_VEHICULO.ToList());
        }

        // GET: INGRESA_VEHICULO/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            INGRESA_VEHICULO iNGRESA_VEHICULO = db.INGRESA_VEHICULO.Find(id);
            if (iNGRESA_VEHICULO == null)
            {
                return HttpNotFound();
            }
            return View(iNGRESA_VEHICULO);
        }

        // GET: INGRESA_VEHICULO/Create
        public ActionResult Create()
        {
            ViewData["servicios"] = db.SERVICIOS.ToList();
            ViewBag.ID_AUTO = new SelectList(db.AUTO, "ID_AUTO", "PATENTE");
            return View();
        }

        // POST: INGRESA_VEHICULO/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "INGR_ID,ID_AUTO,INGR_FECHA_INGRESO,INGR_RUT_CLIENTE")] INGRESA_VEHICULO iNGRESA_VEHICULO, string[] servicios)
        {
            if (ModelState.IsValid)
            {

                foreach (string value in servicios)
                {
                    SERVICIOS servi = new SERVICIOS();
                    int idaux = int.Parse(value);
                    servi = db.SERVICIOS.Where(x => x.SERVI_ID == idaux).First();
                    iNGRESA_VEHICULO.SERVICIOS.Add(servi);

                }

                db.INGRESA_VEHICULO.Add(iNGRESA_VEHICULO);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_AUTO = new SelectList(db.AUTO, "ID_AUTO", "PATENTE", iNGRESA_VEHICULO.ID_AUTO);
            return View(iNGRESA_VEHICULO);
        }

        // GET: INGRESA_VEHICULO/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            INGRESA_VEHICULO iNGRESA_VEHICULO = db.INGRESA_VEHICULO.Find(id);
            if (iNGRESA_VEHICULO == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_AUTO = new SelectList(db.AUTO, "ID_AUTO", "PATENTE", iNGRESA_VEHICULO.ID_AUTO);
            return View(iNGRESA_VEHICULO);
        }

        // POST: INGRESA_VEHICULO/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "INGR_ID,ID_AUTO,INGR_FECHA_INGRESO,INGR_RUT_CLIENTE")] INGRESA_VEHICULO iNGRESA_VEHICULO)
        {
            if (ModelState.IsValid)
            {
                db.Entry(iNGRESA_VEHICULO).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_AUTO = new SelectList(db.AUTO, "ID_AUTO", "PATENTE", iNGRESA_VEHICULO.ID_AUTO);
            return View(iNGRESA_VEHICULO);
        }

        // GET: INGRESA_VEHICULO/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            INGRESA_VEHICULO iNGRESA_VEHICULO = db.INGRESA_VEHICULO.Find(id);
            if (iNGRESA_VEHICULO == null)
            {
                return HttpNotFound();
            }
            return View(iNGRESA_VEHICULO);
        }

        // POST: INGRESA_VEHICULO/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            INGRESA_VEHICULO iNGRESA_VEHICULO = db.INGRESA_VEHICULO.Find(id);
            db.INGRESA_VEHICULO.Remove(iNGRESA_VEHICULO);
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
