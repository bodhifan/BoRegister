using System;
using System.Collections.Generic;
using System.Text;

namespace Utilities
{
    // 支付宝配置文件
    namespace PayConfig
    {
        public class Settings
        {
            // 邮箱保存路径
            public static string EMAILS_PATH = @"emails.txt";

            // 配置文件所在位置
            public static string CONFIG_PATH = @"config.ini";

            // 图片验证码保存位置
            public static string PATH_CHECKCODE_IMG = @"1.jpg";
        }

        public class Elements
        {
            // 邮箱账号
            public static string ACCOUNT_NAME = "J-accName";

            // 邮箱验证
            public static string EMAIL_CHECK = "J-pop-accName";

            // 图片验证码
            public static string IMAGE_CODE = "J-checkcode-img";

            // 图片验证码输入框
            public static string IMAGE_CODE_INPUT = "J-checkcode";

            // 图片验证码是否正确
            public static string IMAGE_CODE_CHECK_ICON = "checkcodeIcon";

            // 提交认证
            public static string SUBMIT_BTN = "J-submit";

            // 检查邮箱
            public static string CHECK_CONFIRM_EMAIL = "submitBtn";

            // 邮箱登录账号
            public static string LOGIN_EMAIL_USER = "idInput";
            // 邮箱登录密码
            public static string LOGIN_EMAIL_PWD = "pwdInput";

            // 邮箱登录密码
            public static string LOGIN_EMAIL_CONFIRM = "loginBtn";

        }

        public class ImageSettings
        {
            public Account account;
            static string TYPE = "TYPE";
            static string PROJTYPE = "PROJTPYE";
            int type;
            int projType;
            static ImageSettings instance = null;
            private ImageSettings()
            {
                account = new ImgCheckAccount();
            }
            public static ImageSettings getInstance()
            {
                if (instance != null) return instance;
                instance = new ImageSettings();
                instance.parse();
                return instance;
            }

            private void parse()
            {
                IniFiles iniConfig = new IniFiles(Settings.CONFIG_PATH);
                string typeName = iniConfig.ReadString(account.GetSectionName(), TYPE, "UU");
                if (typeName.Equals("RK")) {type = 1;projType = 3004;}
                else if (typeName.Equals("UU")){ type = 2;projType = 1004;}
                account.UserName = iniConfig.ReadString(account.GetSectionName(), Account.USER_KEY, "user12234");
                account.Passwd = iniConfig.ReadString(account.GetSectionName(), Account.PASSWORD_KEY, "&36Hdiw#$");
            }

            public int GetType()
            {
                return type;
            }
            public int GetCodeType()
            {
                return projType;
            }
            public void dump()
            {
                IniFiles iniConfig = new IniFiles(Settings.CONFIG_PATH);
                
                string typeName = "UU";
                if (type == 1) typeName = "RK";
                iniConfig.WriteString(account.GetSectionName(), TYPE, typeName);
                iniConfig.WriteString(account.GetSectionName(), Account.USER_KEY, account.UserName);
                iniConfig.WriteString(account.GetSectionName(), Account.PASSWORD_KEY, account.Passwd);               
                iniConfig.UpdateFile();
            }
        }
    }
}
