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
using WindowsInput;
using System.Runtime.Serialization.Formatters.Binary;

namespace PayRegister
{
    public partial class Form1 : Form
    {
        private IWebDriver driver;

        // 用户注册邮箱号
        EntitiesArray entities = EntitiesArray.GetInstance();
        List<AccountEntity> failedEntity = new List<AccountEntity>();
        // 死号
        List<AccountEntity> deadEntity = new List<AccountEntity>();
        AccountEntity curEntity = null;
        UIAccessProcessor processControlor = null;
        StreamWriter writer = null;

        int registerMode = 1;

        List<string> devicesType = new List<string>();
        int sucNum = 0;
        int failedNum = 0;
     //   WinIoSys m_IoSys = new WinIoSys();
        int MAX_LOOP = 5;
        int CUR_LOOP_CNT = 0;
        bool isContditionFinished = true;
        bool beginToClickClearBtn = false;

        TimeSpan totalTimes;

        int emailRegisterCount = 0;
        bool IsUseVPN = false;
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            currentIp =  GetIP();
            if (currentIp=="")
            {
                Thread th = new Thread(new ThreadStart(ReconnectNet));
                th.Start();

               // OutMsg("无网络,尝试连接网络...");
            }
            AddrLabel.Text = "当前IP：" + currentIp;
            
            processControlor = new UIAccessProcessor(ProjectLogHandler);
          //  processControlor.SetAsynCalling(false);
            // 定义处理流程
           
        

          //  m_IoSys.InitSuperKeys();
            // 初始化设备类型
            InitDevicesType();

            totalLabel.Text = entities.count().ToString();
            
