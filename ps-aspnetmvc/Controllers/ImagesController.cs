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
    public class ImagesController : Controller
    {
        ImageStore _store = new ImageStore();

        // GET: Images
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<ActionResult> Upload(HttpPostedFileBase image)
        {
            if (image != null)
            {
                var imageId = await _store.SaveImage(image.InputStream);
                return RedirectToAction(nameof(Show), new { id = imageId });
            }

            return View();
        }

        public ActionResult Show(string id)
        {
            var model = new ShowModel { Uri = _store.UriFor(id) };
            return View(model);
        }
    }
}