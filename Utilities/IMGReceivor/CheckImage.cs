using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Net;
using System.IO;
using System.Security.Cryptography;
using System.Xml;
using System.Text.RegularExpressions;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading;
using Utilities.PayConfig;
using Utilities.Factory;
using System.Diagnostics;

namespace Utilities
{
    public class UUWrapper
    {
        /// <summary>
        /// 校验dll
        /// </summary>
        /// <param name="softId">软件id</param>
        /// <param name="softKey">软件key</param>
        /// <param name="guid">随机guid串</param>
        /// <param name="fileMd5">dll文件的md5值</param>
        /// <param name="fileCrc">dll文件的crc值</param>
        /// <param name="checkResult">校验返回信息</param>
        [DllImport("UUWiseHelper.dll")]
        public static extern void uu_CheckApiSign(int softId, string softKey, string guid, string fileMd5, string fileCrc, StringBuilder checkResult);
        /// <summary>
        /// 设置软件信息
        /// </summary>
        /// <param name="softId">软件id</param>
        /// <param name="softKey">软件的key</param>
        [DllImport("UUWiseHelper.dll")]
        public static extern void uu_setSoftInfo(int softId, string softKey);

        /// <summary>
        /// 登陆系统
        /// </summary>
        /// <param name="u">用户名</param>
        /// <param name="p">密码</param>
        /// <returns>
        /// 大于0  正常登陆
        /// -1001 系统异常
        /// -1002 无法连接远程
        /// -1003 服务器配置错误
        /// -1 参数错误，用户名为空或密码为空
        /// -2 用户不存在
        /// -3 密码错误
        /// -4 账户被锁定
        /// -5 非法登录
        /// -6 用户点数不足，请及时充值
        /// -8 系统维护中
        /// -9 其他
        /// </returns>
        [DllImport("UUWiseHelper.dll")]
        public static extern int uu_login(string u, string p);

        /// <summary>
        /// 注册用户 大于0表示成功
        /// 
        /// </summary>
        /// <param name="u"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        [DllImport("UUWiseHelper.dll")]
        public static extern int uu_reguser(string u, string p, int softid, string softkey);

        /// <summary>
        /// 报告错误 大于等于0表示成功
        /// -1 参数错误或者不全
        //-2 未登录，KEY无效  
        //-3 表示软件KEY错误
        //-4 服务器出错
        //-7 软件代码倍禁用
        //-8 服务未启动
        /// </summary>
        /// <param name="codeid"></param>
        /// <returns></returns>
        [DllImport("UUWiseHelper.dll")]
        public static extern int uu_reportError(int codeid);

        /// <summary>
        /// 获取用户余额
        /// 返回整形
        /// - 1 表示用户名或者密码为空
        /// -3 表示用户名密码错误
        /// -4 表示账户被锁定
        /// -6 表示余额不足
        /// -1001 系统异常
        /// -1002 无法连接远程
        /// </summary>
        /// <param name="codeid"></param>
        /// <returns></returns>
        [DllImport("UUWiseHelper.dll")]
        public static extern int uu_getScore(string username, string password);

        /// <summary>
        ///  充值卡充值 大于0表示卡充值金额
        ///  -1 参数错误或者不全
        ///-2 软件id错误
        ///-3 SKEY无效
        ///-4 充值卡错误
        ///-5 PKEY错误
        ///-6 用户不存在 
        ///-7 软件代码为空，软件未注册
        ///-8 系统繁忙
        ///-9 服务器错误
        ///-101 充值卡号不存在
        ///-102 充值卡无效或者已使用
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="card">卡号</param>
        /// <param name="softId">软件id</param>
        /// <param name="softKey">软件通信key（开发者后台自行定义的）</param>
        /// <returns></returns>
        [DllImport(@"UUWiseHelper.dll")]
        public static extern int uu_pay(string username, string card, int softId, string softKey);

