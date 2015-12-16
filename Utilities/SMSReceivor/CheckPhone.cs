using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;
using System.Threading;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using System.Web;
using Utilities.Factory;
using Utilities.SMSReceivor;

// 短信验证
namespace Utilities
{
    public enum SMSType
    {
        LOGIN,//登录
        PHONENUM,//获取电话号码
        CHECKCODE,//获取验证码

    }
    public class SMSResult
    {
        // 操作状态，标示是否成功!
        public bool Status { get; set; }
        // 
        public string Result { get; set; }

        public SMSType OpType { get; set; }

    }
    public class SMSBase
    {
        protected string user;// 用户账号
        protected string passwd;// 用户密码
        protected string author_id;// 开发ID
        protected string projID;//项目ID
        protected string token;
        protected string mobilenum;
        protected SMSBase(string userName, string pwd, string pid)
        {
            user = userName;
            passwd = pwd;
            projID = pid;
            author_id = "";
            mobilenum = "";
        }


        /// <summary>
        /// 是否已经登录
        /// </summary>
        /// <returns></returns>
        public bool IsLogined()
       {
            if (token != "")
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// 登录
        /// </summary>
        /// <returns></returns>
        public virtual SMSResult Login()
        {
            return null;
        }
        /// <summary>
        /// 获取电话号码
        /// </summary>
        /// <returns></returns>
        public virtual SMSResult GetPhoneNum()
        {
            return null;
        }

        /// <summary>
        /// 获取短信验证码
        /// </summary>
        /// <returns></returns>
        public virtual SMSResult GetMessage()
        {
            return null;
        }
        /// <summary>
        /// 获取短信验证码，并释放手机号
        /// </summary>
        /// <returns></returns>
        public virtual SMSResult GetMessageAndRealase()
        {
            return null;
        }
        public virtual SMSResult AddIgnoreList()
        {
            return null;
        }
        /// <summary>
        /// 释放所有手机号码
        /// </summary>
        /// <returns></returns>
        public virtual SMSResult ReleasePhoneNum(string phoneNum)
        {
            return null;
        }
        public virtual SMSResult ReleasePhoneNum()
        {
            return null;
        }
        public virtual SMSResult ReleaseAll()
        {
            return null;
        }
    }
    public class FQSMS : SMSBase
    {
        string url;
        HttpTool send;
        public FQSMS(string userName, string pwd, string pid):
            base(userName, pwd, pid)
        {
           url = "http://sms.xudan123.com/do.aspx";
           send = new HttpTool();
        }
        public override SMSResult Login()
        {
            SMSResult result = new SMSResult();
            result.OpType = SMSType.LOGIN;
            string postdata = "action=loginIn&uid=" + user + "&pwd=" + passwd;
            string html = getHTML(url, postdata);
            if (checkMsg(ref html) == true)
            {
                token = html.Substring(html.IndexOf("|") + 1);
                result.Status = true;
                result.Result = token;
            }
            else
            {
                result.Status = false;
                result.Result = html;
            }
            return result;
        }

        public override SMSResult GetPhoneNum()
        {
            SMSResult result = new SMSResult();
            result.OpType = SMSType.PHONENUM;
            string postdata = "action=getMobilenum&pid=" + projID + "&uid=" + user + "&token=" + token;
            string html = getHTML(url, postdata);
            string token1;
            if (checkMsg(ref html) == true)
            {
                mobilenum = html.Substring(0, html.IndexOf("|"));
                token1 = html.Substring(html.IndexOf("|") + 1);

                result.Status = true;
                result.Result = mobilenum;
            }
            else
            {
                result.Status = false;
                result.Result = html;
            }
            return result;
        }

        public override SMSResult GetMessage()
        {
            SMSResult result = new SMSResult();
            result.OpType = SMSType.CHECKCODE;
            string postdata = "action=getVcodeAndReleaseMobile&mobile=" + mobilenum + "&token=" + token + "&uid=" + user
                + "&author_uid=devspy";
            string html = getHTML(url, postdata);
            if (checkMsg(ref html) == true)
            {
                result.Status = true;
                result.Result = html.Substring(html.LastIndexOf("|") + 1);
            }
            else
            {
                result.Status = false;
                result.Result = html;
            }
            return result;
        }

