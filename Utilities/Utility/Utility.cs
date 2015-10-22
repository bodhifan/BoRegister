using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Diagnostics;
using System.Threading;
using System.Runtime.Serialization.Formatters.Binary;

namespace Utilities
{

    // 账号信息
    [Serializable]
    public class AccountEntity
    {
      public string emailAccount{get;set;}//邮件
      public string emailPasswd{get;set;}  //密码
      public string phoneNum { get; set; }  //注册的手机号
      public string acount { get; set; }  // 淘宝账号
      public string passwd { get; set; }  // 密码
      public string payPwd { get; set; }  // 支付宝密码
      public string securityAns { get; set; }  // 密保
      public int useCnt { get; set; } //使用次数
      [NonSerialized]
      public DateTime registerTime;// 注册时间
      [NonSerialized]
      public TimeSpan registerConsume;// 注册耗时
      [NonSerialized]
      public string registerIP;// 注册耗时
      public AccountEntity()
      {
          useCnt = 0;
      }
      static public AccountEntity getTmpInstance()
      {
          AccountEntity ae = new AccountEntity();
          ae.emailAccount = "flag654321@163.com";
          ae.emailPasswd = "123456flag";
          return ae;
      }
      public override string ToString()
      {
          return string.Format("{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}|{8}",emailAccount,emailPasswd,phoneNum,acount,passwd,payPwd,securityAns,string.Format("{0}-{1}-{2}-{3}:{4}",registerTime.Month,
              registerTime.Day,registerTime.Hour,registerTime.Minute,registerTime.Second),registerIP);
      }
      public string ToShortString()
      {
          return acount + "|" + passwd;
      }

      // 序列化
      public static void DoSerialize(List<AccountEntity> list, string path)
      {
          FileStream fs = new FileStream(path, FileMode.Create);
          BinaryFormatter bf = new BinaryFormatter();
          bf.Serialize(fs, list);
          fs.Close();
      }
      // 反序列化
      public static List<AccountEntity> DoUnserialize(string path)
      {
          List<AccountEntity> rntList = new List<AccountEntity>();
          if (!File.Exists(path))
          {
              return rntList;
          }
          FileStream fs = new FileStream(path, FileMode.Open);
          BinaryFormatter bf = new BinaryFormatter();
          rntList = bf.Deserialize(fs) as List<AccountEntity>;
          fs.Close();

          return rntList;
      }
    }

    public class EntitiesArray
    {
        public List<AccountEntity> mEntities = null;
        static EntitiesArray mInstance = null;
        
        int index = 0;
        private EntitiesArray()
        {
            mEntities = new List<AccountEntity>();
            index = 0;
            // 测试数据
            mEntities = GetDatas();
        }


       static public EntitiesArray GetInstance()
        {
            if (mInstance == null)
            {
                mInstance = new EntitiesArray();
            }
            return mInstance;
        }
        public bool HasNext()
        {
            if (index < mEntities.Count)
            {
                return true;
            }
            return false;
        }
        public AccountEntity Next()
        {
            if (index >= mEntities.Count)
            {
                return null;
            }
            return mEntities[index++];
        }
        public void Clear()
        {
            mEntities.Clear();
            index = 0;
        }
        public int Reminder()
        {
            return mEntities.Count - index;
        }

        public void AddList(List<AccountEntity> entityList)
        {
            mEntities.AddRange(entityList);
        }
        public int GetIndex()
        {
            return index;
        }
        public void SetIndex(int idx)
        {
            index = idx;
        }
        public int count()
        {
            return mEntities.Count;
        }
        public static List<AccountEntity>  GetDatas()
        {
            List<AccountEntity> rntList = new List<AccountEntity>();
            TxtFiles emailTxt = new TxtFiles(Config.PATH_OF_EMAILS);
            string[] allLines = emailTxt.ReadAllLines();
            List<string> lineList = new List<string>();
            foreach (string str in allLines)
            {
                if (lineList.Contains(str.Trim()))
                {
                    continue;
                }

                string[] accounts = str.Split(new char[]{'|'});
                if (accounts.Length != 2)
                {
                    continue;
                }
                AccountEntity ae = new AccountEntity();
                ae.emailAccount = accounts[0].Trim();
                ae.emailPasswd = accounts[1].Trim();
                rntList.Add(ae);
                lineList.Add(str.Trim());
            }
            return rntList;
        }
        public EntitiesArray Refresh()
        {
            mEntities.Clear();
            index = 0;
            mEntities = GetDatas();
            return this;
        }
        ~EntitiesArray()
        {
            OutputReminder();
            OutputUsed();
        }

