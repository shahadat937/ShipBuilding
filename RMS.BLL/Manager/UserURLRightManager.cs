using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using RMS.BLL.IManager;
using RMS.DAL.IRepository;
using RMS.DAL.Repository;
using RMS.Model;
using RMS.Utility;
using RMS.Web.CustomModel;

namespace RMS.BLL.Manager
{
    public class UserURLRightManager : BaseManager, IUserURLRightManager
    {
        private IUserURLRightRepository _userURLRightRepository;
        public UserURLRightManager()
        {
            _userURLRightRepository = new UserURLRightRepository();
        }
        public List<UserRightTreeView> GetUserRightTreeView(string userId)
        {
            var userRights = _userURLRightRepository.Filter(x => x.Visible && x.UserId == userId).OrderBy(x => x.MenuLevel).ToList();
            var menuList = new List<UserRightTreeView>();
            foreach (var userRight in userRights)
            {
                if (userRight.MenuLevel == "1")
                {
                    var menu = new UserRightTreeView()
                    {
                        //UserId = userRight.UserId,
                        MenuLevel = userRight.MenuLevel,
                        FirstLevel = userRight.FirstLevel,
                        MenuId = userRight.MenuId,
                        MenuObjectName = userRight.MenuObjectName,
                        UrlLink = userRight.UrlLink

                    };
                    menuList.Add(menu);

                }

            }

            List<UserURLRight> userUrlRights = userRights.Where(x => x.MenuLevel.Contains("1")).ToList();

            var leftUrlRights = userRights.Except(userUrlRights).ToList();

            foreach (var userRightTreeView in menuList)
            {
                foreach (var subMenu in leftUrlRights)
                {
                    if (userRightTreeView.MenuId.Trim() == subMenu.FirstLevel.Trim() && subMenu.MenuLevel == "2")
                    {
                        var submenu = new SubMenuModel()
                        {
                            //UserId = menu1.UserId,
                            MenuLevel = subMenu.MenuLevel,
                            FirstLevel = subMenu.FirstLevel,
                            MenuId = subMenu.MenuId,
                            MenuObjectName = subMenu.MenuObjectName,
                            UrlLink = subMenu.UrlLink,
                            NextSubMenus = new List<NextSubMenu>()
                        };

                        userRightTreeView.SubMenuModels.Add(submenu);

                        var nextSub = leftUrlRights.Where(x => x.Visible && x.UserId == userId).Where(x => x.SecondLevel == subMenu.MenuId && x.MenuLevel == "3").OrderBy(x => x.MenuLevel).ToList();

                        var nextSubMenus = new List<NextSubMenu>();
                        foreach (var userUrlRight in nextSub)
                        {
                            var nextSubmenu = new NextSubMenu()
                            {
                                //UserId = menu1.UserId,
                                MenuLevel = userUrlRight.MenuLevel,
                                FirstLevel = userUrlRight.FirstLevel,
                                MenuId = userUrlRight.MenuId,
                                MenuObjectName = userUrlRight.MenuObjectName,
                                UrlLink = userUrlRight.UrlLink,

                            };
                            nextSubMenus.Add(nextSubmenu);

                        }

                        submenu.NextSubMenus = nextSubMenus;

                    }

                }

            }

            //List<UserURLRight> againLeftUserUrlRights = userRights.Where(x => x.MenuLevel.Contains("2")).ToList();
            //leftUrlRights = leftUrlRights.Except(againLeftUserUrlRights).ToList();

            //foreach (var userRightTreeView in menuList)
            //{
            //    foreach (var subMenu in leftUrlRights)
            //    {
            //        if (userRightTreeView.MenuId == subMenu.FirstLevel && subMenu.MenuLevel == "3")
            //        {
            //            var nextSubMenu = new NextSubMenu()
            //            {
            //                MenuLevel = subMenu.MenuLevel,
            //                FirstLevel = subMenu.FirstLevel,
            //                MenuId = subMenu.MenuId,
            //                MenuObjectName = subMenu.MenuObjectName,
            //                UrlLink = subMenu.UrlLink
            //            };
            //            userRightTreeView.NextSubMenu.Add(nextSubMenu);
            //        }

            //    }

            //}

            return menuList;
        }

        public List<UserURLRight> GetUserRighsByRole(string userRole)
        {
            return _userURLRightRepository.Filter(x => x.UserId == userRole).ToList();
        }

        public ResponseModel Save(List<UserURLRight> model)
        {
            int count = 0;
            foreach (var item in model)
            {
                string menuId = item.MenuId;
                string roleId = item.UserId;
                UserURLRight rec = _userURLRightRepository.FindOne(x => x.MenuId == menuId && x.UserId == roleId);
                if (rec != null)
                {
                    rec.MenuObjectName = item.MenuObjectName ?? "";
                    rec.UrlLink = item.UrlLink ?? "";
                    rec.Visible = item.Visible;
                    count += _userURLRightRepository.Edit(rec);
                }

            }
            if (count > 0)
            {
                ResponseModel.Message = "Data has been updated";
                ResponseModel.ResponsStatus = 1;
            }
            else
            {
                ResponseModel.Message = "Data is not updated.";
            }
            return ResponseModel;
        }
    }
}
