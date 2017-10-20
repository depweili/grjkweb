using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuickRMS.Domain.Models.DeviceInfo;

namespace QuickRMS.Core.Service.Authen
{
    public interface IHistoryService
    {
        IQueryable<History> Histories { get; }
    }
}