        public override SMSResult AddIgnoreList()
        {
            SMSResult resp = new SMSResult();
            String result = "";

            try
            {
                result = send.HttpPost(url,
                        "action=addIgnoreList&uid=" + user + "&token=" + token
                                + "&mobiles=" + mobilenum + "&pid=" + projID);
                if (isNumber(result))
                {
                    resp.Status = true;
                    resp.Result = result;

                }
                else
                {
                    resp.Status = false;
                    resp.Result = result;
                }

            }
            catch (Exception e)
            {
                resp.Status = false;
            }
            return resp;
        }
        public override SMSResult GetMessageAndRealase()
        {
            SMSResult result = new SMSResult();
            string postdata = "action=getVcodeAndReleaseMobile&mobile=" + mobilenum + "&token=" + token + "&uid=" + user;
            string html = getHTML(url, postdata);
            if (checkMsg(ref html) == true)
            {
                result.Status = true;
                result.Result = html.Substring(html.LastIndexOf("|") + 1);
            }
            else
            {
                result.Status = false;
                result.Result = html;
            }
            return result;
        }
        public override SMSResult ReleasePhoneNum()
        {
            return ReleasePhoneNum(mobilenum);
        }
         public override SMSResult ReleasePhoneNum(string phoneNum)
        {
            SMSResult resp = new SMSResult();
            String result = "";

            try
            {
                string postdata = "action=cancelSMSRecv&mobile=" + phoneNum + "&token=" + token + "&uid=" + user;
                result = send.HttpPost(url,postdata);
                if (isNumber(result))
                {
                    resp.Status = true;
                    resp.Result = result;

                }
                else
                {
                    resp.Status = false;
                    resp.Result = result;
                }

            }
            catch (Exception e)
            {
                resp.Status = false;
            }
            return resp;
        }
         public override SMSResult ReleaseAll()
         {
             SMSResult resp = new SMSResult();
             String result = "";

             try
             {
                 string postdata = "action=cancelSMSRecvAll&uid=" + user + "&token=" + token;
                 result = send.HttpPost(url, postdata);
                 if (isNumber(result))
                 {
                     resp.Status = true;
                     resp.Result = result;

                 }
                 else
                 {
                     resp.Status = false;
                     resp.Result = result;
                 }

             }
             catch (Exception e)
             {
                 resp.Status = false;
             }
             return resp;
         }
        Regex regex = new Regex("\\d");
        private bool isNumber(string str)
        {
            return regex.Match(str).Success;
        }
        public string getHTML(string url, string postdata)
        {
            string html = "";
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "post";
                request.ContentType = "application/x-www-form-urlencoded";
                Encoding encoding = Encoding.Default;
                byte[] buffer = encoding.GetBytes(postdata);
                request.ContentLength = buffer.Length;
                request.GetRequestStream().Write(buffer, 0, buffer.Length);
                HttpWebResponse response;
                response = (HttpWebResponse)request.GetResponse();
                Stream st = response.GetResponseStream();
                StreamReader reader = new StreamReader(st, System.Text.Encoding.GetEncoding("utf-8"));
                html = reader.ReadToEnd();
            }
            catch (System.Exception ex)
            {
                html = ex.Message;
            }

            return html;
        }
        public Boolean checkMsg(ref string html)
        {
            switch (html)
            {
                case "unknow_error":
                    html = "unknow_error:未知错误,如果在获取号码或获取验证码时返回则再次请求就会正确返回.";
                    return false;
                case "not_login":
                    html = "not_login:没有登录,在没有登录下去访问需要登录的资源，忘记传入uid,token";
                    return false;
                case "not_found_project":
                    html = "not_found_project:没有找到项目,项目ID不正确";
                    return false;
                case "not_found_moblie":
                    html = "not_found_moblie:没有找到手机号";
                    return false;
                case "login_error":
                    html = "login_error:用户名密码错误";
                    return false;
                case "mobile_exists":
                    html = "mobile_exists:手机号己存在";
                    return false;
                case "not_receive":
                    html = "not_receive:还没有接收到验证码,请让程序等待几秒后再次尝试";
                    return false;
                case "parameter_error":
                    html = "parameter_error:传入参数错误";
                    return false;
                case "no_data":
                    html = "no_data:没有数据";
                    return false;
                case "project_state_error":
                    html = "project_state_error:项目状态不对,可能项目还没通过审核";
                    return false;
                case "mobile_state_error":
                    html = "mobile_state_error:手机号状态不对,立即放弃该号码，调用getMobilenum获取新的号码，不用加黑";
                    return false;
                case "max_count_disable":
                    html = "max_count_disable:已经达到了可以获取手机号的最大数量，请调用cancelSMSRecvAll释放所有手机号后再获取手机号";
                    return false;
                default:
                    return true;
            }

        }
    }

