using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using System.IO;
using Utilities;
using Utilities.Factory;
using System.Threading;
using System.Net;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Utilities.Processor;
using OpenQA.Selenium.Chrome;
using System.Text.RegularExpressions;
using OpenQA.Selenium.Interactions;
using Utilities.Utility;
using OpenQA.Selenium.Remote;

namespace PayRegister
{
    public partial class Form1 : Form
    {
        private IWebDriver driver;

        // 用户注册邮箱号
        EntitiesArray entities = EntitiesArray.GetInstance();
        AccountEntity curEntity = null;

        List<AccountEntity> failedAccountList = new List<AccountEntity>();

        UIAccessProcessor processControlor = null;
        StreamWriter writer = null;

        int registerMode = 1;

        // 注册成功的号的个数
        int RegisterSucCnt = 0;
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            currentIp =  GetIP();
            AddrLabel.Text = "当前IP：" + currentIp;
            CheckImageFactory.LoginAsyn();
            processControlor = new UIAccessProcessor(ProjectLogHandler);
          //  processControlor.SetAsynCalling(false);
            // 定义处理流程
             processControlor.AddHandler(InitDriver);
            processControlor.AddHandler(InputEmail);
            processControlor.AddHandler(TrigeImageCodeShow);
            processControlor.AddHandler(CheckEmailStatus);
            processControlor.AddHandler(DumpImageCodeToFile);
            processControlor.AddHandler(InputImageCode);
            processControlor.AddHandler(ClickNext);
            processControlor.AddHandler(CheckReceviceStatus);
            processControlor.AddHandler(ClickLoginButton);
               processControlor.AddHandler(LoginEmail);
               processControlor.AddHandler(FetchMail);
               processControlor.AddHandler(GetRegisterAddr);
               processControlor.AddHandler(SetPassword);
   
               processControlor.AddHandler(SetPayPassword); 
               processControlor.AddHandler(SelectSecurityOption);
               processControlor.AddHandler(CheckSuccessRegister); 
               processControlor.AddHandler(CloseDriver);
             processControlor.AddHandler(ReconnectNetwork);
             processControlor.EndExcute("CloseDriver", 15);
             processControlor.MakeCycle();

            // 解析上次剩余的号
            ParseAccount();
        }

        private void ParseAccount()
        {  
            // 剩余的号
            entities.Clear();
            List<AccountEntity> reminder = AccountEntity.DoUnserialize(@"reminder.bat");
            entities.AddList(reminder);
            TotalLabel.Text = entities.Reminder().ToString();

            // 失败的号
           failedAccountList = AccountEntity.DoUnserialize(@"failed.bat");
           FailedLabel.Text = failedAccountList.Count.ToString();
        }

