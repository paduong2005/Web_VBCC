using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VBCC.Models;
namespace VBCC.Controllers.DANHMUC
{
    public class NamHocController : BaseController
    {
        // GET: NamHoc
        public ActionResult Show()
        {
            return View();
        }
        [HttpGet]
        public ActionResult getNamHoc(int? page, string search = "")
        {
            int pageSize = 50;

            int pageNumber = (page ?? 1);


            var data = db.DM_NamHoc.Where(p => p.NamHoc.Contains(search) && p.MaDonVi == MaDonVi).ToList();

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
        public ActionResult create(DM_NamHoc nh)
        {
            if (String.IsNullOrEmpty(nh.NamHoc))
                return Json(new ResultInfo() { error = 1, msg = "Chưa nhập thông tin năm học" }, JsonRequestBehavior.AllowGet);
            // 20/12/2018
           
            db.DM_NamHoc.Add(nh);
            db.SaveChanges();

            return Json(new ResultInfo() { error = 0, msg = "", data = nh }, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public ActionResult edit(DM_NamHoc nh)
        {
            if (String.IsNullOrEmpty(nh.NamHoc))
                return Json(new ResultInfo() { error = 1, msg = "Chưa nhập năm học" }, JsonRequestBehavior.AllowGet);
            
            var check = db.DM_NamHoc.Where(p => p.MaDonVi == MaDonVi && p.NamHoc == nh.NamHoc).FirstOrDefault();
           
            check.NamHoc = nh.NamHoc;

            db.Entry(check).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

            return Json(new ResultInfo() { error = 0, msg = "", data = check }, JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public ActionResult delete(string namhoc)
        {
            if (String.IsNullOrEmpty(namhoc))
                return Json(new ResultInfo() { error = 1, msg = "Missing info" }, JsonRequestBehavior.AllowGet);

            var check = db.DM_NamHoc.Where(p => p.MaDonVi == MaDonVi && p.NamHoc == namhoc).FirstOrDefault();

            if (check == null)
                return Json(new ResultInfo() { error = 1, msg = "Không tìm thấy thông tin" }, JsonRequestBehavior.AllowGet);
            
            db.Entry(check).State = System.Data.Entity.EntityState.Deleted;
            db.SaveChanges();

            return Json(new ResultInfo() { error = 0, msg = "", data = check }, JsonRequestBehavior.AllowGet);
        }
    }
}