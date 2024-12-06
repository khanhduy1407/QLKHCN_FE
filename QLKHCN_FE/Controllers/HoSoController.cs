using System.Web.Mvc;

namespace QLKHCN_FE.Controllers
{
    public class HoSoController : BaseController
    {
        // GET
        public ActionResult Index(string userId)
        {
            // Kiểm tra xác thực
            if (!IsAuthenticated)
            {
                Session["ReturnUrl"] = Request.Url.AbsoluteUri;
                return RedirectToAction("Login", "NguoiDung");
            }

            if (userId != null)
            {
                ViewBag.userId = userId;
                return View();
            }
            else
            {
                return RedirectToAction("DeTai", "NguoiDung");
            }
        }
    }
}
