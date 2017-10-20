using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;
using Quick.Framework.Mail.Config;
using Quick.Framework.Mail.EventHandler;

namespace Quick.Framework.Mail.Base
{
    /// <summary> 
    /// Mail Queue, to stored the email 
    /// </summary> 
    internal class MailQueue
    {

        private int _queueSize;
        private int _count;

        private string _connectionString;
        public MailQueue(MailQueueElement queueElement)
        {
            _queueSize = queueElement.QueueSize;
            _count = 0;
            //User the Connect Name to get the Connection String. 

            _connectionString = ConfigurationManager.ConnectionStrings[queueElement.ConnectionName].ConnectionString;
        }

        /// <summary> 
        /// Get an email from the queue 
        /// </summary> 
        /// <returns></returns> 
        /// <remarks></remarks> 
        public List<Email> Dequeue()
        {
            List<Email> mailList = new List<Email>();
            try
            {
                mailList = MailQueueDB.GetMailsFromQueue(this._connectionString);
            }
            catch (SqlException ex)
            {
                //Process Mail Error event 
                MailErrorEventArgs errorArgs = new MailErrorEventArgs();
                errorArgs.Exception = ex;
                errorArgs.ErrorType = MailErrorType.Dequeue;
                OnMailError(this, errorArgs);
            }

            MailDequeueMailEventArgs args = new MailDequeueMailEventArgs();
            args.EmailList = mailList;
            //Process the Dequeue Mail event 
            OnDequeueMail(this, args);
            return mailList;
        }


        /// <summary> 
        /// Add the email to the queue 
        /// </summary> 
        /// <param name="mail"></param> 
        /// <remarks></remarks> 
        public bool Enqueue(Email mail, ref string msg)
        {
            if (this._queueSize > this._count)
            {
                try
                {
                    //process adding mail event 
                    MailAddingMailEventArgs addingMailArgs = new MailAddingMailEventArgs();
                    addingMailArgs.Email = mail;
                    OnAddingMail(this, addingMailArgs);

                    this._count = MailQueueDB.SaveMailToQueue(this._connectionString, mail);

                    //process added mail event 
                    MailAddedMailEventArgs addedMailArgs = new MailAddedMailEventArgs();
                    addedMailArgs.Email = mail;
                    OnAddedMail(this, addedMailArgs);
                }
                catch (SqlException ex)
                {
                    msg = ex.Message;
                    //Process Mail Error Event 
                    MailErrorEventArgs args = new MailErrorEventArgs();
                    args.Exception = ex;
                    args.ErrorType = MailErrorType.Enqueue;
                    OnMailError(this, args);

                    return false;
                }
                return true;
            }
            else
            {
                //process Exceed Queue Max Size event 
                MailExceedQueueSizeEventArgs args = new MailExceedQueueSizeEventArgs();
                args.QueueMaxSize = this._queueSize;
                OnExceedQueueSize(this, args);
                msg = "Mail Queue is full, please try later.";
                return false;
            }
        }


        public void Delete(Guid mailID)
        {
            //process deleteing event 
            MailDeletingMailEventArgs deletingArgs = new MailDeletingMailEventArgs();
            deletingArgs.EmailID = mailID;
            deletingArgs.Cancel = false;
            OnDeletingMail(this, deletingArgs);

            try
            {
                MailQueueDB.DeleteMailFromQueue(this._connectionString, mailID);
            }
            catch (SqlException ex)
            {
                //Process Mail Error Event 
                MailErrorEventArgs errorArgs = new MailErrorEventArgs();
                errorArgs.Exception = ex;
                errorArgs.ErrorType = MailErrorType.Delete;
                OnMailError(this, errorArgs);
            }

            //process deleted event 
            MailDeletedMailEventArgs deletedArgs = new MailDeletedMailEventArgs();

            OnDeletedMail(this, deletedArgs);
        }

        public void UpdateFailure(Guid mailID, int failureInterval, int maxNumberOfTries, bool isArchiveFailure)
        {
            MailQueueDB.UpdateFailure(this._connectionString, mailID, failureInterval, maxNumberOfTries, isArchiveFailure);
        }


        #region "event Handle"
        /// <summary> 
        /// The event is after adding the mail into the queue. 
        /// </summary> 
        /// <remarks></remarks> 
        public event EventHandler<MailAddedMailEventArgs> AddedMail;

        /// <summary> 
        /// The event is before adding a mail into the queue. 
        /// </summary> 
        /// <remarks></remarks> 
        public event EventHandler<MailAddingMailEventArgs> AddingMail;

        /// <summary> 
        /// The event is after deleting a email from the queue. 
        /// </summary> 
        /// <remarks></remarks> 
        public event EventHandler<MailDeletedMailEventArgs> DeletedMail;

        /// <summary> 
        /// The event is before deleting a email from the queue. 
        /// </summary> 
        /// <remarks></remarks> 
        public event EventHandler<MailDeletingMailEventArgs> DeletingMail;

        /// <summary> 
        /// The event is risen if the count of the mails in the queue exceeds the queues' size. 
        /// </summary> 
        /// <remarks></remarks> 
        public event EventHandler<MailExceedQueueSizeEventArgs> ExceedQueueSize;

        /// <summary> 
        /// The event is after get the mails that will be sent next time from the mail queue. 
        /// </summary> 
        /// <remarks></remarks> 
        public event EventHandler<MailDequeueMailEventArgs> DequeueMail;

        public event EventHandler<MailErrorEventArgs> MailError;

        /// <summary> 
        /// The event is risen if the times that the mail is sent exceeds the max times. 
        /// </summary> 
        /// <remarks></remarks> 
        public event EventHandler<MailExceedMailMaxTryTimesEventArgs> ExceedMailMaxTryTimes;


        public virtual void OnAddingMail(object sender, MailAddingMailEventArgs e)
        {
            if (AddingMail != null)
            {
                AddingMail(this, e);
            }
        }

        public virtual void OnAddedMail(object sender, MailAddedMailEventArgs e)
        {
            if (AddedMail != null)
            {
                AddedMail(this, e);
            }
        }

        public virtual void OnDeletingMail(object sender, MailDeletingMailEventArgs e)
        {
            if (DeletingMail != null)
            {
                DeletingMail(this, e);
            }
        }

        public virtual void OnDeletedMail(object sender, MailDeletedMailEventArgs e)
        {
            if (DeletedMail != null)
            {
                DeletedMail(this, e);
            }
        }

        public virtual void OnExceedQueueSize(object sender, MailExceedQueueSizeEventArgs e)
        {
            if (ExceedQueueSize != null)
            {
                ExceedQueueSize(this, e);
            }
        }

        public virtual void OnExceedMailMaxTryTimes(object sender, MailExceedMailMaxTryTimesEventArgs e)
        {
            if (ExceedMailMaxTryTimes != null)
            {
                ExceedMailMaxTryTimes(this, e);
            }
        }

        public virtual void OnDequeueMail(object sender, MailDequeueMailEventArgs e)
        {
            if (DequeueMail != null)
            {
                DequeueMail(this, e);
            }
        }

        public virtual void OnMailError(object sender, MailErrorEventArgs e)
        {
            if (MailError != null)
            {
                MailError(this, e);
            }
        }
        #endregion
    }

}
