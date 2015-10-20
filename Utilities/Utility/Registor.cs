using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
namespace Utilities
{
    public class Register
    {
        protected AccountEntity m_AE; // 我的账号

        public AccountEntity GetAccount()
        {
            return m_AE;
        }
        //输入注册使用的邮箱
        virtual public void PutLoginEmail(AccountEntity Ae)
        {

        }
        // 校验邮箱
        virtual public bool CheckEmail()
        {
            return false;
        }
        //确认验证码
        virtual public void PutImgCheckCode(string verifyCode)
        {

        }
        // 校验验证码
        virtual public bool CheckImgCheckCode()
        {
            return false;
        }
        // 执行提交邮箱信息
        virtual public bool SubmitEmail()
        {
            return false;
        }

        // 输入电话号码
        virtual public bool PutPhoneNum(string phoneNum)
        {
            return false;
        }

        // 检查电话号码是否正确
        virtual public bool CheckPhoneNum()
        {
            return false;
        }
        // 获取短信验证码
        virtual public bool SendSMSCheckCode()
        {
            return false;
        }

        // 输入短信验证码
        virtual public bool InputSMSCode(string smsNum)
        {
            return false;
        }
        // 点击下一步
        virtual public bool ClickNext()
        {
            return false;
        }
        // 验证短信验证码是否正确
        virtual public bool CheckPhoneCode()
        {
            return false;
        }
        // 获取图片HTML
        virtual public bool IsImgElementLoaded()
        {
            return false;
        }
        virtual public void Goto(string url)
        {

        }
         virtual public void GotoConfirm(string url)
        {

        }
        virtual public int SaveCheckImage()
        {
            return -1;
        }
        // 判断URL是否加载完成
        virtual public bool IsLoadCompleted(string url)
        {
            return false;
        }


        // 获取确认的URL，该URL转接到短信验证
        public string GetEmailConfirmLink()
        {
            return Utiliy.GetConfirmLink(m_AE);
        }

        // 输入确认信息，最后一步
        virtual public bool InputConfirmInfo(ref AccountEntity ae)
        {
            return false;
        }
        virtual public bool CheckRegisterSuc()
        {
            return false;
        }
        public delegate void GotoHandler(string url);
        public delegate bool LoadHandler(string url);
        public delegate bool ComfirmInfoHandler(ref AccountEntity ae);
        public delegate void PutLoginEmailHandler(AccountEntity Ae);
        protected GotoHandler onGotoHandler;
        protected LoadHandler onLoadHandler;
        protected ComfirmInfoHandler onConfirmHandler;
        protected PutLoginEmailHandler onPutLoginEmailHandler;
        public void SetGotoHandler(GotoHandler gotoHandler)
        {
            onGotoHandler = gotoHandler;
        }
        public void SetLoadHandler(LoadHandler loadHandler)
        {
            onLoadHandler = loadHandler;
        }
        public void SetComfirmInfoHandler(ComfirmInfoHandler loadHandler)
        {
            onConfirmHandler = loadHandler;
        }
        public void SetPutLoginEmailHandler(PutLoginEmailHandler loadHandler)
        {
            onPutLoginEmailHandler = loadHandler;
        }
    }

    public class WebBrowserRegiter : Register
    {
        WebBrowser webBrowser;
        WebBrowser confirmBrowser;
        public WebBrowserRegiter(WebBrowser webdriver)
        {
            webBrowser = webdriver;
        }
        public WebBrowserRegiter(WebBrowser webdriver,WebBrowser confirmBrowser)
        {
            webBrowser = webdriver;
            this.confirmBrowser = confirmBrowser;
        }
        override public void PutLoginEmail(AccountEntity Ae)
        {
            onPutLoginEmailHandler(Ae);
        }
        public delegate bool CheckHandler();
        override public bool CheckEmail()
        {
            if (webBrowser.InvokeRequired)
            {
                CheckHandler checkEmail = new CheckHandler(CheckEmail);
                return (bool)webBrowser.Invoke(checkEmail);
            }
            else
            {
                bool flag = false;
                try
                {
                    HtmlElement element = Utiliy.GetHtmlElement(webBrowser, Config.EMAIL_CHECK_ELEMENT_ID);
                    string classProperty = element.Children[2].InnerHtml;
                    if (classProperty == null)
                    {
                        flag = true;
                    }

                }
                catch (System.Exception ex)
                {

                }
                return flag;
            }
        }
        override public bool CheckImgCheckCode()
        {
            if (webBrowser.InvokeRequired)
            {
                CheckHandler checkImgCheckCode = new CheckHandler(CheckImgCheckCode);
                return (bool)webBrowser.Invoke(checkImgCheckCode);
            }
            else
            {
                HtmlElement element = Utiliy.GetHtmlElement(webBrowser, Config.CHECK_CHECK_ELEMENT_ID);
                if (element.Children.Count == 3)
                {
                    string classProperty = element.Children[2].InnerHtml;
                    if (classProperty == null)
                    {
                        return true;
                    }
                }
                return false;
            }

        }
        override public void PutImgCheckCode(string verifyCode)
        {
            webBrowser.Select();
            HtmlElement element = Utiliy.GetHtmlElement(webBrowser, Config.CHECK_CODE_ELEMENT_ID);
            element.SetAttribute("Value", verifyCode);
            element.Focus();
            System.Windows.Forms.SendKeys.Send("{tab}");
        }
        public override bool SubmitEmail()
        {
            if (!CheckImgCheckCode())
                return false;
            HtmlElement element = Utiliy.GetHtmlElement(webBrowser, Config.BUTTON_SUBMIT_ID);
            element.InvokeMember("click");
            return true;
        }
        public override void Goto(string url)
        {
            onGotoHandler.Invoke(url);
           // webBrowser.Navigate(url);
        }
        override public int SaveCheckImage()
        {
            if (Utiliy.GetHtmlElement(webBrowser, Config.IMG_CHECKCODE_ID) == null)
            {
                return 0;
              //  Utiliy.GetHtmlElement(webBrowser, "J_ImgRefresher1").InvokeMember("click");
            }
//             if (Utilities.Utiliy.SaveCheckCodeImage(Utiliy.GetHtmlElement(webBrowser, Config.IMG_CHECKCODE_ID), Config.PATH_CHECKCODE_IMG))
//                 return 1;
//             else
                return 0;
        }
        override public bool IsLoadCompleted(string url)
        {
            return onLoadHandler(url);

        }
        override public bool IsImgElementLoaded()
        {
            return true;
        }

