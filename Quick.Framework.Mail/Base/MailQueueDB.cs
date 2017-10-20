using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;using System.Data.SqlClient;
using System.Net.Mail;
using System.IO;
using System.Globalization;

namespace Quick.Framework.Mail.Base
{
    internal class MailQueueDB
    {
        public static Int32 GetQueueCount(string connectionString)
        {
            SqlParameter[] sqlparams = { new SqlParameter("@QueueCount", SqlDbType.Int) };

            sqlparams[0].Direction = ParameterDirection.Output;
            Int32 queueCount = 0;


            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand command = conn.CreateCommand();
                command.CommandText = "ComLib_Mail_GetQueueCount";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(sqlparams[0]);
                command.ExecuteNonQuery();

                queueCount = (Int32)sqlparams[0].Value;
                command.Parameters.Clear();
                conn.Close();
            }

            return queueCount;
        }

        public static Int32 SaveMailToQueue(string connectionString, Email mail)
        {
            SqlParameter[] sqlparams = {
				new SqlParameter("@MailID", SqlDbType.UniqueIdentifier),
				new SqlParameter("@MailPriority", SqlDbType.Int),
				new SqlParameter("@MailFrom", SqlDbType.NVarChar),
				new SqlParameter("@MailTo", SqlDbType.NVarChar),
				new SqlParameter("@MailCc", SqlDbType.NVarChar),
				new SqlParameter("@MailBcc", SqlDbType.NVarChar),
				new SqlParameter("@MailSubject", SqlDbType.NVarChar),
				new SqlParameter("@MailBody", SqlDbType.NText),
				new SqlParameter("@IsBodyHtml", SqlDbType.Bit),
				new SqlParameter("@Attachment", SqlDbType.NVarChar),
				new SqlParameter("@QueueCount", SqlDbType.Int)
			};
            sqlparams[0].Value = mail.MailId;
            sqlparams[1].Value = mail.Priority;
            sqlparams[2].Value = mail.From.Address;
            sqlparams[3].Value = mail.To.ToString();
            sqlparams[4].Value = mail.CC.ToString();
            sqlparams[5].Value = mail.Bcc.ToString();
            sqlparams[6].Value = mail.Subject;
            sqlparams[7].Value = mail.Body;
            sqlparams[8].Value = mail.IsBodyHtml;
            string AttachStrs = string.Empty;
            bool flag = false;
            if (mail.Attachment == null)
            {
                foreach (string attachStr in mail.Attachment)
                {
                    if (flag)
                    {
                        AttachStrs += "|";
                    }
                    AttachStrs += attachStr;
                    flag = true;
                }
            }
            sqlparams[9].Value = AttachStrs;
            sqlparams[10].Value = ParameterDirection.Output;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "ComLib_Mail_Enqueue";
                foreach (IDataParameter sqlparam in sqlparams)
                {
                    if (sqlparam != null)
                    {
                        if ((sqlparam.Direction == ParameterDirection.InputOutput || sqlparam.Direction == ParameterDirection.Input) && (sqlparam.Value == null))
                        {
                            sqlparam.Value = DBNull.Value;
                        }
                        command.Parameters.Add(sqlparam);
                    }
                }
                command.ExecuteNonQuery();
                command.Parameters.Clear();
                connection.Close();
            }
            return (Int32)sqlparams[10].Value;
        }

        public static List<Email> GetMailsFromQueue(string connectionString)
        {
            DataSet ds = new DataSet();
            SqlDataAdapter adapter = null;
            Email mail = null;
            List<Email> list = new List<Email>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "ComLib_Mail_Dequeue";
                adapter = new SqlDataAdapter(command);
                adapter.Fill(ds);
                connection.Close();
                DataTable dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        mail = new Email();
                        mail.MailId = (Guid)row["MailID"];
                        mail.Priority = (MailPriority)row["MailPriority"];
                        mail.From = new MailAddress(row["MailFrom"].ToString().Trim());
                        string[] mailStrs = null;
                        if (!string.IsNullOrEmpty(row["MailTo"].ToString()))
                        {
                            mailStrs = row["MailTo"].ToString().Split(new char[] { ',' });
                            foreach (string mailToStr in mailStrs)
                            {
                                mail.To.Add(mailToStr);
                            }
                        }

                        if (!string.IsNullOrEmpty(row["MailCC"].ToString()))
                        {
                            mailStrs = row["MailCC"].ToString().Split(new char[] { ',' });
                            foreach (string mailCCStr in mailStrs)
                            {
                                mail.CC.Add(mailCCStr);
                            }
                        }

                        if (!string.IsNullOrEmpty(row["MailBcc"].ToString()))
                        {
                            mailStrs = row["MailBcc"].ToString().Split(new char[] { ',' });
                            foreach (string mailBccStr in mailStrs)
                            {
                                mail.Bcc.Add(mailBccStr);
                            }
                        }

                        mail.Subject = (string)row["MailSubject"];
                        mail.Body = (string)row["MailBody"];
                        mail.IsBodyHtml = (bool)row["IsBodyHtml"];
                        mail.NextTryTime = (DateTime)row["NextTryTime"];
                        mail.NumberOfTries = (Int32)row["NumberOfTries"];
                        string[] attachStrs = row["Attachment"].ToString().Split(new char[] { '|' });
                        if (attachStrs != null)
                        {
                            foreach (string attachStr in attachStrs)
                            {
                                if (!string.IsNullOrEmpty(attachStr))
                                {
                                    if (File.Exists(attachStr))
                                    {
                                        mail.Attachments.Add(new Attachment(attachStr));
                                    }
                                }
                            }
                        }
                        list.Add(mail);
                    }
                }
                else
                {
                    list = null;
                }
            }
            return list;
        }

        public static void UpdateFailure(string connectionString, Guid mailId, Int32 failureInterval, Int32 maxNumberOfTries, bool isArchiveFailure)
        {
            SqlParameter[] sqlparams = {
				new SqlParameter("@MailID", SqlDbType.UniqueIdentifier),
				new SqlParameter("@FailureInterval", SqlDbType.Int),
				new SqlParameter("@MaxNumberOfTries", SqlDbType.Int),
				new SqlParameter("@IsArchiveFailure", SqlDbType.Bit)
			};
            sqlparams[0].Value = mailId;
            sqlparams[1].Value = failureInterval;
            sqlparams[2].Value = maxNumberOfTries;
            sqlparams[3].Value = isArchiveFailure;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "ComLib_Mail_UpdateFailure";

                foreach (IDataParameter sqlparam in sqlparams)
                {
                    if (sqlparam != null)
                    {
                        if ((sqlparam.Direction == ParameterDirection.InputOutput || sqlparam.Direction == ParameterDirection.Input) && (sqlparam.Value == null))
                        {
                            sqlparam.Value = DBNull.Value;
                        }
                        command.Parameters.Add(sqlparam);
                    }
                }
                command.ExecuteNonQuery();
                command.Parameters.Clear();
                connection.Close();
            }
        }

        public static void DeleteMailFromQueue(string connectionString, Guid mailId)
        {
            SqlParameter[] sqlparams = { new SqlParameter("@MailID", SqlDbType.UniqueIdentifier) };
            sqlparams[0].Value = mailId;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "ComLib_Mail_Delete";
                foreach (IDataParameter sqlparam in sqlparams)
                {
                    if (sqlparam != null)
                    {
                        if ((sqlparam.Direction == ParameterDirection.InputOutput || sqlparam.Direction == ParameterDirection.Input) && (sqlparam.Value == null))
                        {
                            sqlparam.Value = DBNull.Value;
                        }
                        command.Parameters.Add(sqlparam);
                    }
                }
                command.ExecuteNonQuery();
                command.Parameters.Clear();
                connection.Close();
            }
        }

        public static Nullable<DateTime> GetNextSendTime(string connectionString)
        {
            SqlParameter[] sqlparams = { new SqlParameter("@nextTryTime", SqlDbType.DateTime) };
            sqlparams[0].Direction = ParameterDirection.Output;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "ComLib_Mail_GetNextTryTime";
                command.Parameters.Add(sqlparams[0]);
                command.ExecuteNonQuery();
                command.Parameters.Clear();
                connection.Close();

                if (sqlparams[0].Value != null && !object.ReferenceEquals(sqlparams[0].Value, DBNull.Value))
                {
                    return Convert.ToDateTime(sqlparams[0].Value, CultureInfo.InvariantCulture);
                }
                else
                {
                    return null;
                }
            }
        }
    }

}
