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

    /// <summary> 
    /// The Mail Customize Config Section 
    /// </summary> 
    internal class MailSection : ConfigurationSection
    {
        [ConfigurationProperty("enable", DefaultValue = "true", IsRequired = false)]
        public virtual bool Enable
        {
            get { return Convert.ToBoolean(this["enable"]); }
            set { this["enable"] = value; }
        }

        [ConfigurationProperty("enableSSL", DefaultValue = "false", IsRequired = false)]
        public virtual bool EnableSSL
        {
            get { return Convert.ToBoolean(this["enableSSL"]); }
            set { this["enableSSL"] = value; }
        }

        [ConfigurationProperty("eventReceiver", DefaultValue = "", IsRequired = false)]
        public string EventReceiver
        {
            get { return this["eventReceiver"].ToString(); }
            set { this["eventReceiver"] = value; }
        }


        [ConfigurationProperty("queue", IsRequired = true)]
        public MailQueueElement Queue
        {
            get { return (MailQueueElement)this["queue"]; }
        }


        [ConfigurationProperty("job", IsRequired = true)]
        public MailJobElement Job
        {
            get { return (MailJobElement)this["job"]; }
        }
    }

}
