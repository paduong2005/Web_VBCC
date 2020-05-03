using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VBCC.Models
{
    public class IdentityCommon
    {
        public string code { get; set; }

        public string name { get; set; }
    }
    public class ResultInfo
    {
        public int error { get; set; }

        public string msg { get; set; }

        public Object data { get; set; }

    }
    public class ResultWithPaging : ResultInfo
    {
        public int page { get; set; }

        public int toltalSize { get; set; }

        public int pageSize { get; set; }
    }
    public class MenuInfo
    {
        public string name { get; set; }

        public string link { get; set; }
    }
    public class GroupMenuInfo
    {
        public string name { get; set; }

        public string icon { get; set; }

        public List<MenuInfo> menus { get; set; }
    }
    public class USER_GETMENU
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Link { get; set; }
        public string Icon { get; set; }
        public string GroupMenuId { get; set; }
        public int Position { get; set; }
        public string Code { get; set; }
        public string groupName { get; set; }
        public string GroupIcon { get; set; }
    }
    public class LichNhanAnPham
    {
        public string ma { get; set; }
        public string ten { get; set; }
        public string nguoilh { get; set; }
        public string sdt { get; set; }
        public string noidung { get; set; }
    }
    public class ExcelImportErr
    {
        public int Dong { get; set; }
        public int Cot { get; set; }
        public string NoiDung { get; set; }
    }
    public class lstCaBiet
    {
        public string Kho { get; set; }
        public string STT { get; set; }
    }
    public class lstCaBietPhich
    {
        public string STT { get; set; }
        public string NhanDe { get; set; }
        public string TacGia { get; set; }
        public string NamXB { get; set; }
    }
    public class lstMaSach
    {
        public string MaSach { get; set; }
    }
    public class lstMaTruong
    {
        public string MaTruong { get; set; }
    }

}