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
    internal class MailJobElement : ConfigurationElement
    {


        private MailJobElement()
        {
        }

        [ConfigurationProperty("isArchiveFailure", DefaultValue = "false", IsRequired = false)]
        public virtual bool IsArchiveFailure
        {
            get { return Convert.ToBoolean(this["isArchiveFailure"]); }
            set { this["isArchiveFailure"] = value; }
        }

        [IntegerValidator(MinValue = 1, MaxValue = 65535, ExcludeRange = false)]
        [ConfigurationProperty("sendInterval", DefaultValue = "10", IsRequired = true)]
        public virtual int SendInterval
        {
            get { return Convert.ToInt32(this["sendInterval"]); }
            set { this["sendInterval"] = value; }
        }
        [IntegerValidator(MinValue = 1, MaxValue = 65535, ExcludeRange = false)]
        [ConfigurationProperty("failureInterval", DefaultValue = "10", IsRequired = true)]
        public virtual int FailureInterval
        {
            get { return Convert.ToInt32(this["failureInterval"]); }
            set { this["failureInterval"] = value; }
        }
        [IntegerValidator(MinValue = 1, MaxValue = 65535, ExcludeRange = false)]
        [ConfigurationProperty("numberOfTries", DefaultValue = "3", IsRequired = true)]
        public virtual int NumberOfTries
        {
            get { return Convert.ToInt32(this["numberOfTries"]); }
            set { this["numberOfTries"] = value; }
        }
    }

}
