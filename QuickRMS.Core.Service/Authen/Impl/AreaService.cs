using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quick.Framework.Common.ToolsHelper;
using Quick.Framework.Tool;
using Quick.Site.Common.Models;
using QuickRMS.Core.Repository.Authen;
using QuickRMS.Core.Repository.DeviceInfo;
using QuickRMS.Domain.Models.Authen;
using QuickRMS.Domain.Models.DeviceInfo;
using QuickRMS.Site.Models.Authen.Area;
using QuickRMS.Site.Models.Device;

namespace QuickRMS.Core.Service.Authen.Impl
{
    [Export(typeof (IAreaService))]
    public class AreaService : CoreServiceBase, IAreaService
    {
        #region 属性

        [Import]
        public IAreaRepository AreaRepository { get; set; }

        [Import]
        public IDeviceRepository DeviceRepository { get; set; }

        [Import]
        public IUserAreaRepository UserAreaRepository { get; set; }

        public IQueryable<Device> Devices
        {
            get { return DeviceRepository.Entities; }
        }
        public IQueryable<Area> Areas
        {
            get { return AreaRepository.Entities; }
        }

        public IQueryable<UserArea> UserAreas
        {
            get { return UserAreaRepository.Entities; }
        }

        #endregion

        #region 公共方法



        public OperationResult Insert(AreaModel model)
        {
            var entity = new Area
            {
                Name = model.Name,
                ParentId = model.ParentId,
                Description = model.Description
            };
            AreaRepository.Insert(entity);
            return new OperationResult(OperationResultType.Success, "添加成功");
        }

        public OperationResult Update(AreaModel model)
        {
            var entity = Areas.First(t => t.Id == model.Id);
            entity.Name = model.Name;
            entity.Description = model.Description;
            

            AreaRepository.Update(entity);
            return new OperationResult(OperationResultType.Success, "更新成功");
        }

        public OperationResult Delete(int Id)
        {
            var model = Areas.FirstOrDefault(t => t.Id == Id);
            model.IsDeleted = true;
            AreaRepository.Update(model);
            return new OperationResult(OperationResultType.Success, "删除成功");
        }

        #endregion


        public List<AreaDeviceNodeModel> LoadAreaDeviceByUser(int userId)
        {
            var query = from a in Areas
                join b in UserAreas on a.Id equals b.AreaId
                where b.UserId == userId
                select a;

            var areas = query.ToList();
            var nodeList = Areas.OrderBy(r => r.Code).Select(r => new AreaDeviceNodeModel
            {
                Id = r.Id,
                Text = r.Name,
                AreaName=r.Name,
                Longitude = r.Longitude,
                Latitude = r.Latitude,
                ParentId = r.ParentId ?? 0
            }).ToList();

            var parentNodes = nodeList.Where(r => r.ParentId == 0).ToList();
            foreach (var p in parentNodes)
            {
                LoadChildAreaDeviceNodes(p, nodeList);
            }
            return parentNodes;
        }
        public List<AreaNodeModel> LoadAreasTree()
        {
            var nodeList = Areas.OrderBy(r => r.Code).Select(r => new AreaNodeModel
            {
                Id = r.Id,
                Text = r.Name,
                ParentId = r.ParentId ?? 0
            }).ToList();

            var parentNodes = nodeList.Where(r => r.ParentId == 0).ToList();
            foreach (var p in parentNodes)
            {
                LoadChildNodes(p, nodeList);
            }
            return parentNodes;
        }
        public List<AreaNodeModel> LoadAreasTree(int parentId)
        {
            var node = Areas.Where(r => (r.Id == parentId)).Select(r => new AreaNodeModel
            {
                Id = r.Id,
                Text = r.Name,
                ParentId = r.ParentId ?? 0
            }).FirstOrDefault();

            var nodeList = Areas.OrderBy(r => r.Code).Select(r => new AreaNodeModel
            {
                Id = r.Id,
                Text = r.Name,
                ParentId = r.ParentId ?? 0
            }).ToList();

            var list = new List<AreaNodeModel>();
            list = LoadChildNodesList(node, nodeList) ?? new List<AreaNodeModel>();
            list.Add(node);
            return list;
        }

        private List<AreaNodeModel> LoadChildNodesList(AreaNodeModel parentNode, List<AreaNodeModel> allNodes)
        {
            var childNodes = allNodes.Where(r => r.ParentId == parentNode.Id).ToList();
            var list = new List<AreaNodeModel>();
            if (childNodes.Any())
            {
                foreach (var n in childNodes)
                {
                    list.Add(n);
                    var tep=LoadChildNodesList(n,allNodes);
                    if (tep != null && tep.Count > 0)
                    {
                        list.AddRange(tep);
                    }
                }
                return list;
            }
            return null;

        }

        private List<AreaNodeModel> LoadChildNodes(AreaNodeModel parentNode, List<AreaNodeModel> allNodes)
        {
            var childNodes = allNodes.Where(r => r.ParentId == parentNode.Id).ToList();
            if (childNodes.Any())
            {
                foreach (var n in childNodes)
                {
                    n.Nodes = LoadChildNodes(n, allNodes);
                }
                parentNode.Nodes = childNodes;
                return childNodes;
            }
            return null;

        }

        private List<AreaDeviceNodeModel> LoadChildAreaDeviceNodes(AreaDeviceNodeModel parentNode, List<AreaDeviceNodeModel> allNodes)
        {
            var childNodes = allNodes.Where(r => r.ParentId == parentNode.Id).ToList();
            if (childNodes.Any())
            {
                foreach (var n in childNodes)
                {
                    n.Nodes = LoadChildAreaDeviceNodes(n, allNodes);
                    var id = n.Id;
                    var devices = (from q in Devices
                                   where q.AreaId == id
                                   select new AreaDeviceNodeModel
                                   {
                                       Text = q.DeviceName,
                                       IsOnline = q.IsOnline,
                                       AreaId = id,
                                       AreaName=q.Area.Name,
                                       Color = q.IsOnline ? "blue" : "",
                                       DeviceCode = q.DeviceCode,
                                       Id = q.Id,
                                       DeviceName = q.DeviceName,
                                       Icon = "glyphicon glyphicon-blackboard",
                                       CtrlNumber = q.CtrlNumber??0,
                                       Longitude = q.Longitude,
                                       Latitude = q.Latitude,
                                       //Tags = new List<string> { q.Id }
                                   }).ToList();
                    if (devices.Count > 0)
                    {
                        if (n.Nodes == null)
                        {
                            n.Nodes=new List<AreaDeviceNodeModel>();
                        }
                        n.Nodes.AddRange(devices);
                    }
                }
                parentNode.Nodes = childNodes;
                return childNodes;
            }
            return null;

        }



    }
}
