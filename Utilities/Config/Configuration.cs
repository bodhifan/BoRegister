using System;
using System.Collections.Generic;
using System.Text;

namespace Utilities
{
    public class Config
    {
        // 邮件服务器扩展名
        public static string EMAIL_163 = "163.com";
        public static string EMAIL_126 = "126.com";


        // 模式匹配字符
        public static string PATTERN_EMAIL_LINK = @"<a.*?target=""_blank"".*?href=""(?<wz>(.|\n)*?)""";


        // 淘宝注册地址
        public static string TB_EMAIL_REG_URL = "http://reg.taobao.com/member/reg/fill_email.htm";
        public static string TB_PHONE_REG_URL = "http://reg.taobao.com/member/reg/fill_mobile.htm";
        public static string TB_CONFIRM_REG_URL = "http://reg.taobao.com/member/reg/fill_user_info.htm";

        public static string TB_BLANK_URL = "about:blank";
        // 邮箱输入框ID
        public static string EMAIL_REG_ELEMENT_ID = "J_Email";
        // 校验邮箱ID
        public static string EMAIL_CHECK_ELEMENT_ID = "J_MsgEmail";
        public static string EMAIL_CHECK_AFTER_CLICK_ID = "J_MsgEmailForm";
        public static string EMAIL_CONFIRM_CHECKED_ID = "J_Agreement";

        // 验证码输入框ID "J_CheckCodeInput"
        public static string CHECK_CODE_ELEMENT_ID = "J_CheckCodeInput";
        // 校验验证码ID
        public static string CHECK_CHECK_ELEMENT_ID = "J_MsgCheckCode";


        // 提交按钮ID
        public static string BUTTON_SUBMIT_ID = "J_BtnEmailForm";
        // 图片验证码ID
        public static string IMG_CHECKCODE_ID = "J_CheckCodeImg1";

        // 图片验证码本地地址
        public static string PATH_CHECKCODE_IMG = @"1.jpg";
        public static int TYPE_CHECKCODE_IMG = 3004;
        public static int TYPE_RK_CKECKCODE = 3040;
       // public static int TYPE_RK_CKECKCODE = 3004;
        // 电话号码输入框-发送
        public static string SMS_PHONE_INPUT_ID = "J_Mobile";
        public static string SMS_CHECK_PHONE_ID = "J_MsgMobile";
        public static string SMS_CHECK_PHONE_MSG_ID = "J_MsgMobileCode";
        // 获取短信验证码元素id
        public static string SMS_FETCH_CHECK_CODE = "J_BtnMobileCodeForm";
        public static string SMS_RECLICK_ID = "J_BtnMobileCode";
        // 短信验证码输入框
        public static string SMS_INPUT_SMS_CODE = "J_MobileCode";
        // 短信验证码下一步

        public static string BUTTON_SMS_NEXT_ENTER = "J_BtnMobileEnterForm";
        public static string BUTTON_SMS_MSG_ENTER = "J_MsgEnterForm";
        public static string BUTTON_SMS_BACK = "J_ModifyMobile";
        public static string BUTTON_SMS_MSG_NEW = "J_MsgResendCode";

        public static string BUTTON_SMS_NEXT = "J_BtnMobileCodeForm";
        public static string BUTTON_SMS_MSG = "J_MsgMobileCodeForm";
        public static string BUTTON_SUCC_MSG = "J_Email";
        public static string LOG_FILE = "log.txt";


        // 网页登陆邮箱
        public static string EMIAL_ACCOUNT = "idInput";
        public static string EMIAL_PASSWORD = "pwdInput";
        public static string EMIAL_LOGIN_BTN = "loginBtn";
        public static string EMIAL_NOREAD_BTN = "_mail_component_51_51";


        // 确认密码
        public static string CONFIRM_PASSWD1 = "J_Password";
        public static string CONFIRM_PASSWD2 = "J_RePassword";
        public static string CONFIRM_NICKNAME = "J_Nick";
        public static string CONFIRM_NICKNAME_CHECK = "J_MsgNick";
        public static string CONFIRM_BUTTON = "J_BtnInfoForm";

        // 账号保存地址
        public static string PATH_OF_EMAILS = @"Emails.txt";
        public static string PATH_OF_UNUSEDEMAILS = @"UnusedEmails.txt";
        public static string PATH_OF_USEDEMAILS = @"UsedEmails.txt";
        public static string PATH_OF_SUCC = @"tbsucc.txt";
        public static string PATH_OF_FAILED = @"tbfailed.txt";

        public static string PATH_OF_SUCC_SHORT = @"sucshort.txt";
        public static string PATH_OF_FAILED_SHORT = @"failedshort.txt";

        public static string PATH_OF_ALL = @"alltb.txt";

        public static int IMG_CHECK_PROVIDER = 1;// 图片验证码提供商：0，UU 云；1，若快

