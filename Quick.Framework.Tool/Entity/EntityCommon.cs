using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Quick.Framework.Tool.Entity
{
    /// <summary>
    /// 数据公用实体配置
    /// </summary>
    [Serializable]
    public class EntityCommon
    {
        #region 构造函数

        /// <summary>
        /// 数据实体基类
        /// </summary>
        protected EntityCommon()
        {

        }

        #endregion

        #region 属性
        /// <summary>
        /// 创建者Id
        /// </summary>
        [Column("CreateId")]
        public int? CreateId { get; set; }

        /// <summary>
        /// 创建者名称
        /// </summary>
        [MaxLength(50)]
        [Column("CreateBy")]
        public string CreateBy { get; set; }

        /// <summary>
        /// 创建日期
        /// </summary>
        [DataType(DataType.DateTime)]
        [Column("CreateTime")]
        public DateTime? CreateTime { get; set; }

        /// <summary>
        /// 修改者Id
        /// </summary>
        [Column("ModifyId")]
        public int? ModifyId { get; set; }

        /// <summary>
        /// 修改者名称
        /// </summary>
        [MaxLength(50)]
        [Column("ModifyBy")]
        public string ModifyBy { get; set; }

        /// <summary>
        /// 修改者日期
        /// </summary>
        [DataType(DataType.DateTime)]
        [Column("ModifyTime")]
        public DateTime? ModifyTime { get; set; }
        #endregion
    }
}