    // 一码平台
    public class YIMSMS : SMSBase
    {
        string url;
        HttpTool send;
        public YIMSMS(string userName, string pwd, string pid) :
            base(userName, pwd, pid)
        {
            url = "http://www.yzm1.com/api/do.php";
            send = new HttpTool();
        }
        public override SMSResult Login()
        {
            SMSResult result = new SMSResult();
            result.OpType = SMSType.LOGIN;
            string postdata = "action=loginIn&name=" + user + "&password=" + passwd;
            string html = getHTML(url, postdata);
            if (checkMsg(ref html) == true)
            {
                token = html.Substring(html.IndexOf("|") + 1);
                result.Status = true;
                result.Result = token;
            }
            else
            {
                result.Status = false;
                result.Result = html;
            }
            return result;
        }

        public override SMSResult GetPhoneNum()
        {
            SMSResult result = new SMSResult();
            result.OpType = SMSType.PHONENUM;
            string postdata = "action=getPhone&sid=" + projID + "&token=" + token;
            string html = getHTML(url, postdata);
            string token1;
            if (checkMsg(ref html) == true)
            {
               // mobilenum = html.Substring(0, html.IndexOf("|"));
                mobilenum = html.Substring(html.IndexOf("|") + 1);

                result.Status = true;
                result.Result = mobilenum;
            }
            else
            {
                result.Status = false;
                result.Result = html;
            }
            return result;
        }

        public override SMSResult GetMessage()
        {
            SMSResult result = new SMSResult();
            result.OpType = SMSType.CHECKCODE;
            string postdata = "action=getMessage&sid=" + projID + "&phone=" + mobilenum + "&token=" + token + "&uid=" + user
                + "&author=msguser";
            string html = getHTML(url, postdata);
            if (checkMsg(ref html) == true)
            {
                result.Status = true;
                result.Result = html.Substring(html.LastIndexOf("|") + 1);
            }
            else
            {
                result.Status = false;
                result.Result = html;
            }
            return result;
        }

        public override SMSResult AddIgnoreList()
        {
            SMSResult resp = new SMSResult();
            String result = "";

            try
            {
                result = send.HttpPost(url,
                        "action=addBlacklist&uid=" + user + "&token=" + token
                                + "&phone=" + mobilenum + "&sid=" + projID);
                if (isNumber(result))
                {
                    resp.Status = true;
                    resp.Result = result;

                }
                else
                {
                    resp.Status = false;
                    resp.Result = result;
                }

            }
            catch (Exception e)
            {
                resp.Status = false;
            }
            return resp;
        }
        public override SMSResult GetMessageAndRealase()
        {
            SMSResult result = new SMSResult();
            result.Status = false;
            return result;
//             string postdata = "action=getVcodeAndReleaseMobile&mobile=" + mobilenum + "&token=" + token + "&uid=" + user;
//             string html = getHTML(url, postdata);
//             if (checkMsg(ref html) == true)
//             {
//                 result.Status = true;
//                 result.Result = html.Substring(html.LastIndexOf("|") + 1);
//             }
//             else
//             {
//                 result.Status = false;
//                 result.Result = html;
//             }
//             return result;
        }
        public override SMSResult ReleasePhoneNum()
        {
            return ReleasePhoneNum(mobilenum);
        }
        public override SMSResult ReleasePhoneNum(string phoneNum)
        {
            SMSResult resp = new SMSResult();
            String result = "";

            try
            {
                string postdata = "action=cancelRecv&phone=" + phoneNum + "&token=" + token + "&sid=" + projID;
                result = send.HttpPost(url, postdata);
                if (isNumber(result))
                {
                    resp.Status = true;
                    resp.Result = result;

                }
                else
                {
                    resp.Status = false;
                    resp.Result = result;
                }

            }
            catch (Exception e)
            {
                resp.Status = false;
            }
            return resp;
        }
        public override SMSResult ReleaseAll()
        {
            SMSResult resp = new SMSResult();
            String result = "";

            try
            {
                string postdata = "action=cancelAllRecv&token=" + token;
                result = send.HttpPost(url, postdata);
                if (isNumber(result))
                {
                    resp.Status = true;
                    resp.Result = result;

                }
                else
                {
                    resp.Status = false;
                    resp.Result = result;
                }

            }
            catch (Exception e)
            {
                resp.Status = false;
            }
            return resp;
        }
        Regex regex = new Regex("\\d");
        private bool isNumber(string str)
        {
            return regex.Match(str).Success;
        }
        public string getHTML(string url, string postdata)
        {
            string html = "";
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "post";
                request.ContentType = "application/x-www-form-urlencoded";
                Encoding encoding = Encoding.Default;
                byte[] buffer = encoding.GetBytes(postdata);
                request.ContentLength = buffer.Length;
                request.GetRequestStream().Write(buffer, 0, buffer.Length);
                HttpWebResponse response;
                response = (HttpWebResponse)request.GetResponse();
                Stream st = response.GetResponseStream();
                StreamReader reader = new StreamReader(st, System.Text.Encoding.GetEncoding("utf-8"));
                html = reader.ReadToEnd();
            }
            catch (System.Exception ex)
            {
                html = ex.Message;
            }

