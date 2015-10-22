using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace Utilities.Utility
{
    public struct Rect
    {
        public int Left;
        public int Top;
        public int Right;
        public int Bottom;
    }
   public class MouseUtility
    {

        private static readonly int MOUSEEVENTF_LEFTDOWN = 0x0002;//模拟鼠标移动
        private static readonly int MOUSEEVENTF_MOVE = 0x0001;//模拟鼠标左键按下
        private static readonly int MOUSEEVENTF_LEFTUP = 0x0004;//模拟鼠标左键抬起
        private static readonly int MOUSEEVENTF_ABSOLUTE = 0x8000;//鼠标绝对位置
        private static readonly int MOUSEEVENTF_RIGHTDOWN = 0x0008; //模拟鼠标右键按下 
        private static readonly int MOUSEEVENTF_RIGHTUP = 0x0010; //模拟鼠标右键抬起 
        private static readonly int MOUSEEVENTF_MIDDLEDOWN = 0x0020; //模拟鼠标中键按下 
        private static readonly int MOUSEEVENTF_MIDDLEUP = 0x0040;// 模拟鼠标中键抬起


        [DllImport("user32")]
        public static extern void keybd_event(byte bVk, byte bScan, int dwFlags, int dwExtraInfo); 
        [DllImport("user32")]
        public static extern void mouse_event(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo);
        [DllImport("user32.dll")]
        public static extern int SetCursorPos(int x, int y);
        [DllImport("user32.dll", EntryPoint = "FindWindowEx")]
        public static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);
        [DllImport("user32.dll")]
        public static extern int GetWindowRect(IntPtr hwnd, out  Rect lpRect);
        // Get a handle to an application window.
        [DllImport("USER32.DLL", CharSet = CharSet.Unicode)]
        public static extern IntPtr FindWindow(string lpClassName,
            string lpWindowName);

        // Activate an application window.
        [DllImport("USER32.DLL")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        public static void MoveCursor(Point fromPnt, Point toPnt,int moveSpeed=5,int step=3)
        {
            float k = (float)(toPnt.Y - fromPnt.Y) / (float)(toPnt.X - fromPnt.X);
            float b = (float)(toPnt.Y - toPnt.X * k);
            if (toPnt.X < fromPnt.X)
            {
                step = -step;
            }

            int startY = fromPnt.Y;
            for (int startX = fromPnt.X; ; startX+=step )
            {
                if (step<0 && startX <= toPnt.X)
                {
                    break;
                }

                if (step > 0 && startX >= toPnt.X)
                {
                    break;
                }
                Thread.Sleep(moveSpeed);
                startY = (int)(startX * k+b);
                SetCursorPos(startX, startY);
                
            }
            SetCursorPos(toPnt.X, toPnt.Y);
        }

        public static void ClickAtXY(Point point)
        {
            mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_ABSOLUTE, point.X, point.Y, 0, 0);
            Thread.Sleep(100);
            mouse_event(MOUSEEVENTF_LEFTUP | MOUSEEVENTF_ABSOLUTE, point.X, point.Y, 0, 0);

        }
        public static void DragAndDown(Point point,Point toPnt)
        {
            SetCursorPos(point.X,point.Y);
            mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_ABSOLUTE, point.X, point.Y, 0, 0);
            MoveCursor(point, toPnt);
            SetCursorPos(toPnt.X, toPnt.Y);
            mouse_event(MOUSEEVENTF_LEFTUP | MOUSEEVENTF_ABSOLUTE, toPnt.X, toPnt.Y, 0, 0);

        }
        public static void DoPaste()
        {
            keybd_event((byte)Keys.ControlKey, 0, 0, 0);//按下
            Thread.Sleep(100);
            keybd_event((byte)Keys.V, 0, 0, 0);
            Thread.Sleep(100);
            keybd_event((byte)Keys.ControlKey, 0, 0x2, 0);//松开
            Thread.Sleep(100);
            keybd_event((byte)Keys.V, 0, 0x2, 0);

        }
        public static void PressDelete()
        {
            keybd_event((byte)Keys.Delete, 0, 0, 0);//按下
            Thread.Sleep(100);
            keybd_event((byte)Keys.Delete, 0, 0, 0);
        }
        public static void InputText(string content,int millis=200)
        {
            for (int i = 0; i < content.Length;i++ )
            {
                Thread.Sleep(millis + RandomGenerator.getRandom().Next(millis));
                KeyPress(content[i]);
            }
        }
        public static void KeyPress(char key)
        {
            byte innerKey = 0;
            bool usingShift = false;
            if (key>='a' && key <= 'z')
            {
                innerKey = (byte)((key-'a') + 65);
                usingShift = true;
            }
            else if (key >= 'A' && key <= 'Z')
            {
                innerKey = (byte)((key-'A') + 65);
            }
            else if (key >= '0' && key <= '9')
            {
                innerKey = (byte)((key-'0') + 48);
                usingShift = true;
            }
            else if (key == '@')
            {
                innerKey = 50;
            }
            else if (key == '.')
            {
                innerKey = (byte)Keys.OemPeriod;
                usingShift = true;
            }

            if (usingShift)
            {
                keybd_event(innerKey, 0, 0, 0);//按下
                keybd_event(innerKey, 0, 0x2, 0);
            }
            else
            {
                keybd_event((byte)Keys.ShiftKey, 0, 0, 0);//按下
                Thread.Sleep(500);
                keybd_event(innerKey, 0, 0, 0);
                Thread.Sleep(100);
                keybd_event((byte)Keys.ShiftKey, 0, 0x2, 0);//松开
                Thread.Sleep(100);
                keybd_event(innerKey, 0, 0x2, 0);
            }

        }
        public static void DoCopy(string text)
        {
            Clipboard.SetDataObject(text);
        }
    }
}
