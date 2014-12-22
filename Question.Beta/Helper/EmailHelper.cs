using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.ComponentModel;

namespace Question.Beta.Helper
{
    /// <summary>
    /// 邮件发送辅助类
    /// </summary>
    public class EmailHelper
    {
        /// <summary>
        /// SMTP服务器地址
        /// </summary>
         string SMTPHOST = string.Empty;

        /// <summary>
        /// 发送方账号
        /// </summary>
         string SMTPACCOUNT = string.Empty;

        /// <summary>
        /// 发送方密码
        /// </summary>
         string SMTPPWD = string.Empty;

        /// <summary>
        /// 接收方账号
        /// </summary>
         string SMTPRECEIVEACCOUNT = string.Empty;

        /// <summary>
        /// 服务器返回信息
        /// </summary>
        string RETURNMSG = string.Empty;

        /// <summary>
        /// 初始化邮件服务器信息
        /// </summary>
        /// <param name="SMTPHOST">主机名</param>
        /// <param name="SMTPACCOUNT">发送账号</param>
        /// <param name="SMTPPWD">发送房密码</param>
        /// <param name="SMTPRECEIVEACCOUNT">接收方账号</param>
        public EmailHelper(string SMTPHOST, string SMTPACCOUNT, string SMTPPWD, string SMTPRECEIVEACCOUNT) 
        {
            this.SMTPHOST = SMTPHOST;
            this.SMTPACCOUNT = SMTPACCOUNT;
            this.SMTPPWD = SMTPPWD;
            this.SMTPRECEIVEACCOUNT = SMTPRECEIVEACCOUNT;
        }

        public bool SendEmail(string mailBody, string mailTitle)
        {
            SmtpClient smtpClient = new SmtpClient();
            //指定邮件发送方式
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            //邮件服务器地址
            smtpClient.Host = SMTPHOST;
            //发送方的用户名和密码
            smtpClient.Credentials = new System.Net.NetworkCredential(SMTPACCOUNT, SMTPPWD);
            //建立发送主体
            MailMessage mailMessage = new MailMessage(SMTPACCOUNT, SMTPRECEIVEACCOUNT);
            //邮件主题
            mailMessage.Subject = mailTitle;
            //邮件内容
            mailMessage.Body = mailBody;
            mailMessage.BodyEncoding = System.Text.Encoding.UTF8;
            //设置为HTML格式
            mailMessage.IsBodyHtml = true;
            //优先级
            mailMessage.Priority = MailPriority.High;
            try
            {
                smtpClient.SendCompleted += new SendCompletedEventHandler(smtpClient_SendCompleted);
                smtpClient.SendAsync(mailMessage,"usertoken");
                return true;
            }
            catch
            {
                return false;
            }
        }

        protected void smtpClient_SendCompleted(object sender, AsyncCompletedEventArgs e)
        {
            String token = (string)e.UserState;
            if (e.Cancelled)
            {
                RETURNMSG = "发送取消";
            }
            if (e.Error != null)
            {
                RETURNMSG = token + ":" + e.Error.ToString();
            }
            else
            {
                RETURNMSG = "邮件发送成功！";
            }
        }
    }
}