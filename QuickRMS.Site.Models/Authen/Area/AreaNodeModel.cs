using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickRMS.Site.Models.Authen.Area
{
    public class AreaNodeModel
    {
       public string Text { get; set; }
       public int Id { get; set; }
       public int ParentId { get; set; }
       public List<AreaNodeModel> Nodes { get; set; }
    }
}
