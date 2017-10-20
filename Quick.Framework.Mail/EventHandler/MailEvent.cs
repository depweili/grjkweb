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
    /// The Mail Event basic class 
    /// </summary> 
    public class MailEvent
    {
        public virtual void OnAddingMail(object sender, MailAddingMailEventArgs e)
        {
        }

        public virtual void OnAddedMail(object sender, MailAddedMailEventArgs e)
        {
        }

        public virtual void OnDeletingMail(object sender, MailDeletingMailEventArgs e)
        {
        }

        public virtual void OnDeletedMail(object sender, MailDeletedMailEventArgs e)
        {
        }

        public virtual void OnDequeueMail(object sender, MailDequeueMailEventArgs e)
        {
        }

        public virtual void OnExceedMailMaxTryTimes(object sender, MailExceedMailMaxTryTimesEventArgs e)
        {
        }

        public virtual void OnExceedQueueSize(object sender, MailExceedQueueSizeEventArgs e)
        {
        }

        public virtual void OnSendingMail(object sender, MailSendingMailEventArgs e)
        {
        }

        public virtual void OnSendedMail(object sender, MailSendedMailEventArgs e)
        {
        }

        public virtual void OnMailError(object sender, MailErrorEventArgs e)
        {
        }
    }

}
