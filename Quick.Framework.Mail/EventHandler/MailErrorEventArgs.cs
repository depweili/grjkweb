using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Quick.Framework.Mail.EventHandler
{
    public class MailErrorEventArgs : EventArgs
    {
        private Exception _exception;

        private MailErrorType _errorType;
        public Exception Exception
        {
            get { return _exception; }
            set { _exception = value; }
        }

        public MailErrorType ErrorType
        {
            get { return _errorType; }
            set { _errorType = value; }
        }
    }

}
