﻿using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Text;

using Quick.Framework.Tool;


namespace Quick.Framework.EFData
{
    /// <summary>
    ///     数据单元操作类
    /// </summary>
    [Export(typeof(IUnitOfWork))]
    public class EFUnitOfWorkContext : UnitOfWorkContextBase
    {
        /// <summary>
        ///     获取 当前使用的数据访问上下文对象
        /// </summary>
        protected override DbContext Context
        {
            get
            {
                return EFDbContext.Value;
               // return (new EFDbContext());
                
            }
        }

        [Import("EF", typeof(DbContext))]
        private Lazy<EFDbContext> EFDbContext { get; set; }
    }
}