using LumiSoft.Net.IMAP;
using LumiSoft.Net.IMAP.Client;
using LumiSoft.Net.POP3.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace Utilities
{
    // 过滤函数
    public delegate bool Filter(string subject,object obj);
    public static class FetchEmailHtmlBody
    {
        public static string GetHtmlBody(string host, int port, string username, string password)
        {
            FetchEmailBody fetchObj = null;
            if (host.IndexOf("imap") != -1)
            {
                fetchObj = new FetchEmailFromIMAP(host,port,username,password);
            }
            else if (host.IndexOf("pop") != -1 || host.IndexOf("pop3") != -1)
            {
                fetchObj = new FetchEmailFromPOP(host, port, username, password);
            }

            if (fetchObj == null)
                return null;

            fetchObj.init();
            return fetchObj.GetHtmlBody();
                 
        }
    }
    // 上层类
    class FetchEmailBody
    {
        public Filter m_Filter;
        protected string m_user;
        protected string m_pwd;
        protected string m_host;
        protected int m_port;

        protected string m_htmlBody;// 邮件正文

        protected string filterstr;
        public FetchEmailBody(string host, int port, string username, string password)
        {
            m_user = username;
            m_pwd = password;
            m_host = host;
            m_port = port;
            filterstr = "新用户确认通知信";
            m_htmlBody = "";
            m_Filter = new Filter(defaultFilter);
            init();
        }

        public virtual void init(){}

        // 获取邮件的html boy
        public string GetHtmlBody()
        {
            return m_htmlBody;
        }
        // 默认过滤器
        public bool defaultFilter(string subject, object obj)
        {
            if (filterstr == subject)
            {
                return true;
            }
            else
                return false;
        }
    }
    class FetchEmailFromIMAP : FetchEmailBody
    {
        public FetchEmailFromIMAP(string host, int port, string username, string password)
            : base(host, port, username, password) { }
        public override void init()
        {
            using (IMAP_Client imap = new IMAP_Client())
            {
                imap.Connect(m_host, m_port);
                imap.Capability();
                imap.Login(m_user, m_pwd);

                var seqSet = LumiSoft.Net.IMAP.IMAP_t_SeqSet.Parse("1:*");
                var imap_t_Fetch_i = new IMAP_t_Fetch_i[]{
                         new IMAP_t_Fetch_i_Envelope(),//邮件的标题、正文等信息
                         new IMAP_t_Fetch_i_Flags(),//此邮件的标志，应该是已读未读标志
                         new IMAP_t_Fetch_i_InternalDate(),//貌似是收到的日期
                         new IMAP_t_Fetch_i_Rfc822(),//Rfc822是标准的邮件数据流，可以通过Lumisoft.Net.Mail.Mail_Message对象解析出邮件的所有信息（不确定有没有附件的内容）。
                         new IMAP_t_Fetch_i_Uid()
                };

                imap.SelectFolder("INBOX");
                EventHandler<LumiSoft.Net.EventArgs<IMAP_r_u>> lumisoftHandler = new EventHandler<LumiSoft.Net.EventArgs<IMAP_r_u>>(Fetchcallback);
                imap.Fetch(false, seqSet, imap_t_Fetch_i, lumisoftHandler);
            }
        }
        public void Fetchcallback(object sender, LumiSoft.Net.EventArgs<IMAP_r_u> eventArgs)
        {
            //把传入参数重新封装，用于取出邮件的相关信息
            IMAP_r_u_Fetch x = eventArgs.Value as IMAP_r_u_Fetch;
            //这是邮件的标题
            //能有效地取出神马除了取决于邮件本身外，还受到上文imap_t_Fetch_i数组中成员的影响
            if (!m_Filter(x.Envelope.Subject,null))
            {
                return;
            }
            // var st = x.Value.Rfc822.Stream;
            var st = x.Rfc822.Stream;
            st.Position = 0;
            LumiSoft.Net.Mail.Mail_Message mime = LumiSoft.Net.Mail.Mail_Message.ParseFromStream(st);
            m_htmlBody = mime.BodyHtmlText;
        }

    }
    class FetchEmailFromPOP : FetchEmailBody
    {
        public FetchEmailFromPOP(string host, int port, string username, string password)
            : base(host, port, username, password) { }
        public override void init()
        {
            using (POP3_Client popClient = new POP3_Client())
            {
                popClient.Connect(m_host, m_port);
                popClient.Login(m_user, m_pwd);

                foreach (POP3_ClientMessage message in popClient.Messages)
                {
                    LumiSoft.Net.Mail.Mail_Message mime_header = LumiSoft.Net.Mail.Mail_Message.ParseFromByte(message.HeaderToByte());
                    if (mime_header != null && m_Filter(mime_header.Subject, null))
                    {
                        m_htmlBody = mime_header.BodyHtmlText;
                        return;
                    }

                }
            }
        }
    }
}