        /// <summary>
        /// 根据文件路径和验证码类型传识别验证码 同时返回验证码的id
        /// </summary>
        /// <param name="path"></param>
        /// <param name="codeType"></param>
        /// <param name="codeid"></param>
        /// <returns></returns>
        [DllImport(@"UUWiseHelper.dll")]
        public static extern int uu_recognizeByCodeTypeAndPath(string path, int codeType, StringBuilder result);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="picContent"></param>
        /// <param name="codeLength"></param>
        /// <param name="codeType"></param>
        /// <param name="codeId"></param>
        /// <returns></returns>
        [DllImport("UUWiseHelper.dll")]
        public static extern int uu_recognizeByCodeTypeAndBytes(byte[] picContent, int codeLength, int codeType, StringBuilder result);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="picContent"></param>
        /// <param name="codeLength"></param>
        /// <param name="codeType"></param>
        /// <param name="codeId"></param>
        /// <returns></returns>
        [DllImport("UWiseHelper.dll")]
        public static extern int uu_recognizeByCodeTypeAndUrl(string url, string inCookie, int codeType, string cookieResult, StringBuilder result);
    }
   public class CheckImage
    {
       protected int softid;
       protected string softkey;
       protected string md5;
       protected string itemID;
       protected bool bLoginFinshed;
        // 开发账号初始化
        public virtual void Init()
        {
            
        }
        // 用户登录
        public virtual int Login(string user, string passwd)
        {
            return -1;
        }
        // 获取余额
        public virtual int GetAmoutReminder(string user, string passwd)
        {
            return -1;
        }
        // 充值
        public virtual int Pay(string username, string card)
        {
            return -1;
        }
        public virtual string RecognizeByCodeTypeAndPath(string path, int codeType)
        {
            return "";
        }
        public virtual int RecognizeByCodeTypeAndPath(string path, int codeType, StringBuilder result)
        {
            return -1;
        }
        // 题目报错
        public virtual string ReportError()
        {
            return "";
        }

       // 获取题目ID
       public  string GetItemID()
       {
            return itemID;
       }
       public bool IsLoginFinshed()
       {
           return bLoginFinshed;
       }
    }
    // 若快
   public class RKCheckImage : CheckImage
   {
       private Dictionary<object, object> accountInfo;
       private string userName;
       private string passwd;
       public RKCheckImage()
       {
           softid = 24596;
           softkey = "ebade373cf9d4f18a6c57809446e240d";
           //md5 = "522C4218-F56D-4ACF-B557-2AF498DD4D1C";
           accountInfo = new Dictionary<object, object>();
           bLoginFinshed = false;
           Init();
       }
       // 开发账号初始化
       public override void Init()
       {

       }
       // 用户登录
       public override int Login(string user, string passwd)
       {
           bLoginFinshed = false;
           userName = user;
           this.passwd = passwd;
           accountInfo.Clear();
           accountInfo.Add("username", user);
           accountInfo.Add("password", passwd);
           string httpResult;

           XmlDocument xmlDoc = new XmlDocument();
           try
           {
               httpResult = RuoKuaiHttp.Post("http://api.ruokuai.com/info.xml", accountInfo);
               xmlDoc.LoadXml(httpResult);
           }
           catch
           {
               throw new Exception("快豆-登录-返回格式有问题");
           }
           XmlNode scoreNode = xmlDoc.SelectSingleNode("Root/Score");
           XmlNode historyScoreNode = xmlDoc.SelectSingleNode("Root/HistoryScore");
           XmlNode totalTopicNode = xmlDoc.SelectSingleNode("Root/TotalTopic");

           XmlNode errorNode = xmlDoc.SelectSingleNode("Root/Error");
           Regex reg = new Regex("^\\d+$");
           int rnt = 0;
           if (reg.Match(scoreNode.InnerText).Success)
           {
               rnt = Convert.ToInt32(scoreNode.InnerText);
           }
           bLoginFinshed = true;
           return rnt;
       }
       // 获取余额
       public override int GetAmoutReminder(string user, string passwd)
       {
           return Login(user, passwd);
       }
       // 充值
       public override int Pay(string username, string card)
       {
           return -1;
       }
       public override string RecognizeByCodeTypeAndPath(string path, int codeType)
       {
           //必要的参数
           accountInfo.Clear();
           accountInfo.Add("username",userName);
           accountInfo.Add("password",passwd); 
           accountInfo.Add("typeid",codeType.ToString()); 
           accountInfo.Add("timeout","60");
           accountInfo.Add("softid",softid); 
           accountInfo.Add("softkey",softkey);

           using (Image image = Image.FromFile(path))
           {
                byte[] data;
               //把Image转换为byte
               using (MemoryStream ms = new MemoryStream())
               {
                   image.Save(ms, ImageFormat.Jpeg);
                   ms.Position = 0;
                   data = new byte[ms.Length];
                   ms.Read(data, 0, Convert.ToInt32(ms.Length));
                   ms.Flush();
               }

               string httpResult;

               XmlDocument xmlDoc = new XmlDocument();
               try
               {
                   httpResult = RuoKuaiHttp.Post("http://api.ruokuai.com/create.xml", accountInfo, data);
                   xmlDoc.LoadXml(httpResult);
               }
               catch
               {
                   throw new Exception("若快，识别，返回格式有误");
               }
               XmlNode idNode = xmlDoc.SelectSingleNode("Root/Id");
               XmlNode resultNode = xmlDoc.SelectSingleNode("Root/Result");
               XmlNode errorNode = xmlDoc.SelectSingleNode("Root/Error");
               string result = string.Empty;
               itemID = string.Empty;
               if (resultNode != null && idNode != null)
               {
                   itemID = idNode.InnerText;
                   result = resultNode.InnerText;
               }
               return result;
           }

       }