        public static string PATH_OF_CONFIG = @"Config.ini";
    }
    public class TBConfig : IDisposable
    {
        // 账号相关
        // 1. 图片验证码
        public ImgCheckAccount imageAccount;
        public PhoneCheckAccount phoneAccount;
        public int providorId;
        public static string imgSection = "IMGSECTION";
        public static string phoneSection = "PHONESECTION";
        public static string provideID = "Providor";
        public static string user = "User";
        public static string passwd = "Password";
        public static string proj = "ProjID";

        // 循环环境设置
        public static string loopSection = "LOOPSECTION";
        public static string gapSecond = "GapSeconds";
        public int GapSeconds;
        public static string loopTimes = "LoopTimes";
        public int LoopTimes;
        public static string confirmWaitTimes = "ComfirmWaitTimes";
        // 电话号码重新确认
        public static string phoneReCheckCnt = "PhoneReCheckCnt";
        public int phoneRecheckCount;
        public static bool   IsStopRun = false;//是否停止运行
        public static int MaxWaitConfirmTimes = 5;//确认地址最大等待时间
        private string filePath;

        // 账号信息
        public static string accountInfoSection = "ACCOUNTSETTINGS";
        // 密码长度
        public static string pwdLenghtStr = "PasswordLength";
        public int pwdLenght;
        // 字符个数
        public static string symbolsCntStr = "SymbolsCount";
        public int symbolsCnt;
        //用户名英文个数
        public static string enCountStr = "UserEnCount";
        public static string enBaseLength = "BaseLength";
        public int baseLength;
        public int enCount;
        public static string numCountStr = "UserNumCount";
        public int numCount;
        public TBConfig()
        {
            imageAccount = new ImgCheckAccount();
            phoneAccount = new PhoneCheckAccount();
            LoadConfig(Config.PATH_OF_CONFIG);
 
        }

        private void LoadConfig(string path)
        {
            filePath = path;
            IniFiles iniConfig = new IniFiles(path);
            GapSeconds = iniConfig.ReadInteger(loopSection, gapSecond, 4);
            LoopTimes = iniConfig.ReadInteger(loopSection, loopTimes, 3);
            MaxWaitConfirmTimes = iniConfig.ReadInteger(loopSection, confirmWaitTimes, 5);

            // 短信验证供应商
            providorId = iniConfig.ReadInteger(phoneSection, provideID, 1);
            phoneAccount.UserName = iniConfig.ReadString(phoneSection, user, "hispy");
            phoneAccount.Passwd = iniConfig.ReadString(phoneSection, passwd, "123456flag");
            phoneAccount.ProjID = iniConfig.ReadString(phoneSection, proj, "17");

            phoneRecheckCount = iniConfig.ReadInteger(loopSection, phoneReCheckCnt, 4);
                        
            // 账号相关
            pwdLenght = iniConfig.ReadInteger(accountInfoSection, pwdLenghtStr, 15);
            symbolsCnt = iniConfig.ReadInteger(accountInfoSection, symbolsCntStr, 3);
            enCount = iniConfig.ReadInteger(accountInfoSection, enCountStr, 10);
            numCount = iniConfig.ReadInteger(accountInfoSection, numCountStr, 5);
            baseLength = iniConfig.ReadInteger(accountInfoSection, enBaseLength, 5);

        }
        private static TBConfig mInstance = null;
        public static TBConfig getInstance()
        {
            if (mInstance == null)
            {
                mInstance = new TBConfig();
            }
            return mInstance;
        }
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                //执行基本的清理代码
                IniFiles iniConfig = new IniFiles(filePath);
                iniConfig.WriteInteger(loopSection, loopTimes, LoopTimes);
                iniConfig.WriteInteger(loopSection, gapSecond, GapSeconds);
                iniConfig.WriteInteger(loopSection, confirmWaitTimes, MaxWaitConfirmTimes);

                iniConfig.WriteInteger(accountInfoSection, pwdLenghtStr, pwdLenght);
                iniConfig.WriteInteger(accountInfoSection, symbolsCntStr, symbolsCnt);
                
                iniConfig.WriteInteger(accountInfoSection, enCountStr, enCount);
                iniConfig.WriteInteger(accountInfoSection, numCountStr, numCount);
                iniConfig.WriteInteger(accountInfoSection, enBaseLength, baseLength);

                iniConfig.WriteInteger(phoneSection, provideID, providorId);
                iniConfig.WriteString(phoneSection, user, phoneAccount.UserName);
                iniConfig.WriteString(phoneSection, passwd, phoneAccount.Passwd);
                iniConfig.WriteString(phoneSection, proj, phoneAccount.ProjID);
                iniConfig.UpdateFile();
            }
        }

        ~TBConfig()
        {
            this.Dispose(true);
        }

    }
}
