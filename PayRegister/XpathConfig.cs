using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PayRegister
{
    public class RegisterXpath
    {
        public static string SEARCH_TEST = "//*[@id=\"kw\"]";
        public static string SUBMIT_TEST = "//*[@id=\"su\"]";

        // 电子邮件-输入框
        public static string EMIAL_INPUT = "//*[@id=\"J-accName\"]";

        // 电子邮件验证
        public static string CHECK_EMAIL_ELEMENT = "//*[@id=\"J-pop-accName\"]/span";
        // 验证码图片
        public static string IMAGE_CHECK_CODE = "//*[@id=\"J-checkcode-img\"]";

        // 验证码-输入框
        public static string IMGCODE_INPUT = "//*[@id=\"J-checkcode\"]";

        // 提交按钮
        public static string NEXT_SUBMIT = "//*[@id=\"J-submit\"]";



        /**
         *  注册小号
         **/

        // 电话号码输入框
        public static string PHONE_NUM_INPUT = "//*[@id=\"J_Mobile\"]";

        // 图片验证码输入框
        public static string IMAGE_CHECK_INPUT = "//*[@id=\"J_CheckCode\"]";

        // 图片验证码
        public static string IMAGE_CHECK = "//*[@id=\"J_Form\"]/div[4]/div/div[2]/img";

        // 下一步
        public static string NEXT_BUTTON = "//*[@id=\"J-submit\"]";

        // 当前title
        public static string CURRENT_TITLE_LABEL = "/html/body/div[1]/h1";
        public static string REGISTER_TITLE_TEXT = "淘宝注册";
        public static string FILL_CHECK_TITLE_TEXT = "填写校验码";

        /// <summary>
        ///  使用邮箱注册步骤
        /// </summary>
        public static string PHONE_TITLE_TEXT = "手机验证";
        public static string SET_LOGIN_NAME_TEXT = "设置登录名";
        public static string SET_USER_NAME_TEXT = "设置会员名";


        // 短信验证码输入框
        public static string SMS_CODE_INPUT="//*[@id=\"J_Form\"]/div[2]/div/div[1]/div/div/input";

        // 使用邮箱注册 
        public static string USE_EMAIL_REGISTER_BUTTON = "/html/body/div[2]/div[4]/div/a";

        // 邮箱输入框 
        public static string EMAIL_INPUT = "//*[@id=\"J-accName\"]";

        // 设置密码
        public static string PASSWORD_INPUT = "//*[@id=\"queryPwd\"]";
        // 设置会员名输入框
        public static string SET_USER_NAME_INPUT = "//*[@id=\"J_Nick\"]";
       
        // 确认会员名
        public static string CONFIRM_SET_USER_NAME = "/html/body/div[5]/div[3]/span[1]";

        //短信验证码等待时间 30s
        public static int phoneLoopTimes = 30;
        public static int curphoneLoopTimes = 0;
    }
}
