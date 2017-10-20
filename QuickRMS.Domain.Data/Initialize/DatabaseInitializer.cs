using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;

using Quick.Framework.EFData;
using QuickRMS.Domain.Data.Migrations;

namespace QuickRMS.Domain.Data.Initialize
{
    /// <summary>
    /// 数据库初始化操作类
    /// </summary>
    public static class DatabaseInitializer
    {
        /// <summary>
        /// 数据库初始化
        /// </summary>
        public static void Initialize()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<EFDbContext, Configuration>());
			//SetSqlGenerator("System.Data.SqlClient", new NonSystemTableSqlGenerator()); 
		}
    }
}