        private void OutputUsed()
        {
            List<string> allUsed = new List<string>();
            for (int i = 0; i < index; i++)
            {
                string cur = mEntities[i].emailAccount + "|" + mEntities[i].emailPasswd;
                allUsed.Add(cur);
            }
            System.IO.File.WriteAllLines(Config.PATH_OF_USEDEMAILS, allUsed.ToArray(), Encoding.Default);
        }

        private void OutputReminder()
        {
            List<string> allNoUsed = new List<string>();
            for (int i = index; i < mEntities.Count; i++)
            {
                string cur = mEntities[i].emailAccount + "|" + mEntities[i].emailPasswd;
                allNoUsed.Add(cur);
            }
            System.IO.File.WriteAllLines(Config.PATH_OF_UNUSEDEMAILS, allNoUsed.ToArray(), Encoding.Default);
        }
    }

    public enum LogType
    {
        Debug,// 清理缓存
        Release,// 注册邮箱
        REGISTER_PHONE,// 注册手机
        APP_INIT,//软件初试化
        UNDEFINE,//未定义
    }
    public class Utiliy
    {
     
        public static void ClearHistory()
        {
           WebCleanerTools.CleanAll();
        }
        #region 获取邮件确认地址
        public static string GetConfirmLink(AccountEntity account)
        {
            string content = GetConfirmEmailContent(account);

            string[] lines = content.Split(new char[]{'\r','\n'});
            content = "";
            foreach (string line in lines)
            {
                string str = line.Trim(new char[] { '\t', ' ' });
                content += str;
            }
            Regex reg = new Regex(Config.PATTERN_EMAIL_LINK,RegexOptions.Compiled);
            Match m = reg.Match(content);
            if (m == null)
            {
                throw new Exception("淘宝邮件确认连接地址未找到!");
            }
            if (m.Groups.Count == 3)
            {
                return m.Groups[2].ToString();
            }
            else
                return m.Groups[1].ToString();

        }
        public static string GetConfirmEmailContent(AccountEntity account)
        {
            string host = "";
            int port = 0;
            getHostAndPortByEmail(account.emailAccount,ref host, ref port);

            if (host == "" || port == 0)
            {
                throw new Exception("未找到邮件服务器!");
            }
            return FetchEmailHtmlBody.GetHtmlBody(host, port, account.emailAccount, account.emailPasswd);

        }

        private static void getHostAndPortByEmail(string emailAccount, ref string host, ref int port)
        {

            string[] exts = emailAccount.Split('@');
            if (exts.Length !=2)
            {
                return;
            }
            if (exts[1] == Config.EMAIL_163)
            {
                host = "imap.163.com";
                port = 143;
            }
            else
            {
                host = "";
                port = 0;
            }

        }
        public static string GetConfirmEmailContent(string host, int port, string username, string password)
        {
            return FetchEmailHtmlBody.GetHtmlBody(host, port, username, password);
        }
        #endregion