            return html;
        }
        public Boolean checkMsg(ref string html)
        {
            string[] statusLines = html.Split( new char[]{'|'});
            if (statusLines[0] == "1")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
    //爱玛平台
    public class AimaSMS : SMSBase
    {
        public class LogonResp
        {
            /**
             * 调用状态
             */
            private bool state = false;

            public bool State
            {
                get { return state; }
                set { state = value; }
            }
            private String uid;

            public String Uid
            {
                get { return uid; }
                set { uid = value; }
            }
            private String token;

            public String Token
            {
                get { return token; }
                set { token = value; }
            }

            /**
             * 接口响应结果
             */
            private String result;

            public String Result
            {
                get { return result; }
                set { result = value; }
            }


        }

        private HttpTool send = new HttpTool();
        private string url;
        public AimaSMS(string userName, string pwd, string pid) :
            base(userName, pwd, pid)
        {
            url = "http://api.f02.cn/http.do";
        }
        public override SMSResult Login()
        {
            SMSResult smsRnt = new SMSResult();
            string result = "";
            try
            {
                result = send.HttpPost(url, "action=loginIn&uid=" + user + "&pwd=" + passwd);
                smsRnt.Result = result;
                String[] reset = result.Split('|');

                if (reset.Length >= 2 && user.Equals(reset[0]))
                {

                    smsRnt.Status = true;
                  //  resp.Uid = reset[0];
                    token = reset[1];
                  //  resp.Token = reset[1];
                }
                else
                {
                    smsRnt.Status = false;
                    smsRnt.Result = result;
                }
            }
            catch (Exception e)
            {
                smsRnt.Status = false;
            }
            return smsRnt;
        }

        public override SMSResult GetPhoneNum()
        {
            SMSResult resp = new SMSResult();
            String result = "";
            try
            {
                result = send.HttpPost(url, "action=getMobilenum&uid="
                        + user + "&token=" + token + "&pid=" + projID);

                String[] reset = result.Split('|');

                Regex regex = new Regex("\\d");
                if (reset.Length >= 2 && regex.Match(reset[0]).Success)
                {

                    resp.Status = true;
                    resp.Result = reset[0];
                    mobilenum = resp.Result;

                }
                else
                {
                    resp.Status = false;
                    resp.Result = result;
                }

            }
            catch (Exception e)
            {
                resp.Result = "获取一个手机号，账号：" + user + ",token：" + token
                + ",pid:" + projID + ",e=" + e.ToString();
                resp.Status = false;
            }
            return resp;
        }

        public override SMSResult GetMessageAndRealase()
        {
            SMSResult resp = new SMSResult();
            String result = "";
            try
            {
                result = send.HttpPost(url,
                        "action=getVcodeAndHoldMobilenum&uid=" + user + "&token="
                                + token + "&mobile=" + mobilenum + "&next_pid="
                                + projID);

                // 返回值：发送号码|验证码|下次获取验证码的token(暂时无用)
                String[] reset = result.Split('|');
                if (reset.Length >= 2 && isNumber(reset[0]))
                {
                    resp.Status = true;
                    resp.Result = reset[1];

                }
                else
                {
                    resp.Status = false;
                    resp.Result = result;
                }

            }
            catch (Exception e)
            {
                resp.Result = e.Message;
                resp.Status = false;
            }
            return resp;
        }

        public override SMSResult GetMessage()
        {
            SMSResult resp = new SMSResult();
            String result = "";
            try
            {
//                 result = send.HttpPost(url,
//                         "action=getVcodeAndReleaseMobile&uid=" + user + "&token="
//                                 + token + "&mobile=" + mobilenum + "&author_uid="
//                                 + author_uid);

                result = send.HttpPost(url,
                        "action=getVcodeAndReleaseMobile&uid=" + user + "&token="
                                + token + "&mobile=" + mobilenum + "&author_uid=msguser");

                String[] reset = result.Split('|');
                if (reset.Length >= 2 && isNumber(reset[0]))
                {
                    resp.Status = true;
                    //resp.Mobile = reset[0];
                    resp.Result = reset[1];
                 //   resp.Result = result;

                }
                else
                {
                    resp.Status = false;
                    resp.Result = result;
                }
            
            }
            catch (Exception e)
            {
                resp.Result = e.Message;
                resp.Status = false;
            }
            return resp;
        }
        Regex regex = new Regex("\\d");
        private bool isNumber(string str)
        {
            return regex.Match(str).Success;
        }
        public override SMSResult AddIgnoreList()
        {
            SMSResult resp = new SMSResult();
            string result = "";

            try
            {
                result = send.HttpPost(url,
                        "action=addIgnoreList&uid=" + user + "&token=" + token
                                + "&mobiles=" + mobilenum + "&pid=" + projID);

                if (isNumber(result))
                {
                    resp.Status = true;
                   // resp.Row = int.Parse(result);
                    resp.Result = result;

                }
                else
                {
                    resp.Status = false;
                    resp.Result = result;
                }

            }
            catch (Exception e)
            {
                resp.Result = e.Message;
                resp.Status = false;
            }
            return resp;
        }

        public override SMSResult ReleasePhoneNum(string phoneNum)
        {
            SMSResult resp = new SMSResult();
            String result = "";
            try
            {
                result = send.HttpPost(url,
                        "action=cancelSMSRecv&uid=" + user + "&token=" + token
                                + "&mobile=" + phoneNum);

                if ("1".Equals(result))
                {
                    resp.Status = true;
                    //resp.Flag = result;
                    resp.Result = result;
                }
                else
                {
                    resp.Status = false;
                   // resp.Flag = result;
                    resp.Result = result;
                }

            }
            catch (Exception e)
            {
                resp.Result = e.Message;
                resp.Status = false;
            }
            return resp;
        }

        public override SMSResult ReleasePhoneNum()
        {
           return ReleasePhoneNum(mobilenum);
        }

        public override SMSResult ReleaseAll()
        {
            SMSResult resp = new SMSResult();
            String result = "";

            try
            {
                result = send.HttpPost(url,
                        "action=cancelSMSRecvAll&uid=" + user + "&token=" + token);

                if ("1".Equals(result))
                {
                    resp.Status = true;
                    resp.Result = result;
                }
                else
                {
                    resp.Status = false;
                    resp.Result = result;
                }

            }
            catch (Exception e)
            {
                resp.Result = e.Message;
                resp.Status = false;
            }
            return resp;
        }
    }
    // 51平台
    public class WYSMS : SMSBase
    {
        private static readonly HttpHelper http = new HttpHelper();
        private static string loginCookies;
        private static string cookies;
        public static bool logined;
        private string url;

        public WYSMS(string userName, string pwd, string pid) :
            base(userName, pwd, pid)
        {
            url = "http://www.jikesms.com/common/ajax.htm";
        }
        public override SMSResult Login()
        {
            SMSResult rnt = new SMSResult();

            string result = DoPost(string.Format("action={2}&event_name_login={3}&uid={0}&password={1}", user, MD5Encrypt(passwd).ToUpper(), "user%3aUserEventAction", "%E5%8F%96%E6%89%8B%E6%9C%BA%E5%8F%B7"));
            if (result.Contains("登录成功"))
            {
                logined = true;
                rnt.Status = true;
                rnt.Result = "登录成功";
            }
            loginCookies = cookies;
            return rnt;
        }

        public override SMSResult GetPhoneNum()
        {
            SMSResult rnt = new SMSResult();
            rnt.Status = false;
            if (logined)
            {
                string result = DoPost(string.Format("event_name_getPhone=%E5%8F%96%E6%89%8B%E6%9C%BA%E5%8F%B7&action=phone%3APhoneEventAction&serviceId={0}", projID));
                if (result.Contains("true"))
                {
                    string m = Regex.Match(result, @"(\d{11})", RegexOptions.Multiline).Groups[1].Value;
                    if (!string.IsNullOrEmpty(m))
                    {
                        rnt.Result = m;
                        rnt.Status = true;
                        mobilenum = m;
                    }
                    else
                    {
                        rnt.Status = false;
                        rnt.Result = "未获取到号码";
                    }
                }
            }
            else
            {
                rnt.Result = "请先登录";
            }
            return rnt;
        }

        public override SMSResult GetMessage()
        {
            SMSResult rnt = new SMSResult() { Status =false };
            if (logined)
            {
                string result = DoPost(string.Format("event_name_getMessage=%E5%8F%96%E7%9F%AD%E4%BF%A1&action=phone%3APhoneEventAction&serviceId={0}&phone={1}", projID, mobilenum));
                if (result.Contains("true"))
                {
                    string m = result.Split(new string[] { "message" }, StringSplitOptions.None)[1].Replace('"', ' ').Trim();
                    rnt.Result = m;
                    rnt.Status = true;
                }
                else
                {
                    rnt.Result = "尚未收到验证码";
                    rnt.Status = false;
                }
            }
            else
            {
                rnt.Result = "请先登录";
            }
            return rnt;
        }

        public override SMSResult GetMessageAndRealase()
        {
            SMSResult rnt = GetMessage();
            if (rnt.Status)
            {
                ReleasePhoneNum(mobilenum);
                
            }
            return rnt;

        }

        public override SMSResult AddIgnoreList()
        {
            SMSResult rnt = new SMSResult() { Status = false };
            if (logined)
            {
                string result = DoPost(string.Format("event_name_addBlacklist={0}&action=phone%3APhoneEventAction&serviceId={1}&phone={2}", "%E5%8F%96%E6%89%8B%E6%9C%BA%E5%8F%B7", projID, mobilenum));
                if (result.Contains("true"))
                {
                    rnt.Result = result;
                    rnt.Status = true;
                }
                else
                {
                    rnt.Result = result;
                    rnt.Status = false;
                }
            }
            else
            {
                rnt.Result = "请先登录";
            }
            return rnt;
        }

        public override SMSResult ReleasePhoneNum(string phoneNum)
        {
            SMSResult rnt = new SMSResult() { Status = false };
            if (logined)
            {
                var result = DoPost(string.Format("event_name_cancelRecv={0}&action=phone%3APhoneEventAction&serviceId={1}&phone={2}", "%E5%8F%96%E6%89%8B%E6%9C%BA%E5%8F%B7", projID, mobilenum));
                if (result.Contains("true"))
                {
                    rnt.Result = result;
                    rnt.Status = true;
                }
            }
            else
            {
                rnt.Result = "请先登录";
            }
            return rnt;
        }

        public override SMSResult ReleasePhoneNum()
        {
            return ReleasePhoneNum(mobilenum);
        }

        public override SMSResult ReleaseAll()
        {
            SMSResult rnt = new SMSResult() { Status = false };
            if (logined)
            {
                string result = DoPost(string.Format("event_name_cancelAllRecv={0}&action=phone%3APhoneEventAction", "%E5%8F%96%E6%89%8B%E6%9C%BA%E5%8F%B7"));
                if (result.Contains("true"))
                {
                    rnt.Result = result;
                    rnt.Status = true;
                }
                else
                {
                    rnt.Result = result;
                    rnt.Status = false;
                }
            }
            else
            {
                rnt.Result = "请先登录";
            }
            return rnt;
        }

        public string getHTML(string url, string postdata)
        {
            string html = "";
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "post";
                request.ContentType = "application/x-www-form-urlencoded";
                Encoding encoding = Encoding.Default;
                byte[] buffer = encoding.GetBytes(postdata);
                request.ContentLength = buffer.Length;
                request.GetRequestStream().Write(buffer, 0, buffer.Length);
                HttpWebResponse response;
                response = (HttpWebResponse)request.GetResponse();
                Stream st = response.GetResponseStream();
                StreamReader reader = new StreamReader(st, System.Text.Encoding.GetEncoding("utf-8"));
                html = reader.ReadToEnd();
            }
            catch (System.Exception ex)
            {
                html = ex.Message;
            }

            return html;
        }
        private static string DoPost(string postdata)
        {
            var item = new HttpItem()
            {
                Cookie = loginCookies,
                URL = "http://www.jikesms.com/common/ajax.htm", //URL这里都是测试URl   必需项        
                Encoding = Encoding.UTF8,
                ContentType = "application/x-www-form-urlencoded",
                Method = "post", //URL     可选项 默认为Get
                PostDataType = PostDataType.String,
                Postdata = postdata
            };
            item.Header.Add("Accept-Encoding: gzip,deflate,sdch");

            HttpResult result = http.GetHtml(item);
            cookies = result.Cookie;

            return result.Html;
        }

        public static string MD5Encrypt(string beforeStr)
        {
            string afterString = "";
            try
            {
                MD5 md5 = MD5.Create();
                byte[] hashs = md5.ComputeHash(Encoding.UTF8.GetBytes(beforeStr));

                foreach (byte by in hashs)
                    //这里是字母加上数据进行加密.//3y 可以,y3不可以或 x3j等应该是超过32位不可以
                    afterString += by.ToString("x2");
            }
            catch
            {
            }
            return afterString;
        }
    }
    public class SMSAsyn
    {
        public delegate SMSResult AsynHandler();
        private List<bool> bFinishedList;
        private List<SMSResult> rtnList;
        private int index;
        private SMSBase smsInstance = null;
        private static SMSAsyn mInstance = null;
        class CallbackObject
        {
           public AsynHandler onHander;
           public int mIndex;
        };
        private SMSAsyn()
        {
            string user;
            string passwd;
            string projId;

            switch (TBConfig.getInstance().providorId)// 1 是飞Q，2是51,3 是爱玛
            {
                case 1:
                    user = "hispy";
                    passwd = "123456flag";
                   // projId = "17";
                    projId = "21";
                    smsInstance = new FQSMS(user, passwd, projId);
                    break;
                case 2:
                    user = "spy51";
                    passwd = "hispy++";
                    projId = "2";
                    smsInstance = new WYSMS(user, passwd, projId);
                    break;

                default:
                    user = "amspy";
                    passwd = "hispy@am";
                    projId = "27";
                    smsInstance = new AimaSMS(user, passwd, projId);
                    break;


            }            
            bFinishedList = new List<bool>();
            rtnList = new List<SMSResult>();
        }
        public static SMSAsyn GetInstance()
        {
           if (mInstance == null)
           {
               mInstance = new SMSAsyn();
           }
           return mInstance;
        }
        public void AsyncCompleted(IAsyncResult ar)
        {
            if (ar != null)
            {
                CallbackObject callObj = ar.AsyncState as CallbackObject;
                int _index = callObj.mIndex;
                bFinishedList[_index] = true;
                rtnList[_index] = callObj.onHander.EndInvoke(ar);
            }
        }
        /// <summary>
        /// 登录
        /// </summary>
        /// <returns></returns>
        public int Login()
        {
            SMSResult result = new SMSResult() { Status = false };
            rtnList.Add(result);
            bFinishedList.Add(false);
            AsynHandler handler = new AsynHandler(smsInstance.Login);

            CallbackObject obj = new CallbackObject() { onHander = handler, mIndex = index };
            IAsyncResult ar = handler.BeginInvoke(AsyncCompleted, obj);
            
            return index++;
        }
        /// <summary>
        /// 获取电话号码
        /// </summary>
        /// <returns></returns>
        public int GetPhoneNum()
        {
            SMSResult result = new SMSResult() { Status = false };
            rtnList.Add(result);
            bFinishedList.Add(false);
            AsynHandler handler = new AsynHandler(smsInstance.GetPhoneNum);

            CallbackObject obj = new CallbackObject() { onHander = handler, mIndex = index };
            IAsyncResult ar = handler.BeginInvoke(AsyncCompleted, obj);

            return index++;
        }
        /// <summary>
        /// 获取验证码
        /// </summary>
        /// <returns></returns>
        public int GetMessage()
        {
            SMSResult result = new SMSResult() { Status = false };
            rtnList.Add(result);
            bFinishedList.Add(false);
            AsynHandler handler = new AsynHandler(smsInstance.GetMessage);

            CallbackObject obj = new CallbackObject() { onHander = handler, mIndex = index };
            IAsyncResult ar = handler.BeginInvoke(AsyncCompleted, obj);

            return index++;
        }
        /// <summary>
        /// 增加到黑名单
        /// </summary>
        /// <returns></returns>
        public int AddIgnoreList()
        {
            SMSResult result = new SMSResult() { Status = false };
            rtnList.Add(result);
            bFinishedList.Add(false);
            AsynHandler handler = new AsynHandler(smsInstance.AddIgnoreList);

            CallbackObject obj = new CallbackObject() { onHander = handler, mIndex = index };
            IAsyncResult ar = handler.BeginInvoke(AsyncCompleted, obj);

            return index++;
        }
        /// <summary>
        /// 释放所有电话号码
        /// </summary>
        /// <returns></returns>
        public int ReleaseAll()
        {
            SMSResult result = new SMSResult() { Status = false };
            rtnList.Add(result);
            bFinishedList.Add(false);
            AsynHandler handler = new AsynHandler(smsInstance.ReleaseAll);

            CallbackObject obj = new CallbackObject() { onHander = handler, mIndex = index };
            IAsyncResult ar = handler.BeginInvoke(AsyncCompleted, obj);

            return index++;
        }
        /// <summary>
        /// 释放当前电话号码
        /// </summary>
        /// <returns></returns>
        public int ReleaseMobile()
        {
            SMSResult result = new SMSResult() { Status = false };
            rtnList.Add(result);
            bFinishedList.Add(false);
            AsynHandler handler = new AsynHandler(smsInstance.ReleasePhoneNum);

            CallbackObject obj = new CallbackObject() { onHander = handler, mIndex = index };
            IAsyncResult ar = handler.BeginInvoke(AsyncCompleted, obj);
            return index++;
        }