        // 输入电话号码
        override public bool PutPhoneNum(string phoneNum)
        {
            HtmlElement element = Utiliy.GetHtmlElement(webBrowser, Config.SMS_PHONE_INPUT_ID);
            if (element == null)
            {
                return false;
            }
            element.SetAttribute("value", phoneNum);
            webBrowser.Select();
            webBrowser.Focus();
            element.Focus();
            System.Windows.Forms.SendKeys.Send("{tab}");
            System.Windows.Forms.SendKeys.Send(" ");
            return true;
        }
        private void InputValue(HtmlElement element, string value)
        {
            element.SetAttribute("value", value);
            webBrowser.Select();
            webBrowser.Focus();
            element.Focus();
            System.Windows.Forms.SendKeys.Send("{tab}");
            System.Windows.Forms.SendKeys.Send(" ");
        }
        // 检查电话号码是否正确
        override public bool CheckPhoneNum()
        {
            HtmlElement element = Utiliy.GetHtmlElement(webBrowser, Config.SMS_CHECK_PHONE_ID);
            if (element.Children.Count == 3)
            {
                string classProperty = element.Children[2].InnerHtml;
                if (classProperty == null)
                {
                    return true;
                }
            }
            return false;
        }
        // 获取短信验证码
        override public bool SendSMSCheckCode()
        {
            HtmlElement element = Utiliy.GetHtmlElement(webBrowser, Config.SMS_FETCH_CHECK_CODE);
            if (element == null)
            {
                return false;
            }
            element.InvokeMember("click");
            return true;
        }

        // 输入短信验证码
        override public bool InputSMSCode(string smsNum)
        {
            HtmlElement element = Utiliy.GetHtmlElement(webBrowser, Config.SMS_INPUT_SMS_CODE);
            if (element == null)
            {
                return false;
            }
            element.SetAttribute("value", smsNum);
            webBrowser.Select();
            webBrowser.Focus();
            element.Focus();
            System.Windows.Forms.SendKeys.Send("{tab}");
            System.Windows.Forms.SendKeys.Send(" ");
            return true;
        }

        override public bool ClickNext()
        {
            HtmlElement element = Utiliy.GetHtmlElement(webBrowser, Config.BUTTON_SMS_NEXT);
            if (element == null)
            {
                return false;
            }
            element.InvokeMember("click");
            return true;
        }

        override public bool CheckPhoneCode()
        {
            HtmlElement element = Utiliy.GetHtmlElement(webBrowser, Config.BUTTON_SUCC_MSG);
            if (element == null)
            {
                return false;
            }
            return true;
        }
        override public void GotoConfirm(string url)
        {
            confirmBrowser.Navigate(url);
        }
        override public bool InputConfirmInfo(ref AccountEntity ae)
        {
            m_AE = ae;
            return onConfirmHandler(ref ae);
//             string pwd = RandomGenerator.getRandomPwd();
//             ae.passwd = pwd;
//             HtmlElement element = Utiliy.GetHtmlElement(webBrowser, Config.CONFIRM_PASSWD1);
//             InputValue(element, pwd);
//             element = Utiliy.GetHtmlElement(webBrowser, Config.CONFIRM_PASSWD2);
//             InputValue(element, pwd);
// 
//             element = Utiliy.GetHtmlElement(webBrowser, Config.CONFIRM_NICKNAME);
//             string nickName = RandomGenerator.getRandomEnName();
//             InputValue(element, nickName);
// 
//             element = Utiliy.GetHtmlElement(webBrowser, Config.CONFIRM_NICKNAME_CHECK);
//             while (element.Children[2].InnerText != null)
//             {
//                 nickName = RandomGenerator.getRandomName();
//                 InputValue(element, nickName);
//             }
//             ae.acount = nickName;
//             element = Utiliy.GetHtmlElement(webBrowser, Config.CONFIRM_BUTTON);
//             element.InvokeMember("click");

            //return false;
        }
        override public bool CheckRegisterSuc()
        {
            if (webBrowser.Url.ToString().Trim() == "http://reg.taobao.com/member/reg/reg_success.htm")
            {
                return true;
            }
            return false;
        }
    }
}
