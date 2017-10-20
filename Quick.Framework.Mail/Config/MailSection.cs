using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Configuration;

namespace Quick.Framework.Mail.Config
{
    internal class MailQueueElement : ConfigurationElement
    {

        private MailQueueElement()
        {
        }
        [ConfigurationProperty("queueSize", DefaultValue = int.MaxValue, IsRequired = false)]
        public virtual int QueueSize
        {
            get { return Convert.ToInt32(this["queueSize"]); }
            set { this["queueSize"] = value; }
        }

        [ConfigurationProperty("connectionName", IsRequired = true)]
        public virtual string ConnectionName
        {
            get { return this["connectionName"].ToString(); }
            set { this["connectionName"] = value; }
        }

    }

}