        /// <summary>
        /// 如果idx任务完成则返回true,否false
        /// </summary>
        /// <param name="idx"></param>
        /// <returns></returns>
        public bool IsIdxCompleted(int idx)
        {
            if (idx < 0)
            {
                return false;
            }
            return bFinishedList[idx];
        }
        public SMSResult GetResult(int idx)
        {
            if (idx < 0)
            {
                return null;
            }
            return rtnList[idx];
        }
        public bool ClearAll()
        {
            foreach (bool b in bFinishedList)
            {
                if (b == false)
                {
                    return false;
                }
            }
            bFinishedList.Clear();
            rtnList.Clear();
            index = 0;
            return true;
        }

    }
    public class SMSProxy
    {
        private static SMSBase smsInstance = null;
        private static int type = 0;
        private static SMSResult result = new SMSResult() { Status = false};
        public static Thread AsynThread = null;
        static public void InitInstance(string user, string passwd, string projid, int type = 1)
        {
            type = 1;
            string platform = "FQ";
            switch (type)
            {
                case 1:
                    platform = "FQ";
                    break;
                case 2:
                    platform = "51";
                    break;
                case 3:
                    platform = "AM";
                    break;
                default:
                    break;
            }
            InitInstance(user, passwd, projid, platform);
        }
        static public void InitInstance(string user, string passwd, string projid, string platform)
        {

            switch (platform)
            {
                case "FQ":
                    smsInstance = new FQSMS(user, passwd, "17");
                    break;
                case "51":
                    smsInstance = new WYSMS(user, passwd, "2");
                    break;
                case "AM":
                    smsInstance = new AimaSMS(user, passwd, "27");
                    break;
                case "YM":
                    smsInstance = new YMSMS(user, passwd, "8299");
                    break;
                case "KM":
                    smsInstance = new KMSMS(user, passwd, "125");
                    break;
                case "YPY":
                    smsInstance = new YPYSMS(user, passwd, "3913");
                    break;
                case "ZM":
                    smsInstance = new ZMSMS(user, passwd, "3102");
                    break;
                default:
                    smsInstance = null;
                    break;
            }
        }
        /// <summary>
        /// 异步登录
        /// </summary>
        public static void AsynLogin()
        {
            AsynThread = new Thread(new ThreadStart(BeginLogin));
            AsynThread.Start();
        }
        static public bool IsLogined()
        {
            if (AsynThread != null)
            {
                if (result.Status && smsInstance.IsLogined())
                {
                    return true;
                }
            }
            return false;
        }
        static public SMSBase GetSmsInstance()
        {
            if (smsInstance == null)
            {
                SMSProxy.InitInstance(AccountFactory.getInstance().getSMSAcc().UserName, AccountFactory.getInstance().getSMSAcc().Passwd, AccountFactory.getInstance().getSMSAcc().ProjID,
                AccountFactory.getInstance().getSMSAcc().PlatformName);
            }
            return smsInstance;
        }
         private static void BeginLogin()
        {
             if (smsInstance == null)
             {
                 GetSmsInstance();
             }
            result = smsInstance.Login();
        }
    }
}
