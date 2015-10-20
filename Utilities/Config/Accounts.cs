using System;
using System.Collections.Generic;
using System.Text;
using Utilities.Factory;

namespace Utilities
{
   public class Account
    {
       // 用户名键值
       public static string USER_KEY = "USERNAME";
       // 密码键值
       public static string PASSWORD_KEY = "PASSWORD";
       // 项目键值
       public static string PORJ_KEY = "PROJID";
       // 平台键值
       public static string PLATFORM_KEY = "PLATFROM";


       public string UserName;
       public string Passwd;

       // 项目id号
       public string ProjID;

       // 平台
       public string PlatformName;

       public virtual string GetSectionName(){return "DEFAULTACCOUNT";}

       public string ToString()
       {
           return string.Format("{0} user:{1} pwd:{2}", GetSectionName(), UserName, Passwd);
       }
       public virtual void Save()
       {
           ConfigFactory.getInstance().WriteString(GetSectionName(), USER_KEY, UserName);
           ConfigFactory.getInstance().WriteString(GetSectionName(), PASSWORD_KEY, Passwd);
           ConfigFactory.getInstance().WriteString(GetSectionName(), PORJ_KEY, ProjID);
           ConfigFactory.getInstance().WriteString(GetSectionName(), PLATFORM_KEY, PlatformName);


       }

       public virtual void Parse()
       {
          UserName = ConfigFactory.getInstance().ReadString(GetSectionName(), USER_KEY, "");
          Passwd = ConfigFactory.getInstance().ReadString(GetSectionName(), PASSWORD_KEY, "");
          ProjID = ConfigFactory.getInstance().ReadString(GetSectionName(), PORJ_KEY, "");
          PlatformName = ConfigFactory.getInstance().ReadString(GetSectionName(), PLATFORM_KEY, "");
       }

    }
   public class ADSLAccount : Account
    {
       public override string GetSectionName()
       {
           return "ADSLACCOUNT";
       }
    }

   public class ImgCheckAccount : Account
    {
       public override string GetSectionName()
       {
           return "IMAGEACCOUNT";
       }
//        public override void Parse()
//        {
//            string typeName = ConfigFactory.getInstance().ReadString(GetSectionName(), PLATFORM_KEY, "UU");
//            if (typeName.Equals("RK")) { ProjID = "3004"; }
//            else if (typeName.Equals("UU")) { ProjID = "1004"; }
//            PlatformName = typeName;
//            UserName = ConfigFactory.getInstance().ReadString(GetSectionName(), USER_KEY, "user12234");
//            Passwd = ConfigFactory.getInstance().ReadString(GetSectionName(), PASSWORD_KEY, "&36Hdiw#$");
//        }
    }

   public class PhoneCheckAccount : Account
    {

       public override string GetSectionName()
       {
           return "PHONEACCOUNT";
       }
    }
}
