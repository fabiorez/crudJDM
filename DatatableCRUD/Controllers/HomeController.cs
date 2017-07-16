using System.Linq;
using System.Web.Mvc;
using DatatableCRUD.Models;

namespace DatatableCRUD.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetUsuarios()
        {
            using (MyDatabaseEntities dc = new MyDatabaseEntities())
            {
                var usuarios = dc.Usuarios.OrderBy(u => u.UsuarioNome).ToList();
                return Json(new { data = usuarios }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult Save(int id)
        {
            using (MyDatabaseEntities dc = new MyDatabaseEntities())
            {
                var v = dc.Usuarios.Where(a => a.UsuarioId == id).FirstOrDefault();
                return View(v);
            }
        }

        [HttpPost]
        public ActionResult Save(Usuarios usu)
        {
            bool status = false;
            if (ModelState.IsValid)
            {
                using (MyDatabaseEntities dc = new MyDatabaseEntities())
                {
                    if (usu.UsuarioId > 0)
                    {
                        //Edit 
                        var v = dc.Usuarios.Where(a => a.UsuarioId == usu.UsuarioId).FirstOrDefault();
                        if (v != null)
                        {
                            v.UsuarioNome = usu.UsuarioNome;
                            v.UsuarioSobrenome = usu.UsuarioSobrenome;
                            v.UsuarioEmail = usu.UsuarioEmail;
                        }
                    }
                    else
                    {
                        //Save
                        dc.Usuarios.Add(usu);
                    }
                    dc.SaveChanges();
                    status = true;
                }
            }
            return new JsonResult { Data = new { status = status } };
        }


        [HttpGet]
        public ActionResult Delete(int id)
        {
            using (MyDatabaseEntities dc = new MyDatabaseEntities())
            {
                var v = dc.Usuarios.Where(a => a.UsuarioId == id).FirstOrDefault();
                if (v != null)
                {
                    return View(v);
                }
                else
                {
                    return HttpNotFound();
                }
            }
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteEmployee(int id)
        {
            bool status = false;
            using (MyDatabaseEntities dc = new MyDatabaseEntities())
            {
                var v = dc.Usuarios.Where(a => a.UsuarioId == id).FirstOrDefault();
                if (v != null)
                {
                    dc.Usuarios.Remove(v);
                    dc.SaveChanges();
                    status = true;
                }
            }
            return new JsonResult { Data = new { status = status } };
        }
    }
}
