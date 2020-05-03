using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VBCC.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.SqlClient;
namespace VBCC.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {
        // GET: Base
        protected VBCCEntities db = new VBCCEntities();
        protected RoleManager<IdentityRole> RoleManager { get; private set; }

        protected UserManager<ApplicationUser> UserManager { get; private set; }

        private ApplicationDbContext sdb = new ApplicationDbContext();

        public string MaDonVi = "SUP";
        public string TenDonVi = "SUP Admin";
        public string UType;
        public bool? IsPhong = false;


        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            //MaTruong = "LQDON";
            base.Initialize(requestContext);

            if (requestContext.HttpContext.User.Identity.IsAuthenticated)
            {
                var checkUser = db.AspNetUsers.Where(p => p.UserName == requestContext.HttpContext.User.Identity.Name).FirstOrDefault();
                UType = checkUser.UType;
                if (checkUser != null && UType != "SUP")
                {
                    MaDonVi = checkUser.MaDonVi;
                    var checkloai = db.DM_DonVi.Where(x => x.MaDonVi == MaDonVi).FirstOrDefault();
                    TenDonVi = checkloai.TenDonVi;
                    IsPhong = checkloai.IsPhong;
                }
            }
        }
        public BaseController()
        {
            RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(sdb));
            UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(sdb));
        }
        [HttpGet]
        public ActionResult GetFileDinhKem(string idvb, string madonvi)
        {
            var data = db.VB_TT_File.Where(p => p.MaDonVi == madonvi && p.IDVB == idvb).Select(p => new
            {
                name = p.TenFile,
                extension = p.Extension,
                code = p.ID,
                idvb = p.IDVB,
                madonvi = p.MaDonVi,
                isAdd = true
            }).ToList();

            return Json(data, JsonRequestBehavior.AllowGet);
        }


        // file
        [HttpPost]
        public ActionResult DeleteFile(string code, string idvb, string madonvi)
        {
            var find = db.VB_TT_File.Where(p => p.MaDonVi == madonvi && p.ID == code && p.IDVB == idvb).FirstOrDefault();

            if (find != null)
            {

                var fileSave = Server.MapPath(find.DuongDan);

                if (System.IO.File.Exists(fileSave))
                {
                    System.IO.File.Delete(fileSave);
                }

                db.VB_TT_File.Remove(find);
                db.SaveChanges();

                return Json(new { error = 0, msg = "File không tìm thấy" }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { error = 1, msg = "File không tìm thấy" }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult DownLoadFileDinhKem(string code, string idvb, string madonvi)
        {
            var find = db.VB_TT_File.Where(p => p.MaDonVi == madonvi && p.ID == code && p.IDVB == idvb).FirstOrDefault();

            if (find != null)
            {

                var fileSave = Server.MapPath(find.DuongDan);

                if (System.IO.File.Exists(fileSave))
                {
                    byte[] fileBytes = System.IO.File.ReadAllBytes(fileSave);
                    string fileName = find.TenFile;
                    return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
                }


                return Json(new { error = 0, msg = "File không tìm thấy" }, JsonRequestBehavior.AllowGet);
            }

            return Content("Không tải được");
        }



    }
}