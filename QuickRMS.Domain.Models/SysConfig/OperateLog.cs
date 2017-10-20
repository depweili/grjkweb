using Quick.Framework.Tool.Entity;
using QuickRMS.Domain.Models.Authen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickRMS.Domain.Models.SysConfig
{
	public class OperateLog : EntityBase<long>
	{
		public string Area { get; set; }
		public string Controller { get; set; }
		public string Action { get; set; }
		public string IPAddress { get; set; }
		public string Description { get; set; }
		public DateTime? LogTime { get; set; }
		public string LoginName { get; set; }
		public int UserId { get; set; }

        public virtual User User { get; set; }
	}
}
