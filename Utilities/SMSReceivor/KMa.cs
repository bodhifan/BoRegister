using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Net;
using System.IO;

namespace Utilities.SMSReceivor
{
    public class KMSMS: SMSBase
    {
        string url;
        HttpTool send;
        public KMSMS(string userName, string pwd, string pid) :
            base(userName, pwd, pid)
        {
            url = "http://www.kuaima9.com:7002/";
           send = new HttpTool();
        }
        public override SMSResult Login()
        {
            SMSResult result = new SMSResult();
            result.OpType = SMSType.LOGIN;
            string postdata = "Api/userLogin?uName=" + user + "&pWord=" + passwd;
            string html = getHTML(url, postdata);
            if (checkMsg(ref html) == true)
            {
                token = html.Substring(0,html.IndexOf("&"));
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
            string postdata = "Api/userGetPhone?ItemId=" + projID + "&token=" + token + "&PhoneType=0";
            string html = getHTML(url, postdata);
            if (checkMsg(ref html) == true)
            {
                int idx = html.IndexOf(";");
                mobilenum = html;
                if (idx > 0)
                {
                    mobilenum = html.Substring(0, html.IndexOf(";"));
                }
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
            string postdata = "Api/userGetMessage?token=" + token;
            string html = getHTML(url, postdata);
            if (checkMsg(ref html) == true)
            {
                string startStr = "MSG&"+projID+"&"+mobilenum;
                if (html.Contains(startStr))
                {
                    result.Status = true;
                    result.Result = html.Substring(html.LastIndexOf("&") + 1);
                }
                else
                {
                    result.Status = false;
                    result.Result = html;
                }
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
            resp.Status = true;
            String result = "";

            try
            {
                string postdata = "Api/userAddBlack?token=" + token + "&phoneList="+projID+"-"+mobilenum;
                string html = getHTML(url, postdata);
                resp.Status = true;
//                 if (isNumber(result))
//                 {
//                     resp.Status = true;
//                     resp.Result = result;
// 
//                 }
//                 else
//                 {
//                     resp.Status = false;
//                     resp.Result = result;
//                 }

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
            string postdata = "Api/userReleasePhone?token=" + token + "&phoneList=" + mobilenum + "-" + projID;
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
            SMSResult result = new SMSResult();
            string postdata = "Api/userReleasePhone?token=" + token + "&phoneList=" + mobilenum + "-" + projID;
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
         public override SMSResult ReleaseAll()
         {
             SMSResult resp = new SMSResult();
             String result = "";
             resp.Status = true;
//              try
//              {
//                  string postdata = "action=ReleaseMobile&uid=" + user + "&token=" + token;
//                  result = send.HttpPost(url, postdata);
//                  if (isNumber(result))
//                  {
//                      resp.Status = true;
//                      resp.Result = result;
// 
//                  }
//                  else
//                  {
//                      resp.Status = false;
//                      resp.Result = result;
//                  }
// 
//              }
//              catch (Exception e)
//              {
//                  resp.Status = false;
//              }
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
                url += postdata;
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            //    request.Timeout = 1000 * 20;
                request.Method = "get";
                request.ContentType = "application/x-www-form-urlencoded";
                Encoding encoding = Encoding.GetEncoding("gb2312");
//                 byte[] buffer = encoding.GetBytes(postdata);
//                 request.ContentLength = buffer.Length;
//                 request.GetRequestStream().Write(buffer, 0, buffer.Length);
                HttpWebResponse response;
                response = (HttpWebResponse)request.GetResponse();
                Stream st = response.GetResponseStream();
                StreamReader reader = new StreamReader(st, System.Text.Encoding.GetEncoding("gb2312"));
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
            return true;
//             switch (html)
//             {
//                 case "unknow_error":
//                     html = "unknow_error:未知错误,如果在获取号码或获取验证码时返回则再次请求就会正确返回.";
//                     return false;
//                 case "not_login":
//                     html = "没有登录,在没有登录下去访问需要登录的资源，忘记传入uid,token";
//                     return false;
//                 case "not_found_project":
//                     html = "not_found_project:没有找到项目,项目ID不正确";
//                     return false;
//                 case "not_found_moblie":
//                     html = "not_found_moblie:没有找到手机号";
//                     return false;
//                 case "login_error":
//                     html = "login_error:用户名密码错误";
//                     return false;
//                 case "account_is_locked":
//                     html = "账号被锁定";
//                     return false;
//                 case "mobile_exists":
//                     html = "mobile_exists:手机号己存在";
//                     return false;
//                 case "not_receive":
//                     html = "not_receive:还没有接收到验证码,请让程序等待几秒后再次尝试";
//                     return false;
//                 case "parameter_error":
//                     html = "parameter_error:传入参数错误";
//                     return false;
//                 case "no_data":
//                     html = "no_data:系统暂时没有可用号码了";
//                     return false;
//                 case "project_state_error":
//                     html = "project_state_error:项目状态不对,可能项目还没通过审核";
//                     return false;
//                 case "mobile_state_error":
//                     html = "mobile_state_error:手机号状态不对,立即放弃该号码，调用getMobilenum获取新的号码，不用加黑";
//                     return false;
//                 case "max_count_disable":
//                     html = "已经达到了可以获取手机号的最大数量，请调用cancelSMSRecvAll释放所有手机号后再获取手机号";
//                     return false;
//                 case "操作超时":
//                     html = "操作超时";
//                     return false;
//                 default:
//                     return true;
//             }

        }

    }
}