            SucLabel.Text = failedLabel.Text = "0";
            ParseAccount();

            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
        }

        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            try
            {
                Exception ex = (Exception)e.ExceptionObject;
                Trace.WriteLine("未处理的异常：" + ex.StackTrace);
            }
            catch (Exception exc)
            {
                try
                {
                    MessageBox.Show("Fatal Non-UI Error",
                        "Fatal Non-UI Error. Could not write the error to the event log. Reason: "
                        + exc.Message, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
                finally
                {
                    Application.Exit();
                }
            }
        }
        private void ReconnectNet()
        {
            ReconnectNetwork(null, null);
        }
        private void InitDevicesType()
        {
            devicesType.Add("Mozilla/5.0 (iPhone; U; CPU iPhone OS 4_2_1 like Mac OS X; en-us) AppleWebKit/533.17.9 (KHTML, like Gecko) Version/5.0.2 Mobile/8C148 Safari/6533.18.5");
            devicesType.Add("Mozilla/5.0 (iPhone; CPU iPhone OS 7_0 like Mac OS X; en-us) AppleWebKit/537.51.1 (KHTML, like Gecko) Version/7.0 Mobile/11A465 Safari/9537.53");
            devicesType.Add("Mozilla/5.0 (iPhone; CPU iPhone OS 8_0 like Mac OS X) AppleWebKit/600.1.3 (KHTML, like Gecko) Version/8.0 Mobile/12A4345d Safari/600.1.4");
            devicesType.Add("Mozilla/5.0 (iPhone; CPU iPhone OS 8_0 like Mac OS X) AppleWebKit/600.1.3 (KHTML, like Gecko) Version/8.0 Mobile/12A4345d Safari/600.1.4");
//             devicesType.Add("Mozilla/5.0 (Linux; Android 4.3; Nexus 10 Build/JSS15Q) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/42.0.2307.2 Safari/537.36");
//             devicesType.Add("Mozilla/5.0 (Linux; Android 4.4.2; Nexus 4 Build/KOT49H) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/35.0.1916.122 Mobile Safari/537.36");
//             devicesType.Add("Mozilla/5.0 (Linux; Android 4.4.4; Nexus 5 Build/KTU84P) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/38.0.2125.114 Mobile Safari/537.36");
//             devicesType.Add("Mozilla/5.0 (Linux; Android 5.1.1; Nexus 6 Build/LYZ28E) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/44.0.2403.20 Mobile Safari/537.36");
//             devicesType.Add("Mozilla/5.0 (Linux; U; Android 4.3; en-us; SM-N900T Build/JSS15J) AppleWebKit/534.30 (KHTML, like Gecko) Version/4.0 Mobile Safari/534.30");
//             devicesType.Add("Mozilla/5.0 (Linux; U; Android 4.1; en-us; GT-N7100 Build/JRO03C) AppleWebKit/534.30 (KHTML, like Gecko) Version/4.0 Mobile Safari/534.30");
//             devicesType.Add("Mozilla/5.0 (Linux; U; Android 4.0; en-us; GT-I9300 Build/IMM76D) AppleWebKit/534.30 (KHTML, like Gecko) Version/4.0 Mobile Safari/534.30");
//             devicesType.Add("Mozilla/5.0 (Linux; Android 4.2.2; GT-I9505 Build/JDQ39) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/31.0.1650.59 Mobile Safari/537.36");
//             devicesType.Add("Mozilla/5.0 (Linux; U; Android 4.4.2; en-us; LGMS323 Build/KOT49I.MS32310c) AppleWebKit/537.36 (KHTML, like Gecko) Version/4.0 Chrome/30.0.1599.103 Mobile Safari/537.36");
//             devicesType.Add("Mozilla/5.0 (MeeGo; NokiaN9) AppleWebKit/534.13 (KHTML, like Gecko) NokiaBrowser/8.5.0 Mobile Safari/534.13");
        }
        private void ParseAccount()
        {
            // 剩余的号
            entities.Clear();
            if (File.Exists("reminder.bat"))
            {
                List<AccountEntity> reminder = AccountEntity.DoUnserialize(@"reminder.bat");
                entities.AddList(reminder);
            }
            totalLabel.Text = entities.Reminder().ToString();
            reminderLabel.Text = entities.Reminder().ToString();

            if (File.Exists("failed.bat"))
            {
                // 失败的号
                failedEntity = AccountEntity.DoUnserialize(@"failed.bat");
                failedLabel.Text = failedEntity.Count.ToString();
            }


            if (File.Exists("dead.bat"))
            {
                // 死号
                deadEntity = AccountEntity.DoUnserialize(@"dead.bat");
                labDead.Text = deadEntity.Count.ToString();
            }

        }

        private void DumpAccount()
        {
            // 剩余的号
            List<AccountEntity> reminder = new List<AccountEntity>(entities.Reminder());
            int remind = entities.GetIndex();
            while (entities.HasNext())
            {
                reminder.Add(entities.Next());
            }
            AccountEntity.DoSerialize(reminder, @"reminder.bat");
            AccountEntity.DoSerialize(failedEntity, @"failed.bat");
            AccountEntity.DoSerialize(deadEntity, @"dead.bat");

            entities.SetIndex(remind);
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
            button1.Enabled = false;
            // 清空日志
            if (File.Exists("日志.txt"))
            {
                File.Delete("日志.txt");
            }

            IsUseVPN = chUseVPN.Checked;
            InitProcessControler();
            if (radioBtnB.Checked && GlobalSettings.getInstance().adslGap == "C"
                || radioBtnC.Checked && GlobalSettings.getInstance().adslGap == "B")
            {
                if (radioBtnB.Checked)
                {
                    GlobalSettings.getInstance().adslGap = "B";
                    DumpIpAddr();
                    ParseIPAddr();
                }
                else if (radioBtnC.Checked)
                {
                    GlobalSettings.getInstance().adslGap = "C";
                    DumpIpAddr();
                    ParseIPAddr();
                }

            }
            timer2.Start();
            Thread th = new Thread(new ThreadStart(BeginRegister));
           // th.SetApartmentState(ApartmentState.STA);
            th.Start();

            // 添加本地IP
            if (currentIp==null || currentIp == "")
            {
                currentIp = GetIP();
                
            }

            string temp = GetIPSection(currentIp, GlobalSettings.getInstance().adslGap);
            if (!ipAddrList.Contains(temp))
            {
                ipAddrList.Add(currentIp);
            }

            button1.Enabled = true;
        }

        private void InitProcessControler()
        {
            processControlor.Reset();
            processControlor.SetAsynCalling(true);
            processControlor.SetCondition(ExcuteCondition);
            processControlor.SetProcessAtFirstTime(PreHandler);
            processControlor.AddHandler(InitDriver);
            processControlor.AddHandler(ReNavigateToMall);
            processControlor.AddHandler(NavigateToLogin);
            processControlor.SetExcuteTimeOuts(GlobalSettings.getInstance().taskTimesout*1000);
            processControlor.AddHandler(InputEmail, 5000);
            if (!chHalfAuto.Checked)
            {
                
                processControlor.AddHandler(InputEmailPassword);
                processControlor.AddHandler(CheckDragButton);
                processControlor.AddHandler(DumpDragImage);
                processControlor.AddHandler(ClickDragImage);
                processControlor.AddHandler(CheckDragImageStatus);
                processControlor.AddHandler(ClickLogin, 1000);

                processControlor.AddHandler(CheckLoginSuc, 500);

                processControlor.AddHandler(FillUserName);
                processControlor.AddHandler(TrigeImageCodeShow, 500);

                processControlor.AddHandler(DumpImageCodeToFile, 300);

                processControlor.AddHandler(InputImageCode);
                processControlor.AddHandler(ClickNext);
                processControlor.AddHandler(ConfirmUserName);


                processControlor.AddHandler(InputPhone);
                processControlor.AddHandler(ClickPhoneNextBtn);

                processControlor.AddHandler(CheckPhoneCodePage);
                processControlor.AddHandler(InputPhoneCode, 2000);
                processControlor.AddHandler(ClickPhoneCodeNextBtn);
                processControlor.AddHandler(CheckSuccessRegister, 1000);
                
            }
            processControlor.AddHandler(CloseDriver);
            processControlor.AddHandler(ReconnectNetwork);
            processControlor.MakeCycle();
            processControlor.EndExcute("CloseDriver", 20);
        }

        private bool InitDriver(Object sender, Object param)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new AccessHandler(InitDriver),sender,param);
            }
            else
            {
                 if (!entities.HasNext() && chHalfAuto.Checked == false)
                 {
                     OutMsg("重复使用失败的小号: " + failedEntity.Count.ToString());
                     if (chUseMode.Checked && failedEntity.Count > 0)
                     {
                        
                          entities.Clear();
                          entities.AddList(failedEntity);
                          failedEntity.Clear();

                          StreamWriter stream = new StreamWriter(@"失败小号.txt");
                          stream.WriteLine();
                          stream.Close();

                     }
                     else
                     {
                         // OutPutFailedAndDeadAccount();
                          DumpAccount();
                          processControlor.Stop();
                          timer2.Stop();
                          return true;
                     }
                 }
                try
                {                    // 杀死进程
                    ProcessUtility.KillProcess("chromedriver");
                    ProcessUtility.KillProcess("chrome");
                    if (registerMode == 1)
                    {
                        curEntity = entities.Next();
                    }
#region  初始化变量

                    registerMode = 1;
                    isRegisterSuc = false;
                    CUR_LOOP_CNT = 0;
                    beginToClickClearBtn = false;
                    statusBox.Clear();
                    emailRegisterCount = 0;
#endregion
                    CheckImageFactory.LoginAsyn();
                    var driverService = ChromeDriverService.CreateDefaultService();
                    driverService.HideCommandPromptWindow = true;

                    int idx = RandomGenerator.getRandom().Next(devicesType.Count-1);
                    string ios6ua = devicesType[idx];
                    ios6ua = "Mozilla/5.0 (iPhone; CPU iPhone OS 8_0 like Mac OS X) AppleWebKit/600.1.3 (KHTML, like Gecko) Version/8.0 Mobile/12A4345d Safari/600.1.4";
                    ChromeOptions chromeOptions = new ChromeOptions();
                    chromeOptions.AddArgument("--user-agent=" + ios6ua);
                    OutMsg("AGENT:" + ios6ua);

                    if (chMode.Checked)
                    {
                        chromeOptions.AddArgument("-incognito");
                    }
//                     FirefoxOptions firefoxOptions = new FirefoxOptions();
//                     firefoxOptions.ToCapabilities().("--user-agent=" + ios6ua);
                    driver =  new ChromeDriver(driverService, chromeOptions);
                    
                    driver.Manage().Cookies.DeleteAllCookies();
                    driver.Manage().Timeouts().SetPageLoadTimeout(new TimeSpan(0, 0, 10));
                    driver.Manage().Timeouts().ImplicitlyWait(new TimeSpan(0, 0, 5));

                    if (!chMode.Checked)
                    {
                        driver.Manage().Window.Size = new System.Drawing.Size(500, driver.Manage().Window.Size.Height);
                    }

                    if (chMode.Checked)
                    {
                         driver.Navigate().GoToUrl("chrome://extensions-frame");
                         IWebElement checkbox = driver.FindElement(By.XPath("//*[@id=\"aapnijgdinlhnhlmodcfapnahmbfebeb\"]/div/div[1]/div[6]/div[1]/label/input"));
                         if (!checkbox.Selected)
                         {
                              checkbox.Click();
                         }
                    }

                    // //reg.taobao.com/member/reg/h5/fill_email.htm
                    driver.Navigate().GoToUrl("http://www.baidu.com");
                   // driver.Navigate().GoToUrl("https://login.taobao.com/member/login.jhtml?newMini=true&from=tmall-wap&redirectURL=https%3A%2F%2Fwww.tmall.com");
                    SMSProxy.InitInstance(AccountFactory.getInstance().getSMSAcc().UserName,AccountFactory.getInstance().getSMSAcc().Passwd,
                         AccountFactory.getInstance().getSMSAcc().ProjID, AccountFactory.getInstance().getSMSAcc().PlatformName);
                    SMSProxy.AsynLogin();

                    // 初始化dom位置
                    if (chHalfAuto.Checked)
                    {
                        GetDomPosition();
                    }
                }
                catch (System.Exception ex)
                {
                    processControlor.GotoHanlderUnit("CloseDriver", 1000);
                }

            }
            return true;
        }

        private void OutPutFailedAndDeadAccount()
        {
//             try
//             {
//                 StreamWriter failAccount = new StreamWriter(@"失败小号.txt");
// 
//                 if (deadEntity.Count > 0)
//                 {
//                     failAccount.WriteLine(string.Format("=============== {0} 个超过3次未激活成功===============", deadEntity.Count));
//                     foreach (AccountEntity ae in deadEntity)
//                     {
//                         failAccount.WriteLine(ae.ToString());
//                     }
//                 }
//                 if (failedEntity.Count > 0)
//                 {
//                     failAccount.WriteLine(string.Format("=============== {0} 个激活失败 ===============", failedEntity.Count));
//                     foreach (AccountEntity ae in failedEntity)
//                     {
//                         failAccount.WriteLine(ae.ToString());
//                     }
//                 }
// 
//                 if (entities.Reminder() > 0)
//                 {
//                     failAccount.WriteLine(string.Format("===============剩余 {0} 个没有激活===============", entities.Reminder().ToString()));
//                     int oldIdx = entities.GetIndex();
//                     while (entities.HasNext())
//                     {
//                         failAccount.WriteLine(entities.Next().ToString());
//                     }
//                     entities.SetIndex(oldIdx);
//                 }
// 
//                 failAccount.Flush();
//                 failAccount.Close();
//             }
//             catch (System.Exception ex)
//             {
//                 OutMsg("保存失败号失败," + ex.Message);
//             }
//             
         }

        private int confirmTimes = 5;
        private int curConfirmTimes = 0;
        private bool isRegisterSuc = true;

        private bool CheckSuccessRegister(Object sender, Object param)
        {
            try
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
                        return true;
                    }
                    try
                    {
                        driver.Manage().Timeouts().ImplicitlyWait(new TimeSpan(0, 0, 1));
                        //*[@id="J_MyInfoTrigger"]
                        // //*[@id="J_SearchForm"]/div/div/input
                        IWebElement submit = driver.FindElement(By.XPath("//*[@id=\"J_MobileSearch\"]"));
                        if (submit == null)
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
                        try
                        {
                            driver = driver.SwitchTo().ParentFrame();
                            OutMsg("跳转到父frame");
                            driver.Manage().Timeouts().ImplicitlyWait(new TimeSpan(0, 0, 1));
                            IWebElement fram = driver.FindElement(By.XPath("//*[@id=\"m\"]"));
                            driver = driver.SwitchTo().Frame(fram);
                        }
                        catch (System.Exception ex1)
                        {
                            OutMsg("没有内嵌的frame");
                        }
                        OutMsg(ex.Message);
                        callingOnce = false;
                    }
                    finally
                    {
                        driver.Manage().Timeouts().ImplicitlyWait(new TimeSpan(0, 0, 5));
                    }
                    if (!callingOnce)
                    {
                        return callingOnce;
                    }

                    isRegisterSuc = true;
                }
            }
            catch (System.ObjectDisposedException ex)
            {
            	
            }
            return callingOnce;
        }

        private bool CloseDriver(Object sender, Object param)
        {
            try
            {

                if (this.InvokeRequired)
                {
                    this.Invoke(new AccessHandler(CloseDriver), sender, param);
                }
                else
                {
                    if (driver == null)
                    {
                        isDial = false;
                        return true;
                    }
                    UpdateAccountStatus();
                    try
                    {
                        DumpAccount();
                        driver.Quit();
                        driver = null;
                    }
                    catch
                    {
                        OutMsg("退出错误");
                    }
                    isDial = false;
                    isContditionFinished = true;
                }
            }
            catch (System.ObjectDisposedException ex)
            {
            	
            }
            return true;
        }

        private void UpdateAccountStatus()
        {
            if (isRegisterSuc)
            {
                curEntity.registerIP = currentIp;
                curEntity.registerTime = DateTime.Now;
                OutMsg("注册成功：" + curEntity.ToString());
                sucNum++;
                SucLabel.Text = sucNum.ToString();

                OutputSucAccount(@"成功小号.txt");

            }
            else
            {
                OutMsg("注册失败：" + curEntity.ToString());
                curEntity.useCnt++;

                OutputSucAccount(@"失败小号.txt");

                if (curEntity.useCnt >= 3)
                {
                    deadEntity.Add(curEntity);
                }
                else
                {
                    failedEntity.Add(curEntity);
                }
                failedLabel.Text = failedEntity.Count.ToString();
                labDead.Text = deadEntity.Count.ToString();

            }
            reminderLabel.Text = entities.Reminder().ToString();
        }

        private void OutputSucAccount(string p)
        {
            StreamWriter sucAccout = new StreamWriter(p, true);
            sucAccout.WriteLine(curEntity.ToString());
            sucAccout.Flush();
            sucAccout.Close();
        }

        bool isDial = false;
        string tempIp = "";
        private void ChangeIP()
        {
            try
            {
                // 自动拨号
                OutMsg("断线重拨，等待" + DailConfig.getInstance().waittingTime.ToString() + "秒...");

                try
                {
                    if (IsUseVPN)
                    {
                        DisVpn();
                    }
                    else
                    {
                        DailConfig.getInstance().DisConnect();
                    }
                }
                catch
                {
                    OutMsg("断开连接失败...");
                }

                OutMsg("已断开连接");
                if (!IsUseVPN)
                {
                    try
                    {
                        System.Threading.Thread.Sleep(DailConfig.getInstance().waittingTime * 1000);
                    }
                    catch
                    {
                        OutMsg("等待失败");
                    }
                }

                OutMsg("启动连接...");

                int rtn = 0;
                if (IsUseVPN)
                {
                    LoginVpn();
                    
                }
                else
                {
                    rtn = DailConfig.getInstance().Connect();
                }

                if (rtn == 0)
                {
                    if (IsUseVPN)
                    {
                        try
                        {
                            System.Threading.Thread.Sleep(DailConfig.getInstance().waittingTime * 1000);
                        }
                        catch
                        {
                            OutMsg("等待失败");
                        }

                        tempIp = GetIP();
                       // tempIp = GetIPSection(tempIp, GlobalSettings.getInstance().adslGap);
                        while (tempIp == string.Empty||
                               tempIp == currentIp)
                        {
                            OutMsg("等待VPN连接...");
                            System.Threading.Thread.Sleep(1000 * 2);
                            tempIp = GetIP();
                        }
                    }
                    tempIp = GetIP();
                    if (tempIp == "" || IsGetIpTimeouts)
                    {
                        OutMsg("重拨失败,网速过慢,重新拨号");
                        isDial = false;
                        callingOnce = false;
                    }
                    else
                    {
                        OutMsg("重拨成功!");
                        isDial = true;
                    }

                }
                else
                {
                    OutMsg("重拨失败!,继续重拨...");
                    isDial = false;
                    callingOnce = false;
                }

                isFinished = true;
            }
            catch 
            {
                OutMsg("切换IP失败");
            }
           
          //  isPasteFinished = true;
        }

        bool isFinished = true;
        Mutex mutex = new Mutex();
        private bool ReconnectNetwork(Object sender, Object param)
        {
            try
            {

                if (this.InvokeRequired)
                {
                    this.Invoke(new AccessHandler(ReconnectNetwork), sender, param);
                }
                else
                {
                    try
                    {

                        callingOnce = true;

                        if (!isDial)
                        {
                            isDial = true;
                            isFinished = false;
                            //  isPasteFinished = false;
                            //    ChangeIP();
                            Thread th = new Thread(new ThreadStart(ChangeIP));
                            th.Start();

                        }

                        if (!isFinished)
                        {
                            OutMsg("等待连接...");
                            callingOnce = false;
                            return callingOnce;
                        }

                        AddrLabel.Text = "当前IP:" + tempIp;
                        if (!isRegisterSuc)
                        {
                            if (tempIp.Equals(currentIp))
                            {
                                isDial = false;
                                callingOnce = false;
                                return true;
                            }
                        }
                        else
                        {
                            // 检查是否满足拨号条件
                            OutMsg("检查是否满足拨号条件");
                            if (!IsSatifyIpRole(tempIp))
                            {
                                isDial = false;
                                OutMsg("不满足拨号规则,继续重拨...");
                                callingOnce = false;
                                return true;
                            }
                        }
                        if (!callingOnce)
                        {
                            return false;
                        }
                        currentIp = tempIp;
                        DumpIpAddr();
                    }
                    catch
                    {
                        OutMsg("错误:");
                    }
                }
            }
            catch (System.ObjectDisposedException ex)
            {
            	
            }
            return callingOnce;
        }

        // 是否满足ip拨号规则
        private bool IsSatifyIpRole(string temp)
        {
            if (temp.Trim() == "")
            {
                return false;
            }
            string num = GetIPSection(temp, GlobalSettings.getInstance().adslGap);
            if (num == string.Empty)
            {
                return false;
            }
            bool flag = true;
            if (ipAddrList.Contains(num))
            {
                flag = false;
            }
            ipAddrList.Add(num);

            return flag;
        }

        private string GetIPSection(string temp, string p)
        {
            string[] allSecs = temp.Split(new char[] { '.' });
            if (allSecs.Length != 4)
            {
                return string.Empty;
            }
            string rnt = allSecs[0] + "." + allSecs[1];
            if (p == "C")
            {
                rnt += "." + allSecs[2];
            }
            return rnt;
        }

        private bool ClickNext(Object sender, Object param)
        {
            try
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
                        OutMsg(ex.Message);
                        processControlor.GotoHanlderUnit("CloseDriver", 1000);
                    }
                }
            }
            catch (System.ObjectDisposedException ex)
            {
            	
            }
            return true;
        }

        private bool Navigate2EmailPage(Object sender, Object param)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new AccessHandler(Navigate2EmailPage), sender, param);
            }
            else
            {
                
                driver.Navigate().GoToUrl("http://mail.163.com/?dv=pc");
            }
            return true;

        }
        private bool ExcuteCondition(Object sender, Object param)
        {
            return isContditionFinished;
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

                    ClickElementByXpath(RegisterXpath.NEXT_BUTTON);

                }
                catch (System.Exception ex)
                {
                    OutMsg("SetPassword" + ex.Message);
                    processControlor.GotoHanlderUnit("CloseDriver", 1000);
                }

            }
            return true;
        }

        private bool SetUserName(Object sender, Object param)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new AccessHandler(SetUserName), sender, param);
            }
            else
            {
                callingOnce = true;
                IWebElement titleElement = driver.FindElement(By.XPath(RegisterXpath.CURRENT_TITLE_LABEL));
                if (!titleElement.Text.Trim().Equals(RegisterXpath.SET_USER_NAME_TEXT))
                {
                    callingOnce = false;
                    return true;
                }
                curEntity.acount = AccountFactory.getInstance().getAccountRoles().GetRandomAccName();
                IWebElement userElement = driver.FindElement(By.XPath(RegisterXpath.SET_USER_NAME_INPUT));
                InputKeys(userElement, curEntity.acount);

                // 点击设置
                ClickElementByXpath(RegisterXpath.NEXT_BUTTON);

            }
            return callingOnce;
        }

        private bool ConfirmUserName(Object sender, Object param)
        {
            try
            {

                if (this.InvokeRequired)
                {
                    this.Invoke(new AccessHandler(ConfirmUserName), sender, param);
                }
                else
                {
                    // 点击设置
                    try
                    {
                        try
                        {
                            ClickElementByXpath(RegisterXpath.CONFIRM_SET_USER_NAME);
                        }
                        catch (System.Exception ex)
                        {
                            ClickElementByXpath("/html/body/div[6]/div[3]/span[1]");
                        }
                        curConfirmTimes = 0;
                    }
                    catch (System.Exception ex)
                    {
                        OutMsg("erro:" + ex.Message);
                        processControlor.GotoHanlderUnit("CloseDriver", 1000);
                    }
                    CUR_LOOP_CNT = 0;
                    IsTheFirstTime = true;

                }
            }
            catch (System.ObjectDisposedException ex)
            {
            	
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
                    ReadOnlyCollection<IWebElement> allFrames = driver.FindElements(By.TagName("iframe"));
                    IWebElement curFrame = null;
                    for (int i = 0; i < allFrames.Count; i++)
                    {
                        if (allFrames[i].GetAttribute("id").Contains("frameBody"))
                        {
                            curFrame = allFrames[i];
                            break;
                        }
                    }

                    driver.SwitchTo().Frame(curFrame);
                    IWebElement element = driver.FindElement(By.XPath("/html/body/div/div[2]/div[2]/span"));
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
            Point pnt = GetElementPoint(element,new Point(element.Size.Width/2,element.Size.Height/2));
            MouseUtility.MoveCursor(fromPnt, pnt,SimulatorSettings.getInstance().GetMouseGap());
            
            element.Click();
        }

        private void WaitImageLoginFinished()
        {
//             if (!CheckImageFactory.GetCheckImage().IsLoginFinshed())
//             {
//                 System.Threading.Thread.Sleep(1000);
//                 WaitImageLoginFinished();
//             }
        }

        bool callingOnce = true;
        bool inputEmailing = false;
       private bool InputEmail(Object sender, Object param)
        {
            try
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
                        //                     SendKeys.SendWait(System.Windows.Forms.Keys.Tab.ToString());
                        //                     SendKeys.SendWait("ylc809324@163.com");
                        inputEmailing = true;
                        IWebElement emailElement = driver.FindElement(By.XPath(RegisterXpath.EMIAL_INPUT));
                        InputKeys(emailElement, curEntity.emailAccount);
                    }
                    catch (System.Exception ex)
                    {
                        OutMsg(ex.Message);
                        processControlor.GotoHanlderUnit("CloseDriver", 1000);
                    }

                }
            }
           catch (System.ObjectDisposedException ex)
           {

           }

            return callingOnce;

        }

       private bool ReNavigateToMall(Object sender, Object param)
       {
           try
           {

               if (this.InvokeRequired)
               {
                   this.Invoke(new AccessHandler(ReNavigateToMall), sender, param);
               }
               else
               {
                   try
                   {
                       // //*[@id="kw"]
                       callingOnce = true;
                       if (IsTheFirstTime)
                       {
                           driver.Manage().Timeouts().ImplicitlyWait(new TimeSpan(0, 0, 10));
                           IWebElement input = FindElement(By.XPath("//*[@id=\"index-kw\"]"));
                           input.SendKeys("天猫商城");

                           IWebElement search = FindElement(By.XPath("//*[@id=\"index-bn\"]"));
                           search.Click();
                           callingOnce = false;
                           IsTheFirstTime = false;
                           return true;
                       }
                       IWebElement inputText = FindElement(By.XPath("//*[@id=\"kw\"]"));
                       OutMsg(inputText.GetAttribute("value"));

                       MouseUtility.ClickAtXY(new Point(150, 250));

                   }
                   catch (System.Exception ex)
                   {
                       OutMsg(ex.Message);
                       processControlor.GotoHanlderUnit("CloseDriver", 1000);
                   }
                   finally
                   {
                       driver.Manage().Timeouts().ImplicitlyWait(new TimeSpan(0, 0, 5));
                   }

               }

               return callingOnce;
           }
           catch (System.ObjectDisposedException ex)
           {
               OutMsg("超时...");
           }

           return true;

       }

       private bool NavigateToLogin(Object sender, Object param)
       {
           try
           {

               if (this.InvokeRequired)
               {
                   this.Invoke(new AccessHandler(NavigateToLogin), sender, param);
               }
               else
               {
                   try
                   {
                       if (++curLoopTimes >= 5)
                       {
                           OutMsg("进入登录页面失败，重新注册");
                           callingOnce = true;
                           processControlor.GotoHanlderUnit("CloseDriver", 1000);
                           return true;
                       }
                       callingOnce = true;
                       IWebElement login = driver.FindElement(By.XPath("//*[@id=\"J_MyInfoTrigger\"]"));
                       login.Click();
                   }
                   catch (System.Exception ex)
                   {
                       callingOnce = false;
                       OutMsg("进入登录页面失败...，重新进入");
                   }

               }
           }
           catch (System.ObjectDisposedException ex)
           {
               OutMsg("超时...");
           }
           return callingOnce;

       }
       private bool InputEmailPassword(Object sender, Object param)
       {
           try
           {

               if (this.InvokeRequired)
               {
                   this.Invoke(new AccessHandler(InputEmailPassword), sender, param);
               }
               else
               {
                   try
                   {
                       callingOnce = true;
                       inputEmailing = true;
                       IWebElement emailPwdElement = driver.FindElement(By.XPath(RegisterXpath.EMIAL_PWD_INPUT));
                       InputKeys(emailPwdElement, curEntity.emailPasswd);

                   }
                   catch (System.Exception ex)
                   {
                       OutMsg(ex.Message);
                       processControlor.GotoHanlderUnit("CloseDriver", 1000);
                   }

               }
           }
           catch (System.ObjectDisposedException ex)
           {
               OutMsg("超时...");
           }
           return callingOnce;

       }

       private bool CheckDragButton(Object sender, Object param)
       {
           try
           {

               if (this.InvokeRequired)
               {
                   this.Invoke(new AccessHandler(CheckDragButton), sender, param);
               }
               else
               {
                   try
                   {
                       IWebElement dragBtn = driver.FindElement(By.XPath("//*[@id=\"_n1z\"]"));
                       if (dragBtn != null &&
                           dragBtn.Size.Width > 0)
                       {
                           Point pnt = GetElementPoint(dragBtn, new Point(20, 20));
                           Point toPnt = new Point(pnt.X + 300, pnt.Y);
                           MouseUtility.DragAndDown(pnt, toPnt);
                       }
                       else
                       {
                           OutMsg("没有滑动图片");
                           processControlor.GotoHanlderUnit("ClickLogin", 1000);
                       }

                   }
                   catch (System.Exception ex)
                   {
                       OutMsg(ex.Message);
                       processControlor.GotoHanlderUnit("CloseDriver", 1000);
                   }

               }
           }
           catch (System.ObjectDisposedException ex)
           {
               OutMsg("超时...");
           }

           return true;

       }

       float scaleX, scaleY = 0.0f;
       bool isImageCodeFinished = false;
       private bool DumpDragImage(Object sender, Object param)
       {
           if (this.InvokeRequired)
           {
               this.Invoke(new AccessHandler(DumpDragImage), sender, param);
           }
           else
           {
               try
               {
                   callingOnce = true;
                   try
                   {
                       IWebElement error = driver.FindElement(By.XPath("//*[@id=\"clickCaptcha\"]/div[4]/p"));
                       if (!error.Text.Contains("请点击图中的"))
                       {
                           callingOnce = false;
                       }
                   }
                   catch (System.Exception ex)
                   {
                   	   callingOnce = false;
                   }

                   if (!callingOnce)
                   {
                       OutMsg("图片验证码尚未显示");
                       return true;
                   }
                   Bitmap topBmp = TakeScreenshot(By.XPath("//*[@id=\"clickCaptcha\"]/div[4]/p"));
                   Bitmap topTargetBmp = new Bitmap(200, 30);
                   Graphics gTop = Graphics.FromImage(topTargetBmp);
                   gTop.DrawImage(topBmp, new Rectangle(0, 0, 200, 30), new Rectangle(0, 0, topBmp.Width, topBmp.Height), GraphicsUnit.Pixel);
                   topTargetBmp.Save(@"top.jpg");
                   topTargetBmp.Dispose();
                   topBmp.Dispose();

                   Thread.Sleep(1000);
                   
                   Bitmap buttomBmp = TakeScreenshot(By.XPath("//*[@id=\"clickCaptcha\"]/div[2]/img"));
                   OutMsg("orgin X Y" + buttomBmp.Width.ToString() + " " + buttomBmp.Height.ToString());
                   buttomBmp.Save("buttom1.jpg");
                   Bitmap targetBmp = new Bitmap(200, 200);

                   scaleX = 200.0f / buttomBmp.Width;
                   scaleY = 200.0f / buttomBmp.Height;
                   Graphics g = Graphics.FromImage(targetBmp);
                   g.DrawImage(buttomBmp, new Rectangle(0, 0, 200, 200), new Rectangle(0, 0, buttomBmp.Width, buttomBmp.Height), GraphicsUnit.Pixel);
                   targetBmp.Save(@"buttom.jpg");
                   targetBmp.Dispose();
                   buttomBmp.Dispose();

                   // GC.Collect();
                   // 提交图片验证码
                   isImageCodeFinished = false;
                   Thread th = new Thread(new ThreadStart(SubmitDragImage));
                   th.Start();

                   return true;

               }
               catch (System.Exception ex)
               {
                   OutMsg(ex.Message);
                   processControlor.GotoHanlderUnit("CloseDriver", 1000);
               }

           }

           return callingOnce;

       }

       string coords = ""; //返回的图片坐标
       private void SubmitDragImage()
       {
           WaitImageLoginFinished();
           try
           {
               ImageUtility.CombinImage(@"top.jpg", @"buttom.jpg", "drag.jpg");
               int codeType = Convert.ToInt32(AccountFactory.getInstance().getImageCodeAcc().ProjID);
               coords = CheckImageFactory.GetCheckImage().RecognizeByCodeTypeAndPath("drag.jpg", codeType);
               isImageCodeFinished = true;
           }
           catch (System.Exception ex)
           {
               OutMsg("提交验证码失败：" + ex.Message);
           }


       }
       // 点击图片
       private bool ClickDragImage(Object sender, Object param)
       {

           try
           {

               if (this.InvokeRequired)
               {
                   this.Invoke(new AccessHandler(ClickDragImage), sender, param);
               }
               else
               {
                   try
                   {
                       callingOnce = true;
                       if (!isImageCodeFinished)
                       {
                           OutMsg("尚未接收到验证坐标");
                           callingOnce = false;
                           return true;
                       }

                       string[] xy = coords.Split(new char[] { ',' });
                       if (xy.Length != 2)
                       {
                           OutMsg("获取图片验证码失败");
                           return true;
                       }

                       OutMsg("返回坐标：" + coords);
                       int x = (int)(Convert.ToInt32(xy[0]) / scaleX);
                       int y = (int)((Convert.ToInt32(xy[1]) - 30) / scaleY);

                       IWebElement web = driver.FindElement(By.XPath("//*[@id=\"clickCaptcha\"]/div[2]/img"));
                       ActiveBrowserWindow();
                       domPnt = GetDomPosition();
                       // ScrollToTop();
                       Point clickPoint = new Point(domPnt.X + x + web.Location.X, domPnt.Y + y + web.Location.Y);
                       OutMsg(string.Format("X {0} Y {1}", clickPoint.X, clickPoint.Y));
                       ActiveBrowserWindow();
                       MouseUtility.ClickAtXY(clickPoint);

                   }
                   catch (System.Exception ex)
                   {
                       OutMsg(ex.Message);
                       processControlor.GotoHanlderUnit("CloseDriver", 1000);
                   }

               }
           }
          catch (System.ObjectDisposedException ex)
          {
          	
          }
           return callingOnce;

       }
       // 检查图片验证码是否通过
       private bool CheckDragImageStatus(Object sender, Object param)
       {
           if (this.InvokeRequired)
           {
               this.Invoke(new AccessHandler(CheckDragImageStatus), sender, param);
           }
           else
           {
               try
               {
                   IWebElement errorMsg = driver.FindElement(By.XPath("//*[@id=\"_scale_text\"]/span/b"));
                   if (errorMsg.Text.Contains("验证通过"))
                   {
                       OutMsg(errorMsg.Text);
                       return true;
                   }
               }
               catch (System.Exception ex)
               {
                   try
                   {
                       emailRegisterCount++;
                       if (emailRegisterCount <= GlobalSettings.getInstance().ImageLoopTimes)
                       {
                           OutMsg("图片验证码失败,刷新继续注册...");
                           driver.Navigate().Refresh();
                           processControlor.GotoHanlderUnit("InputEmail", 1000);
                       }
                       else
                       {
                           OutMsg("图片验证码失败,继续下一个账号注册");
                           processControlor.GotoHanlderUnit("CloseDriver", 1000);
                       }
                   }
                   catch (System.Exception ex1)
                   {
                       OutMsg("错误：" + ex1.Message);
                       processControlor.GotoHanlderUnit("CloseDriver", 1000);
                   }


                   return true;
               }

           }

           return true;

       }

       private bool SubmitImageCode(Object sender, Object param)
       {
           if (this.InvokeRequired)
           {
               this.Invoke(new AccessHandler(SubmitImageCode), sender, param);
           }
           else
           {
               try
               {

                   Bitmap bitmap = TakeScreenshot(By.XPath("//*[@id=\"J_StandardCode_m\"]"));
                   bitmap.Save("1.jpg");
                   bitmap.Dispose();

                   Thread.Sleep(1000);

                   WaitImageLoginFinished();
                   IWebElement image = driver.FindElement(By.XPath("//*[@id=\"J_CodeInput_i\"]"));
                   int codeType = Convert.ToInt32(AccountFactory.getInstance().getImageCodeAcc().ProjID);
                   string imageCode = CheckImageFactory.GetCheckImage().RecognizeByCodeTypeAndPath(Config.PATH_CHECKCODE_IMG, codeType);
                   OutMsg("图片验证码：" + imageCode);
                   InputKeys(image, imageCode);
               }
               catch (System.Exception ex)
               {
                   OutMsg(ex.Message);
                   processControlor.GotoHanlderUnit("CloseDriver", 1000);
               }

           }

           return callingOnce;

       }
       private bool ClickLogin(Object sender, Object param)
       {
           if (this.InvokeRequired)
           {
               this.Invoke(new AccessHandler(ClickLogin), sender, param);
           }
           else
           {
               try
               {
                   ClickElementByXpath(RegisterXpath.LOGIN_BUTTON);
                   CUR_LOOP_CNT = 0;

                 
               }
               catch (System.Exception ex)
               {
                   OutMsg(ex.Message);
                   processControlor.GotoHanlderUnit("CloseDriver", 1000);
               }

           }

           return callingOnce;

       }
       private bool CheckShowImageCheck(Object sender, Object param)
       {
           if (this.InvokeRequired)
           {
               this.Invoke(new AccessHandler(CheckShowImageCheck), sender, param);
           }
           else
           {
               try
               {

                   callingOnce = true;
                   bool imageCodeShow = false;
                   try
                   {

                       IWebElement imageElemnet = driver.FindElement(By.XPath("//*[@id=\"J_CodeInput_i\"]"));
                       imageCodeShow = true;
                   }
                   catch (System.Exception ex)
                   {
                       
                   }
                   if (!imageCodeShow)
                   {
                       processControlor.GotoHanlderUnit("ClickLogin", 1000);
                       callingOnce = true;
                       return true;
                   }

                   try
                   {

                       IWebElement image = driver.FindElement(By.XPath("//*[@id=\"J_StandardCode_m\"]"));
                       if (image==null)
                       {
                           OutMsg("等待图片验证码出现...");
                           callingOnce = false;
                           return true;
                       }
                   }
                   catch
                   {
                       callingOnce = false;
                   }
                   return callingOnce;
               }
               catch (System.Exception ex)
               {
                   OutMsg(ex.Message);
                   processControlor.GotoHanlderUnit("CloseDriver", 1000);
               }

           }

           return callingOnce;

       }

       // IP地址规则
       List<string> ipAddrList = new List<string>();
       string bIpAddr = @"Ip.txt";
       private void ParseIPAddr()
       {
           // 剩余的号
           ipAddrList.Clear();
           if (!File.Exists(bIpAddr))
           {
               return;
           }
           FileStream fs = new FileStream(GlobalSettings.getInstance().adslGap + bIpAddr, FileMode.Open);
           BinaryFormatter bf = new BinaryFormatter();
           ipAddrList = bf.Deserialize(fs) as List<string>;
           fs.Close();
       }

       private void DumpIpAddr()
       {
           FileStream fs = new FileStream(GlobalSettings.getInstance().adslGap + bIpAddr, FileMode.Create);
           BinaryFormatter bf = new BinaryFormatter();
           bf.Serialize(fs, ipAddrList);
           fs.Close();
       }
       private bool CheckLoginSuc(Object sender, Object param)
       {
           try
           {

               if (this.InvokeRequired)
               {
                   this.Invoke(new AccessHandler(CheckLoginSuc), sender, param);
               }
               else
               {
                   callingOnce = true;
                   try
                   {
                       CUR_LOOP_CNT++;
                       driver.Manage().Timeouts().ImplicitlyWait(new TimeSpan(0, 0, 1));
                       IWebElement title = driver.FindElement(By.XPath(RegisterXpath.CURRENT_TITLE_XPATH));
                       OutMsg(title.Text);
                       if (title.Text.Contains(RegisterXpath.TITLE_FILL_USER_NAME))
                       {
                           callingOnce = true;
                           return true;
                       }
                       callingOnce = false;
                   }
                   catch (System.Exception ex)
                   {
                       callingOnce = false;
                       if (CUR_LOOP_CNT >= MAX_LOOP)
                       {
                           OutMsg("登录异常...");
                           curConfirmTimes = 0;
                           processControlor.GotoHanlderUnit("CheckSuccessRegister", 1000);
                       }
                       else
                       {
                           OutMsg("等待登录...");
                       }

                       try
                       {
                           //  driver = driver.SwitchTo().Frame("m");

                           try
                           {
                               IWebElement frame = driver.FindElement(By.XPath("//*[@id=\"m\"]"));
                               driver = driver.SwitchTo().Frame(frame);
                           }
                           catch
                           {

                           }
                           driver.Manage().Timeouts().ImplicitlyWait(new TimeSpan(0, 0, 1));
                           IWebElement title = driver.FindElement(By.XPath("//*[@id=\"J_Message\"]/p"));
                           if (title.Text != "")
                           {
                               callingOnce = true;
                               OutMsg("错误：" + title.Text);
                               processControlor.GotoHanlderUnit("CloseDriver", 1000);
                           }

                       }
                       catch
                       {
                           callingOnce = false;
                           OutMsg("正在登陆...");
                       }

                   }
                   finally
                   {

                       driver.Manage().Timeouts().ImplicitlyWait(new TimeSpan(0, 0, 5));
                   }

               }
           }

           catch (System.ObjectDisposedException ex)
           {
               OutMsg("超时");
           }
           return callingOnce;

       }

       bool IsTheFirstTime = true;
       SMSResult smsRlt;
       private bool PreHandler(Object sender, Object param)
       {
           if (this.InvokeRequired)
           {
               this.Invoke(new AccessHandler(PreHandler), sender, param);
           }
           else
           {
               IsTheFirstTime = true;
               repeatTimes = 0;
               curLoopTimes = 0;
           }
           return true;
       }

       int repeatTimes = 0;
       private bool InputPhone(Object sender, Object param)
       {
           try
           {

               if (this.InvokeRequired)
               {
                   this.Invoke(new AccessHandler(InputPhone), sender, param);
               }
               else
               {
                   try
                   {
                       callingOnce = true;
                       IWebElement titleElement = driver.FindElement(By.XPath(RegisterXpath.CURRENT_TITLE_XPATH));

                       if (!titleElement.Text.Trim().Equals(RegisterXpath.TITLE_IDENTITY_VERIFY))
                       {
                           OutMsg("图片验证码错误...");
                           CUR_LOOP_CNT = 0;
                           beginToClickClearBtn = true;
                           processControlor.GotoHanlderUnit("FillUserName", 1000);
                           return false;
                       }

                       if (!SMSProxy.IsLogined())
                       {
                           OutMsg("等待短信验证码登录...");
                           callingOnce = false;
                           return false;
                       }
                       OutMsg("短信验证码登录成功");
                       // SMSProxy.GetSmsInstance().ReleaseAll();
                       if (!SMSProxy.GetSmsInstance().IsLogined())
                       {
                           callingOnce = false;
                           return false;
                       }

                       if (IsTheFirstTime)
                       {
                           IsTheFirstTime = false;
                           isFinished = false;
                           Thread th = new Thread(new ThreadStart(delegate()
                           {
                               smsRlt = SMSProxy.GetSmsInstance().GetPhoneNum();
                               OutMsg(smsRlt.Result);
                               isFinished = true;
                           }));
                           th.Start();
                       }

                       if (isFinished == false)
                       {
                           OutMsg("等待获取电话号码....");
                           if (++repeatTimes >= 10)
                           {
                               repeatTimes = 0;
                               IsTheFirstTime = true;
                               OutMsg("重新登录，释放所有号码,重新获取手机号码");
                               Thread th = new Thread(new ThreadStart(delegate()
                               {
                                   SMSProxy.GetSmsInstance().Login();
                                   SMSProxy.GetSmsInstance().ReleaseAll();
                               }));
                               th.Start();
                           }
                           processControlor.ResetRepeatTimes();
                           callingOnce = false;
                           return false;
                       }

                       if (smsRlt.Status == false)
                       {
                           OutMsg(smsRlt.Result);
                           callingOnce = false;
                           IsTheFirstTime = true;
                           return false;
                       }
                       curEntity.phoneNum = smsRlt.Result;
                       IWebElement phoneCodeElement = driver.FindElement(By.XPath(RegisterXpath.PHONE_NUM_INPUT));
                       InputKeys(phoneCodeElement, curEntity.phoneNum);

                   }
                   catch (System.Exception ex)
                   {
                       OutMsg(ex.Message);
                       processControlor.GotoHanlderUnit("CloseDriver", 1000);
                   }

               }
           }
           catch (System.ObjectDisposedException ex)
           {
           	
           }

           return callingOnce;

       }

       private bool ClickPhoneNextBtn(Object sender, Object param)
       {
           try
           {

               if (this.InvokeRequired)
               {
                   this.Invoke(new AccessHandler(ClickPhoneNextBtn), sender, param);
               }
               else
               {
                   try
                   {
                       ClickElementByXpath(RegisterXpath.NEXT_BUTTON);
                   }
                   catch (System.Exception ex)
                   {
                       OutMsg(ex.Message);
                       processControlor.GotoHanlderUnit("CloseDriver", 1000);
                   }

               }
           }
           catch (System.ObjectDisposedException ex)
           {
           	
           }

           return callingOnce;

       }
       
       private bool FillUserName(Object sender, Object param)
       {
           try
           {

               if (this.InvokeRequired)
               {
                   this.Invoke(new AccessHandler(FillUserName), sender, param);
               }
               else
               {
                   try
                   {
                       //                    CUR_LOOP_CNT++;
                       //                    if (CUR_LOOP_CNT >= MAX_LOOP)
                       //                    {
                       //                        OutMsg("重新开始...");
                       //                        processControlor.GotoHanlderUnit("CloseDriver", 1000);
                       //                    }
                       callingOnce = true;
                       IWebElement titleElement = driver.FindElement(By.XPath(RegisterXpath.CURRENT_TITLE_XPATH));
                       if (!titleElement.Text.Trim().Equals(RegisterXpath.TITLE_FILL_USER_NAME))
                       {
                           OutMsg("正在登录...");
                           callingOnce = false;
                           return false;
                       }

                       curEntity.acount = AccountFactory.getInstance().getAccountRoles().GetRandomAccName();// RandomGenerator.generateEnName(4, 0) + RandomGenerator.getRandom().Next(9) + RandomGenerator.getRandom().Next(9);
                       IWebElement userElement = driver.FindElement(By.XPath(RegisterXpath.USER_NAME_INPUT));
                       if (AccountFactory.getInstance().getAccountRoles().IsRandomChinaName &&
                           AccountFactory.getInstance().getAccountRoles().randomChina != "")
                       {
                           ClickAtElement(userElement);
                           Thread.Sleep(500);
                           SendKeys.SendWait(curEntity.acount);
                       }
                       else
                       {
                           InputKeys(userElement, curEntity.acount);
                       }


                       //                    Thread.Sleep(500);
                       // 
                       //                    IWebElement imgCodeElement = driver.FindElement(By.XPath(RegisterXpath.IMAGE_CODE_INPUT));
                       //                    InputKeys(imgCodeElement, "");

                   }
                   catch (System.Exception ex)
                   {
                       OutMsg(ex.Message);
                       processControlor.GotoHanlderUnit("CloseDriver", 1000);
                   }

               }
           }
           catch (System.ObjectDisposedException ex)
           {
           	
           }
           return callingOnce;

       }
       private bool TrigeImageCodeShow(Object sender, Object param)
       {
           try
           {

               if (this.InvokeRequired)
               {
                   this.Invoke(new AccessHandler(TrigeImageCodeShow), sender, param);
               }
               else
               {
                   try
                   {

                       IWebElement imgCodeElement = driver.FindElement(By.XPath(RegisterXpath.IMAGE_CODE_INPUT));
                       InputKeys(imgCodeElement, "");

                   }
                   catch (System.Exception ex)
                   {
                       OutMsg(ex.Message);
                       processControlor.GotoHanlderUnit("CloseDriver", 1000);
                   }

               }
           }
           catch (System.ObjectDisposedException ex)
           {
           	
           }

           return callingOnce;

       }


       Point fromPnt = Point.Empty;
       Point toPnt = Point.Empty;
       Point offset = new Point(80, 10);
       Point offsetEmpty = new Point(0, 0);
       IWebElement currentInputElement = null;
       public delegate void InputHandler(IWebElement sender, string param);
        private void InputKeys(IWebElement email, string p)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new InputHandler(InputKeys), email,p);
            }
            else
            {
                try
                {
                    if (beginToClickClearBtn)
                    {
                        OutMsg("清除...");
                        ClickRightCenter(email);
                    }


                    ClickAtElement(email);

                    OutMsg("输入：" + p);
                    fromPnt = toPnt;
                    currentInputElement = email;
                    if (p.Length > 0)
                    {
                        //  MouseUtility.DoCopy(p);
                        InvokeInput(p);
                    }
                }
                catch (System.Exception ex)
                {
                    OutMsg(ex.Message);
                    processControlor.GotoHanlderUnit("CloseDriver", 1000);
                }
               
            }
        }

        private void ClickAtElement(IWebElement email)
        {
            try
            {
                toPnt = GetElementPoint(email, offset);
                if (fromPnt == Point.Empty)
                {
                    fromPnt = new Point(toPnt.X + 300, toPnt.Y + 200);
                }
                ActiveBrowserWindow();
                //  MouseUtility.ClickAtXY(fromPnt);
                MouseUtility.MoveCursor(fromPnt, toPnt, SimulatorSettings.getInstance().mouse_speed, SimulatorSettings.getInstance().GetMouseGap());
                MouseUtility.ClickAtXY(toPnt);
            }
            catch (System.Exception ex)
            {
            	
            }

        }

        private void ClickRightCenter(IWebElement email)
        {
            ILocatable locale = (ILocatable)email;
            Point pnt = GetDomPosition();
            int gap = GetXGap();
            pnt = new Point(pnt.X + locale.Coordinates.LocationInDom.X + email.Size.Width + gap, pnt.Y + locale.Coordinates.LocationInDom.Y + (email.Size.Height / 2));
            if (fromPnt == Point.Empty)
            {
                fromPnt = new Point(pnt.X + 300, pnt.Y + 200);
            }
            ActiveBrowserWindow();
            MouseUtility.MoveCursor(fromPnt, pnt,SimulatorSettings.getInstance().GetMouseGap());
            MouseUtility.ClickAtXY(pnt);
            fromPnt = pnt;
        }

        private int GetXGap()
        {
            return SimulatorSettings.getInstance().GetGap();
        }

        string currentInputText = "";
        private void InvokeInput(string p)
        {
            currentInputText = p;
            timer1.Interval = 376 + RandomGenerator.getRandom().Next(300);
            timer1.Start();
            isContditionFinished = false;
        }

        private void ActiveBrowserWindow()
        {
            IntPtr pHandler = MouseUtility.FindWindow("Chrome_WidgetWin_1", "天猫 - 尚天猫，就购了 - Google Chrome");
            MouseUtility.SetForegroundWindow(pHandler);
        }
  
        private Point GetElementPoint(IWebElement email,Point offset)
        {
            
            ILocatable locale = (ILocatable)email;

            Point pnt = GetDomPosition();
            return new Point(pnt.X+locale.Coordinates.LocationInDom.X+offset.X,pnt.Y+locale.Coordinates.LocationInDom.Y+offset.Y);
            
        }
        Point domPnt = Point.Empty;
        private Point GetDomPosition()
        {
            if (domPnt == Point.Empty)
            {
                IntPtr pHandler = MouseUtility.FindWindow("Chrome_WidgetWin_1", "天猫 - 尚天猫，就购了 - Google Chrome");
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

        int curLoopTimes = 0;
        private bool InputPhoneCode(Object sender, Object param)
        {
            try
            {

                if (this.InvokeRequired)
                {
                    this.Invoke(new AccessHandler(InputPhoneCode), sender, param);
                }
                else
                {
                    callingOnce = true;
                    SMSResult msg = SMSProxy.GetSmsInstance().GetMessage();
                    curLoopTimes++;
                    if (curLoopTimes >= GlobalSettings.getInstance().smsLoopTimes)
                    {
                        OutMsg("超过等待时间");
                        SMSProxy.GetSmsInstance().AddIgnoreList();
                        try
                        {
                            IWebElement reback = driver.FindElement(By.XPath("//*[@id=\"J_PageBack\"]"));
                            reback.Click();
                            processControlor.GotoHanlderUnit("InputPhone", 1000);

                        }
                        catch (System.Exception ex)
                        {
                            processControlor.GotoHanlderUnit("CloseDriver", 1000);
                        }



                        return true;
                    }
                    if (!msg.Status)
                    {
                        callingOnce = false;
                        OutMsg("等待获取短信验证码... \r\n" + msg.Result);
                        return false;
                    }

                    Regex reg = new Regex("校验码是([\\d]+)");
                    Match m = reg.Match(msg.Result);

                    string phoneCode = "";
                    if (m.Success)
                    {
                        phoneCode = m.Groups[1].Value;
                    }

                    OutMsg("短信验证码为：" + phoneCode);

                    try
                    {
                        IWebElement phoneElement = driver.FindElement(By.XPath(RegisterXpath.SMS_CODE_INPUT));
                        InputKeys(phoneElement, phoneCode);
                    }
                    catch (System.Exception ex)
                    {
                        OutMsg(ex.Message);
                        processControlor.GotoHanlderUnit("CloseDriver", 1000);
                    }


                }
            }
            catch (System.ObjectDisposedException ex)
            {
            	
            }
            return callingOnce;
        }

        private bool ClickPhoneCodeNextBtn(Object sender, Object param)
        {
            try
            {

                if (this.InvokeRequired)
                {
                    this.Invoke(new AccessHandler(ClickPhoneCodeNextBtn), sender, param);
                }
                else
                {
                    try
                    {
                        ClickElementByXpath(RegisterXpath.NEXT_BUTTON);
                    }
                    catch (System.Exception ex)
                    {
                        OutMsg(ex.Message);
                        processControlor.GotoHanlderUnit("CloseDriver", 1000);
                    }
                }
            }
            catch (System.ObjectDisposedException ex)
            {
            	
            }
            return true;
        }
        private bool CheckPhoneCodePage(Object sender, Object param)
        {
            try
            {


                if (this.InvokeRequired)
                {
                    this.Invoke(new AccessHandler(CheckPhoneCodePage), sender, param);
                }
                else
                {
                    try
                    {
                        callingOnce = true;
                        IWebElement codeCheckIcon = driver.FindElement(By.XPath(RegisterXpath.CURRENT_TITLE_LABEL));
                        string title = codeCheckIcon.Text;
                        OutMsg(title);
                        if (!title.Trim().Equals(RegisterXpath.FILL_CHECK_TITLE_TEXT))
                        {
                            OutMsg("手机号码可不使用...,重新换手机号码");
                            if (IsTheFirstTime)
                            {
                                isFinished = false;
                                Thread th = new Thread(new ThreadStart(delegate()
                                {
                                    smsRlt = SMSProxy.GetSmsInstance().AddIgnoreList();
                                    isFinished = true;
                                }));
                                IsTheFirstTime = true;
                                th.Start();
                            }
                            beginToClickClearBtn = true;
                            processControlor.GotoHanlderUnit("InputPhone", 1000);
                        }
                        RegisterXpath.curphoneLoopTimes = 0;
                    }
                    catch (System.Exception ex)
                    {
                        OutMsg(ex.Message);
                        processControlor.GotoHanlderUnit("CloseDriver", 1000);
                    }

                }
            }
            catch (System.ObjectDisposedException ex)
            {


            }
            return true;
        }

        private bool CheckPhoneCheckPage(Object sender, Object param)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new AccessHandler(CheckPhoneCheckPage), sender, param);
            }
            else
            {
                callingOnce = true;
                IWebElement codeCheckIcon = driver.FindElement(By.XPath(RegisterXpath.CURRENT_TITLE_LABEL));
                string title = codeCheckIcon.Text;
                OutMsg(title);
                if (!title.Trim().Equals(RegisterXpath.PHONE_TITLE_TEXT))
                {
                    OutMsg("改手机可以直接注册");
                    registerMode = 2;
                    // 不用使用邮箱注册,跳转到不用使用邮箱的step
                    processControlor.GotoHanlderUnit("SetPassword", 1000);
                    return true;
                }

                ClickElementByXpath(RegisterXpath.USE_EMAIL_REGISTER_BUTTON);
            }
            return callingOnce;
        }

        bool getImageCodeFromNet = false;// 是否已经从打码平台得到图片验证码
        string imageCode; // 当前图片验证码
        private bool DumpImageCodeToFile(Object sender, Object param)
        {
            try
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
                        IWebElement codeCheckIcon = driver.FindElement(By.XPath(RegisterXpath.IMAGE_CODE_IMAGE));
                        if (codeCheckIcon == null)
                        {
                            callingOnce = false;
                        }

                        if (codeCheckIcon.Size.Width <= 0 ||
                            codeCheckIcon.Size.Height <= 0)
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
                        OutMsg("等待图片验证码显示");
                        processControlor.GotoHanlderUnit("TrigeImageCodeShow", 1000);
                        return false;
                    }
                    try
                    {
                        //                     IWebElement image = driver.FindElement(By.XPath(RegisterXpath.IMAGE_CODE_INPUT));
                        //                     InputKeys(image, "tss");
                        // 
                        //                     OutMsg("保存验证码成功");
                        Bitmap bitmap = TakeScreenshot(By.XPath(RegisterXpath.IMAGE_CODE_IMAGE));
                        bitmap.Save("1.jpg");
                        bitmap.Dispose();
                        OutMsg("保存验证码成功");

                        //                     // 检查图片是否正确
                        if (!Utilities.Utiliy.IsImageVaild("1.jpg"))
                        {
                            OutMsg("图片验证码错误!");
                            // 不用使用邮箱注册,跳转到不用使用邮箱的step
                            processControlor.GotoHanlderUnit("CloseDriver", 1000);
                            return true;
                        }

                        getImageCodeFromNet = false;
                        Thread th = new Thread(new ThreadStart(GetImageCode));
                        th.Start();

                    }
                    catch (System.Exception ex)
                    {
                        OutMsg(ex.Message);
                        // 不用使用邮箱注册,跳转到不用使用邮箱的step
                        processControlor.GotoHanlderUnit("CloseDriver", 1000);
                        return true;
                    }

                    return true;
                }
            }
            catch (System.ObjectDisposedException ex)
            {
            	
            }
            return callingOnce;


        }
        private void GetImageCode()
        {
            try
            {
                WaitImageLoginFinished();
                int codeType = Convert.ToInt32(GlobalSettings.getInstance().imageProjectType);
                imageCode = CheckImageFactory.GetCheckImage().RecognizeByCodeTypeAndPath(Config.PATH_CHECKCODE_IMG, codeType);
                getImageCodeFromNet = true;
            }
            catch (System.Exception ex)
            {
                OutMsg("图片验证码识别失败" + ex.Message);
            }

        }
        private bool InputImageCode(Object sender, Object param)
        {
            try
            {

                if (this.InvokeRequired)
                {
                    this.Invoke(new AccessHandler(InputImageCode), sender, param);
                }
                else
                {
                    callingOnce = true;

                    if (getImageCodeFromNet == false)
                    {
                        callingOnce = false;
                        return true;
                    }
                    IWebElement image = driver.FindElement(By.XPath(RegisterXpath.IMAGE_CODE_INPUT));
                    imageCode = imageCode.ToLower();
                    OutMsg("图片验证码：" + imageCode);

                    beginToClickClearBtn = true;
                    InputKeys(image, imageCode);

                }
            }
            catch (System.ObjectDisposedException ex)
            {
            	
            }
            return callingOnce;
        }

        public Bitmap TakeScreenshot(By by)
        {
//             // 1. Make screenshot of all screen
//             var screenshotDriver = driver as ITakesScreenshot;
//             Screenshot screenshot = screenshotDriver.GetScreenshot();
//             var bmpScreen = new Bitmap(new MemoryStream(screenshot.AsByteArray));
//             // 2. Get screenshot of specific element
//             IWebElement element = FindElement(by);
//             var cropArea = new Rectangle(element.Location, element.Size);
//             return bmpScreen.Clone(cropArea, bmpScreen.PixelFormat);

            IWebElement element = FindElement(by);
            Point pnt = GetElementPoint(element, Point.Empty);
            return ImageUtility.CaptureScreen(pnt.X, pnt.Y, element.Size.Width, element.Size.Height);
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
            UpdateAccountStatus();
            DumpAccount();
            DumpIpAddr();
            DumpIPAddrText();
        }
        string currentIp;
        bool IsGetIpTimeouts = false;
        private string GetIP()
        {
            IsGetIpTimeouts = false;
            string tempip = string.Empty;
            try
            {
                int startTime = System.Environment.TickCount;  
                WebRequest wr = WebRequest.Create("http://www.ip138.com/ips138.asp");
                wr.Timeout = GlobalSettings.getInstance().dailTimesout*1000;
                Stream s = wr.GetResponse().GetResponseStream();
                StreamReader sr = new StreamReader(s, Encoding.Default);
                string all = sr.ReadToEnd(); //读取网站的数据
                int endTime = System.Environment.TickCount;
                int runTime = endTime - startTime;

                int start = all.IndexOf("您的IP地址是：[") + 9;
                int end = all.IndexOf("]", start);
                tempip = all.Substring(start, end - start);
                sr.Close();
                s.Close();

                // 转化为KB
                double downLoadSize = all.ToCharArray().Length * 16 / 1024.0/1024.0;
                double times = runTime / 1000.0;;
                double speed = downLoadSize / times;

                OutMsg("当前网速为：" + speed + " M/s");
                if ((speed - GlobalSettings.getInstance().dailSpeed) < 0)
                {
                    OutMsg("网速过慢，重连网络");
                    IsGetIpTimeouts = true;
                }
            }
            catch
            {
                IsGetIpTimeouts = true;
            }
            return tempip;
        }

        public void OutMsg(string msg)
        {
            OutMsg(statusBox, msg, Color.Black);
        }
        public void OutMsg(RichTextBox rtb, string msg, Color color)
        {
            // 保存到日志文件
            Trace.WriteLine(msg);
            if (rtb != null)
            {
                try
                {
                    rtb.Invoke(new EventHandler(delegate
                    {
                        rtb.SelectionStart = rtb.Text.Length;//设置插入符位置为文本框末
                        rtb.SelectionColor = color;//设置文本颜色
                        string msgCxt = DateTime.Now.ToLongTimeString() + ": " + msg;
                        rtb.AppendText(msgCxt + "\r\n");//输出文本，换行
                        rtb.ScrollToCaret();//滚动条滚到到最新插入行
                    }));
                }
                catch
                {

                }

            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
           // entities = EntitiesArray.GetInstance().Refresh();
            DailConfig.getInstance().DisConnect();
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
            IWebElement emailElement = driver.FindElement(By.XPath(RegisterXpath.EMIAL_INPUT));
            emailElement.Click();
            string javaScript = "var evObj = document.createEvent('MouseEvents');" +
                               "evObj.initMouseEvent(\"mouseover\",true, false, window, 0, 0, 0, 0, 0, false, false, false, false, 0, null);" +
                               "arguments[0].dispatchEvent(evObj);";
            IJavaScriptExecutor js = driver as IJavaScriptExecutor;
            js.ExecuteScript(javaScript, emailElement);

            OutMsg("finished");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            DumpIPAddrText();
        }

        private void DumpIPAddrText()
        {
            StreamWriter write = new StreamWriter(@"IP地址库.txt");
            foreach (string ip in ipAddrList)
            {
                write.WriteLine(ip);
            }
            write.Flush();
            write.Close();
        }

        private void button7_Click(object sender, EventArgs e)
        {

        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            DumpAccount();
            DumpIpAddr();
            AccountFactory.getInstance().Save();
            DailConfig.getInstance().Save();
            GlobalSettings.getInstance().Save();
            SimulatorSettings.getInstance().Save();
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
          int i = DailConfig.getInstance().Connect();
            if (i ==0 )
            {
                MessageBox.Show("成功!");
            }
            else
            {
                MessageBox.Show("失败");
            }
        }

        private void SucLabel_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            IntPtr pHandler = MouseUtility.FindWindow("Chrome_WidgetWin_1", "天猫 - 尚天猫，就购了 - Google Chrome");
            IntPtr cHandler = IntPtr.Zero;
            do 
            {
                cHandler = MouseUtility.FindWindowEx(pHandler, cHandler, "Chrome_RenderWidgetHostHWND", null);
                if (cHandler == IntPtr.Zero)
                {
                    break;
                }
                Rect rect = new Rect();
                MouseUtility.GetWindowRect(cHandler, out rect);
                OutMsg(string.Format("子窗口 top:{0},left:{1},buttom:{2},right:{3}", rect.Top, rect.Left, rect.Bottom, rect.Right));
                MouseUtility.SetCursorPos(rect.Left, rect.Top);
            } while (cHandler != IntPtr.Zero);

        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            IntPtr handler = MouseUtility.FindWindow("Chrome_WidgetWin_1", "天猫 - 尚天猫，就购了 - Google Chrome");
            Rect rect = new Rect();
            MouseUtility.GetWindowRect(handler, out rect);
            OutMsg(string.Format("浏览器 top:{0},left:{1},buttom:{2},right:{3}", rect.Top, rect.Left, rect.Bottom, rect.Right));
            MouseUtility.SetCursorPos(rect.Left, rect.Top);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
           
//                 if (inputEmailing)
//                 {
//                     string text = Clipboard.GetText();
//                   //  OutMsg("剪切板内容：" + text);
//                     MouseUtility.ClickAtXY(fromPnt);
//                     MouseUtility.DoPaste();
//                     inputEmailing = false;
//                 }
//                 else
//                 {
            MouseUtility.InputText(currentInputText, SimulatorSettings.getInstance().GetInputGap());
//                }
            timer1.Stop();
            isContditionFinished = true;
        }

        private void button5_Click_2(object sender, EventArgs e)
        {
            IntPtr handler = MouseUtility.FindWindow("Notepad", "11.txt - 记事本");
            MouseUtility.SetForegroundWindow(handler);
            MouseUtility.InputText("123456");
        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            InitDriver(null, null);
        }

        private void button3_Click_2(object sender, EventArgs e)
        {
            isRegisterSuc = true;
            isDial = false;
            ReconnectNetwork(null, null);
        }

        
        private void btnLoginSMS_Click(object sender, EventArgs e)
        {
            btnLoginSMS.Enabled = false;
           Thread th = new Thread(new ThreadStart(delegate()
            {
                SMSResult rnt = SMSProxy.GetSmsInstance().Login();
                OutMsg(rnt.Result);
                isFinished = true;

                this.Invoke(new EventHandler(delegate
                {
                    btnLoginSMS.Enabled = true;
                }));
                
            }));
            IsTheFirstTime = false;
            th.Start();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            totalTimes += new TimeSpan(0, 0, 1);
            //TimeSpan temp = DateTime.Now.Subtract(beginTime);
            labelTotalTime.Text = string.Format("{0}:{1}:{2}:{3}", totalTimes.Days, totalTimes.Hours, totalTimes.Minutes, totalTimes.Seconds);
        }

        private void button4_Click_2(object sender, EventArgs e)
        {
            ProcessUtility.KillProcess("chromedriver");
            ProcessUtility.KillProcess("chrome");
            OutMsg("清理完成");
        }

        private void button5_Click_3(object sender, EventArgs e)
        {
            int index = entities.GetIndex();
            try
            {
               
                StreamWriter write = new StreamWriter(@"未使用号.txt");
                while (entities.HasNext())
                {
                    write.WriteLine(entities.Next().ToString());
                }
                write.Flush();
                write.Close();
                
                OutMsg("导出未使用号成功");
            }
            catch (System.Exception ex)
            {
                OutMsg("导出未使用号失败");
            }
            finally
            {
                entities.SetIndex(index);
            }

        }

        private void button7_Click_1(object sender, EventArgs e)
        {
            string accoutn = AccountFactory.getInstance().getAccountRoles().GetRandomAccName();
            MessageBox.Show(accoutn);
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            entities.AddList(EntitiesArray.GetDatas());
            totalLabel.Text = entities.count().ToString();
            reminderLabel.Text = entities.Reminder().ToString();
        }

        private void btnClearAll_Click(object sender, EventArgs e)
        {
            entities.Clear();
            totalLabel.Text = "0";
            failedEntity.Clear();

            failedLabel.Text = "0";
            SucLabel.Text = "0";
            reminderLabel.Text = "0";
            CUR_LOOP_CNT = 0;
        }

        private void btnClearFailed_Click(object sender, EventArgs e)
        {
            failedEntity.Clear();
            failedLabel.Text = "0";
        }

        private void btnDeadLoop_Click(object sender, EventArgs e)
        {
            foreach (AccountEntity en in deadEntity)
            {
                en.useCnt = 0;
                entities.Add(en);
            }
            
            totalLabel.Text = entities.count().ToString();

            deadEntity.Clear();
            labDead.Text = "0";
            CUR_LOOP_CNT = 0;
        }

        private void btnExportDead_Click(object sender, EventArgs e)
        {
            try
            {

                StreamWriter write = new StreamWriter(@"使用三次未成功的号.txt");
                for (int i = 0; i < deadEntity.Count;i++ )
                {
                    write.WriteLine(deadEntity[i].ToString());
                }
                write.Flush();
                write.Close();

                OutMsg("导出死号成功");
            }
            catch (System.Exception ex)
            {
                OutMsg("导出死号失败");
            }
        }

        private void btnHalfFinish_Click(object sender, EventArgs e)
        {
            isContditionFinished = true;
        }

        private void btnGetPhoneNumber_Click(object sender, EventArgs e)
        {
            btnGetPhoneNumber.Enabled = false;
            Thread th = new Thread(new ThreadStart(delegate()
            {
                SMSResult rnt = SMSProxy.GetSmsInstance().GetPhoneNum();
                OutMsg(rnt.Result);
                isFinished = true;
                this.Invoke(new EventHandler(delegate
                {
                    btnGetPhoneNumber.Enabled = true;
                }));
                
            }));
            IsTheFirstTime = false;
            th.Start();
        }

        private void btnAddIgnore_Click(object sender, EventArgs e)
        {
            btnAddIgnore.Enabled = false;
            Thread th = new Thread(new ThreadStart(delegate()
            {
                SMSResult rnt = SMSProxy.GetSmsInstance().AddIgnoreList();
                OutMsg(rnt.Result);
                isFinished = true;
                this.Invoke(new EventHandler(delegate
                {
                    btnAddIgnore.Enabled = true;
                }));
                
            }));
            IsTheFirstTime = false;
            th.Start();
        }

        private void btnGetMessage_Click(object sender, EventArgs e)
        {
            btnGetMessage.Enabled = false;
            Thread th = new Thread(new ThreadStart(delegate()
            {
                SMSResult rnt = SMSProxy.GetSmsInstance().GetMessage();
                OutMsg(rnt.Result);
                isFinished = true;
                this.Invoke(new EventHandler(delegate
                {
                    btnGetMessage.Enabled = true;
                }));
               
            }));
            IsTheFirstTime = false;
            th.Start();
        }

        private void btnReleaseAllPhones_Click(object sender, EventArgs e)
        {
            btnReleaseAllPhones.Enabled = false;
            Thread th = new Thread(new ThreadStart(delegate()
            {
                SMSResult rnt = SMSProxy.GetSmsInstance().ReleaseAll();
                OutMsg(rnt.Result);
                isFinished = true;
                this.Invoke(new EventHandler(delegate
                {
                    btnReleaseAllPhones.Enabled = true;
                }));
                
            }));
            IsTheFirstTime = false;
            th.Start();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {

            LoginVpn();

            // MouseUtility.ClickAtXY();
        }

        private void LoginVpn()
        {
            IntPtr pHandler = MouseUtility.FindWindow("TFrmMain", "IP加速器 v15.0");
            MouseUtility.ShowWindow(pHandler, 1);
            Thread.Sleep(1000);
            MouseUtility.SetForegroundWindow(pHandler);
            Rect rect = new Rect();
            MouseUtility.GetWindowRect(pHandler, out rect);
            Point pnt = new Point(rect.Left + GlobalSettings.getInstance().vpn_login.X, rect.Top + GlobalSettings.getInstance().vpn_login.Y); // 272,207
            MouseUtility.SetCursorPos(pnt);
            MouseUtility.ClickAtXY(pnt);
            OutMsg(pnt.ToString());
        }

        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            DisVpn();
        }

        private void DisVpn()
        {
            IntPtr pHandler = MouseUtility.FindWindow("TFrmMain", "IP加速器 v15.0");
            MouseUtility.ShowWindow(pHandler, 1);
            Thread.Sleep(1000);
            Rect rect = new Rect();
            MouseUtility.SetForegroundWindow(pHandler);
            MouseUtility.GetWindowRect(pHandler, out rect);
            Point pnt = new Point(rect.Left + GlobalSettings.getInstance().vpn_discnn.X, rect.Top + GlobalSettings.getInstance().vpn_discnn.Y); // 272,207
            MouseUtility.SetCursorPos(pnt);
            MouseUtility.ClickAtXY(pnt);
            OutMsg(pnt.ToString());
        }

        private void btnVPNState_Click(object sender, EventArgs e)
        {
            IntPtr pHandler = MouseUtility.FindWindow("WindowsForms10.Window.8.app.0.17ad52b", "卓码客户端 1.3.9");
            Utilities.Utility.MouseUtility.WINDOWPLACEMENT plcase = MouseUtility.GetPlacement(pHandler);
            OutMsg(plcase.showCmd.ToString());
            bool mode = MouseUtility.IsMinWindow(pHandler);
            OutMsg(mode.ToString());
        }

        private bool GetVpnWindowState()
        {
            IntPtr pHandler = MouseUtility.FindWindow("TFrmMain", "IP加速器 v15.0");
            Utilities.Utility.MouseUtility.WINDOWPLACEMENT plcase = MouseUtility.GetPlacement(pHandler);
            return MouseUtility.IsMinWindow(pHandler);
        }

        private void btnTestNetwork_Click(object sender, EventArgs e)
        {
            GetIP();
            if (IsGetIpTimeouts)
            {
                MessageBox.Show("连接超时");
            }
            else
            {
                MessageBox.Show("网络正常");
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            IntPtr pHandler = MouseUtility.FindWindow("TFrmMain", "IP加速器 v15.0");

            bool mode = MouseUtility.IsMinWindow(pHandler);
            MessageBox.Show(mode.ToString());
        }


    }
}
