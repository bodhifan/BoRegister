using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using WindowsInput;

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
        public static extern void mouse_event(int dwFlags, int dx, int dy, int dwData, IntPtr dwExtraInfo);
        [DllImport("user32.dll")]
        public static extern int SetCursorPos(int x, int y);
        [DllImport("user32.dll")]
        public static extern bool GetCursorPos(ref Point lpPoint);

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

        [DllImport("user32.dll", EntryPoint = "ShowWindow", CharSet = CharSet.Auto)]
        public static extern int ShowWindow(IntPtr hwnd, int nCmdShow);

        [DllImport("user32.dll")]
        public static extern bool IsIconic(IntPtr hWnd);

        [DllImport("user32.dll", EntryPoint = "GetWindowLong", CharSet = CharSet.Auto)]
        private static extern int GetWindowLong32(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll", EntryPoint = "GetWindowLongPtr", CharSet = CharSet.Auto)]
        private static extern IntPtr GetWindowLongPtr64(IntPtr hWnd, int nIndex);
        const int GWL_STYLE = -16;
        const long WS_MINIMIZEBOX = 0x00020000L;
        const long WS_MAXIMIZEBOX = 0x00010000L;


        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool GetWindowPlacement(IntPtr hWnd, ref WINDOWPLACEMENT lpwndpl);
        public static WINDOWPLACEMENT GetPlacement(IntPtr handle)
       {
           WINDOWPLACEMENT placement = new WINDOWPLACEMENT();
           placement.length = Marshal.SizeOf(placement);
           GetWindowPlacement(handle, ref placement);
           return placement;
        }
        public struct WINDOWPLACEMENT
        {
            public int length;
            public int flags;
            public int showCmd;
            public System.Drawing.Point ptMinPosition;
            public System.Drawing.Point ptMaxPosition;
            public System.Drawing.Rectangle rcNormalPosition;
        }

        const int SW_HIDE = 0;
        const int SW_SHOWNORMAL = 1;
        const int SW_NORMAL = 1;
        const int SW_SHOWMINIMIZED = 2;
        const int SW_SHOWMAXIMIZED = 3;
        const int SW_MAXIMIZE = 3;
        const int SW_SHOWNOACTIVATE = 4;
        const int SW_SHOW = 5;
        const int SW_MINIMIZE = 6;
        const int SW_SHOWMINNOACTIVE = 7;
        const int SW_SHOWNA = 8;
        const int SW_RESTORE = 9;
        
        public static bool IsMinWindow(IntPtr handle)
        {
//             int style = ShowWindow(handle, 8);
// 
//             if (style == 0)
//             {
//                 return true;
//             }
//             return false;
            WINDOWPLACEMENT plcase = GetPlacement(handle);
            
            if (plcase.showCmd == SW_SHOWMINIMIZED)
            {
                return true;
            }
            return false;
        }

        public static int SetCursorPos(Point pnt)
        {
            return SetCursorPos(pnt.X, pnt.Y);
        }
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

        public static void MoveCursorLeftDown(Point fromPnt, Point toPnt, int moveSpeed = 5, int step = 3)
        {
            float k = (float)(toPnt.Y - fromPnt.Y) / (float)(toPnt.X - fromPnt.X);
            float b = (float)(toPnt.Y - toPnt.X * k);
            if (toPnt.X < fromPnt.X)
            {
                step = -step;
            }

            int startY = fromPnt.Y;
            for (int startX = fromPnt.X; ; startX += step)
            {
                if (step < 0 && startX <= toPnt.X)
                {
                    break;
                }

                if (step > 0 && startX >= toPnt.X)
                {
                    break;
                }
                Thread.Sleep(moveSpeed);
                startY = (int)(startX * k + b);

                SetCursorPos(startX, startY);
                mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_ABSOLUTE, 0, 0, 0, IntPtr.Zero);

            }
            SetCursorPos(toPnt.X, toPnt.Y);
            mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_ABSOLUTE, 0, 0, 0, IntPtr.Zero);
        }

        public static void UpMouse()
        {
            Point pnt = new Point();
            GetCursorPos(ref pnt);
            mouse_event(MOUSEEVENTF_LEFTUP | MOUSEEVENTF_ABSOLUTE, pnt.X, pnt.Y, 0, IntPtr.Zero);
        }
        public static void ClickAtXY(Point point)
        {
            SetCursorPos(point.X, point.Y);
            mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_ABSOLUTE, 0, 0, 0, IntPtr.Zero);
            mouse_event(MOUSEEVENTF_LEFTUP | MOUSEEVENTF_ABSOLUTE, 0, 0, 0, IntPtr.Zero);
        }
        public static void DragAndDown(Point point,Point toPnt)
        {
            SetCursorPos(point.X,point.Y);
            mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_ABSOLUTE, 0, 0, 0, IntPtr.Zero);
            MoveCursor(point, toPnt,3,1);

           // Thread.Sleep(200);
            SetCursorPos(toPnt.X, toPnt.Y);
            mouse_event(MOUSEEVENTF_LEFTUP | MOUSEEVENTF_ABSOLUTE, 0, 0, 0, IntPtr.Zero);

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
        public static void PressKey(byte key)
        {
            keybd_event(key, 0, 0, 0);//按下
            Thread.Sleep(100);
            keybd_event(key, 0, 0x2, 0);
        }
        public static void PressTab()
        {
            keybd_event((byte)Keys.Tab, 0, 0, 0);//按下
            Thread.Sleep(100);
            keybd_event((byte)Keys.Tab, 0, 0x2, 0);
        }
        public static void PressDelete()
        {
            keybd_event((byte)Keys.Delete, 0, 0, 0);//按下
            Thread.Sleep(100);
            keybd_event((byte)Keys.Delete, 0, 0x2, 0);
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
                Thread.Sleep(100);
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
