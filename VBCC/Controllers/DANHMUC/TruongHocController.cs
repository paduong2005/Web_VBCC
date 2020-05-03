using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VBCC.Models;
namespace VBCC.Controllers.DANHMUC
{
    public class TruongHocController : BaseController
    {
        // GET: TruongHoc
        public ActionResult Show()
        {
            return View();
        }
        public ActionResult getTruong(int? page, string search = "")
        {
            int pageSize = 50;

            int pageNumber = (page ?? 1);

            //comment
            var data = db.DM_Truong.Where(p => p.TenTruong.Contains(search) && p.MaDonVi == MaDonVi).ToList();

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
        public ActionResult create(DM_Truong dm)
        {

            if (String.IsNullOrEmpty(dm.TenTruong))
                return Json(new ResultInfo() { error = 1, msg = "Chưa nhập tên trường" }, JsonRequestBehavior.AllowGet);

            if (String.IsNullOrEmpty(dm.DiaChi))
                return Json(new ResultInfo() { error = 1, msg = "Chưa nhập địa chỉ" }, JsonRequestBehavior.AllowGet);

            var check = db.DM_Truong.Where(p => p.MaDonVi == MaDonVi && p.TenTruong == dm.TenTruong).FirstOrDefault();

            if (check != null)
                return Json(new ResultInfo() { error = 1, msg = "Đã tồn tại trường học" }, JsonRequestBehavior.AllowGet);
           
            dm.MaDonVi = MaDonVi;
            db.DM_Truong.Add(dm);
            db.SaveChanges();

            return Json(new ResultInfo() { error = 0, msg = "", data = dm }, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public ActionResult edit(DM_Truong dm)
        {

            var check = db.DM_Truong.Where(p => p.ID == dm.ID).FirstOrDefault();

            if (check == null)
                return Json(new ResultInfo() { error = 1, msg = "Không tìm thấy môn học" }, JsonRequestBehavior.AllowGet);

            
            db.Entry(check).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

            return Json(new ResultInfo() { error = 0, msg = "", data = check }, JsonRequestBehavior.AllowGet);

        }
        //[HttpPost]
        //public ActionResult delete(DM_MonHocNew mh)
        //{
        //    if (String.IsNullOrEmpty(mh.MonHoc))
        //        return Json(new ResultInfo() { error = 1, msg = "Missing info" }, JsonRequestBehavior.AllowGet);

        //    var check = db.DM_MonHocNew.Where(p => p.ID == mh.ID).FirstOrDefault();

        //    if (check == null)
        //        return Json(new ResultInfo() { error = 1, msg = "Không tìm thấy thông tin" }, JsonRequestBehavior.AllowGet);

        //    var checklop = db.DM_LopHocMonHoc.Where(p => p.MaTruong == MaDonVi && p.IDMonHoc == mh.ID).FirstOrDefault();

        //    if (checklop != null)
        //        return Json(new ResultInfo() { error = 1, msg = "Không thể xóa môn học đã được đăng ký cho lớp" }, JsonRequestBehavior.AllowGet);


        //    db.Entry(check).State = System.Data.Entity.EntityState.Deleted;
        //    db.SaveChanges();

        //    return Json(new ResultInfo() { error = 0, msg = "", data = check }, JsonRequestBehavior.AllowGet);
        //}
    }
}