        void BeginRegister()
        {
            if (!entities.HasNext())
            {
                OutMsg("没有可以使用的邮箱号！");
            }

            processControlor.Excute();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            timer2.Start();
            RegisterSucCnt = 0;
            Thread th = new Thread(new ThreadStart(BeginRegister));
            th.SetApartmentState(ApartmentState.STA);
            th.Start();

        }
        int loopTimes = 0;
        private bool InitDriver(Object sender, Object param)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new AccessHandler(InitDriver),sender,param);
            }
            else
            {
                 if (!entities.HasNext())
                 {
                     loopTimes++;
                     if (loopTimes >= GlobalSettings.getInstance().loopTimes)
                     {
                         timer2.Stop();
                         processControlor.Stop();
                         return true;
                     }
                     else
                     {
                         entities.Clear();
                         entities.AddList(failedAccountList);
                         failedAccountList.Clear();
                     }

                 }
                try
                {
                    if (registerMode == 1)
                    {
                        curEntity = entities.Next();
                    }
#region  初始化变量

                    registerMode = 1;
                    isRegisterSuc = false;
                    statusBox.Clear();
#endregion

                    var driverService = ChromeDriverService.CreateDefaultService();
                    driverService.HideCommandPromptWindow = true;
                    //                 ChromeOptions options = new ChromeOptions();
                    //                 options.BinaryLocation = @"C:\Program Files (x86)\Google\Chrome\Application\chrome.exe";
                    var ios6ua = "https://memberprod.alipay.com/account/reg/index.htm";

//                     DesiredCapabilities capabilities = DesiredCapabilities.chrome();
//                     capabilities.setCapability("chrome.switches", );
//                     WebDriver driver = new ChromeDriver(capabilities);
                    string proxyString = "61.223.163.178:8888";
                  var chromeOptions = new ChromeOptions();
                 
                  //chromeOptions.AddArgument("--refer=" + ios6ua);
                 // chromeOptions.AddArguments("--proxy-server="+proxyString);
                    driver = new ChromeDriver(driverService, chromeOptions);


                    driver.Manage().Timeouts().ImplicitlyWait(new TimeSpan(0, 0, 5));
                    driver.Manage().Window.Size = new System.Drawing.Size(1000,driver.Manage().Window.Size.Height);

                    // //reg.taobao.com/member/reg/h5/fill_email.htm
                    driver.Navigate().GoToUrl("https://memberprod.alipay.com/account/reg/enterpriseIndex.htm");

                   // driver.Navigate().GoToUrl("http://proxies.site-digger.com/proxy-detect/");

                }
                catch (System.Exception ex)
                {
                    processControlor.GotoHanlderUnit("CloseDriver", 1000);
                }

            }
            return true;
        }

        private int confirmTimes = 5;
        private int curConfirmTimes = 0;
        private bool isRegisterSuc = true;

        private bool CheckSuccessRegister(Object sender, Object param)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new AccessHandler(CheckSuccessRegister), sender, param);
            }
            else
            {
                callingOnce = true;
                curConfirmTimes++;
                if (curConfirmTimes > confirmTimes)
                {
                    isRegisterSuc = false;
                    callingOnce = true;
                    curConfirmTimes = 0;
                    return true;
                }
                try
                {

                    IWebElement submit = driver.FindElement(By.XPath("//*[@id=\"J-nav-single\"]/ul/li[2]"));
                    if (submit == null ||
                        (submit != null && submit.Text == ""))
                    {

                        callingOnce = false;
                    }
                    else
                    {
                        OutMsg(submit.Text);
                    }
                }
                catch (System.Exception ex)
                {
                    callingOnce = false;
                }
                if (!callingOnce)
                {
                    return callingOnce;
                }

                isRegisterSuc = true;
            }
            return callingOnce;
        }
        private bool CloseDriverFork(Object sender, Object param)
        {
            return true;
        }
        private bool CloseDriver(Object sender, Object param)
        {
             if (this.InvokeRequired)
            {
                this.Invoke(new AccessHandler(CloseDriver), sender, param);
            }
            else
            {
                 if (isRegisterSuc)
                 {
                     curEntity.registerTime = DateTime.Now;
                     curEntity.registerIP = currentIp;
                     OutMsg("注册成功：" + curEntity.ToString());
                     OutputSuccAccount();
                     RegisterSucCnt++;
                     SucLabel.Text = RegisterSucCnt.ToString();
                 }
                 else
                 {
                     OutMsg("注册失败：" + curEntity.ToString());
                     OutputFailedAccount();
                     if (!isIgnore)
                     {
                         failedAccountList.Add(curEntity);
                     }
                     isIgnore = false;
                     FailedLabel.Text = failedAccountList.Count.ToString();
                 }
                 ReminderLabel.Text = entities.Reminder().ToString();
                 driver.Quit();
            }
            return true;
        }

        private void OutputFailedAccount()
        {
            StreamWriter writer = new StreamWriter(@"注册失败.txt",true);
            writer.WriteLine(curEntity.ToString());
            writer.Flush();
            writer.Close();
        }

        private void OutputSuccAccount()
        {
            StreamWriter writer = new StreamWriter(@"注册成功.txt",true);
            writer.WriteLine(curEntity.ToString());
            writer.Flush();
            writer.Close();
        }

        int dailCnt = 0;
        private bool ReconnectNetwork(Object sender, Object param)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new AccessHandler(ReconnectNetwork), sender, param);
            }
            else
            {
                callingOnce = true;
                dailCnt++;
                if (dailCnt % DailConfig.getInstance().dailGap == 0)
                {
                    // 自动拨号
                    OutMsg("断线重拨，等待" + DailConfig.getInstance().waittingTime.ToString() + "秒...");
                    DailConfig.getInstance().DisConnect();
                    System.Threading.Thread.Sleep(DailConfig.getInstance().waittingTime * 1000);
                    int rtn = DailConfig.getInstance().Connect();
                    if (rtn == 0)
                    {
                        OutMsg("重拨成功!");
                    }
                    else
                    {
                        OutMsg("重拨失败!,继续重拨...");
                        callingOnce = false;
                    }
                    string temp = GetIP();
                    if (temp.Equals(currentIp))
                    {
                        OutMsg("IP与上一次一样,继续重拨...");
                        callingOnce = false;
                    }
                    if (!callingOnce)
                    {
                        return false;
                    }
                    currentIp = temp;
                    dailCnt = 0;
                }
                
                AddrLabel.Text = "当前IP:" + currentIp;
            }
            return callingOnce;
        }

        private bool ClickNext(Object sender, Object param)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new AccessHandler(ClickNext), sender, param);
            }
            else
            {
                try
                {
                    ClickElementByXpath(RegisterXpath.NEXT_BUTTON);
                }
                catch (System.Exception ex)
                {
                	// 验证码错误
                    CheckImageFactory.GetCheckImage().ReportError();
                    processControlor.GotoHanlderUnit("CloseDriver", 1000);
                }
                
            }
            return true;
        }

        private bool ClickLoginButton(Object sender, Object param)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new AccessHandler(ClickLoginButton), sender, param);
            }
            else
            {
                driver.Navigate().GoToUrl("http://mail.163.com/?dv=pc");
            }
            return true;

        }
        private bool CheckReceviceStatus(Object sender, Object param)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new AccessHandler(CheckReceviceStatus), sender, param);
            }
            else
            {
                callingOnce = true;
                try
                {
                    IWebElement confirmFrame = SwitchFrameByName("xbox-iframe");
                    confirmFrame.FindElement(By.XPath("//*[@id=\"submitBtn\"]"));
                }
                catch (System.Exception ex)
                {
                    processControlor.GotoHanlderUnit("CloseDriver", 1000);
                }

            }
            return true;
        }
        private bool LoginEmail(Object sender, Object param)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new AccessHandler(LoginEmail), sender, param);
            }
            else
            {
                try
                {
                    InputText("//*[@id=\"idInput\"]", curEntity.emailAccount);
                    InputText("//*[@id=\"pwdInput\"]", curEntity.emailPasswd);
                    ClickElementByXpath("//*[@id=\"loginBtn\"]");
                }
                catch (System.Exception ex)
                {
                    processControlor.GotoHanlderUnit("CloseDriver", 1000);
                }

            }
            return true;
        }

        private bool FetchMail(Object sender, Object param)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new AccessHandler(FetchMail), sender, param);
            }
            else
            {
                try
                {
                    ClickElementByXpath("//*[@id=\"dvNavTree\"]/ul/li[1]/div[1]");
                    // #_mail_userlabel_0_214
                    // //*[@id="_dvModuleContainer_mbox.ListModule_0"]
                    IWebElement element = driver.FindElement(By.XPath("//*[@id=\"_dvModuleContainer_mbox.ListModule_0\"]/div/div/div[4]/div[2]"));
                    element.Click();
                }
                catch (System.Exception ex)
                {
                    processControlor.GotoHanlderUnit("CloseDriver", 1000);
                }

            }
            return true;

        }
        private bool SetPassword(Object sender, Object param)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new AccessHandler(SetPassword), sender, param);
            }
            else
            {

                if (registerAddr == "")
                {
                    processControlor.GotoHanlderUnit("CloseDriver", 1000);
                    return true;
                }
                try
                {
                    
                    curEntity.passwd = AccountFactory.getInstance().getPwdRoles().GetRandomPassword();

                    IWebElement pwdElement = driver.FindElement(By.XPath(RegisterXpath.PASSWORD_INPUT));
                    InputKeys(pwdElement, curEntity.passwd);

                    IWebElement pwdElementConfirm = driver.FindElement(By.XPath("//*[@id=\"queryPwdConfirm\"]"));
                    InputKeys(pwdElementConfirm, curEntity.passwd);

                }
                catch (System.Exception ex)
                {
                    OutMsg("SetPassword" + ex.Message);
                    processControlor.GotoHanlderUnit("CloseDriver", 1000);
                }

            }
            return true;
        }

        private bool SetPayPassword(Object sender, Object param)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new AccessHandler(SetPayPassword), sender, param);
            }
            else
            {
                curEntity.payPwd = AccountFactory.getInstance().getPwdRoles().GetRandomPassword();
                IWebElement payElement = driver.FindElement(By.XPath("//*[@id=\"payPwd\"]"));
                InputKeys(payElement, curEntity.payPwd);

                IWebElement payElementConfirm = driver.FindElement(By.XPath("//*[@id=\"payPwdConfirm\"]"));
                InputKeys(payElementConfirm, curEntity.payPwd);

            }
            return callingOnce;
        }

        private bool SelectSecurityOption(Object sender, Object param)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new AccessHandler(SelectSecurityOption), sender, param);
            }
            else
            {
                // 点击设置
                ClickElementByXpath("//*[@id=\"J-complete-form\"]/fieldset/div[9]/a");

                Thread.Sleep(500);
                ClickElementByXpath("/html/body/div[4]/ul/li[3]");

                curEntity.securityAns = AccountFactory.getInstance().getAccountRoles().GetRandomAccName();
                IWebElement securityElement = driver.FindElement(By.XPath("//*[@id=\"protectPasswordKey\"]"));
                InputKeys(securityElement, curEntity.securityAns);
                
                // 点击下一步
                Thread.Sleep(500);
                ClickElementByXpath("//*[@id=\"J-submit\"]");

                curConfirmTimes = 0;
            }
            return true;
        }

        private string registerAddr = "";
        private bool GetRegisterAddr(Object sender, Object param)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new AccessHandler(GetRegisterAddr), sender, param);
            }
            else
            {
                // //*[@id="1441722241241_frameBody"]
                try
                {

                    IWebElement curFrame = SwitchFrameByName("frameBody");
                    driver.SwitchTo().Frame(curFrame);
                    IWebElement element = driver.FindElement(By.XPath("/html/body/table/tbody/tr/td/div/div[2]/p[4]"));
                    //  OutMsg(element.Text);
                    registerAddr = element.Text;

                    driver.Navigate().GoToUrl(registerAddr);
                }
                catch (System.Exception ex)
                {
                    processControlor.GotoHanlderUnit("CloseDriver", 1000);
                }

            }

            return true;
        }





        /// <summary>
        /// 输入文本
        /// </summary>
        /// <param name="elementXpath"></param>
        /// <param name="txt"></param>
        private void InputText(string elementXpath, string txt)
        {
            IWebElement element = driver.FindElement(By.XPath(elementXpath));
            element.SendKeys(txt);
            Thread.Sleep(1000);
        }


        IWebElement SwitchFrameByName(string partName)
        {
            ReadOnlyCollection<IWebElement> allFrames = driver.FindElements(By.TagName("iframe"));
            IWebElement curFrame = null;
            for (int i = 0; i < allFrames.Count; i++)
            {
                OutMsg(allFrames[i].GetAttribute("name"));
                if (allFrames[i].GetAttribute("name").Contains(partName))
                {
                    curFrame = allFrames[i];
                    break;
                }
            }
            return curFrame;
        }
        private string WaitForElement(string xpath)
        {
            IWebElement element = driver.FindElement(By.XPath(xpath));
            OutMsg("wait element to be available" + element.Text);
            return element.Text;
        }

        /// <summary>
        /// 点击元素
        /// </summary>
        /// <param name="xpath"></param>
        private void ClickElementByXpath(string xpath)
        {
            IWebElement element = driver.FindElement(By.XPath(xpath));
            element.Click();
            Thread.Sleep(500);
        }

        private void WaitImageLoginFinished()
        {
            if (!CheckImageFactory.GetCheckImage().IsLoginFinshed())
            {
                System.Threading.Thread.Sleep(1000);
                WaitImageLoginFinished();
            }
        }
        // 
        bool callingOnce = true;
        private bool InputEmail(Object sender, Object param)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new AccessHandler(InputEmail), sender, param);
            }
            else
            {
                try
                {
                    callingOnce = true;
                    IWebElement emailElement = driver.FindElement(By.XPath(RegisterXpath.EMAIL_INPUT));
                    InputKeys(emailElement, curEntity.emailAccount);

                    OutMsg(curEntity.ToString());

                }
                catch (System.Exception ex)
                {
                    OutMsg(ex.Message);
                    processControlor.GotoHanlderUnit("CloseDriver", 1000);
                }

              }

            return callingOnce;

        }

        private bool TrigeImageCodeShow(Object sender, Object param)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new AccessHandler(TrigeImageCodeShow), sender, param);
            }
            else
            {
                try
                {
                    callingOnce = true;
                    IWebElement dragElement = driver.FindElement(By.XPath("//*[@id=\"_n1z\"]"));
                    Actions action = new Actions(driver);
                    action.DragAndDropToOffset(dragElement, 300, 0).Perform();

                    OutMsg("drag done");

                }
                catch (System.Exception ex)
                {
                    processControlor.GotoHanlderUnit("CloseDriver", 1000);
                }

            }

            return callingOnce;

        }

        bool isIgnore = false;
        //CheckEmailStatus
        private bool CheckEmailStatus(Object sender, Object param)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new AccessHandler(CheckEmailStatus), sender, param);
            }
            else
            {
                try
                {
                    callingOnce = true;
                    IWebElement dragElement = driver.FindElement(By.XPath("//*[@id=\"J-pop-accName\"]"));
                    if (dragElement.Text.Contains("已注册"))
                    {
                        OutMsg("改邮箱已经被注册");
                        isIgnore = true;
                        processControlor.GotoHanlderUnit("CloseDriver", 1000);
                        return true;
                    }

                }
                catch (System.Exception ex)
                {
                    processControlor.GotoHanlderUnit("CloseDriver", 1000);
                }

            }

            return callingOnce;

        }
        private void InputKeys(IWebElement email, string p)
        {
            Actions action = new Actions(driver);
            action.MoveToElement(email);
            email.Clear();
            action.Click(email);

            if (p.Length>0)
            {
                email.SendKeys(p);
            }
            
        }
        Point clickPoint = Point.Empty;
        private bool InputImageCode(Object sender, Object param)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new AccessHandler(InputImageCode), sender, param);
            }
            else
            {
                callingOnce = true;
                ScrollToTop();
                if (!isImageCodeFinished)
                {
                    OutMsg("尚未接收到验证坐标");
                    callingOnce = false;
                    return true;
                }
                string[] xy = coords.Split(new char[]{','});
                if (xy.Length != 2)
                {
                    OutMsg("获取图片验证码失败");
                    return true;
                }

                OutMsg("返回坐标：" + coords);
                int x = (int)(Convert.ToInt32(xy[0])/scaleX);
                int y = (int)((Convert.ToInt32(xy[1])-30)/scaleY);

                IWebElement web = driver.FindElement(By.XPath("//*[@id=\"clickCaptcha\"]/div[2]/img"));
                SetChromeForegroundWindow();                
                domPnt = GetDomPosition();
               // ScrollToTop();
                clickPoint = new Point(domPnt.X + x + web.Location.X, domPnt.Y + y + web.Location.Y);
                OutMsg(string.Format("X {0} Y {1}", clickPoint.X,clickPoint.Y));
                SetChromeForegroundWindow();

                MouseUtility.SetCursorPos(clickPoint.X, clickPoint.Y);
                MouseUtility.ClickAtXY(clickPoint);
        
//                 string imageCode =  CheckImageFactory.GetCheckImage().RecognizeByCodeTypeAndPath(Config.PATH_CHECKCODE_IMG, codeType);
//                 OutMsg("图片验证码：" + imageCode);
//                 image.Clear();
//                 image.SendKeys(imageCode);

            }
            return callingOnce;
        }

        private void ScrollToTop()
        {
            string js = "var q=document.documentElement.scrollTop=10000";
            IJavaScriptExecutor jsExecutor = (IJavaScriptExecutor)driver;
            jsExecutor.ExecuteScript(js);
            Thread.Sleep(3);
        }

        private void SetChromeForegroundWindow()
        {
            IntPtr pHandler = MouseUtility.FindWindow("Chrome_WidgetWin_1", "注册 - 支付宝 - Google Chrome");
            MouseUtility.SetForegroundWindow(pHandler);
        }

        Point domPnt = Point.Empty;
        private Point GetDomPosition()
        {
            if (domPnt == Point.Empty)
            {
                IntPtr pHandler = MouseUtility.FindWindow("Chrome_WidgetWin_1", "注册 - 支付宝 - Google Chrome");
                IntPtr cHandler = IntPtr.Zero;

                cHandler = MouseUtility.FindWindowEx(pHandler, cHandler, "Chrome_RenderWidgetHostHWND", null);
                if (cHandler == IntPtr.Zero)
                {
                    OutMsg("获取dom 位置失败");
                }
                Rect rect = new Rect();
                MouseUtility.GetWindowRect(cHandler, out rect);
                domPnt = new Point(rect.Left, rect.Top);
            }
            return domPnt;
        }

        float scaleX = 1.0f;
        float scaleY = 1.0f;
        private bool DumpImageCodeToFile(Object sender, Object param)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new AccessHandler(DumpImageCodeToFile), sender, param);
            }
            else
            {
                callingOnce = true;
                try
                {
                    IWebElement codeCheckIcon = driver.FindElement(By.XPath("//*[@id=\"_bg\"]"));
                    
                    if (codeCheckIcon == null || codeCheckIcon.Size.Width < 100)
                    {
                        callingOnce = false;
                    }
                }
                catch
                {
                    callingOnce = false;
                }

                if (!callingOnce)
                {
                    processControlor.GotoHanlderUnit("TrigeImageCodeShow", 1000);
                    callingOnce = true;
                    OutMsg("等待图片验证码显示");
                    return true;
                }
                try
                {
                    Bitmap topBmp = TakeScreenshot(By.XPath("//*[@id=\"_bg\"]"));
                    Rectangle cropArea = new Rectangle(new Point(0, 0), new Size(200, 30));
                    topBmp = topBmp.Clone(cropArea, topBmp.PixelFormat);
                    topBmp.Save(@"top.jpg");
                    topBmp.Dispose();

                    Bitmap buttomBmp = TakeScreenshot(By.XPath("//*[@id=\"clickCaptcha\"]/div[2]/img"));
                    OutMsg("orgin X Y" + buttomBmp.Width.ToString() + " " + buttomBmp.Height.ToString());
                    Bitmap targetBmp = new Bitmap(200, 200);

                    scaleX = 200.0f / buttomBmp.Width;
                    scaleY = 200.0f / buttomBmp.Height;

                    Graphics g = Graphics.FromImage(targetBmp);
                    g.DrawImage(buttomBmp, new Rectangle(0, 0, 200, 200), new Rectangle(0, 0, buttomBmp.Width, buttomBmp.Height), GraphicsUnit.Pixel);
                    targetBmp.Save(@"buttom.jpg");
                    targetBmp.Dispose();
                    buttomBmp.Dispose();

                    // 提交图片验证码
                    isImageCodeFinished = false;
                    Thread th = new Thread(new ThreadStart(SubmitImageCode));
                    th.Start();
                    return true;
                }
                catch (System.Exception ex)
                {
                    OutMsg("保存验证码失败");
                    processControlor.GotoHanlderUnit("CloseDriver", 1000);
                }
                
            }
            return callingOnce;


        }
        bool isImageCodeFinished = false;
        string coords = "";// 图片验证码结果
        private void SubmitImageCode()
        {
            WaitImageLoginFinished();
            ImageUtility.CombinImage(@"top.jpg", @"buttom.jpg", Config.PATH_CHECKCODE_IMG);
            int codeType = Convert.ToInt32(AccountFactory.getInstance().getImageCodeAcc().ProjID);
            coords = CheckImageFactory.GetCheckImage().RecognizeByCodeTypeAndPath(Config.PATH_CHECKCODE_IMG, codeType);
            isImageCodeFinished = true;
        }
        public Bitmap TakeScreenshot(By by)
        {
            // 1. Make screenshot of all screen
            var screenshotDriver = driver as ITakesScreenshot;
            Screenshot screenshot = screenshotDriver.GetScreenshot();
            var bmpScreen = new Bitmap(new MemoryStream(screenshot.AsByteArray));
            // 2. Get screenshot of specific element
            IWebElement element = FindElement(by);
            var cropArea = new Rectangle(element.Location, element.Size);
            return bmpScreen.Clone(cropArea, bmpScreen.PixelFormat);
        }

        private IWebElement FindElement(By by)
        {
            return driver.FindElement(by);
        }


        bool ProjectLogHandler(Object sender, string param, LogType type)
        {
            OutMsg(param);
            return true;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            timer2.Stop();
            processControlor.Stop();
        }
        string currentIp;
        private string GetIP()
        {
            string tempip = "";
            try
            {
                WebRequest wr = WebRequest.Create("http://www.ip138.com/ips138.asp");
                Stream s = wr.GetResponse().GetResponseStream();
                StreamReader sr = new StreamReader(s, Encoding.Default);
                string all = sr.ReadToEnd(); //读取网站的数据

                int start = all.IndexOf("您的IP地址是：[") + 9;
                int end = all.IndexOf("]", start);
                tempip = all.Substring(start, end - start);
                sr.Close();
                s.Close();
            }
            catch
            {
            }
            return tempip;
        }

        public void OutMsg(string msg)
        {
            OutMsg(statusBox, msg, Color.Black);
        }
        public void OutMsg(RichTextBox rtb, string msg, Color color)
        {
            rtb.Invoke(new EventHandler(delegate
            {
                rtb.SelectionStart = rtb.Text.Length;//设置插入符位置为文本框末
                rtb.SelectionColor = color;//设置文本颜色
                string msgCxt = DateTime.Now.ToString() + ": " + msg;
                rtb.AppendText(msgCxt + "\r\n");//输出文本，换行
                rtb.ScrollToCaret();//滚动条滚到到最新插入行

                // 保存到日志文件
                Trace.WriteLine(msgCxt);

            }));
        }

        private void button3_Click(object sender, EventArgs e)
        {
            entities.AddList(EntitiesArray.GetDatas());
            TotalLabel.Text = entities.Reminder().ToString();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            AccountFactory.getInstance().getPwdRoles().Save();
            AccountFactory.getInstance().getAccountRoles().Save();
            AccountFactory.getInstance().getImageCodeAcc().Save();
            AccountFactory.getInstance().getSMSAcc().Save();

        }

        private void button5_Click(object sender, EventArgs e)
        {

            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            int X = Convert.ToInt32(tbXCoord.Text.Trim());
            int Y = Convert.ToInt32(tbYCoord.Text.Trim());
            //  SetChromeForegroundWindow();
            MouseUtility.SetCursorPos(X, Y);
            MouseUtility.ClickAtXY(new Point(X, Y));
            timer1.Stop();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            GlobalSettings.getInstance().Save();
            DailConfig.getInstance().Save();
            DumpAccount();
        }

        private void DumpAccount()
        {
            // 剩余的号
            List<AccountEntity> reminder = new List<AccountEntity>(entities.Reminder());
            while (entities.HasNext())
            {
                reminder.Add(entities.Next());
            }
            AccountEntity.DoSerialize(reminder, @"reminder.bat");
            AccountEntity.DoSerialize(failedAccountList, @"failed.bat");
        }

        private void btnClearAll_Click(object sender, EventArgs e)
        {
            entities.Clear();
            TotalLabel.Text = "0";
            failedAccountList.Clear();
            FailedLabel.Text = "0";
            SucLabel.Text = "0";
            ReminderLabel.Text = "0";
            loopTimes = 0;
        }

        private void btnClearFailed_Click(object sender, EventArgs e)
        {
            failedAccountList.Clear();
            FailedLabel.Text = "0";
        }

        TimeSpan totalTimes = new TimeSpan();
        private void timer2_Tick(object sender, EventArgs e)
        {
            totalTimes += new TimeSpan(0, 0, 1);
            ConsumeTimesLabel.Text = string.Format("{0}:{1}:{2}:{3}", totalTimes.Days, totalTimes.Hours, totalTimes.Minutes, totalTimes.Seconds);
        }

    }
}
