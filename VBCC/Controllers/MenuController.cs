using VBCC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VBCC.Controllers
{

    public class MenuController : BaseController
    {
        [HttpGet]
        public ActionResult AdminMenu()
        {
            if(IsPhong == true)
            {
                if (UType == "SUP")
                {
                    return PartialView("_SUPMenu");
                }
                else if (UType == "AD")
                {
                    return PartialView("_ADMenu");
                }
                else
                {
                    return PartialView("_NoneView");
                }
            }else
            {
                return PartialView("_TruongMenu");
            }
            
        }


        [HttpGet]
        public ActionResult Menus()
        {
            var user = User.Identity.Name;
            List<USER_GETMENU_Result> getMenu = new List<USER_GETMENU_Result>();
            if (UType == "AD")
            {
                //getMenu = db.GETMENU_ALL().ToList();
                string groupid = "E5A2B3E1-25AB-4BAD-8853-2560C0D2036B";
                getMenu = db.Database.SqlQuery<USER_GETMENU_Result>("GETMENU_ALL").Where(p => p.GroupMenuId != groupid).ToList();

            }
            else if (UType == "US")
            {
                getMenu = db.USER_GETMENU(user).ToList();
            }


            List<GroupMenuInfo> groupMenus = new List<GroupMenuInfo>();

            var listGroup = db.UMS_GroupMenu.OrderBy(p => p.Position).ToList();

            foreach (var item in listGroup)
            {
                GroupMenuInfo groupInfo = new GroupMenuInfo()
                {
                    name = item.Name,
                    icon = item.Icon,
                    menus = new List<MenuInfo>()
                };

                var listMenu = getMenu.Where(p => p.GroupMenuId == item.Id).OrderBy(p => p.Position).ToList();

                if (listMenu.Count() > 0)
                {
                    foreach (var menuItem in listMenu)
                    {
                        groupInfo.menus.Add(new MenuInfo()
                        {
                            name = menuItem.Name,
                            link = menuItem.Link
                        });
                    }
                    groupMenus.Add(groupInfo);
                }
            }

            return PartialView("_MenuUser", groupMenus);
        }


    }
}