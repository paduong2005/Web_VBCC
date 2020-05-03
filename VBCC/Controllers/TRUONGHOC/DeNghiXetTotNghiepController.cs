using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VBCC.Models;
namespace VBCC.Controllers.TRUONGHOC
{
    public class DeNghiXetTotNghiepController : BaseController
    {
        // GET: DeNghiXetTotNghiep
        public ActionResult Show()
        {
            return View();
        }
        [HttpGet]
        public ActionResult gethocsinh(int? page, string search = "")
        {
            int pageSize = 50;
            int pageNumber = (page ?? 1);
            var data = db.DM_HocSinh.Where(p => p.MaTruong == MaDonVi && p.Ten.Contains(search) && p.TrangThai == 1).ToList();
            ResultInfo result = new ResultWithPaging()
            {
                error = 0,
                msg = "",
                page = pageNumber,
                pageSize = pageSize,
                toltalSize = data.Count(),
                data = data.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList()
            };
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}