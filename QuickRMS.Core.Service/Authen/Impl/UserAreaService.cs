using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quick.Framework.Tool;
using QuickRMS.Core.Repository.Authen;
using QuickRMS.Domain.Models.Authen;
using QuickRMS.Site.Models.Authen.UserArea;


namespace QuickRMS.Core.Service.Authen.Impl
{
    [Export(typeof(IUserAreaService))]
    public class UserAreaService : CoreServiceBase, IUserAreaService
    {

        [Import]
        public IUserAreaRepository UserAreaRepository { get; set; }

        public IQueryable<UserArea> UserAreas
        {
            get { return UserAreaRepository.Entities; }
        }

        public OperationResult SetUserArea(UserAreaModel model)
        {
            var entity =
                UserAreas.FirstOrDefault(
                    t =>
                        t.UserId == model.UserId && t.AreaId == model.AreaId);
            if (entity == null)
            {
                entity = new UserArea
                {
                   UserId = model.UserId,
                   AreaId = model.AreaId,
                };
                UserAreaRepository.Insert(entity);
                return new OperationResult(OperationResultType.Success, "添加成功");
            }
            else
            {
                return new OperationResult(OperationResultType.Success, "已经存在");
            }
        }


        public OperationResult RemoveUserArea(UserAreaModel model)
        {

            var entity =
                UserAreas.FirstOrDefault(
                    t =>
                        t.UserId == model.UserId && t.AreaId == model.AreaId);
            if (entity == null)
            {
                
                return new OperationResult(OperationResultType.Success, "已经删除");
            }
            else
            {
                UserAreaRepository.Delete(entity);
                return new OperationResult(OperationResultType.Success, "删除成功");
            }
        }
    }
}
