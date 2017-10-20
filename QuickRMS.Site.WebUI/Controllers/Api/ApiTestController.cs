using QuickRMS.Core.Service.Authen;
using QuickRMS.Site.ApiModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.OData.Query;

namespace QuickRMS.Site.WebUI.Controllers.Api
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ApiTestController : ApiController
    {
        #region 属性
        [Import]
        public IRoleService RoleService { get; set; }
        #endregion

        [Queryable]
        public IQueryable<RoleModel> GetAll()
        {
            var model = RoleService.Roles.AsQueryable().Select(t => new RoleModel
            { 
                Name = t.Name,
                Description = t.Description
            });
            return model;
        }
    }
}
