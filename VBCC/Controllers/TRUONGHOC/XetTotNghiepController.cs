using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VBCC.Models;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System.IO;
using OfficeOpenXml;
using Microsoft.AspNet.Identity.Owin;
using VBCC.Filters;
namespace VBCC.Controllers.TRUONGHOC
{
    public class XetTotNghiepController : BaseController
    {
        // GET: XetTotNghiep
        public ActionResult Show()
        {
            ViewBag.NamHoc = db.DM_NamHoc.ToList();
            ViewBag.LopHoc = db.DM_LopHoc.Where(p => p.MaTruong == MaDonVi).ToList();
            ViewBag.Ngay = db.HT_Ngay.ToList();
            ViewBag.Thang = db.HT_Thang.ToList();
            ViewBag.Nam = db.HT_Nam.ToList();
            return View();
        }
        [HttpGet]
        public ActionResult gethocsinh(int? page, string search = "")
        {
            int pageSize = 50;
            int pageNumber = (page ?? 1);
            var data = db.DM_HocSinh.Where(p => p.MaTruong == MaDonVi && p.Ten.Contains(search)).ToList();
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
        public ActionResult create(DM_HocSinh hs, string nam)
        {

            if (String.IsNullOrEmpty(hs.HoChuLot))
                return Json(new ResultInfo() { error = 1, msg = "Chưa nhập mã họ và chữ lót" }, JsonRequestBehavior.AllowGet);

            if (String.IsNullOrEmpty(hs.Ten))
                return Json(new ResultInfo() { error = 1, msg = "Chưa nhập tên học sinh" }, JsonRequestBehavior.AllowGet);

            if (String.IsNullOrEmpty(hs.NamHoc))
                return Json(new ResultInfo() { error = 1, msg = "Chưa chọn năm học" }, JsonRequestBehavior.AllowGet);

            if (String.IsNullOrEmpty(hs.Lop))
                return Json(new ResultInfo() { error = 1, msg = "Chưa nhập lớp học" }, JsonRequestBehavior.AllowGet);
            if (nam.Length < 8)
                return Json(new ResultInfo() { error = 1, msg = "Ngày tháng năm sinh không đúng" }, JsonRequestBehavior.AllowGet);


            //var check = db.DM_HocSinh.Where(p => p.MaTruong == MaDonVi).FirstOrDefault();

            //if (check != null)
            //    return Json(new ResultInfo() { error = 1, msg = "Đã tồn tại học sinh mã " + hs.MaHocSinh }, JsonRequestBehavior.AllowGet);

            hs.NamSinh = DateTime.Parse(nam);
            hs.MaTruong = MaDonVi;
            hs.NguoiTao = "";
            hs.NgayTao = DateTime.Now;
            hs.TrangThai = 0;
            if (hs.Ngay.Length == 1)
            {
                hs.Ngay = "0" + hs.Ngay;
            }
            if (hs.Thang.Length == 1)
            {
                hs.Thang = "0" + hs.Thang;
            }
            db.DM_HocSinh.Add(hs);
            db.SaveChanges();
            return Json(new ResultInfo() { error = 0, msg = "", data = hs }, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public ActionResult edit(DM_HocSinh hs, string nam)
        {
            if (String.IsNullOrEmpty(hs.HoChuLot))
                return Json(new ResultInfo() { error = 1, msg = "Chưa nhập mã họ và chữ lót" }, JsonRequestBehavior.AllowGet);

            if (String.IsNullOrEmpty(hs.Ten))
                return Json(new ResultInfo() { error = 1, msg = "Chưa nhập tên học sinh" }, JsonRequestBehavior.AllowGet);

            if (String.IsNullOrEmpty(hs.NamHoc))
                return Json(new ResultInfo() { error = 1, msg = "Chưa chọn năm học" }, JsonRequestBehavior.AllowGet);

            if (String.IsNullOrEmpty(hs.Lop))
                return Json(new ResultInfo() { error = 1, msg = "Chưa nhập lớp học" }, JsonRequestBehavior.AllowGet);

            if (nam.Length < 8)
                return Json(new ResultInfo() { error = 1, msg = "Ngày tháng năm sinh không đúng" }, JsonRequestBehavior.AllowGet);

            var check = db.DM_HocSinh.Where(p => p.MaTruong == MaDonVi && p.ID == hs.ID).FirstOrDefault();

            if (check == null)
                return Json(new ResultInfo() { error = 1, msg = "Không tìm thấy thông tin" }, JsonRequestBehavior.AllowGet);

            check.MaTruong = hs.MaTruong;
            check.NamHoc = hs.NamHoc;
            check.HoChuLot = hs.HoChuLot;
            check.Ten = hs.Ten;
            check.GioiTinh = hs.GioiTinh;
            check.NamSinh = DateTime.Parse(nam);
            check.DanToc = hs.DanToc;
            check.NoiSinh = hs.NoiSinh;
            check.DienUT = hs.DienUT;
            check.DienKK = hs.DienKK;
            check.Toan = hs.Toan;
            check.Ly = hs.Ly;
            check.Hoa = hs.Hoa;
            check.Sinh = hs.Sinh;
            check.NguVan = hs.NguVan;
            check.Su = hs.Su;
            check.Dia = hs.Dia;
            check.TiengAnh = hs.TiengAnh;
            check.GDCD = hs.GDCD;
            check.CongNghe = hs.CongNghe;
            check.TiengPhap = hs.TiengPhap;
            check.TheDuc = hs.TheDuc;
            check.Nhac = hs.Nhac;
            check.MyThuat = hs.MyThuat;
            check.TBCN = hs.TBCN;
            check.XepLoaiHL = hs.XepLoaiHL;
            check.XepLoaiHK = hs.XepLoaiHK;
            check.XepLoaiTN = hs.XepLoaiTN;
            check.Lop = hs.Lop;
            check.BuoiNghi = hs.BuoiNghi;
            check.GhiChu = hs.GhiChu;
            if (hs.Ngay.Length == 1)
            {
                check.Ngay = "0" + hs.Ngay;
            }
            if (hs.Thang.Length == 1)
            {
                check.Thang = "0" + hs.Thang;
            }
            check.Nam = hs.Nam;

            db.Entry(check).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

            return Json(new ResultInfo() { error = 0, msg = "", data = check }, JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public ActionResult delete(int ma)
        {
            if (String.IsNullOrEmpty(ma.ToString()))
                return Json(new ResultInfo() { error = 1, msg = "Missing info" }, JsonRequestBehavior.AllowGet);

            var check = db.DM_HocSinh.Where(p => p.MaTruong == MaDonVi && p.ID == ma).FirstOrDefault();

            if (check == null)
                return Json(new ResultInfo() { error = 1, msg = "Không tìm thấy thông tin" }, JsonRequestBehavior.AllowGet);

            db.Entry(check).State = System.Data.Entity.EntityState.Deleted;
            db.SaveChanges();

            return Json(new ResultInfo() { error = 0, msg = "", data = check }, JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public ActionResult xoadshs(List<int> hocsinh)
        {
            foreach (var item in hocsinh)
            {
                if (String.IsNullOrEmpty(item.ToString()))
                    return Json(new ResultInfo() { error = 1, msg = "Missing info" }, JsonRequestBehavior.AllowGet);

                var check = db.DM_HocSinh.Where(p => p.MaTruong == MaDonVi && p.ID == item).FirstOrDefault();

                if (check == null)
                    return Json(new ResultInfo() { error = 1, msg = "Không tìm thấy thông tin" }, JsonRequestBehavior.AllowGet);

                db.Entry(check).State = System.Data.Entity.EntityState.Deleted;
                db.SaveChanges();
            }

            return Json(new ResultInfo() { error = 0, msg = "", data = hocsinh }, JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public ActionResult chuyenphong(List<int> hocsinh)
        {
            foreach (var item in hocsinh)
            {
                if (String.IsNullOrEmpty(item.ToString()))
                    return Json(new ResultInfo() { error = 1, msg = "Missing info" }, JsonRequestBehavior.AllowGet);

                var check = db.DM_HocSinh.Where(p => p.MaTruong == MaDonVi && p.ID == item).FirstOrDefault();

                if (check == null)
                    return Json(new ResultInfo() { error = 1, msg = "Không tìm thấy thông tin" }, JsonRequestBehavior.AllowGet);

                check.TrangThai = 1;

                db.Entry(check).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }

            return Json(new ResultInfo() { error = 0, msg = "", data = hocsinh }, JsonRequestBehavior.AllowGet);

        }
        public async System.Threading.Tasks.Task<ActionResult> UploadFile(HttpPostedFileBase file, String data)
        {
            bool update = true;
            List<ExcelImportErr> error = new List<ExcelImportErr>();
            int errorchk = 0;
            var result = new ResultInfo()
            {
                error = 0,
                msg = "Đã tải",
                data = ""
            };
            string path = "";
            try
            {

                if (file == null || file.ContentLength <= 0)
                    throw new Exception("Thiếu file Excel");

                string extension = System.IO.Path.GetExtension(file.FileName);
                if (extension.Equals(".xlsx"))
                {
                    string fileSave = MaDonVi + "hs" + DateTime.Now.ToString("ddMMyyyyhhmmss") + extension;
                    path = Server.MapPath("~/TaiLieu/Temps/" + fileSave);
                    if (System.IO.File.Exists(path))
                    {
                        System.IO.File.Delete(path);
                    }

                    file.SaveAs(path);
                    FileInfo newFile = new FileInfo(path);
                    var package = new ExcelPackage(newFile);

                    ExcelWorksheet sheet = package.Workbook.Worksheets[1];

                    int totalRows = sheet.Dimension.End.Row;
                    int totalCols = sheet.Dimension.End.Column;
                    //List<CreateEmail> lste = new List<CreateEmail>();
                    for (int i = 2; i <= totalRows; i++)
                    {
                        #region readexcel
                        string hovachulot = Convert.ToString(sheet.Cells[i, 2].Value);
                        if (String.IsNullOrEmpty(hovachulot))
                            error.Add(new ExcelImportErr() { Dong = i, Cot = 2, NoiDung = "Chưa nhập họ và chữ lót" });


                        string ten = Convert.ToString(sheet.Cells[i, 3].Value);
                        if (String.IsNullOrEmpty(ten))
                        {
                            error.Add(new ExcelImportErr() { Dong = i, Cot = 3, NoiDung = "Chưa nhập tên" });
                            errorchk = 1;
                        }

                        string namhoc = Convert.ToString(sheet.Cells[i, 4].Value);
                        if (String.IsNullOrEmpty(namhoc))
                        {
                            error.Add(new ExcelImportErr() { Dong = i, Cot = 4, NoiDung = "Chưa nhập năm hoc" });
                            errorchk = 1;
                        }
                        if (namhoc.Length != 4)
                        {
                            error.Add(new ExcelImportErr() { Dong = i, Cot = 4, NoiDung = "Năm học chưa đúng" });
                            errorchk = 1;
                        }

                        string lop = Convert.ToString(sheet.Cells[i, 5].Value);
                        if (String.IsNullOrEmpty(lop))
                        {
                            error.Add(new ExcelImportErr() { Dong = i, Cot = 5, NoiDung = "Chưa nhập lớp" });
                            errorchk = 1;
                        }

                        string gioitinh = Convert.ToString(sheet.Cells[i, 6].Value);
                        if (String.IsNullOrEmpty(gioitinh))
                        {
                            error.Add(new ExcelImportErr() { Dong = i, Cot = 6, NoiDung = "Chưa nhập giới tính" });
                            errorchk = 1;
                        }

                        if (gioitinh != "Nam" && gioitinh != "Nữ")
                        {
                            error.Add(new ExcelImportErr() { Dong = i, Cot = 6, NoiDung = "Giới tính là Nam hoặc Nữ" });
                            errorchk = 1;
                        }


                        string namsinh = Convert.ToString(sheet.Cells[i, 7].Value);
                        if (String.IsNullOrEmpty(namhoc))
                        {
                            error.Add(new ExcelImportErr() { Dong = i, Cot = 7, NoiDung = "Chưa nhập năm sinh" });
                            errorchk = 1;
                        }

                        DateTime? dnamsinh = UserFunction.ToDate(namsinh);
                        if (dnamsinh == null)
                        {
                            error.Add(new ExcelImportErr() { Dong = i, Cot = 7, NoiDung = "Năm sinh chưa đúng" });
                            errorchk = 1;
                        }

                        string dantoc = Convert.ToString(sheet.Cells[i, 8].Value);
                        string noisinh = Convert.ToString(sheet.Cells[i, 9].Value);
                        string dienut = Convert.ToString(sheet.Cells[i, 10].Value);
                        string dienkk = Convert.ToString(sheet.Cells[i, 11].Value);
                        string toan = Convert.ToString(sheet.Cells[i, 12].Value);
                        string ly = Convert.ToString(sheet.Cells[i, 13].Value);
                        string hoa = Convert.ToString(sheet.Cells[i, 14].Value);
                        string sinh = Convert.ToString(sheet.Cells[i, 15].Value);
                        string nguvan = Convert.ToString(sheet.Cells[i, 16].Value);
                        string su = Convert.ToString(sheet.Cells[i, 17].Value);
                        string dia = Convert.ToString(sheet.Cells[i, 18].Value);
                        string tienganh = Convert.ToString(sheet.Cells[i, 19].Value);
                        string gdcd = Convert.ToString(sheet.Cells[i, 20].Value);
                        string congnghe = Convert.ToString(sheet.Cells[i, 21].Value);
                        string tiengphap = Convert.ToString(sheet.Cells[i, 22].Value);
                        string theduc = Convert.ToString(sheet.Cells[i, 23].Value);
                        string nhac = Convert.ToString(sheet.Cells[i, 24].Value);
                        string mythuat = Convert.ToString(sheet.Cells[i, 25].Value);
                        string tbcn = Convert.ToString(sheet.Cells[i, 26].Value);
                        string xeploaihl = Convert.ToString(sheet.Cells[i, 27].Value);
                        string xeploaihk = Convert.ToString(sheet.Cells[i, 28].Value);
                        string xeploaitn = Convert.ToString(sheet.Cells[i, 29].Value);
                        int buoinghi = 0;
                        try
                        {
                            buoinghi = Convert.ToInt32(sheet.Cells[i, 30].Value);
                        }
                        catch { }

                        #endregion
                        //them moi du lieu
                        if (errorchk == 0)
                        {
                            
                            DM_HocSinh hs = new DM_HocSinh();
                            hs.MaTruong = MaDonVi;
                            hs.NamHoc = namhoc;
                            hs.HoChuLot = hovachulot;
                            hs.Ten = ten;
                            hs.GioiTinh = gioitinh;
                            hs.NamSinh = DateTime.Parse(namsinh);
                            hs.DanToc = dantoc;
                            hs.NoiSinh = noisinh;
                            hs.DienUT = dienut;
                            hs.DienKK = dienkk;
                            hs.Toan = toan;
                            hs.Ly = ly;
                            hs.Hoa = hoa;
                            hs.Sinh = sinh;
                            hs.NguVan = nguvan;
                            hs.Su = su;
                            hs.Dia = dia;
                            hs.TiengAnh = tienganh;
                            hs.GDCD = gdcd;
                            hs.CongNghe = congnghe;
                            hs.TiengPhap = tiengphap;
                            hs.TheDuc = theduc;
                            hs.Nhac = nhac;
                            hs.MyThuat = mythuat;
                            hs.TBCN = tbcn;
                            hs.XepLoaiHL = xeploaihl;
                            hs.XepLoaiHK = xeploaihk;
                            hs.XepLoaiTN = xeploaitn;
                            hs.Lop = lop;
                            hs.BuoiNghi = buoinghi;
                            hs.GhiChu = "";
                            hs.TrangThai = 0;
                            hs.NgayTao = DateTime.Now;
                            db.DM_HocSinh.Add(hs);

                        }
                        else
                        {
                            update = false;
                        }
                        //ket thuc them moi du lieu

                    }
                    if (update == true)
                    {
                        //sau khi lưu thành công mới tiến hành add email
                        db.SaveChanges();

                    }
                    // xoa file temp
                    package.Dispose();
                    if (System.IO.File.Exists(path))
                    {
                        System.IO.File.Delete(path);
                    }
                }
                else
                {
                    error.Add(new ExcelImportErr() { Dong = 0, Cot = 0, NoiDung = "Định dạng yêu cầu: Excel 2007 (xlsx)" });
                    errorchk = 1;
                }

                // result.data = error;
            }
            catch (Exception e)
            {
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
                result.error = 1;
                result.msg = e.Message;
            }
            return Json(new ResultInfo() { error = 0, msg = "", data = error }, JsonRequestBehavior.AllowGet);
        }
    }
}
