using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Net.Mail;

namespace Quick.Framework.Mail.Base
{

    public class Email : MailMessage
    {
        private Guid _mailId;
        private DateTime _nextTryTime;
        private Int32 _numberOfTries;
        private List<string> _attachment;

        private DateTime _createTime;
        internal Email()
            : base()
        {
            _mailId = Guid.Empty;
            _attachment = new List<string>();
            _nextTryTime = DateTime.Now;
            _numberOfTries = 0;
            _createTime = DateTime.Now;
        }

        public Guid MailId
        {
            get
            {
                if (_mailId == Guid.Empty)
                {
                    _mailId = Guid.NewGuid();
                }
                return _mailId;
            }
            set { _mailId = value; }
        }

        public DateTime NextTryTime
        {
            get { return _nextTryTime; }
            set { _nextTryTime = value; }
        }

        public Int32 NumberOfTries
        {
            get { return _numberOfTries; }
            set { _numberOfTries = value; }
        }

        public List<string> Attachment
        {
            get { return _attachment; }
            set { _attachment = value; }
        }

        public DateTime CreateTime
        {
            get { return _createTime; }
            set { _createTime = value; }
        }
    }

}
