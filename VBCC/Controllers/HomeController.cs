using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VBCC.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            ViewBag.MaTruong = MaDonVi;
            ViewBag.TenTruong = TenDonVi;
            return View();
        }
        public ActionResult ShowInfoTruong()
        {
            return Content(TenDonVi);
        }

    }
}