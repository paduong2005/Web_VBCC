using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VBCC.Models;
namespace VBCC.Controllers.HETHONG
{
    public class DonViController : BaseController
    {
        // GET: DonVi
        public ActionResult Show()
        {
            ViewBag.Info = db.DM_DonVi.Where(p => p.MaDonVi == MaDonVi).FirstOrDefault();
            return View();
        }
        [HttpPost]
        public ActionResult edit(DM_DonVi dv)
        {
            if (String.IsNullOrEmpty(dv.TenDonVi))
                return Json(new ResultInfo() { error = 1, msg = "Chưa nhập thông tin đơn vị" }, JsonRequestBehavior.AllowGet);

            var check = db.DM_DonVi.Where(p => p.MaDonVi == MaDonVi).FirstOrDefault();

            if (check == null)
            {
                dv.MaDonVi = MaDonVi;
                db.DM_DonVi.Add(dv);
                db.SaveChanges();
            }
            else
            {
                check.TenDonVi = dv.TenDonVi;
                check.DiaChi = dv.DiaChi;
                check.SoDienThoai = dv.SoDienThoai;
                check.Fax = dv.Fax;
                check.Email = dv.Email;
                check.Website = dv.Website;
                check.DonViCapTren = dv.DonViCapTren;
                check.LapBang = dv.LapBang;
                check.KeToanTruong = dv.KeToanTruong;
                check.HieuTruong = dv.HieuTruong;
                check.MaQHNS = dv.MaQHNS;

                db.Entry(check).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
            return Json(new ResultInfo() { error = 0, msg = "", data = check }, JsonRequestBehavior.AllowGet);

        }
    }
}