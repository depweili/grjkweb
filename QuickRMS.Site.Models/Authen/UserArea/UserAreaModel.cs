using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickRMS.Site.Models.Authen.UserArea
{
    public class UserAreaModel : IEquatable<UserAreaModel>
    {
        public int Id { get; set; }


        [Required(ErrorMessage = "用户不能为空")]
        public int UserId { get; set; }

        [Required(ErrorMessage = "机构不能为空")]
        public int AreaId { get; set; }

         [Display(Name = "机构名称")]
        public string AreaName { get; set; }

       
        public bool Equals(UserAreaModel uowpm)
        {

            //Check whether the compared object is null. 
            if (Object.ReferenceEquals(uowpm, null)) return false;

            //Check whether the compared object references the same data. 
            if (Object.ReferenceEquals(this, uowpm)) return true;

            //Check whether the objects properties are equal. 
            return UserId.Equals(uowpm.UserId) && AreaId.Equals(uowpm.AreaId);
        }

        public override int GetHashCode()
        {
          
            int hashUserId = UserId.GetHashCode();
          
            int hashOrgId = AreaId.GetHashCode();


            return hashUserId ^ hashOrgId;
        }
    }
}
