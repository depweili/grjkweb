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
    [Export(typeof(IDeviceCureLibraryService))]
    public class DeviceCureLibraryService : CoreServiceBase,IDeviceCureLibraryService
    {
        [Import]
        public IDeviceCureLibraryRepository DeviceCureLibraryRepository { get; set; }

        public IQueryable<DeviceCureLibrary> DeviceCureLibraries
        {
            get { return DeviceCureLibraryRepository.Entities; }
        }


        public List<DeviceCureLibrary> GetDeviceCureLibraries(int deviceId)
        {
            return DeviceCureLibraries.Where(r => r.DeviceId == deviceId).ToList();
        }
    }
}
