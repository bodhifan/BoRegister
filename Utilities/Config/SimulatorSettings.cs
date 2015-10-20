using System;
using System.Collections.Generic;
using System.Text;
using Utilities.Factory;

namespace PayRegister
{
   public class SimulatorSettings
    {
        private static SimulatorSettings instance = null;
        public static SimulatorSettings getInstance()
        {
            if (instance == null)
            {
                instance = new SimulatorSettings();
                instance.Parse();
            }
            return instance;
        }

        private void Parse()
        {
            input_gap = ConfigFactory.getInstance().ReadInteger("按键", INPUT_GAP, 200);
            mouse_gap = ConfigFactory.getInstance().ReadInteger("按键", MOUSE_GAP, 5);
        }
        public int GetGap()
        {
           x_gap = ConfigFactory.getInstance().ReadInteger("按键", X_GAP, 15);
           return x_gap;
        }

        public int GetInputGap()
        {
            return input_gap;
        }
        public int GetMouseGap()
        {
            return mouse_gap;
        }
        public void Save()
        {
            ConfigFactory.getInstance().WriteInteger("按键", X_GAP, x_gap);
            ConfigFactory.getInstance().WriteInteger("按键", INPUT_GAP, input_gap);
            ConfigFactory.getInstance().WriteInteger("按键", MOUSE_GAP, mouse_gap);
        }
        public static string X_GAP = "清除按钮偏移量";
        public static string INPUT_GAP = "键盘输入间隔";
        public static string MOUSE_GAP = "鼠标移动速度";
        public int x_gap = 15;
        public int input_gap=200;
        public int mouse_gap = 5;
    }
}
