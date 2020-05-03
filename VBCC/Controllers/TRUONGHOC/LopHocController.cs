using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VBCC.Models;
namespace VBCC.Controllers.TRUONGHOC
{
    public class LopHocController : BaseController
    {
        // GET: LopHoc
        public ActionResult Show()
        {
            return View();
        }
        [HttpGet]
        public ActionResult getDM(int? page, string search = "")
        {
            int pageSize = 50;
            int pageNumber = (page ?? 1);
            var data = db.DM_LopHoc.Where(p => p.MaTruong == MaDonVi && p.Lop.Contains(search)).ToList();

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
        public ActionResult create(DM_LopHoc dm)
        {

            var check = db.DM_LopHoc.Where(p => p.MaTruong == MaDonVi && p.Lop == dm.Lop).FirstOrDefault();

            if (check != null)
                return Json(new ResultInfo() { error = 1, msg = "Đã tồn tại thông tin " + dm.Lop }, JsonRequestBehavior.AllowGet);
            dm.Lop = dm.Lop.ToUpper();
            dm.MaTruong = MaDonVi;
            db.DM_LopHoc.Add(dm);
            db.SaveChanges();

            return Json(new ResultInfo() { error = 0, msg = "", data = dm }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult edit(DM_LopHoc dm)
        {
            var check = db.DM_LopHoc.Where(p => p.MaTruong == MaDonVi && p.ID == dm.ID).FirstOrDefault();
            if (check == null)
                return Json(new ResultInfo() { error = 1, msg = "Không tìm thấy thông tin" }, JsonRequestBehavior.AllowGet);
    
            check.Lop = dm.Lop.ToUpper();           
            db.Entry(check).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

            return Json(new ResultInfo() { error = 0, msg = "", data = dm }, JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public ActionResult delete(int ma)
        {
            if (String.IsNullOrEmpty(ma.ToString()))
                return Json(new ResultInfo() { error = 1, msg = "Không tìm thấy thông tin" }, JsonRequestBehavior.AllowGet);

            var check = db.DM_LopHoc.Where(p => p.MaTruong == MaDonVi && p.ID == ma).FirstOrDefault();           

            db.Entry(check).State = System.Data.Entity.EntityState.Deleted;
            db.SaveChanges();

            return Json(new ResultInfo() { error = 0, msg = "", data = check }, JsonRequestBehavior.AllowGet);
        }       
    }
}