        public static void AppendFile(string filePath, string content)
        {
            // 读取文件的源路径及其读取流
            string strWriteFilePath = filePath;
            StreamWriter swWriteFile = null;
            try
            {
                if (File.Exists(strWriteFilePath))
                {
                    swWriteFile = new StreamWriter(strWriteFilePath, true);
                }
                else
                {
                    swWriteFile = File.CreateText(strWriteFilePath);
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }

            // 读取流直至文件末尾结束
            swWriteFile.WriteLine(content);
            // 关闭读取流文件
            swWriteFile.Close();
        }


//         #region 获取验证码图片 相关API
//         // 获取验证码图片
//         public static void GetImg2PictureBox(WebBrowser wb, PictureBox pic)
//         {
//             HtmlElement element = GetHtmlElement(wb, Config.IMG_CHECKCODE_ID);
//             if (element != null)
//             {
//                 CopyImgtoPictureBox(element,pic);
//                 // 测试用
//                 SaveCheckCodeImage(element, Config.PATH_CHECKCODE_IMG);
//             }
//         }


//         // 获取图片验证码的图像
//         public static Image GetCheckCodeImage(HtmlElement element)
//         {
//             Image image = null;
//             try
//             {
//                 HTMLDocument html = (HTMLDocument)element.Document.DomDocument;
//                 IHTMLControlElement img = (IHTMLControlElement)element.DomElement;
//                 IHTMLControlRange range = (IHTMLControlRange)((HTMLBody)html.body).createControlRange();
//                 range.add(img);
//                 range.execCommand("Copy", false, null);
//                 img = null;
//                 range = null;
//                 html = null;
// 
//                 if (Clipboard.ContainsImage())
//                 {
//                     image = Clipboard.GetImage();
//                 }
//                 Clipboard.Clear();
//             }
//             catch (System.Exception ex)
//             {
//                 image = null;
//                 throw new Exception("你 没啊");
//             }
//             return image;
//         }
//         // 获取图片验证码并保存在本地
//         public static bool SaveCheckCodeImage(HtmlElement element, string path)
//         {
//             bool flag = false;
//             Image image = null;
//             try
//             {
//                 image = GetCheckCodeImage(element);
//                 if (image != null)
//                 {
//                     if (File.Exists(path))
//                     {
//                         File.Delete(path);
//                     }
//                     image.Save(path);
//                     flag = true;
//                 }
//                 else
//                 {
//                     throw new Exception("image is null");
//                 }
// 
//             }
//             catch (System.Exception ex)
//             {
//                 flag = false;
//                 throw new Exception("save has some problem "+ex.Message);
//             }
//             finally
//             {
//                 if (image != null)
//                 {
//                     image.Dispose();
//                 }
//             }
//             return flag;
//         }
        public static bool IsImageVaild(string path)
        {
            // 检查图片的倒数第二、顺数第二列是否出现了绿色标记，
            // 如果存在绿色标记，说明该图片是非法图片
            Bitmap bitMap = new Bitmap(path,false);
            bool flag = true;
            try
            {
                if (bitMap == null)
                {
                    return false;
                }
                for (int i = 0; i < bitMap.Height; i++)
                {
                    // 先判断倒数第二
                    Color color = bitMap.GetPixel(bitMap.Width - 1, i);
                    double fc = Math.Sqrt(Math.Pow(color.R - 20, 2) + Math.Pow(color.G - 10, 2) + Math.Pow(color.B - 255, 2));
                    if (fc < 100.0)
                    {
                        return false;
                    }
                    // 顺数第二
                    color = bitMap.GetPixel(2, i);
                    fc = Math.Sqrt(Math.Pow(color.R - 20, 2) + Math.Pow(color.G - 10, 2) + Math.Pow(color.B - 255, 2));
                    if (fc < 100.0)
                    {
                        flag = false;
                    }
                }
            }
            catch (System.Exception ex)
            {
            	
            }
            finally
            {
                if (bitMap != null)
                {
                    bitMap.Dispose();
                }
            }
            return flag;
        }
//         private static void CopyImgtoPictureBox(HtmlElement element, PictureBox pic)
//         {
//             Image image = null;
//             if ((image = GetCheckCodeImage(element)) != null)
//             {
//                 pic.Image = image;
//             }
//             else
//             {
//                 MessageBox.Show("执行不成功");
//             }
//         }
//        #endregion

        // 获取IP地址
        public static string GetIPAddress()
        {
            //设置获取IP地址和国家源码的网址 
            string url = "http://www.ip138.com/ip2city.asp";
            Regex reg = new Regex(@"\[(.+)]");
            string html = GetHtml(url);
            Match match = reg.Match(html);
            if (match != null)
            {
                return match.Groups[1].ToString();
            }
            return "";
        }
        private static string GetAdrByIp(string ip)
        {
            string url = "http://www.cz88.net/ip/?ip=" + ip;
            string regStr = "(?<=<span\\s*id=\\\"cz_addr\\\">).*?(?=</span>)";
            //得到网页源码 
            string html = GetHtml(url);
            Regex reg = new Regex(regStr, RegexOptions.None);
            Match ma = reg.Match(html);
            html = ma.Value;
            string[] arr = html.Split(' ');
            return arr[0];
        } 
        private static string GetHtml(string url)
        {
            string str = "";
            try
            {
                Uri uri = new Uri(url);
                System.Net.WebRequest wr = System.Net.WebRequest.Create(uri);
                Stream s = wr.GetResponse().GetResponseStream();
                StreamReader sr = new StreamReader(s, Encoding.Default);
                str = sr.ReadToEnd();
            }
            catch (Exception e)
            {
            }
            return str;
        } 
        // 获取元素通过ID
        public static HtmlElement GetHtmlElement(WebBrowser webBrowser, string eleId)
        {
            HtmlElement element = null;
            try
            {
                element = webBrowser.Document.GetElementById(eleId);
                if (element == null)
                {
                    //throw new Exception("注册邮箱元素ID：" + eleId + " 未找到!");
                }
            }
            catch
            {
                try
                {
                    while (webBrowser.ReadyState != WebBrowserReadyState.Complete)
                    {
                        Thread.Sleep(1000);
                    }
                }
                catch (System.Exception ex)
                {
                    return null;
                }

                element = webBrowser.Document.GetElementById(eleId);
            }
            return element;
        }

        public static bool HasFile(string fileName, string dirPath)
        {
            string[] files = System.IO.Directory.GetFiles(dirPath);
            foreach (string filename in files)
            {
                if (filename.Equals(fileName))
                {
                    return true;
                }
            }
            return false;
        }

        // 写日志
        ///  Log.Instance.LogDirectory=@"C:\"; 默认为程序运行目录
        ///  Log.Instance.FileNamePrefix="cxd";默认为log_
        ///  Log.Instance.CurrentMsgType = MsgLevel.Debug;默认为Error
        ///  Log.Instance.logFileSplit = LogFileSplit.Daily; 日志拆分类型LogFileSplit.Sizely 大小
        ///  Log.Instance.MaxFileSize = 5; 默认大小为2M，只有LogFileSplit.Sizely的时候配置有效
        public static void WriteLog(string logInfo,LogType logType = LogType.UNDEFINE)
        {
//             Log.Instance.CurrentMsgType = MsgLevel.Debug;
//             Log.Instance.LogWrite(logInfo, MsgLevel.Debug);
        }

    }

   