       public override string ReportError()
       {
           accountInfo.Clear();
           accountInfo.Add("username", userName);
           accountInfo.Add("password", passwd);
           accountInfo.Add("id", itemID);
           string httpResult = RuoKuaiHttp.Post("http://api.ruokuai.com/reporterror.xml", accountInfo);

           return httpResult;
       }
       public override int RecognizeByCodeTypeAndPath(string path, int codeType, StringBuilder result)
       {
           return -1;
       }
   }
     public class UUCheckImage : CheckImage
    {

        public UUCheckImage()
        {
            softid = 100716;
            softkey = "ccc7cbf94da94520bb28115d49ed138e";
            md5 = "522C4218-F56D-4ACF-B557-2AF498DD4D1C";
            bLoginFinshed = false;
            Init();
        }
        public override void Init()
        {
            UUWrapper.uu_setSoftInfo(softid, softkey);
        }

        public override int Login(string user, string passwd)
        {
//             string DLLPath = System.Environment.CurrentDirectory + "\\UUWiseHelper.dll";
//             string Md5 = GetFileMD5(DLLPath);
//             if (md5 != Md5)
//             {
//                 return -1;
//             }
            int rtn =  UUWrapper.uu_login(user, passwd);
            bLoginFinshed = true;
            return rtn;
        }

        public override int GetAmoutReminder(string user, string passwd)
        {
            return UUWrapper.uu_getScore(user, passwd);
        }

        public override int Pay(string username, string card)
        {
            return UUWrapper.uu_pay(username, card, softid, softkey);
        }

        public override string RecognizeByCodeTypeAndPath(string path, int codeType)
        {
            StringBuilder result = new StringBuilder(50);

            string rtn = "";
            try
            {
                UUWrapper.uu_recognizeByCodeTypeAndPath(path, codeType, result);
                string[] strlist = result.ToString().Split(new char[] { '_' });
                rtn = strlist[1];
            }
            catch (System.Exception ex)
            {
                rtn = "";
                //throw ex;
            }
            return rtn;
        }
        public override int RecognizeByCodeTypeAndPath(string path, int codeType, StringBuilder result)
        {
            int rnt = -1;
            try
            {
                rnt = UUWrapper.uu_recognizeByCodeTypeAndPath(path, codeType, result);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            return rnt;
        }
        #region 根据路径获取文件MD5
        /// <summary>
        /// 获取文件MD5校验值
        /// </summary>
        /// <param name="filePath">校验文件路径</param>
        /// <returns>MD5校验字符串</returns>
        private string GetFileMD5(string filePath)
        {
            FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] md5byte = md5.ComputeHash(fs);
            int i, j;
            StringBuilder sb = new StringBuilder(16);
            foreach (byte b in md5byte)
            {
                i = Convert.ToInt32(b);
                j = i >> 4;
                sb.Append(Convert.ToString(j, 16));
                j = ((i << 4) & 0x00ff) >> 4;
                sb.Append(Convert.ToString(j, 16));
            }
            return sb.ToString();
        }
        #endregion
    }

    public class CheckImageFactory
    {
        static string user;
        static string pwd;
        static CheckImage checkImg = null; 
        static public CheckImage GetCheckImage()
        {
           string type = AccountFactory.getInstance().getImageCodeAcc().PlatformName;
           if (checkImg == null)
           {
               switch (type)
               {
                   case "UU":
                       checkImg = new UUCheckImage();
                       break;
                   case "RK":
                       checkImg = new RKCheckImage();
                       break;
                   default:
                       break;
               }
               
           }

           if (checkImg != null)
           {

              checkImg.Login(AccountFactory.getInstance().getImageCodeAcc().UserName, AccountFactory.getInstance().getImageCodeAcc().Passwd);
              
           }
           return checkImg;
        }
        public static System.Threading.Thread AsynThread = null;
        public static void LoginAsyn(string user, string pwd)
        {
            AsynThread = new System.Threading.Thread(new ThreadStart(Login));
            AsynThread.Start();
        }
        public static void LoginAsyn()
        {
            checkImg = null;
            AsynThread = new System.Threading.Thread(new ThreadStart(Login));
            AsynThread.Start();
        }
        public static void Login()
        {
            do 
            {
                try
                {
                    GetCheckImage();
                }
                catch (System.Exception ex)
                {
                    checkImg = null;
                    Trace.WriteLine("登录 图片验证码失败，重新登录" + ex.StackTrace);
                    Thread.Sleep(1000);
                }
                

            } while (checkImg == null || !checkImg.IsLoginFinshed());
            Trace.WriteLine("登录 图片验证码 成功");
            
        }
    }
}
