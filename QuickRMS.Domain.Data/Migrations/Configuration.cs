using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Data.Entity.Migrations;

using Quick.Framework.EFData;
using QuickRMS.Domain.Models.Authen;
using QuickRMS.Domain.Models.DeviceInfo;


namespace QuickRMS.Domain.Data.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<EFDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(EFDbContext context)
        {
            //角色
            var roles = new List<Role>
            {
                new Role { Id = 1, Name = "系统管理员", Description = "开发人员、系统配置人员使用", OrderSort = 1, Enabled = true, IsDeleted = false, CreateBy = "admin", CreateId = 1, CreateTime = DateTime.Now, ModifyId = 1, ModifyBy = "admin", ModifyTime = DateTime.Now },
                new Role { Id = 2, Name = "普通管理员", Description = "普通管理员", OrderSort = 2, Enabled = true, IsDeleted = false, CreateBy = "admin", CreateId = 1, CreateTime = DateTime.Now, ModifyId = 1, ModifyBy = "admin", ModifyTime = DateTime.Now },
                new Role { Id = 3, Name = "业务人员", Description = "业务人员", OrderSort = 3, Enabled = true, IsDeleted = false, CreateBy = "admin", CreateId = 1, CreateTime = DateTime.Now, ModifyId = 1, ModifyBy = "admin", ModifyTime = DateTime.Now },
                new Role { Id = 4, Name = "公司领导", Description = "公司领导", OrderSort = 4, Enabled = true, IsDeleted = false, CreateBy = "admin", CreateId = 1, CreateTime = DateTime.Now, ModifyId = 1, ModifyBy = "admin", ModifyTime = DateTime.Now },
                
            };
            DbSet<Role> roleSet = context.Set<Role>();
            roleSet.AddOrUpdate(t => new { t.Id }, roles.ToArray());
            context.SaveChanges();

            //地区
            var Areas = new List<Area>
            {
                new Area { Id = 1, Name = "中国",Code = "0",Description = "中国",IsLeaf  = false,IsDeleted = false, CreateBy = "admin", CreateId = 1, CreateTime = DateTime.Now, ModifyId = 1, ModifyBy = "admin", ModifyTime = DateTime.Now },
         new Area { Id=2,Name="北京市",ParentId =1,Description="北京市",Longitude = 116.404355m,Latitude = 39.914776m,Code="10",IsLeaf=false,IsDeleted = false, CreateBy = "admin", CreateId = 1, CreateTime = DateTime.Now, ModifyId = 1, ModifyBy = "admin", ModifyTime = DateTime.Now},
   new Area { Id=3,Name="天津市",ParentId =1,Description="天津市",Code="20",IsLeaf=false,IsDeleted = false, CreateBy = "admin", CreateId = 1, CreateTime = DateTime.Now, ModifyId = 1, ModifyBy = "admin", ModifyTime = DateTime.Now},
   new Area { Id=4,Name="河北省",ParentId =1,Description="河北省",Code="3",IsLeaf=false,IsDeleted = false, CreateBy = "admin", CreateId = 1, CreateTime = DateTime.Now, ModifyId = 1, ModifyBy = "admin", ModifyTime = DateTime.Now},
   new Area { Id=5,Name="河西区",ParentId =3,Description="河西区",Code="2001",IsLeaf=true,IsDeleted = false, CreateBy = "admin", CreateId = 1, CreateTime = DateTime.Now, ModifyId = 1, ModifyBy = "admin", ModifyTime = DateTime.Now},
   new Area { Id=6,Name="新疆",ParentId =1,Description="新疆",Code="991",IsLeaf=false,IsDeleted = false, CreateBy = "admin", CreateId = 1, CreateTime = DateTime.Now, ModifyId = 1, ModifyBy = "admin", ModifyTime = DateTime.Now},
   new Area { Id=7,Name="丰台区",ParentId =2,Description="丰台区",Longitude = 116.290522m,Latitude = 39.86628m,Code="100002",IsLeaf=true,IsDeleted = false, CreateBy = "admin", CreateId = 1, CreateTime = DateTime.Now, ModifyId = 1, ModifyBy = "admin", ModifyTime = DateTime.Now},
   new Area { Id=8,Name="河南",ParentId =1,Description="河南",Code="379",IsLeaf=false,IsDeleted = false, CreateBy = "admin", CreateId = 1, CreateTime = DateTime.Now, ModifyId = 1, ModifyBy = "admin", ModifyTime = DateTime.Now},
   new Area { Id=9,Name="辽宁省",ParentId =1,Description="辽宁省",Code="421",IsLeaf=false,IsDeleted = false, CreateBy = "admin", CreateId = 1, CreateTime = DateTime.Now, ModifyId = 1, ModifyBy = "admin", ModifyTime = DateTime.Now},
   new Area { Id=10,Name="昌平区",ParentId =2,Description="昌平区",Longitude = 116.237055m,Latitude = 40.227725m,Code="100003",IsLeaf=true,IsDeleted = false, CreateBy = "admin", CreateId = 1, CreateTime = DateTime.Now, ModifyId = 1, ModifyBy = "admin", ModifyTime = DateTime.Now},
   new Area { Id=11,Name="海淀区",ParentId =2,Description="海淀区",Longitude = 116.302595m,Latitude = 39.966556m,Code="100001",IsLeaf=true,IsDeleted = false, CreateBy = "admin", CreateId = 1, CreateTime = DateTime.Now, ModifyId = 1, ModifyBy = "admin", ModifyTime = DateTime.Now},
   new Area { Id=12,Name="通州区",ParentId =2,Description="通州区",Longitude = 116.663642m,Latitude = 39.917211m,Code="100004",IsLeaf=true,IsDeleted = false, CreateBy = "admin", CreateId = 1, CreateTime = DateTime.Now, ModifyId = 1, ModifyBy = "admin", ModifyTime = DateTime.Now},
   new Area { Id=13,Name="朝阳区",ParentId =2,Description="朝阳区",Longitude = 116.449199m,Latitude = 39.927393m,Code="100005",IsLeaf=true,IsDeleted = false, CreateBy = "admin", CreateId = 1, CreateTime = DateTime.Now, ModifyId = 1, ModifyBy = "admin", ModifyTime = DateTime.Now},
   new Area { Id=14,Name="沈阳市",ParentId =9,Description="沈阳市",Code="24",IsLeaf=true,IsDeleted = false, CreateBy = "admin", CreateId = 1, CreateTime = DateTime.Now, ModifyId = 1, ModifyBy = "admin", ModifyTime = DateTime.Now},
   new Area { Id=15,Name="承德市",ParentId =4,Description="承德市",Code="314",IsLeaf=true,IsDeleted = false, CreateBy = "admin", CreateId = 1, CreateTime = DateTime.Now, ModifyId = 1, ModifyBy = "admin", ModifyTime = DateTime.Now},
   new Area { Id=16,Name="五家渠市",ParentId =6,Description="五家渠市",Code="994",IsLeaf=true,IsDeleted = false, CreateBy = "admin", CreateId = 1, CreateTime = DateTime.Now, ModifyId = 1, ModifyBy = "admin", ModifyTime = DateTime.Now},
   new Area { Id=17,Name="乌鲁木齐市",ParentId =6,Description="乌鲁木齐市",Code="991",IsLeaf=false,IsDeleted = false, CreateBy = "admin", CreateId = 1, CreateTime = DateTime.Now, ModifyId = 1, ModifyBy = "admin", ModifyTime = DateTime.Now},
   new Area { Id=18,Name="洛阳市",ParentId =8,Description="洛阳市",Code="379",IsLeaf=true,IsDeleted = false, CreateBy = "admin", CreateId = 1, CreateTime = DateTime.Now, ModifyId = 1, ModifyBy = "admin", ModifyTime = DateTime.Now},
   new Area { Id=19,Name="郑州市",ParentId =8,Description="郑州市",Code="371",IsLeaf=true,IsDeleted = false, CreateBy = "admin", CreateId = 1, CreateTime = DateTime.Now, ModifyId = 1, ModifyBy = "admin", ModifyTime = DateTime.Now},
   new Area { Id=20,Name="塔城",ParentId =6,Description="塔城",Code="901",IsLeaf=true,IsDeleted = false, CreateBy = "admin", CreateId = 1, CreateTime = DateTime.Now, ModifyId = 1, ModifyBy = "admin", ModifyTime = DateTime.Now},
   new Area { Id=21,Name="张家口",ParentId =4,Description="张家口",Code="313",IsLeaf=true,IsDeleted = false, CreateBy = "admin", CreateId = 1, CreateTime = DateTime.Now, ModifyId = 1, ModifyBy = "admin", ModifyTime = DateTime.Now},
   new Area { Id=22,Name="朝阳市",ParentId =4,Description="朝阳市",Code="421",IsLeaf=true,IsDeleted = false, CreateBy = "admin", CreateId = 1, CreateTime = DateTime.Now, ModifyId = 1, ModifyBy = "admin", ModifyTime = DateTime.Now},
   new Area { Id=23,Name="唐山市",ParentId =4,Description="唐山市",Longitude = 118.184736m,Latitude = 39.637229m,Code="315",IsLeaf=true,IsDeleted = false, CreateBy = "admin", CreateId = 1, CreateTime = DateTime.Now, ModifyId = 1, ModifyBy = "admin", ModifyTime = DateTime.Now},
   new Area { Id=24,Name="大连市",ParentId =9,Description="大连市",Code="411",IsLeaf=true,IsDeleted = false, CreateBy = "admin", CreateId = 1, CreateTime = DateTime.Now, ModifyId = 1, ModifyBy = "admin", ModifyTime = DateTime.Now},
   new Area { Id=25,Name="山东省",ParentId =1,Description="山东省",Code="500",IsLeaf=false,IsDeleted = false, CreateBy = "admin", CreateId = 1, CreateTime = DateTime.Now, ModifyId = 1, ModifyBy = "admin", ModifyTime = DateTime.Now},
   new Area { Id=26,Name="烟台市",ParentId =25,Description="烟台市",Code="535",IsLeaf=true,IsDeleted = false, CreateBy = "admin", CreateId = 1, CreateTime = DateTime.Now, ModifyId = 1, ModifyBy = "admin", ModifyTime = DateTime.Now},
   new Area { Id=27,Name="泰安市",ParentId =25,Description="泰安市",Code="538",IsLeaf=true,IsDeleted = false, CreateBy = "admin", CreateId = 1, CreateTime = DateTime.Now, ModifyId = 1, ModifyBy = "admin", ModifyTime = DateTime.Now},
   new Area { Id=28,Name="宝坻区",ParentId =3,Description="宝坻区",Code="22",IsLeaf=true,IsDeleted = false, CreateBy = "admin", CreateId = 1, CreateTime = DateTime.Now, ModifyId = 1, ModifyBy = "admin", ModifyTime = DateTime.Now},
   new Area { Id=29,Name="蓝天热力",ParentId =17,Description="蓝天热力",Code="1",IsLeaf=true,IsDeleted = false, CreateBy = "admin", CreateId = 1, CreateTime = DateTime.Now, ModifyId = 1, ModifyBy = "admin", ModifyTime = DateTime.Now},
   new Area { Id=30,Name="财经学院",ParentId =17,Description="财经学院",Code="2",IsLeaf=true,IsDeleted = false, CreateBy = "admin", CreateId = 1, CreateTime = DateTime.Now, ModifyId = 1, ModifyBy = "admin", ModifyTime = DateTime.Now},
   new Area { Id=31,Name="新胜热力",ParentId =17,Description="新胜热力",Code="3",IsLeaf=true,IsDeleted = false, CreateBy = "admin", CreateId = 1, CreateTime = DateTime.Now, ModifyId = 1, ModifyBy = "admin", ModifyTime = DateTime.Now},
   new Area { Id=32,Name="吉林省",ParentId =1,Description="吉林省",Code="432",IsLeaf=false,IsDeleted = false, CreateBy = "admin", CreateId = 1, CreateTime = DateTime.Now, ModifyId = 1, ModifyBy = "admin", ModifyTime = DateTime.Now},
   new Area { Id=33,Name="通化市",ParentId =32,Description="通化市",Code="435",IsLeaf=true,IsDeleted = false, CreateBy = "admin", CreateId = 1, CreateTime = DateTime.Now, ModifyId = 1, ModifyBy = "admin", ModifyTime = DateTime.Now},
   new Area { Id=34,Name="白山市",ParentId =32,Description="白山市",Code="439",IsLeaf=true,IsDeleted = false, CreateBy = "admin", CreateId = 1, CreateTime = DateTime.Now, ModifyId = 1, ModifyBy = "admin", ModifyTime = DateTime.Now},

            };
            DbSet<Area> AreaSet = context.Set<Area>();
            AreaSet.AddOrUpdate(t => new { t.Id }, Areas.ToArray());
            context.SaveChanges();


        

            //用户
            var users = new List<User>
            {
                new User { Id = 1, UserCode = "admin", UserPwd = "admin123456", UserName="admin", Memo = "", Tel ="18653908880", Enabled = true, IsDeleted = false, PwdErrorCount = 0, LoginCount = 0, RegisterTime = DateTime.Now, LastLoginTime = DateTime.Now, CreateBy = "admin", CreateId = 1, CreateTime = DateTime.Now, ModifyId = 1, ModifyBy = "admin", ModifyTime = DateTime.Now },
                new User { Id = 2, UserCode = "111111", UserPwd = "admin123456", UserName="普通管理员", Memo = "", Tel ="18653908880", Enabled = true, IsDeleted = false, PwdErrorCount = 0, LoginCount = 0, RegisterTime = DateTime.Now, LastLoginTime = DateTime.Now, CreateBy = "admin", CreateId = 1, CreateTime = DateTime.Now, ModifyId = 1, ModifyBy = "admin", ModifyTime = DateTime.Now }
            };
            DbSet<User> userSet = context.Set<User>();
            userSet.AddOrUpdate(t => new { t.Id }, users.ToArray());
            context.SaveChanges();

            //设备
            var devices = new List<Device>
            {
               new Device { Id=1,DeviceCode="4210001",DeviceName="朝阳县政府食堂站",IP="",Port=5003,CtrlMode=0,AreaId=34,InstallTime=DateTime.Now, CreateBy = "admin", CreateId = 1, CreateTime = DateTime.Now, ModifyId = 1, ModifyBy = "admin", ModifyTime = DateTime.Now},
new Device { Id=2,DeviceCode="9010001",DeviceName="额敏县",IP="",Port=5003,CtrlMode=0,AreaId=32,InstallTime=DateTime.Now, CreateBy = "admin", CreateId = 1, CreateTime = DateTime.Now, ModifyId = 1, ModifyBy = "admin", ModifyTime = DateTime.Now},
new Device { Id=3,DeviceCode="9940001",DeviceName="君豪二期站",IP="",Port=5003,CtrlMode=0,AreaId=28,InstallTime=DateTime.Now, CreateBy = "admin", CreateId = 1, CreateTime = DateTime.Now, ModifyId = 1, ModifyBy = "admin", ModifyTime = DateTime.Now},
new Device { Id=4,DeviceCode="3790001",DeviceName="河科大工科组热力站",IP="",Port=5003,CtrlMode=0,AreaId=30,InstallTime=DateTime.Now, CreateBy = "admin", CreateId = 1, CreateTime = DateTime.Now, ModifyId = 1, ModifyBy = "admin", ModifyTime = DateTime.Now},
new Device { Id=5,DeviceCode="77777777",DeviceName="区域设备测试",IP="",Port=5003,CtrlMode=0,AreaId=23,InstallTime=DateTime.Now, CreateBy = "admin", CreateId = 1, CreateTime = DateTime.Now, ModifyId = 1, ModifyBy = "admin", ModifyTime = DateTime.Now},
new Device { Id=6,DeviceCode="88888888",DeviceName="赵拆芯8号机",IP="",Port=5003,CtrlMode=0,AreaId=23,InstallTime=DateTime.Now, CreateBy = "admin", CreateId = 1, CreateTime = DateTime.Now, ModifyId = 1, ModifyBy = "admin", ModifyTime = DateTime.Now},
new Device { Id=7,DeviceCode="99999999",DeviceName="通州设备",IP="",Port=5003,CtrlMode=0,AreaId=12,InstallTime=DateTime.Now, CreateBy = "admin", CreateId = 1, CreateTime = DateTime.Now, ModifyId = 1, ModifyBy = "admin", ModifyTime = DateTime.Now},
new Device { Id=8,DeviceCode="55555555",DeviceName="测试5号机",Longitude = 116.31584m,Latitude = 39.998172m,IP="",Port=5003,CtrlMode=0,IsOnline = true,AreaId=11,InstallTime=DateTime.Now, CreateBy = "admin", CreateId = 1, CreateTime = DateTime.Now, ModifyId = 1, ModifyBy = "admin", ModifyTime = DateTime.Now},
new Device { Id=9,DeviceCode="66666666",DeviceName="测试6号机",Longitude = 116.314977m,Latitude = 39.992686m,IP="",Port=5003,CtrlMode=0,AreaId=11,InstallTime=DateTime.Now, CreateBy = "admin", CreateId = 1, CreateTime = DateTime.Now, ModifyId = 1, ModifyBy = "admin", ModifyTime = DateTime.Now},
new Device { Id=10,DeviceCode="22222222",DeviceName="测试2号机",Longitude = 116.308977m,Latitude = 39.99983m,IP="",Port=5003,CtrlMode=0,IsOnline = true,AreaId=11,InstallTime=DateTime.Now, CreateBy = "admin", CreateId = 1, CreateTime = DateTime.Now, ModifyId = 1, ModifyBy = "admin", ModifyTime = DateTime.Now},
new Device { Id=11,DeviceCode="11111111",DeviceName="测试1号机",Longitude = 116.308689m,Latitude = 40.000521m,IP="",Port=5003,CtrlMode=0,AreaId=11,InstallTime=DateTime.Now, CreateBy = "admin", CreateId = 1, CreateTime = DateTime.Now, ModifyId = 1, ModifyBy = "admin", ModifyTime = DateTime.Now},
//new Device { Id=12,DeviceCode="4350001",DeviceName="自安小区",IP="",Port=5003,CtrlMode=0,AreaId=45,InstallTime=DateTime.Now, CreateBy = "admin", CreateId = 1, CreateTime = DateTime.Now, ModifyId = 1, ModifyBy = "admin", ModifyTime = DateTime.Now},
//new Device { Id=13,DeviceCode="4350002",DeviceName="六合盛站",IP="",Port=5003,CtrlMode=0,AreaId=45,InstallTime=DateTime.Now, CreateBy = "admin", CreateId = 1, CreateTime = DateTime.Now, ModifyId = 1, ModifyBy = "admin", ModifyTime = DateTime.Now},
//new Device { Id=14,DeviceCode="4390001",DeviceName="10号站",IP="",Port=5003,CtrlMode=0,AreaId=46,InstallTime=DateTime.Now, CreateBy = "admin", CreateId = 1, CreateTime = DateTime.Now, ModifyId = 1, ModifyBy = "admin", ModifyTime = DateTime.Now},
//new Device { Id=15,DeviceCode="4390002",DeviceName="16号站",IP="",Port=5003,CtrlMode=0,AreaId=46,InstallTime=DateTime.Now, CreateBy = "admin", CreateId = 1, CreateTime = DateTime.Now, ModifyId = 1, ModifyBy = "admin", ModifyTime = DateTime.Now},
new Device { Id=16,DeviceCode="3790002",DeviceName="河科大 宿舍区",IP="",Port=5003,CtrlMode=0,AreaId=30,InstallTime=DateTime.Now, CreateBy = "admin", CreateId = 1, CreateTime = DateTime.Now, ModifyId = 1, ModifyBy = "admin", ModifyTime = DateTime.Now},
new Device { Id=17,DeviceCode="44444444",DeviceName="北京测试",IP="",Port=5003,CtrlMode=0,AreaId=11,InstallTime=DateTime.Now, CreateBy = "admin", CreateId = 1, CreateTime = DateTime.Now, ModifyId = 1, ModifyBy = "admin", ModifyTime = DateTime.Now},
new Device { Id=18,DeviceCode="9940002",DeviceName="101幸福小区",IP="",Port=5003,CtrlMode=0,AreaId=28,InstallTime=DateTime.Now, CreateBy = "admin", CreateId = 1, CreateTime = DateTime.Now, ModifyId = 1, ModifyBy = "admin", ModifyTime = DateTime.Now},
new Device { Id=19,DeviceCode="9940003",DeviceName="设备3",IP="",Port=5003,CtrlMode=0,AreaId=28,InstallTime=DateTime.Now, CreateBy = "admin", CreateId = 1, CreateTime = DateTime.Now, ModifyId = 1, ModifyBy = "admin", ModifyTime = DateTime.Now},
new Device { Id=20,DeviceCode="42424242",DeviceName="郑的42测试",IP="",Port=5003,CtrlMode=0,AreaId=11,InstallTime=DateTime.Now, CreateBy = "admin", CreateId = 1, CreateTime = DateTime.Now, ModifyId = 1, ModifyBy = "admin", ModifyTime = DateTime.Now},
new Device { Id=21,DeviceCode="33333333",DeviceName="孟的测试设备",IP="",Port=5003,CtrlMode=0,IsOnline = true,AreaId=11,InstallTime=DateTime.Now, CreateBy = "admin", CreateId = 1, CreateTime = DateTime.Now, ModifyId = 1, ModifyBy = "admin", ModifyTime = DateTime.Now},
new Device { Id=22,DeviceCode="0",DeviceName="调试设备",IP="",Port=5003,CtrlMode=0,AreaId=11,InstallTime=DateTime.Now, CreateBy = "admin", CreateId = 1, CreateTime = DateTime.Now, ModifyId = 1, ModifyBy = "admin", ModifyTime = DateTime.Now},

            };
            DbSet<Device> deviceSet = context.Set<Device>();
            deviceSet.AddOrUpdate(t => new { t.Id }, devices.ToArray());
            context.SaveChanges();

            //用户
            var ua = new List<UserArea>
            {
                new UserArea { Id = 1,UserId  = 1,AreaId = 1,IsDeleted = false, CreateBy = "admin", CreateId = 1, CreateTime = DateTime.Now, ModifyId = 1, ModifyBy = "admin", ModifyTime = DateTime.Now },
                new UserArea { Id = 2,UserId  = 1,AreaId = 2,IsDeleted = false,  CreateBy = "admin", CreateId = 1, CreateTime = DateTime.Now, ModifyId = 1, ModifyBy = "admin", ModifyTime = DateTime.Now },
                  new UserArea { Id = 3,UserId  = 1,AreaId = 3,IsDeleted = false, CreateBy = "admin", CreateId = 1, CreateTime = DateTime.Now, ModifyId = 1, ModifyBy = "admin", ModifyTime = DateTime.Now },
                new UserArea { Id = 4,UserId  = 1,AreaId = 4,IsDeleted = false,  CreateBy = "admin", CreateId = 1, CreateTime = DateTime.Now, ModifyId = 1, ModifyBy = "admin", ModifyTime = DateTime.Now },
                  new UserArea { Id = 5,UserId  = 1,AreaId = 5,IsDeleted = false, CreateBy = "admin", CreateId = 1, CreateTime = DateTime.Now, ModifyId = 1, ModifyBy = "admin", ModifyTime = DateTime.Now },
                new UserArea { Id = 6,UserId  = 1,AreaId = 6,IsDeleted = false,  CreateBy = "admin", CreateId = 1, CreateTime = DateTime.Now, ModifyId = 1, ModifyBy = "admin", ModifyTime = DateTime.Now },
                  new UserArea { Id = 7,UserId  = 1,AreaId = 7,IsDeleted = false, CreateBy = "admin", CreateId = 1, CreateTime = DateTime.Now, ModifyId = 1, ModifyBy = "admin", ModifyTime = DateTime.Now },
                new UserArea { Id = 8,UserId  = 1,AreaId = 8,IsDeleted = false,  CreateBy = "admin", CreateId = 1, CreateTime = DateTime.Now, ModifyId = 1, ModifyBy = "admin", ModifyTime = DateTime.Now },
                  new UserArea { Id = 9,UserId  = 1,AreaId = 9,IsDeleted = false, CreateBy = "admin", CreateId = 1, CreateTime = DateTime.Now, ModifyId = 1, ModifyBy = "admin", ModifyTime = DateTime.Now },
                new UserArea { Id = 10,UserId  = 1,AreaId = 10,IsDeleted = false,  CreateBy = "admin", CreateId = 1, CreateTime = DateTime.Now, ModifyId = 1, ModifyBy = "admin", ModifyTime = DateTime.Now },
                  new UserArea { Id = 11,UserId  = 1,AreaId = 11,IsDeleted = false, CreateBy = "admin", CreateId = 1, CreateTime = DateTime.Now, ModifyId = 1, ModifyBy = "admin", ModifyTime = DateTime.Now },
                new UserArea { Id = 12,UserId  = 1,AreaId = 12,IsDeleted = false,  CreateBy = "admin", CreateId = 1, CreateTime = DateTime.Now, ModifyId = 1, ModifyBy = "admin", ModifyTime = DateTime.Now },
                  new UserArea { Id = 13,UserId  = 1,AreaId = 13,IsDeleted = false, CreateBy = "admin", CreateId = 1, CreateTime = DateTime.Now, ModifyId = 1, ModifyBy = "admin", ModifyTime = DateTime.Now },
                new UserArea { Id = 14,UserId  = 1,AreaId = 14,IsDeleted = false,  CreateBy = "admin", CreateId = 1, CreateTime = DateTime.Now, ModifyId = 1, ModifyBy = "admin", ModifyTime = DateTime.Now },
                  new UserArea { Id = 15,UserId  = 1,AreaId = 15,IsDeleted = false, CreateBy = "admin", CreateId = 1, CreateTime = DateTime.Now, ModifyId = 1, ModifyBy = "admin", ModifyTime = DateTime.Now },
                new UserArea { Id = 16,UserId  = 1,AreaId = 16,IsDeleted = false,  CreateBy = "admin", CreateId = 1, CreateTime = DateTime.Now, ModifyId = 1, ModifyBy = "admin", ModifyTime = DateTime.Now },
                new UserArea { Id = 17,UserId  = 1,AreaId = 17,IsDeleted = false,  CreateBy = "admin", CreateId = 1, CreateTime = DateTime.Now, ModifyId = 1, ModifyBy = "admin", ModifyTime = DateTime.Now },
            };
            DbSet<UserArea> userAreas = context.Set<UserArea>();
            userAreas.AddOrUpdate(t => new { t.Id }, userAreas.ToArray());
            context.SaveChanges();

            //用户-角色
            var userRoles = new List<UserRole>
            {
                new UserRole { UserId = 1, RoleId = 1, IsDeleted = false },
                 new UserRole { UserId = 2, RoleId = 2, IsDeleted = false },
            };
            DbSet<UserRole> userRoleSet = context.Set<UserRole>();
            userRoleSet.AddOrUpdate(t => new { t.UserId }, userRoles.ToArray());
            context.SaveChanges();

            //模块
            var modules = new List<Module>
            {
                new Module { Id = 1, ParentId = null, Name = "权限管理", LinkUrl = null, Area = null, Controller = null, Action = null, Icon = "icon-sitemap", Code = "20", OrderSort = 1, Description = null, IsMenu = true, Enabled = true, IsDeleted = false, CreateBy = "admin", CreateId = 1, CreateTime = DateTime.Now, ModifyId = 1, ModifyBy = "admin", ModifyTime = DateTime.Now },
                new Module { Id = 2, ParentId = 1, Name = "角色管理", LinkUrl = "Authen/Role/Index", Area = "Authen", Controller = "Role", Action = "Index", Icon = "", Code = "2001", OrderSort = 2, Description = null, IsMenu = true, Enabled = true, IsDeleted = false, CreateBy = "admin", CreateId = 1, CreateTime = DateTime.Now, ModifyId = 1, ModifyBy = "admin", ModifyTime = DateTime.Now },
                new Module { Id = 3, ParentId = 1, Name = "区域管理", LinkUrl = "Authen/Area/Index", Area = "Authen", Controller = "Area", Action = "Index", Icon = "", Code = "2002", OrderSort = 3, Description = null, IsMenu = true, Enabled = true, IsDeleted = false, CreateBy = "admin", CreateId = 1, CreateTime = DateTime.Now, ModifyId = 1, ModifyBy = "admin", ModifyTime = DateTime.Now },
                new Module { Id = 4, ParentId = 1, Name = "用户管理", LinkUrl = "Authen/User/Index", Area = "Authen", Controller = "User", Action = "Index", Icon = "", Code = "2003", OrderSort = 4, Description = null, IsMenu = true, Enabled = true, IsDeleted = false, CreateBy = "admin", CreateId = 1, CreateTime = DateTime.Now, ModifyId = 1, ModifyBy = "admin", ModifyTime = DateTime.Now },
                new Module { Id = 5, ParentId = 1, Name = "模块管理", LinkUrl = "Authen/Module/Index", Area = "Authen", Controller = "Module", Action = "Index", Icon = "", Code = "2004", OrderSort = 5, Description = null, IsMenu = true, Enabled = true, IsDeleted = false, CreateBy = "admin", CreateId = 1, CreateTime = DateTime.Now, ModifyId = 1, ModifyBy = "admin", ModifyTime = DateTime.Now },
                new Module { Id = 6, ParentId = 1, Name = "权限管理", LinkUrl = "Authen/Permission/Index", Area = "Authen", Controller = "Permission", Action = "Index", Icon = "", Code = "2005", OrderSort = 6, Description = null, IsMenu = true, Enabled = true, IsDeleted = false, CreateBy = "admin", CreateId = 1, CreateTime = DateTime.Now, ModifyId = 1, ModifyBy = "admin", ModifyTime = DateTime.Now },
               
              
                new Module { Id = 7, ParentId = null, Name = "系统应用", LinkUrl = null, Area = null, Controller = null, Action = null, Icon = "icon-cloud", Code = "30", OrderSort = 7, Description = null, IsMenu = true, Enabled = true, IsDeleted = false, CreateBy = "admin", CreateId = 1, CreateTime = DateTime.Now, ModifyId = 1, ModifyBy = "admin", ModifyTime = DateTime.Now },
                new Module { Id = 8, ParentId = 7, Name = "操作日志管理", LinkUrl = "SysConfig/OperateLog/Index", Area = "SysConfig", Controller = "OperateLog", Action = "Index", Icon = "", Code = "3001", OrderSort = 2, Description = null, IsMenu = true, Enabled = true, IsDeleted = false, CreateBy = "admin", CreateId = 1, CreateTime = DateTime.Now, ModifyId = 1, ModifyBy = "admin", ModifyTime = DateTime.Now },
                new Module { Id = 9, ParentId = 7, Name = "图标附录", LinkUrl = "SysConfig/Appendix/Icon", Area = "SysConfig", Controller = "Appendix", Action = "Icon", Icon = "", Code = "3002", OrderSort = 3, Description = null, IsMenu = true, Enabled = true, IsDeleted = false, CreateBy = "admin", CreateId = 1, CreateTime = DateTime.Now, ModifyId = 1, ModifyBy = "admin", ModifyTime = DateTime.Now },
                new Module { Id = 10, ParentId = null, Name = "个人资料", LinkUrl = "Common/Profile/Index", Area = "Common", Controller = "Profile", Action = "Index", Icon = "", Code = "9001", OrderSort = 1, Description = null, IsMenu = false, Enabled = true, IsDeleted = false, CreateBy = "admin", CreateId = 1, CreateTime = DateTime.Now, ModifyId = 1, ModifyBy = "amdin", ModifyTime = DateTime.Now },
                new Module { Id = 11, ParentId = null, Name = "修改密码", LinkUrl = "Common/Profile/ChangePwd", Area = "Common", Controller = "Profile", Action = "Index", Icon = "", Code = "9002", OrderSort = 1, Description = null, IsMenu = false, Enabled = true, IsDeleted = false, CreateBy = "admin", CreateId = 1, CreateTime = DateTime.Now, ModifyId = 1, ModifyBy = "amdin", ModifyTime = DateTime.Now },

                  new Module { Id = 12, ParentId = null, Name = "监控管理", LinkUrl = null, Area = null, Controller = null, Action = null, Icon = "icon-tasks", Code = "40", OrderSort = 3, Description = null, IsMenu = true, Enabled = true, IsDeleted = false, CreateBy = "admin", CreateId = 1, CreateTime = DateTime.Now, ModifyId = 1, ModifyBy = "admin", ModifyTime = DateTime.Now },
                new Module { Id = 13, ParentId = 12, Name = "系统状态", LinkUrl = "Authen/Device/SystemState", Area = "Authen", Controller = "Device", Action = "SystemState", Icon = "", Code = "4001", OrderSort = 4, Description = null, IsMenu = true, Enabled = true, IsDeleted = false, CreateBy = "admin", CreateId = 1, CreateTime = DateTime.Now, ModifyId = 1, ModifyBy = "admin", ModifyTime = DateTime.Now },
                new Module { Id = 14, ParentId = 12, Name = "时间段设置", LinkUrl = "Authen/Device/TimePeriodSetting", Area = "Authen", Controller = "Device", Action = "TimePeriodSetting", Icon = "", Code = "4002", OrderSort = 5, Description = null, IsMenu = true, Enabled = true, IsDeleted = false, CreateBy = "admin", CreateId = 1, CreateTime = DateTime.Now, ModifyId = 1, ModifyBy = "admin", ModifyTime = DateTime.Now },
                new Module { Id = 15, ParentId = 12, Name = "模式设置", LinkUrl = "Authen/Device/ModeSetting", Area = "Authen", Controller = "Device", Action = "ModeSetting", Icon = "", Code = "4003", OrderSort =6, Description = null, IsMenu = true, Enabled = true, IsDeleted = false, CreateBy = "admin", CreateId = 1, CreateTime = DateTime.Now, ModifyId = 1, ModifyBy = "admin", ModifyTime = DateTime.Now },
                 new Module { Id = 16, ParentId = 12, Name = "阀门设置", LinkUrl = "Authen/Device/ValveSetting", Area = "Authen", Controller = "Device", Action = "ValveSetting", Icon = "", Code = "4003", OrderSort =6, Description = null, IsMenu = true, Enabled = true, IsDeleted = false, CreateBy = "admin", CreateId = 1, CreateTime = DateTime.Now, ModifyId = 1, ModifyBy = "admin", ModifyTime = DateTime.Now },
                  new Module { Id = 17, ParentId = 12, Name = "温度设置", LinkUrl = "Authen/Device/TemperatureSetting", Area = "Authen", Controller = "Device", Action = "TemperatureSetting", Icon = "", Code = "4003", OrderSort =6, Description = null, IsMenu = true, Enabled = true, IsDeleted = false, CreateBy = "admin", CreateId = 1, CreateTime = DateTime.Now, ModifyId = 1, ModifyBy = "admin", ModifyTime = DateTime.Now },
                   new Module { Id = 18, ParentId = 12, Name = "终端维护", LinkUrl = "Authen/Device/TerminalSetting", Area = "Authen", Controller = "Device", Action = "TerminalSetting", Icon = "", Code = "4003", OrderSort =6, Description = null, IsMenu = true, Enabled = true, IsDeleted = false, CreateBy = "admin", CreateId = 1, CreateTime = DateTime.Now, ModifyId = 1, ModifyBy = "admin", ModifyTime = DateTime.Now },

                     new Module { Id = 19, ParentId = 12, Name = "设备地图", LinkUrl = "Authen/Device/DeviceMap", Area = "Authen", Controller = "Device", Action = "DeviceMap", Icon = "", Code = "4003", OrderSort =7, Description = null, IsMenu = true, Enabled = true, IsDeleted = false, CreateBy = "admin", CreateId = 1, CreateTime = DateTime.Now, ModifyId = 1, ModifyBy = "admin", ModifyTime = DateTime.Now },

                new Module { Id = 20, ParentId = null, Name = "设备管理", LinkUrl = null, Area = null, Controller = null, Action = null, Icon = "icon-dashboard", Code = "50", OrderSort = 2, Description = null, IsMenu = true, Enabled = true, IsDeleted = false, CreateBy = "admin", CreateId = 1, CreateTime = DateTime.Now, ModifyId = 1, ModifyBy = "admin", ModifyTime = DateTime.Now },
                new Module { Id = 21, ParentId = 20, Name = "监控显示", LinkUrl = "Authen/Device/DeviceManage", Area = "Authen", Controller = "Device", Action = "DeviceManage", Icon = "", Code = "5001", OrderSort = 4, Description = null, IsMenu = true, Enabled = true, IsDeleted = false, CreateBy = "admin", CreateId = 1, CreateTime = DateTime.Now, ModifyId = 1, ModifyBy = "admin", ModifyTime = DateTime.Now },
               
               
            };
            DbSet<Module> moduleSet = context.Set<Module>();
            moduleSet.AddOrUpdate(t => new { t.Id }, modules.ToArray());
            context.SaveChanges();

            //权限
            var permissions = new List<Permission> 
            {
                new Permission { Id = 1, Code = "Index", Name = "浏览", OrderSort = 1, Icon = null, Description = null, Enabled = true, IsDeleted = false, CreateBy = "admin", CreateId = 1, CreateTime = DateTime.Now, ModifyId = 1, ModifyBy = "amdin", ModifyTime = DateTime.Now  },
                new Permission { Id = 2, Code = "Create", Name = "新增", OrderSort = 2, Icon = "icon-plus", Description = null, Enabled = true, IsDeleted = false, CreateBy = "admin", CreateId = 1, CreateTime = DateTime.Now, ModifyId = 1, ModifyBy = "amdin", ModifyTime = DateTime.Now  },
                new Permission { Id = 3, Code = "Edit", Name = "编辑", OrderSort = 3, Icon = "icon-pencil", Description = null, Enabled = true, IsDeleted = false, CreateBy = "admin", CreateId = 1, CreateTime = DateTime.Now, ModifyId = 1, ModifyBy = "amdin", ModifyTime = DateTime.Now  },
                new Permission { Id = 4, Code = "Delete", Name = "删除", OrderSort = 4, Icon = "icon-remove", Description = null, Enabled = true, IsDeleted = false, CreateBy = "admin", CreateId = 1, CreateTime = DateTime.Now, ModifyId = 1, ModifyBy = "amdin", ModifyTime = DateTime.Now  },
                new Permission { Id = 5, Code = "SetButton", Name = "设置按钮", OrderSort = 5, Icon = "icon-legal", Description = null, Enabled = true, IsDeleted = false, CreateBy = "admin", CreateId = 1, CreateTime = DateTime.Now, ModifyId = 1, ModifyBy = "amdin", ModifyTime = DateTime.Now  },
                new Permission { Id = 6, Code = "SetPermission", Name = "设置权限", OrderSort = 6, Icon = "icon-sitemap", Description = null, Enabled = true, IsDeleted = false, CreateBy = "admin", CreateId = 1, CreateTime = DateTime.Now, ModifyId = 1, ModifyBy = "amdin", ModifyTime = DateTime.Now  },
                new Permission { Id = 7, Code = "ChangePwd", Name = "修改密码", OrderSort = 7, Icon = "icon-key", Description = null, Enabled = true, IsDeleted = false, CreateBy = "admin", CreateId = 1, CreateTime = DateTime.Now, ModifyId = 1, ModifyBy = "amdin", ModifyTime = DateTime.Now  },
                new Permission { Id = 8, Code = "DeleteAll", Name = "删除全部", OrderSort = 8, Icon = "icon-trash", Description = null, Enabled = true, IsDeleted = false, CreateBy = "admin", CreateId = 1, CreateTime = DateTime.Now, ModifyId = 1, ModifyBy = "amdin", ModifyTime = DateTime.Now  },
                new Permission { Id = 9, Code = "SetArea", Name = "设置区域", OrderSort = 9, Icon = "icon-sitemap", Description = null, Enabled = true, IsDeleted = false, CreateBy = "admin", CreateId = 1, CreateTime = DateTime.Now, ModifyId = 1, ModifyBy = "amdin", ModifyTime = DateTime.Now  },
                new Permission { Id = 10, Code = "Index2V", Name = "经理查看", OrderSort = 10, Icon = null, Description = null, Enabled = true, IsDeleted = false, CreateBy = "admin", CreateId = 1, CreateTime = DateTime.Now, ModifyId = 1, ModifyBy = "amdin", ModifyTime = DateTime.Now  },
                new Permission { Id = 11, Code = "View", Name = "查看", OrderSort = 11, Icon = "icon-search", Description = null, Enabled = true, IsDeleted = false, CreateBy = "admin", CreateId = 1, CreateTime = DateTime.Now, ModifyId = 1, ModifyBy = "amdin", ModifyTime = DateTime.Now  },
                 new Permission { Id = 12, Code = "SystemState", Name = "系统状态", OrderSort = 11, Icon = null, Description = null, Enabled = true, IsDeleted = false, CreateBy = "admin", CreateId = 1, CreateTime = DateTime.Now, ModifyId = 1, ModifyBy = "amdin", ModifyTime = DateTime.Now  },
                   new Permission { Id = 13, Code = "TimePeriodSetting", Name = "时间段设置", OrderSort = 11, Icon = null, Description = null, Enabled = true, IsDeleted = false, CreateBy = "admin", CreateId = 1, CreateTime = DateTime.Now, ModifyId = 1, ModifyBy = "amdin", ModifyTime = DateTime.Now  },
                     new Permission { Id = 14, Code = "ModeSetting", Name = "模式设置", OrderSort = 11, Icon = null, Description = null, Enabled = true, IsDeleted = false, CreateBy = "admin", CreateId = 1, CreateTime = DateTime.Now, ModifyId = 1, ModifyBy = "amdin", ModifyTime = DateTime.Now  },
                       new Permission { Id = 15, Code = "ValveSetting", Name = "阀门设置", OrderSort = 11, Icon = null, Description = null, Enabled = true, IsDeleted = false, CreateBy = "admin", CreateId = 1, CreateTime = DateTime.Now, ModifyId = 1, ModifyBy = "amdin", ModifyTime = DateTime.Now  },
                         new Permission { Id = 16, Code = "TemperatureSetting", Name = "温度设置", OrderSort = 11, Icon = null, Description = null, Enabled = true, IsDeleted = false, CreateBy = "admin", CreateId = 1, CreateTime = DateTime.Now, ModifyId = 1, ModifyBy = "amdin", ModifyTime = DateTime.Now  },
                           new Permission { Id = 17, Code = "TerminalSetting", Name = "终端维护", OrderSort = 11, Icon = null, Description = null, Enabled = true, IsDeleted = false, CreateBy = "admin", CreateId = 1, CreateTime = DateTime.Now, ModifyId = 1, ModifyBy = "amdin", ModifyTime = DateTime.Now  },
                              new Permission { Id = 18, Code = "DeviceMap", Name = "终端维护", OrderSort = 12, Icon = null, Description = null, Enabled = true, IsDeleted = false, CreateBy = "admin", CreateId = 1, CreateTime = DateTime.Now, ModifyId = 1, ModifyBy = "amdin", ModifyTime = DateTime.Now  },
                new Permission { Id = 19, Code = "DeviceManage", Name = "设备监控", OrderSort = 12, Icon = null, Description = null, Enabled = true, IsDeleted = false, CreateBy = "admin", CreateId = 1, CreateTime = DateTime.Now, ModifyId = 1, ModifyBy = "amdin", ModifyTime = DateTime.Now  },
            };
            DbSet<Permission> permissionSet = context.Set<Permission>();
            permissionSet.AddOrUpdate(t => new { t.Id }, permissions.ToArray());
            context.SaveChanges();

            //模块-权限
            var modulePermissions = new List<ModulePermission> 
            {
                new ModulePermission { Id = 1, ModuleId = 2, PermissionId = 1, IsDeleted = false, CreateBy = "admin", CreateId = 1, CreateTime = DateTime.Now, ModifyId = 1, ModifyBy = "amdin", ModifyTime = DateTime.Now  },
                new ModulePermission { Id = 2, ModuleId = 2, PermissionId = 2, IsDeleted = false, CreateBy = "admin", CreateId = 1, CreateTime = DateTime.Now, ModifyId = 1, ModifyBy = "amdin", ModifyTime = DateTime.Now  },
                new ModulePermission { Id = 3, ModuleId = 2, PermissionId = 3, IsDeleted = false, CreateBy = "admin", CreateId = 1, CreateTime = DateTime.Now, ModifyId = 1, ModifyBy = "amdin", ModifyTime = DateTime.Now  },
                new ModulePermission { Id = 4, ModuleId = 2, PermissionId = 4, IsDeleted = false, CreateBy = "admin", CreateId = 1, CreateTime = DateTime.Now, ModifyId = 1, ModifyBy = "amdin", ModifyTime = DateTime.Now  },
                new ModulePermission { Id = 5, ModuleId = 2, PermissionId = 6, IsDeleted = false, CreateBy = "admin", CreateId = 1, CreateTime = DateTime.Now, ModifyId = 1, ModifyBy = "amdin", ModifyTime = DateTime.Now  },

                new ModulePermission { Id = 6, ModuleId = 3, PermissionId = 1, IsDeleted = false, CreateBy = "admin", CreateId = 1, CreateTime = DateTime.Now, ModifyId = 1, ModifyBy = "amdin", ModifyTime = DateTime.Now  },
                new ModulePermission { Id = 7, ModuleId = 3, PermissionId = 2, IsDeleted = false, CreateBy = "admin", CreateId = 1, CreateTime = DateTime.Now, ModifyId = 1, ModifyBy = "amdin", ModifyTime = DateTime.Now  },
                new ModulePermission { Id = 8, ModuleId = 3, PermissionId = 3, IsDeleted = false, CreateBy = "admin", CreateId = 1, CreateTime = DateTime.Now, ModifyId = 1, ModifyBy = "amdin", ModifyTime = DateTime.Now  },
                new ModulePermission { Id = 9, ModuleId = 3, PermissionId = 4, IsDeleted = false, CreateBy = "admin", CreateId = 1, CreateTime = DateTime.Now, ModifyId = 1, ModifyBy = "amdin", ModifyTime = DateTime.Now  },
                new ModulePermission { Id = 10, ModuleId = 3, PermissionId = 7, IsDeleted = false, CreateBy = "admin", CreateId = 1, CreateTime = DateTime.Now, ModifyId = 1, ModifyBy = "amdin", ModifyTime = DateTime.Now  },
                 new ModulePermission { Id = 30, ModuleId = 3, PermissionId = 9, IsDeleted = false, CreateBy = "admin", CreateId = 1, CreateTime = DateTime.Now, ModifyId = 1, ModifyBy = "amdin", ModifyTime = DateTime.Now  },

                new ModulePermission { Id = 11, ModuleId = 4, PermissionId = 1, IsDeleted = false, CreateBy = "admin", CreateId = 1, CreateTime = DateTime.Now, ModifyId = 1, ModifyBy = "amdin", ModifyTime = DateTime.Now  },
                new ModulePermission { Id = 12, ModuleId = 4, PermissionId = 2, IsDeleted = false, CreateBy = "admin", CreateId = 1, CreateTime = DateTime.Now, ModifyId = 1, ModifyBy = "amdin", ModifyTime = DateTime.Now  },
                new ModulePermission { Id = 13, ModuleId = 4, PermissionId = 3, IsDeleted = false, CreateBy = "admin", CreateId = 1, CreateTime = DateTime.Now, ModifyId = 1, ModifyBy = "amdin", ModifyTime = DateTime.Now  },
                new ModulePermission { Id = 14, ModuleId = 4, PermissionId = 4, IsDeleted = false, CreateBy = "admin", CreateId = 1, CreateTime = DateTime.Now, ModifyId = 1, ModifyBy = "amdin", ModifyTime = DateTime.Now  },
                new ModulePermission { Id = 15, ModuleId = 4, PermissionId = 5, IsDeleted = false, CreateBy = "admin", CreateId = 1, CreateTime = DateTime.Now, ModifyId = 1, ModifyBy = "amdin", ModifyTime = DateTime.Now  },

                new ModulePermission { Id = 16, ModuleId = 5, PermissionId = 1, IsDeleted = false, CreateBy = "admin", CreateId = 1, CreateTime = DateTime.Now, ModifyId = 1, ModifyBy = "amdin", ModifyTime = DateTime.Now  },
                new ModulePermission { Id = 17, ModuleId = 5, PermissionId = 2, IsDeleted = false, CreateBy = "admin", CreateId = 1, CreateTime = DateTime.Now, ModifyId = 1, ModifyBy = "amdin", ModifyTime = DateTime.Now  },
                new ModulePermission { Id = 18, ModuleId = 5, PermissionId = 3, IsDeleted = false, CreateBy = "admin", CreateId = 1, CreateTime = DateTime.Now, ModifyId = 1, ModifyBy = "amdin", ModifyTime = DateTime.Now  },
                new ModulePermission { Id = 19, ModuleId = 5, PermissionId = 4, IsDeleted = false, CreateBy = "admin", CreateId = 1, CreateTime = DateTime.Now, ModifyId = 1, ModifyBy = "amdin", ModifyTime = DateTime.Now  },

                
                new ModulePermission { Id = 20, ModuleId = 6, PermissionId = 1, IsDeleted = false, CreateBy = "admin", CreateId = 1, CreateTime = DateTime.Now, ModifyId = 1, ModifyBy = "amdin", ModifyTime = DateTime.Now  },
                new ModulePermission { Id = 21, ModuleId = 6, PermissionId = 2, IsDeleted = false, CreateBy = "admin", CreateId = 1, CreateTime = DateTime.Now, ModifyId = 1, ModifyBy = "amdin", ModifyTime = DateTime.Now  },
                new ModulePermission { Id = 22, ModuleId = 6, PermissionId = 3, IsDeleted = false, CreateBy = "admin", CreateId = 1, CreateTime = DateTime.Now, ModifyId = 1, ModifyBy = "amdin", ModifyTime = DateTime.Now  },
                new ModulePermission { Id = 23, ModuleId = 6, PermissionId = 4, IsDeleted = false, CreateBy = "admin", CreateId = 1, CreateTime = DateTime.Now, ModifyId = 1, ModifyBy = "amdin", ModifyTime = DateTime.Now  },


              
                new ModulePermission { Id = 26, ModuleId = 8, PermissionId = 1, IsDeleted = false, CreateBy = "admin", CreateId = 1, CreateTime = DateTime.Now, ModifyId = 1, ModifyBy = "amdin", ModifyTime = DateTime.Now  },
                new ModulePermission { Id = 27, ModuleId = 9, PermissionId = 1, IsDeleted = false, CreateBy = "admin", CreateId = 1, CreateTime = DateTime.Now, ModifyId = 1, ModifyBy = "amdin", ModifyTime = DateTime.Now  },
          
                
             
                 new ModulePermission { Id = 44, ModuleId = 13, PermissionId = 1, IsDeleted = false, CreateBy = "admin", CreateId = 1, CreateTime = DateTime.Now, ModifyId = 1, ModifyBy = "amdin", ModifyTime = DateTime.Now  },
              
                 new ModulePermission { Id = 48, ModuleId = 14, PermissionId = 1, IsDeleted = false, CreateBy = "admin", CreateId = 1, CreateTime = DateTime.Now, ModifyId = 1, ModifyBy = "amdin", ModifyTime = DateTime.Now  },
              
                 new ModulePermission { Id = 49, ModuleId = 15, PermissionId = 1, IsDeleted = false, CreateBy = "admin", CreateId = 1, CreateTime = DateTime.Now, ModifyId = 1, ModifyBy = "amdin", ModifyTime = DateTime.Now  },
              
                  new ModulePermission { Id = 53, ModuleId = 16, PermissionId = 1, IsDeleted = false, CreateBy = "admin", CreateId = 1, CreateTime = DateTime.Now, ModifyId = 1, ModifyBy = "amdin", ModifyTime = DateTime.Now  },
               
                 new ModulePermission { Id = 57, ModuleId = 17, PermissionId = 1, IsDeleted = false, CreateBy = "admin", CreateId = 1, CreateTime = DateTime.Now, ModifyId = 1, ModifyBy = "amdin", ModifyTime = DateTime.Now  },
             
                 new ModulePermission { Id = 61, ModuleId = 18, PermissionId = 1, IsDeleted = false, CreateBy = "admin", CreateId = 1, CreateTime = DateTime.Now, ModifyId = 1, ModifyBy = "amdin", ModifyTime = DateTime.Now  },
                 new ModulePermission { Id = 62, ModuleId = 19, PermissionId = 1, IsDeleted = false, CreateBy = "admin", CreateId = 1, CreateTime = DateTime.Now, ModifyId = 1, ModifyBy = "amdin", ModifyTime = DateTime.Now  },
                new ModulePermission { Id = 63, ModuleId = 21, PermissionId = 1, IsDeleted = false, CreateBy = "admin", CreateId = 1, CreateTime = DateTime.Now, ModifyId = 1, ModifyBy = "amdin", ModifyTime = DateTime.Now  },

             
            };

            DbSet<ModulePermission> modulePermissionsSet = context.Set<ModulePermission>();
            modulePermissionsSet.AddOrUpdate(t => new { t.Id }, modulePermissions.ToArray());
            context.SaveChanges();

            //角色-模块-权限
            var roleModulePermissions = new List<RoleModulePermission> 
            {
                 new RoleModulePermission { Id = 1, RoleId = 1, ModuleId = 1, PermissionId = null, IsDeleted = false, CreateBy = "admin", CreateId = 1, CreateTime = DateTime.Now, ModifyId = 1, ModifyBy = "amdin", ModifyTime = DateTime.Now  },

                new RoleModulePermission { Id = 2, RoleId = 1, ModuleId = 2, PermissionId = 1, IsDeleted = false, CreateBy = "admin", CreateId = 1, CreateTime = DateTime.Now, ModifyId = 1, ModifyBy = "amdin", ModifyTime = DateTime.Now  },
                new RoleModulePermission { Id = 3, RoleId = 1, ModuleId = 2, PermissionId = 2, IsDeleted = false, CreateBy = "admin", CreateId = 1, CreateTime = DateTime.Now, ModifyId = 1, ModifyBy = "amdin", ModifyTime = DateTime.Now  },
                new RoleModulePermission { Id = 4, RoleId = 1, ModuleId = 2, PermissionId = 3, IsDeleted = false, CreateBy = "admin", CreateId = 1, CreateTime = DateTime.Now, ModifyId = 1, ModifyBy = "amdin", ModifyTime = DateTime.Now  },
                new RoleModulePermission { Id = 5, RoleId = 1, ModuleId = 2, PermissionId = 4, IsDeleted = false, CreateBy = "admin", CreateId = 1, CreateTime = DateTime.Now, ModifyId = 1, ModifyBy = "amdin", ModifyTime = DateTime.Now  },
                new RoleModulePermission { Id = 6, RoleId = 1, ModuleId = 2, PermissionId = 6, IsDeleted = false, CreateBy = "admin", CreateId = 1, CreateTime = DateTime.Now, ModifyId = 1, ModifyBy = "amdin", ModifyTime = DateTime.Now  },

                new RoleModulePermission { Id = 7, RoleId = 1, ModuleId = 3, PermissionId = 1, IsDeleted = false, CreateBy = "admin", CreateId = 1, CreateTime = DateTime.Now, ModifyId = 1, ModifyBy = "amdin", ModifyTime = DateTime.Now  },
                new RoleModulePermission { Id = 8, RoleId = 1, ModuleId = 3, PermissionId = 2, IsDeleted = false, CreateBy = "admin", CreateId = 1, CreateTime = DateTime.Now, ModifyId = 1, ModifyBy = "amdin", ModifyTime = DateTime.Now  },
                new RoleModulePermission { Id = 9, RoleId = 1, ModuleId = 3, PermissionId = 3, IsDeleted = false, CreateBy = "admin", CreateId = 1, CreateTime = DateTime.Now, ModifyId = 1, ModifyBy = "amdin", ModifyTime = DateTime.Now  },
                new RoleModulePermission { Id = 10, RoleId = 1, ModuleId = 3, PermissionId = 4, IsDeleted = false, CreateBy = "admin", CreateId = 1, CreateTime = DateTime.Now, ModifyId = 1, ModifyBy = "amdin", ModifyTime = DateTime.Now  },
                new RoleModulePermission { Id = 11, RoleId = 1, ModuleId = 3, PermissionId = 7, IsDeleted = false, CreateBy = "admin", CreateId = 1, CreateTime = DateTime.Now, ModifyId = 1, ModifyBy = "amdin", ModifyTime = DateTime.Now  },
                new RoleModulePermission { Id = 40, RoleId = 1, ModuleId = 3, PermissionId = 9, IsDeleted = false, CreateBy = "admin", CreateId = 1, CreateTime = DateTime.Now, ModifyId = 1, ModifyBy = "amdin", ModifyTime = DateTime.Now  },

                new RoleModulePermission { Id = 12, RoleId = 1, ModuleId = 4, PermissionId = 1, IsDeleted = false, CreateBy = "admin", CreateId = 1, CreateTime = DateTime.Now, ModifyId = 1, ModifyBy = "amdin", ModifyTime = DateTime.Now  },
                new RoleModulePermission { Id = 13, RoleId = 1, ModuleId = 4, PermissionId = 2, IsDeleted = false, CreateBy = "admin", CreateId = 1, CreateTime = DateTime.Now, ModifyId = 1, ModifyBy = "amdin", ModifyTime = DateTime.Now  },
                new RoleModulePermission { Id = 14, RoleId = 1, ModuleId = 4, PermissionId = 3, IsDeleted = false, CreateBy = "admin", CreateId = 1, CreateTime = DateTime.Now, ModifyId = 1, ModifyBy = "amdin", ModifyTime = DateTime.Now  },
                new RoleModulePermission { Id = 15, RoleId = 1, ModuleId = 4, PermissionId = 4, IsDeleted = false, CreateBy = "admin", CreateId = 1, CreateTime = DateTime.Now, ModifyId = 1, ModifyBy = "amdin", ModifyTime = DateTime.Now  },
                new RoleModulePermission { Id = 16, RoleId = 1, ModuleId = 4, PermissionId = 5, IsDeleted = false, CreateBy = "admin", CreateId = 1, CreateTime = DateTime.Now, ModifyId = 1, ModifyBy = "amdin", ModifyTime = DateTime.Now  },
                new RoleModulePermission { Id = 41, RoleId = 1, ModuleId = 4, PermissionId = 9, IsDeleted = false, CreateBy = "admin", CreateId = 1, CreateTime = DateTime.Now, ModifyId = 1, ModifyBy = "amdin", ModifyTime = DateTime.Now  },

                new RoleModulePermission { Id = 17, RoleId = 1, ModuleId = 5, PermissionId = 1, IsDeleted = false, CreateBy = "admin", CreateId = 1, CreateTime = DateTime.Now, ModifyId = 1, ModifyBy = "amdin", ModifyTime = DateTime.Now  },
                new RoleModulePermission { Id = 18, RoleId = 1, ModuleId = 5, PermissionId = 2, IsDeleted = false, CreateBy = "admin", CreateId = 1, CreateTime = DateTime.Now, ModifyId = 1, ModifyBy = "amdin", ModifyTime = DateTime.Now  },
                new RoleModulePermission { Id = 19, RoleId = 1, ModuleId = 5, PermissionId = 3, IsDeleted = false, CreateBy = "admin", CreateId = 1, CreateTime = DateTime.Now, ModifyId = 1, ModifyBy = "amdin", ModifyTime = DateTime.Now  },
                new RoleModulePermission { Id = 20, RoleId = 1, ModuleId = 5, PermissionId = 4, IsDeleted = false, CreateBy = "admin", CreateId = 1, CreateTime = DateTime.Now, ModifyId = 1, ModifyBy = "amdin", ModifyTime = DateTime.Now  },

                 new RoleModulePermission { Id = 21, RoleId = 1, ModuleId = 6, PermissionId = 1, IsDeleted = false, CreateBy = "admin", CreateId = 1, CreateTime = DateTime.Now, ModifyId = 1, ModifyBy = "amdin", ModifyTime = DateTime.Now  },
                new RoleModulePermission { Id = 22, RoleId = 1, ModuleId = 6, PermissionId = 2, IsDeleted = false, CreateBy = "admin", CreateId = 1, CreateTime = DateTime.Now, ModifyId = 1, ModifyBy = "amdin", ModifyTime = DateTime.Now  },
                new RoleModulePermission { Id = 23, RoleId = 1, ModuleId = 6, PermissionId = 3, IsDeleted = false, CreateBy = "admin", CreateId = 1, CreateTime = DateTime.Now, ModifyId = 1, ModifyBy = "amdin", ModifyTime = DateTime.Now  },
                new RoleModulePermission { Id = 24, RoleId = 1, ModuleId = 6, PermissionId = 4, IsDeleted = false, CreateBy = "admin", CreateId = 1, CreateTime = DateTime.Now, ModifyId = 1, ModifyBy = "amdin", ModifyTime = DateTime.Now  },


                new RoleModulePermission { Id = 50, RoleId = 1, ModuleId = 7, PermissionId = null, IsDeleted = false, CreateBy = "admin", CreateId = 1, CreateTime = DateTime.Now, ModifyId = 1, ModifyBy = "amdin", ModifyTime = DateTime.Now  },

              
                new RoleModulePermission { Id = 25, RoleId = 1, ModuleId = 8, PermissionId = 1, IsDeleted = false, CreateBy = "admin", CreateId = 1, CreateTime = DateTime.Now, ModifyId = 1, ModifyBy = "amdin", ModifyTime = DateTime.Now  },
                new RoleModulePermission { Id = 26, RoleId = 1, ModuleId = 9, PermissionId = 1, IsDeleted = false, CreateBy = "admin", CreateId = 1, CreateTime = DateTime.Now, ModifyId = 1, ModifyBy = "amdin", ModifyTime = DateTime.Now  },
           
            
               
                 new RoleModulePermission { Id = 50, RoleId = 1, ModuleId = 12, PermissionId = null, IsDeleted = false, CreateBy = "admin", CreateId = 1, CreateTime = DateTime.Now, ModifyId = 1, ModifyBy = "amdin", ModifyTime = DateTime.Now  },
                
                 new RoleModulePermission { Id = 51, RoleId = 1, ModuleId = 13, PermissionId = 1, IsDeleted = false, CreateBy = "admin", CreateId = 1, CreateTime = DateTime.Now, ModifyId = 1, ModifyBy = "amdin", ModifyTime = DateTime.Now  },
                new RoleModulePermission { Id = 52, RoleId = 1, ModuleId = 13, PermissionId = 12, IsDeleted = false, CreateBy = "admin", CreateId = 1, CreateTime = DateTime.Now, ModifyId = 1, ModifyBy = "amdin", ModifyTime = DateTime.Now  },
                
                 new RoleModulePermission { Id = 51, RoleId = 1, ModuleId = 14, PermissionId = 1, IsDeleted = false, CreateBy = "admin", CreateId = 1, CreateTime = DateTime.Now, ModifyId = 1, ModifyBy = "amdin", ModifyTime = DateTime.Now  },
                new RoleModulePermission { Id = 52, RoleId = 1, ModuleId = 14, PermissionId = 13, IsDeleted = false, CreateBy = "admin", CreateId = 1, CreateTime = DateTime.Now, ModifyId = 1, ModifyBy = "amdin", ModifyTime = DateTime.Now  },
              
                 new RoleModulePermission { Id = 51, RoleId = 1, ModuleId = 15, PermissionId = 1, IsDeleted = false, CreateBy = "admin", CreateId = 1, CreateTime = DateTime.Now, ModifyId = 1, ModifyBy = "amdin", ModifyTime = DateTime.Now  },
                new RoleModulePermission { Id = 52, RoleId = 1, ModuleId = 15, PermissionId = 14, IsDeleted = false, CreateBy = "admin", CreateId = 1, CreateTime = DateTime.Now, ModifyId = 1, ModifyBy = "amdin", ModifyTime = DateTime.Now  },
               
                 new RoleModulePermission { Id = 55, RoleId = 1, ModuleId = 16, PermissionId = 1, IsDeleted = false, CreateBy = "admin", CreateId = 1, CreateTime = DateTime.Now, ModifyId = 1, ModifyBy = "amdin", ModifyTime = DateTime.Now  },
                new RoleModulePermission { Id = 56, RoleId = 1, ModuleId = 16, PermissionId = 15, IsDeleted = false, CreateBy = "admin", CreateId = 1, CreateTime = DateTime.Now, ModifyId = 1, ModifyBy = "amdin", ModifyTime = DateTime.Now  },
               
                 new RoleModulePermission { Id = 59, RoleId = 1, ModuleId = 17, PermissionId = 1, IsDeleted = false, CreateBy = "admin", CreateId = 1, CreateTime = DateTime.Now, ModifyId = 1, ModifyBy = "amdin", ModifyTime = DateTime.Now  },
              new RoleModulePermission { Id = 60, RoleId = 1, ModuleId = 17, PermissionId = 16, IsDeleted = false, CreateBy = "admin", CreateId = 1, CreateTime = DateTime.Now, ModifyId = 1, ModifyBy = "amdin", ModifyTime = DateTime.Now  },
              
                 new RoleModulePermission { Id = 63, RoleId = 1, ModuleId = 18, PermissionId = 1, IsDeleted = false, CreateBy = "admin", CreateId = 1, CreateTime = DateTime.Now, ModifyId = 1, ModifyBy = "amdin", ModifyTime = DateTime.Now  },
                new RoleModulePermission { Id = 64, RoleId = 1, ModuleId = 18, PermissionId = 17, IsDeleted = false, CreateBy = "admin", CreateId = 1, CreateTime = DateTime.Now, ModifyId = 1, ModifyBy = "amdin", ModifyTime = DateTime.Now  },

                new RoleModulePermission { Id = 64, RoleId = 1, ModuleId = 19, PermissionId = 18, IsDeleted = false, CreateBy = "admin", CreateId = 1, CreateTime = DateTime.Now, ModifyId = 1, ModifyBy = "amdin", ModifyTime = DateTime.Now  },
              
                new RoleModulePermission { Id = 65, RoleId = 1, ModuleId = 20, PermissionId = null, IsDeleted = false, CreateBy = "admin", CreateId = 1, CreateTime = DateTime.Now, ModifyId = 1, ModifyBy = "amdin", ModifyTime = DateTime.Now  },
                
                new RoleModulePermission { Id = 66, RoleId = 1, ModuleId = 21, PermissionId = 19, IsDeleted = false, CreateBy = "admin", CreateId = 1, CreateTime = DateTime.Now, ModifyId = 1, ModifyBy = "amdin", ModifyTime = DateTime.Now  },
            
              
            };

            DbSet<RoleModulePermission> roleModulePermissionSet = context.Set<RoleModulePermission>();
            roleModulePermissionSet.AddOrUpdate(t => new { t.Id }, roleModulePermissions.ToArray());
            context.SaveChanges();
        }
    }
}
