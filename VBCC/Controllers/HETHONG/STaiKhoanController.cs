using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VBCC.Models;
namespace VBCC.Controllers.HETHONG
{
    public class STaiKhoanController : BaseController
    {
        public ActionResult Show()
        {
            return View();
        }
        [HttpPost]
        public ActionResult GetAccounts(string MaDonVi)
        {
            var data = db.AspNetUsers.Where(p => p.MaDonVi == MaDonVi).Select(p => new
            {
                UserName = p.UserName,
                IsActive = p.IsActive,
                FullName = p.HoTen

            }).ToList();

            return Json(new ResultInfo()
            {
                error = 0,
                data = data
            }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult GetFrist(int matinh)
        {
            var data = db.DM_DonVi.Where(p => p.MaTinh == matinh).Select(p => new IdentityCommon()
            {
                code = p.MaDonVi,
                name = p.TenDonVi
            }).ToList();

            return Json(new { schools = data }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult ActiveAccounts(string UserName, bool IsActive)
        {
            var find = db.AspNetUsers.Where(p => p.UserName == UserName).FirstOrDefault();
            if (find != null)
            {
                find.IsActive = IsActive;
                db.Entry(find).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }

            return Json(new ResultInfo()
            {
                error = 0
            }, JsonRequestBehavior.AllowGet);
        }
    }
}