    public static class WebCleanerTools
    {
        [DllImport("wininet.dll", SetLastError = true)]
        private static extern bool InternetSetOption(IntPtr hInternet, int dwOption, IntPtr lpBuffer, int lpdwBufferLength);
        //清空session
        public static void ResetSession()
        {
            //Session的选项ID为42
            InternetSetOption(IntPtr.Zero, 42, IntPtr.Zero, 0);
        }
        //清空cookie
        public static void ResetCookie(WebBrowser c_web)
        {
            if (c_web.Document != null)
            {
                c_web.Document.Cookie.Remove(0);

            }
            string[] theCookies = System.IO.Directory.GetFiles(Environment.GetFolderPath(Environment.SpecialFolder.Cookies));
            foreach (string currentFile in theCookies)
            {
                try
                {
                    System.IO.File.Delete(currentFile);
                }
                catch (Exception ex)
                {
                }
            }
        }
        /*
         * 7 个静态函数
         * 私有函数
         * private bool FileDelete()    : 删除文件
         * private void FolderClear()   : 清除文件夹内的所有文件
         * private void RunCmd()        : 运行内部命令
         * 
         * 公有函数
         * public void CleanCookie()    : 删除Cookie
         * public void CleanHistory()   : 删除历史记录
         * public void CleanTempFiles() : 删除临时文件
         * public void CleanAll()       : 删除所有
         * 
         * 
         * 
         * */


        public enum ShowCommands : int
        {
            SW_HIDE = 0,
            SW_SHOWNOrmAL = 1,
            SW_NOrmAL = 1,
            SW_SHOWMINIMIZED = 2,
            SW_SHOWMAXIMIZED = 3,
            SW_MAXIMIZE = 3,
            SW_SHOWNOACTIVATE = 4,
            SW_SHOW = 5,
            SW_MINIMIZE = 6,
            SW_SHOWMINNOACTIVE = 7,
            SW_SHOWNA = 8,
            SW_RESTORE = 9,
            SW_SHOWDEFAULT = 10,
            SW_FORCEMINIMIZE = 11,
            SW_MAX = 11
        }
    
        [DllImport("shell32.dll")]
        public static extern IntPtr ShellExecute(IntPtr hwnd, string lpOperation, string lpFile, string lpParameters, string lpDirectory, ShowCommands nShowCmd);
       
