using ps_aspnetmvc.Data;
using ps_aspnetmvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ps_aspnetmvc.Controllers
{
    public class DocumentsController : Controller
    {
        CourseStore _store;

        public DocumentsController()
        {
            _store = new CourseStore();
        }
                 

        // GET: Documents
        public ActionResult Index()
        {
            var model = _store.GetAllCourses();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Insert()
        {
            var data = new SampleData().GetCourses();
            await _store.InsertCourses(data);

            return RedirectToAction(nameof(Index));
        }
    }
}