using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Quick.Framework.Mail.EventHandler
{
    /// <summary> 
    /// Custom Event Args for Mail event 
    /// </summary> 
    /// <remarks></remarks> 
    public class MailExceedQueueSizeEventArgs : EventArgs
    {

        private int _queueMaxSize;
        public int QueueMaxSize
        {
            get { return _queueMaxSize; }
            set { _queueMaxSize = value; }
        }
    }

}
