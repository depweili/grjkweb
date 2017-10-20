using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Quick.Framework.Mail.EventHandler
{
    public class MailDeletingMailEventArgs : CancelEventArgs
    {

        private Guid _emailID;
        public Guid EmailID
        {
            get { return _emailID; }
            set { _emailID = value; }
        }
    }

}
