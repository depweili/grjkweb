using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuickRMS.Core.Repository.DeviceInfo;
using QuickRMS.Domain.Models.DeviceInfo;

namespace QuickRMS.Core.Service.Authen.Impl
{
    [Export(typeof(IHistoryService))]
  public  class HistoryService : CoreServiceBase, IHistoryService
    {

        [Import]
        public IHistoryRepository HistoryRepository { get; set; }
        public IQueryable<History> Histories
        {
            get { return HistoryRepository.Entities; }
        }
    }
}
