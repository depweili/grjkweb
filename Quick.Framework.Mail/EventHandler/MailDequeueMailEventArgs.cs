using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Quick.Framework.Mail.Base;

namespace Quick.Framework.Mail.EventHandler
{
    /// <summary> 
    /// Custom Event Args for Mail event 
    /// </summary> 
    /// <remarks></remarks> 
    public class MailDequeueMailEventArgs : EventArgs
    {

        private List<Email> _emailList;
        public List<Email> EmailList
        {
            get { return _emailList; }
            set { _emailList = value; }
        }
    }

}
