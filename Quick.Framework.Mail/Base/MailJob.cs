using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Net.Mail;
using Quick.Framework.Mail.Config;
using Quick.Framework.Mail.EventHandler;

namespace Quick.Framework.Mail.Base
{
    internal class MailJob
    {

        protected SmtpClient smtp;
        private int _sendInterval;
        private int _failureInterval;
        private int _numberOfTries;

        private bool _isArchiveFailure;

        private bool _isActived;

        private MailQueue _queue;


        public int SendInterval
        {
            get { return _sendInterval; }
            private set { _sendInterval = value; }
        }

        public int FailureInterval
        {
            get { return _failureInterval; }
            private set { _failureInterval = value; }
        }

        public int NumberOfTries
        {
            get { return _numberOfTries; }
            private set { _numberOfTries = value; }
        }


        /// <summary> 
        /// The Failure Mail whether archive 
        /// </summary> 
        /// <value></value> 
        /// <returns></returns> 
        /// <remarks></remarks> 
        public bool IsArchiveFailure
        {
            get { return _isArchiveFailure; }
            set { _isArchiveFailure = value; }
        }

        public bool IsActived
        {
            get { return _isActived; }
            set { _isActived = value; }
        }


        public MailJob(MailJobElement jobSection, MailQueue queue, bool enableSSL)
        {
            _queue = queue;

            smtp = new SmtpClient();

            smtp.EnableSsl = enableSSL;
            SendInterval = jobSection.SendInterval;
            IsArchiveFailure = jobSection.IsArchiveFailure;
            FailureInterval = jobSection.FailureInterval;
            NumberOfTries = jobSection.NumberOfTries;
        }


        public void Run()
        {

            List<Email> mailList = new List<Email>();
            mailList = _queue.Dequeue();


            if (mailList != null)
            {
                foreach (Email mail in mailList)
                {
                    //Send mail 

                    try
                    {
                        //process the Sending Mail event 
                        MailSendingMailEventArgs sendingArgs = new MailSendingMailEventArgs();
                        sendingArgs.Email = mail;
                        sendingArgs.Cancel = false;
                        OnSendingMail(this, sendingArgs);

                        if (sendingArgs.Cancel == false)
                        {
                            smtp.Send(mail);
                            //process the SendedMail event 
                            MailSendedMailEventArgs sendedArgs = new MailSendedMailEventArgs();
                            //sendedArgs.Email = mail 
                            OnSendedMail(this, sendedArgs);
                        }
                        _queue.Delete(mail.MailId);
                    }
                    catch (SmtpFailedRecipientsException ex)
                    {
                        //added by linxu on 208-7-28 
                        _queue.UpdateFailure(mail.MailId, FailureInterval, NumberOfTries, IsArchiveFailure);
                        //Send Mail Error Event 
                        MailErrorEventArgs args = new MailErrorEventArgs();
                        args.ErrorType = MailErrorType.SendMail;
                        args.Exception = ex;
                        OnMailError(this, args);
                    }
                    catch (SmtpException ex)
                    {
                        //Send failed 
                        _queue.UpdateFailure(mail.MailId, FailureInterval, NumberOfTries, IsArchiveFailure);
                        //Send Mail Error Event 
                        MailErrorEventArgs args = new MailErrorEventArgs();
                        args.ErrorType = MailErrorType.SendMail;
                        args.Exception = ex;
                        OnMailError(this, args);
                    }
                    catch (ArgumentNullException ex)
                    {
                        //added by linxu on 208-7-28 
                        _queue.UpdateFailure(mail.MailId, FailureInterval, NumberOfTries, IsArchiveFailure);
                        //Send Mail Error Event 
                        MailErrorEventArgs args = new MailErrorEventArgs();
                        args.ErrorType = MailErrorType.SendMail;
                        args.Exception = ex;
                        OnMailError(this, args);
                    }
                    catch (ArgumentOutOfRangeException ex)
                    {
                        //added by linxu on 208-7-28 
                        _queue.UpdateFailure(mail.MailId, FailureInterval, NumberOfTries, IsArchiveFailure);
                        //Send Mail Error Event 
                        MailErrorEventArgs args = new MailErrorEventArgs();
                        args.ErrorType = MailErrorType.SendMail;
                        args.Exception = ex;
                        OnMailError(this, args);

                    }


                }
            }
        }

        #region "Event"
        /// <summary> 
        /// The event is risen before the mail has been sent out. 
        /// </summary> 
        /// <remarks></remarks> 
        public event EventHandler<MailSendingMailEventArgs> SendingMail;

        /// <summary> 
        /// The event is risen after the mail has been sent out. 
        /// </summary> 
        /// <remarks></remarks> 
        public event EventHandler<MailSendedMailEventArgs> SendedMail;


        public event EventHandler<MailErrorEventArgs> MailError;

        public virtual void OnSendingMail(object sender, MailSendingMailEventArgs e)
        {
            if (SendingMail != null)
            {
                SendingMail(sender, e);
            }
        }

        public virtual void OnSendedMail(object sender, MailSendedMailEventArgs e)
        {
            if (SendedMail != null)
            {
                SendedMail(sender, e);
            }
        }


        public virtual void OnMailError(object sender, MailErrorEventArgs e)
        {
            if (MailError != null)
            {
                MailError(sender, e);
            }
        }



        #endregion
    }

}
