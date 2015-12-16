using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PayRegister
{
    public class RegisterXpath
    {
       
        /**
         *  注册小号
         **/



        // 图片验证码输入框
        public static string IMAGE_CHECK_INPUT = "//*[@id=\"J_CheckCode\"]";

        // 图片验证码
        public static string IMAGE_CHECK = "//*[@id=\"J_Form\"]/div[4]/div/div[2]/img";

       

        // 当前title
        public static string CURRENT_TITLE_LABEL = "/html/body/div[1]/h1";
        public static string REGISTER_TITLE_TEXT = "淘宝注册";

        /// <summary>
        ///  使用邮箱注册步骤
        /// </summary>
        public static string PHONE_TITLE_TEXT = "手机验证";
        public static string SET_LOGIN_NAME_TEXT = "设置登录名";
        public static string SET_USER_NAME_TEXT = "设置会员名";




        // 使用邮箱注册 
        public static string USE_EMAIL_REGISTER_BUTTON = "/html/body/div[2]/div[4]/div/a";

        // 设置密码
        public static string PASSWORD_INPUT = "//*[@id=\"newPwd\"]";
        // 设置会员名输入框
        public static string SET_USER_NAME_INPUT = "//*[@id=\"J_Nick\"]";

        

        //短信验证码等待时间 30s
        public static int phoneLoopTimes = 30;
        public static int curphoneLoopTimes = 0;


        /*
         * 激活支付宝
         */

        public static string EMIAL_INPUT = "//*[@id=\"TPL_username_1\"]";
        public static string EMIAL_PWD_INPUT = "//*[@id=\"TPL_password_1\"]";

        public static string LOGIN_BUTTON = "//*[@id=\"J_SubmitStatic\"]";

        // 当前tile名字 
        // /html/body/div[1]/h1
        public static string CURRENT_TITLE_XPATH = "/html/body/div[1]/h1";
        public static string TITLE_FILL_USER_NAME = "补全淘宝会员名";
        public static string TITLE_IDENTITY_VERIFY = "身份验证";
        public static string FILL_CHECK_TITLE_TEXT = "填写校验码";

        // 用户名
        public static string USER_NAME_INPUT = "//*[@id=\"J_Nick\"]";

        // 图片验证码输入框
        public static string IMAGE_CODE_INPUT = "//*[@id=\"J_CheckCode\"]";

        // 图片验证码 //*[@id="J_Form"]/div[4]/div/div[2]/img
        public static string IMAGE_CODE_IMAGE="//*[@id=\"J_Form\"]/div[4]/div/div[2]/img";

        // 下一步
        public static string NEXT_BUTTON = "//*[@id=\"btn-submit\"]";

        // 确认会员名 
        public static string CONFIRM_SET_USER_NAME = "/html/body/div[5]/div[3]/span[1]";

        // 电话号码输入框
        public static string PHONE_NUM_INPUT = "//*[@id=\"J_Mobile\"]";

        // 短信验证码输入框
        public static string SMS_CODE_INPUT = "//*[@id=\"J_Form\"]/div[2]/div/div[1]/div/div/input";
    }
}
