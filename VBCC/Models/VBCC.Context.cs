﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace VBCC.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class VBCCEntities : DbContext
    {
        public VBCCEntities()
            : base("name=VBCCEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<AspNetRole> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
        public virtual DbSet<DM_NhanVien_DaoTao> DM_NhanVien_DaoTao { get; set; }
        public virtual DbSet<VB_TT_File> VB_TT_File { get; set; }
        public virtual DbSet<UMS_GroupMenu> UMS_GroupMenu { get; set; }
        public virtual DbSet<UMS_Menu> UMS_Menu { get; set; }
        public virtual DbSet<UMS_MenuGroupUser> UMS_MenuGroupUser { get; set; }
        public virtual DbSet<UMS_UserGroups> UMS_UserGroups { get; set; }
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<DM_SoGocVanBang> DM_SoGocVanBang { get; set; }
        public virtual DbSet<DM_SoGocVanBang_ChiTiet> DM_SoGocVanBang_ChiTiet { get; set; }
        public virtual DbSet<DM_Truong> DM_Truong { get; set; }
        public virtual DbSet<DM_NamHoc> DM_NamHoc { get; set; }
        public virtual DbSet<DM_DonVi> DM_DonVi { get; set; }
        public virtual DbSet<DM_LopHoc> DM_LopHoc { get; set; }
        public virtual DbSet<HT_Nam> HT_Nam { get; set; }
        public virtual DbSet<HT_Ngay> HT_Ngay { get; set; }
        public virtual DbSet<HT_Thang> HT_Thang { get; set; }
        public virtual DbSet<DM_HocSinh> DM_HocSinh { get; set; }
    
        public virtual ObjectResult<USER_CHECKACCESS_Result> USER_CHECKACCESS(string groupId, string menuCode)
        {
            var groupIdParameter = groupId != null ?
                new ObjectParameter("groupId", groupId) :
                new ObjectParameter("groupId", typeof(string));
    
            var menuCodeParameter = menuCode != null ?
                new ObjectParameter("menuCode", menuCode) :
                new ObjectParameter("menuCode", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<USER_CHECKACCESS_Result>("USER_CHECKACCESS", groupIdParameter, menuCodeParameter);
        }
    
        public virtual ObjectResult<USER_CHECKADMIN_Result> USER_CHECKADMIN(string user, string role)
        {
            var userParameter = user != null ?
                new ObjectParameter("user", user) :
                new ObjectParameter("user", typeof(string));
    
            var roleParameter = role != null ?
                new ObjectParameter("role", role) :
                new ObjectParameter("role", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<USER_CHECKADMIN_Result>("USER_CHECKADMIN", userParameter, roleParameter);
        }
    
        public virtual ObjectResult<USER_GETMENU_Result> USER_GETMENU(string user)
        {
            var userParameter = user != null ?
                new ObjectParameter("user", user) :
                new ObjectParameter("user", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<USER_GETMENU_Result>("USER_GETMENU", userParameter);
        }
    
        public virtual ObjectResult<USERS_GETALL_Result> USERS_GETALL(string madonvi)
        {
            var madonviParameter = madonvi != null ?
                new ObjectParameter("madonvi", madonvi) :
                new ObjectParameter("madonvi", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<USERS_GETALL_Result>("USERS_GETALL", madonviParameter);
        }
    }
}