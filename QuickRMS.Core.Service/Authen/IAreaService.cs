using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quick.Framework.Tool;
using Quick.Site.Common.Models;
using QuickRMS.Domain.Models.Authen;
using QuickRMS.Site.Models.Authen.Area;
using QuickRMS.Site.Models.Device;


namespace QuickRMS.Core.Service.Authen
{
   public interface IAreaService
    {
        #region 属性

       IQueryable<Area> Areas { get; }

        #endregion

        #region 公共方法

       OperationResult Insert(AreaModel model);

        OperationResult Update(AreaModel model);

        /// <summary>
        /// 逻辑删除
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        OperationResult Delete(int Id);

        List<AreaDeviceNodeModel> LoadAreaDeviceByUser(int userId);
       List<AreaNodeModel> LoadAreasTree(int parentId);

       List<AreaNodeModel> LoadAreasTree();
       #endregion
    }
}
