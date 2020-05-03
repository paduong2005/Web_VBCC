using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VBCC.Models;
namespace VBCC.Controllers.PHONG
{
    public class PXetTotNghiepController : BaseController
    {
        // GET: PXetTotNghiep
        public ActionResult Show()
        {
            ViewBag.AllTruong = db.DM_DonVi.Where(p => p.IDCapTren == MaDonVi).ToList();
            return View();
        }
        [HttpGet]
        public ActionResult gethocsinh(int? page, string matruong,string search = "")
        {
            int pageSize = 50;
            int pageNumber = (page ?? 1);
            var data = db.DM_HocSinh.Where(p => p.MaTruong == matruong && p.Ten.Contains(search)).ToList();
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
        [HttpPost]
        public ActionResult xetduyet(List<int> hocsinh)
        {
            foreach (var item in hocsinh)
            {
                if (String.IsNullOrEmpty(item.ToString()))
                    return Json(new ResultInfo() { error = 1, msg = "Missing info" }, JsonRequestBehavior.AllowGet);

                var check = db.DM_HocSinh.Where(p => p.MaTruong == MaDonVi && p.ID == item).FirstOrDefault();

                if (check == null)
                    return Json(new ResultInfo() { error = 1, msg = "Không tìm thấy thông tin" }, JsonRequestBehavior.AllowGet);

                check.TrangThai = 2;

                db.Entry(check).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }

            return Json(new ResultInfo() { error = 0, msg = "", data = hocsinh }, JsonRequestBehavior.AllowGet);

        }
    }
}