        /// 删除一个文件，System.IO.File.Delete()函数不可以删除只读文件，这个函数可以强行把只读文件删除。
        /// 
        /// 文件路径
        /// 是否被删除
        /// 
        public static bool FileDelete(string path)
        {
            //first set the File\'s ReadOnly to 0
            //if EXP, restore its Attributes
            System.IO.FileInfo file = new System.IO.FileInfo(path);
            System.IO.FileAttributes att = 0;
            bool attModified = false;
            try
            {
                //### ATT_GETnSET
                att = file.Attributes;
                file.Attributes &= (~System.IO.FileAttributes.ReadOnly);
                attModified = true;
                file.Delete();
            }
            catch (Exception e)
            {
                if (attModified)
                    file.Attributes = att;
                return false;
            }
            return true;
        }
        //public 
        /// 
        /// 清除文件夹
        /// 
        /// 文件夹路径
        static void FolderClear(string path)
        {
            System.IO.DirectoryInfo diPath = new System.IO.DirectoryInfo(path);
            foreach (System.IO.FileInfo fiCurrFile in diPath.GetFiles())
            {
                FileDelete(fiCurrFile.FullName);
            }
            foreach (System.IO.DirectoryInfo diSubFolder in diPath.GetDirectories())
            {
                FolderClear(diSubFolder.FullName); // Call recursively for all subfolders
            }
        }
//         static void RunCmd(string cmd)
//         {
//             //Utiliy.RunCmd("/c"+cmd);
//             ShellExecute(IntPtr.Zero, "open", "rundll32.exe", " InetCpl.cpl,ClearMyTracksByProcess 255", "", ShowCommands.SW_HIDE);
//            // System.Diagnostics.Process.Start("cmd.exe", "/C " + cmd);
//         }
        /// 
        /// 删除历史记录
        /// 
        private static void CleanHistory()
        {
            string[] theFiles = System.IO.Directory.GetFiles(Environment.GetFolderPath(Environment.SpecialFolder.History), "*", System.IO.SearchOption.AllDirectories);
            foreach (string s in theFiles)
                FileDelete(s);
           // RunCmd("RunDll32.exe InetCpl.cpl,ClearMyTracksByProcess 1");
        }
        /// 
        /// 删除临时文件
        /// 
        private static void CleanTempFiles()
        {
            FolderClear(Environment.GetFolderPath(Environment.SpecialFolder.InternetCache));
           // RunCmd("RunDll32.exe InetCpl.cpl,ClearMyTracksByProcess 8");
        }
        /// 
        /// 删除Cookie
        /// 
        private static void CleanCookie()
        {
            string[] theFiles = System.IO.Directory.GetFiles(Environment.GetFolderPath(Environment.SpecialFolder.Cookies), "*", System.IO.SearchOption.AllDirectories);
            foreach (string s in theFiles)
                FileDelete(s);
           // RunCmd("RunDll32.exe InetCpl.cpl,ClearMyTracksByProcess 2");
        }
        /// 
        /// 删除全部
        /// 
        public static void CleanAll()
        {
            CleanHistory();
            CleanCookie();
            CleanTempFiles();
            RunCmd("RunDll32.exe InetCpl.cpl,ClearMyTracksByProcess 255");
        }
        static public string RunCmd(string command)
        {
            //创建并启动一个对进程
            Process p = new Process();

            //Process类有一个StartInfo属性，这是ProcessStartInfo类，包括了一些属性和方法，例如：
            p.StartInfo.FileName = "cmd.exe"; //程序名
            p.StartInfo.Arguments = " /c " + command; //执行参数
            p.StartInfo.UseShellExecute = false; //关闭Shell的使用
            p.StartInfo.RedirectStandardInput = true; //重定向标准输入
            p.StartInfo.RedirectStandardOutput = true; //重定向标准输出
            p.StartInfo.RedirectStandardError = true; //重定向错误输出
            p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            p.StartInfo.CreateNoWindow = true; //设置不显示窗口

            p.Start();
            //p.StandardInput.WriteLine(command); //也可以用这种方式输入要执行的命令
            //p.StandardInput.WriteLine("exit"); //不过要记得加上Exit要不然下一行程式执行的时候会当机

            p.WaitForExit(50000);
            //必须创建可以自动转换完成以后，结束进程的代码
            return p.StandardOutput.ReadToEnd(); //从输出流取得命令执行结果   

        }
    }

}
