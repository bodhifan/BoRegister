using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Utilities.Factory
{
    public class PasswordRoles
    {
        public string SECTION_NAME_KEY = "密码规则";
        public string RAND_NUM_KEY = "随机数字";
        public string RAND_ENG_KEY = "随机英文";
        public string RAND_NUM_CNT_KEY = "随机数字个数";
        public string RAND_ENG_CNT_KEY = "随机英文个数";

        public string randomNumName = "";
        public string randomEngName = "";

        public int randNumMinCnt = 0;
        public int randNumMaxCnt = 0;
        public int randEngMinCnt = 0;
        public int randEngMaxCnt = 0;

        public string GetRandomPassword()
        {
            int numCnt = randNumMinCnt + RandomGenerator.getRandom().Next(randNumMaxCnt - randNumMinCnt);
            int engCnt = randEngMinCnt + RandomGenerator.getRandom().Next(randEngMaxCnt - randEngMinCnt);
            return RandomGenerator.GenerateEnName(engCnt, 0) + RandomGenerator.GenerateRandomNum(numCnt);
        }
        public void Save()
        {

            ConfigFactory.getInstance().WriteString(SECTION_NAME_KEY, RAND_NUM_KEY, randomNumName);
            ConfigFactory.getInstance().WriteString(SECTION_NAME_KEY, RAND_ENG_KEY, randomEngName);
            ConfigFactory.getInstance().WriteString(SECTION_NAME_KEY, RAND_NUM_CNT_KEY, randNumMinCnt + "-" + randNumMaxCnt);
            ConfigFactory.getInstance().WriteString(SECTION_NAME_KEY, RAND_ENG_CNT_KEY, randEngMinCnt + "-" + randEngMaxCnt);
        }
        public void Parse()
        {
            randomNumName = ConfigFactory.getInstance().ReadString(SECTION_NAME_KEY, RAND_NUM_KEY, "启用");
            randomEngName = ConfigFactory.getInstance().ReadString(SECTION_NAME_KEY, RAND_ENG_KEY, "启用");
            string temp = ConfigFactory.getInstance().ReadString(SECTION_NAME_KEY, RAND_NUM_CNT_KEY, "3-5");
            string[] chinaCntList = temp.Split(new char[] { '-' });
            if (chinaCntList.Length == 2)
            {
                randNumMinCnt = Convert.ToInt32(chinaCntList[0]);
                randNumMaxCnt = Convert.ToInt32(chinaCntList[1]);
            }
            else if (chinaCntList.Length == 1)
            {
                randNumMinCnt = randNumMaxCnt = Convert.ToInt32(chinaCntList[0]);
            }

            temp = ConfigFactory.getInstance().ReadString(SECTION_NAME_KEY, RAND_ENG_CNT_KEY, "3-5");
            chinaCntList = temp.Split(new char[] { '-' });
            if (chinaCntList.Length == 2)
            {
                randEngMinCnt = Convert.ToInt32(chinaCntList[0]);
                randEngMaxCnt = Convert.ToInt32(chinaCntList[1]);
            }
            else if (chinaCntList.Length == 1)
            {
                randEngMinCnt = randEngMaxCnt = Convert.ToInt32(chinaCntList[0]);
            }
        }

    }
    /// <summary>
    /// 账号规则
    /// </summary>
    public class AccountRoles
    {
        public string SECTION_NAME_KEY = "账号规则";
        public string SURNAME_KEY = "随机姓氏";
        public string RAND_CHINA_KEY = "随机中文";
        public string RAND_ENG_KEY = "随机英文";
        public string RAND_CHINA_CNT_KEY = "随机中文个数";
        public string RAND_ENG_CNT_KEY = "随机英文个数";
        public string randomSurName = "";
        public string randomChinaName = "";
        public string randomEngName = "";
        public int randChinaMinCnt = 0;
        public int randChinaMaxCnt = 0;
        public int randEngMinCnt = 0;
        public int randEngMaxCnt = 0;

        public string GetRandomAccName()
        {
            int randomChina = randChinaMinCnt + RandomGenerator.getRandom().Next(randChinaMaxCnt - randChinaMinCnt);
            int randomEnglish = randEngMinCnt + RandomGenerator.getRandom().Next(randEngMaxCnt - randEngMinCnt);
            return RandomGenerator.GenerateRandomChinese(randomChina) + RandomGenerator.getRandomEnName(randomEnglish);
        }
        public void Save()
        {
            ConfigFactory.getInstance().WriteString(SECTION_NAME_KEY, SURNAME_KEY, randomSurName);
            ConfigFactory.getInstance().WriteString(SECTION_NAME_KEY, RAND_CHINA_KEY, randomChinaName);
            ConfigFactory.getInstance().WriteString(SECTION_NAME_KEY, RAND_ENG_KEY, randomEngName);
            ConfigFactory.getInstance().WriteString(SECTION_NAME_KEY, RAND_CHINA_CNT_KEY, randChinaMinCnt+"-"+randChinaMaxCnt);
            ConfigFactory.getInstance().WriteString(SECTION_NAME_KEY, RAND_ENG_CNT_KEY, randEngMinCnt+"-"+randEngMaxCnt);
        }
        public void Parse()
        {
           randomSurName = ConfigFactory.getInstance().ReadString(SECTION_NAME_KEY, SURNAME_KEY, "");
           randomChinaName = ConfigFactory.getInstance().ReadString(SECTION_NAME_KEY, RAND_CHINA_KEY, "");
           randomEngName = ConfigFactory.getInstance().ReadString(SECTION_NAME_KEY, RAND_ENG_KEY, "");
           string temp = ConfigFactory.getInstance().ReadString(SECTION_NAME_KEY, RAND_CHINA_CNT_KEY, "0");
           string[] chinaCntList = temp.Split(new char[]{'-'});
            if (chinaCntList.Length == 2)
            {
                randChinaMinCnt = Convert.ToInt32(chinaCntList[0]);
                randChinaMaxCnt = Convert.ToInt32(chinaCntList[1]);
            }
            else if (chinaCntList.Length == 1)
            {
                randChinaMinCnt = randChinaMaxCnt = Convert.ToInt32(chinaCntList[0]);
            }
           
           temp = ConfigFactory.getInstance().ReadString(SECTION_NAME_KEY, RAND_ENG_CNT_KEY,"0");
           chinaCntList = temp.Split(new char[] { '-' });
           if (chinaCntList.Length == 2)
           {
               randEngMinCnt = Convert.ToInt32(chinaCntList[0]);
               randEngMaxCnt = Convert.ToInt32(chinaCntList[1]);
           }
           else if (chinaCntList.Length == 1)
           {
               randEngMinCnt = randEngMaxCnt = Convert.ToInt32(chinaCntList[0]);
           }
        }
    }
    public class ConfigFactory
    {
        // 配置文件默认位置
        private string configPath = Application.StartupPath + "/config.ini";
        private static ConfigFactory _instance = null;
        IniFiles iniFile = null;
        private ConfigFactory()
        {
            iniFile = new IniFiles(configPath);
        }
        public static ConfigFactory getInstance()
        {
            if (_instance == null)
            {
                _instance = new ConfigFactory();
            }
            return _instance;
        }

        public void WriteString(string Section, string Ident, string Value)
        {
            iniFile.WriteString(Section, Ident, Value);
        }

        //读取INI文件指定
        public string ReadString(string Section, string Ident, string Default)
        {
            return iniFile.ReadString(Section, Ident, Default);
        }

        public void WriteInteger(string Section, string Ident, int Value)
        {
            iniFile.WriteInteger(Section, Ident, Value);
        }

        //读取INI文件指定
        public int ReadInteger(string Section, string Ident, int Default)
        {
            return iniFile.ReadInteger(Section, Ident, Default);
        }

    }
    public class AccountFactory
    {
        // 图片验证码账号
        private Account imageAcc = null;

        // 短信验证码账号
        private  Account smsAcc = null;
        
        // 宽带破号账号
        private  Account adslAcc = null;

        private AccountRoles accountRoles = null;
        PasswordRoles pwdRoles = null;
        private static AccountFactory _instance = null;
        public static AccountFactory getInstance()
        {
            if (_instance == null)
            {
                _instance = new AccountFactory();                
            }
            return _instance;
        }
        public void Save()
        {
            if (imageAcc != null)
            {
                imageAcc.Save();
            }
            if (smsAcc != null)
            {
                smsAcc.Save();
            }
            if (adslAcc != null)
            {
                adslAcc.Save();
            }
            if (accountRoles != null)
            {
                accountRoles.Save();
            }
            if (pwdRoles != null)
            {
                pwdRoles.Save();
            }
        }

        public AccountRoles getAccountRoles()
        {
            if (accountRoles == null)
            {
                accountRoles = new AccountRoles();
                accountRoles.Parse();
            }
            return accountRoles;
        }
        public Account getImageCodeAcc()
        {
            if (imageAcc == null)
            {
                imageAcc = new ImgCheckAccount();
                imageAcc.Parse();
            }
            return imageAcc;
        }
        public Account getSMSAcc()
        {
            if (smsAcc == null)
            {
                smsAcc = new PhoneCheckAccount();
                smsAcc.Parse();
            }
            return smsAcc;
        }

        public Account getAdslAcc()
        {
            if (adslAcc == null)
            {
                adslAcc = new ADSLAccount();
                adslAcc.Parse();
            }
            return adslAcc;
        }
        public PasswordRoles getPwdRoles()
        {
            if (pwdRoles == null)
            {
                pwdRoles = new PasswordRoles();
                pwdRoles.Parse();
            }

            return pwdRoles;
        }
    }
}
