using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Threading;
using System.Text.RegularExpressions;

namespace Utilities.Utility
{
    public class ElementUtils
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = false)]
        static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr GetWindow(IntPtr hWnd, uint uCmd);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static extern int GetClassName(IntPtr hWnd, StringBuilder lpClassName, int nMaxCount);

        public const int WM_NCLBUTTONDOWN = 0x00A1;
        public const int WM_NCLBUTTONUP = 0x00A2;

        public static Point GetElementPos(HtmlElement el)
        {
            //get element pos
            Point pos = new Point(el.OffsetRectangle.Left, el.OffsetRectangle.Top);

            //get the parents pos
            HtmlElement tempEl = el.OffsetParent;
            while (tempEl != null)
            {
                pos.X += tempEl.OffsetRectangle.Left;
                pos.Y += tempEl.OffsetRectangle.Top;
                tempEl = tempEl.OffsetParent;
            }

            return pos;
        }
        public static void ClickXY(IntPtr handler,int x,int y)
        {
            StringBuilder className = new StringBuilder(100);
            while (className.ToString() != "Internet Explorer_Server") // The class control for the browser 
            {
                handler = GetWindow(handler, 5); // Get a handle to the child window 
                GetClassName(handler, className, className.Capacity);
            }
            IntPtr lParam = (IntPtr)((y << 16) | x); // The coordinates 
            IntPtr wParam = IntPtr.Zero; // Additional parameters for the click (e.g. Ctrl) 
            const uint downCode = 0x201; // Left click down code 
            const uint upCode = 0x202; // Left click up code 
            SendMessage(handler, downCode, wParam, lParam); // Mouse button down 
            SendMessage(handler, upCode, wParam, lParam); // Mouse button down 
        }

        public static void ClickElement(HtmlElement el,IntPtr webBrowserHandler)
        {
            Point point = GetElementPos(el);
            int x = point.X; // X coordinate of the click 
            int y = point.Y; // Y coordinate of the click

            ClickXY(webBrowserHandler, x, y);
            
        }

        public static HtmlElement GetElementByTagPropertyText(WebBrowser webBrowser, string tag,string propName,string regTxt)
        {
            HtmlElement element = null;
            HtmlElementCollection divCols = webBrowser.Document.GetElementsByTagName(tag);
            foreach (HtmlElement he in divCols)
            {
                string title = he.GetAttribute(propName);
                Regex reg = new Regex(regTxt);

                if (title != null)
                {
                    Match m = reg.Match(title);
                    if (m.Success)
                    {
                        element = he;
                        break;
                    }

                }
            }
            return element;
        }
         // 获取元素通过ID
        public static HtmlElement GetElementByTagAndText(WebBrowser webBrowser, string tag,string regTxt)
        {
            HtmlElement element = null;
            HtmlElementCollection divCols = webBrowser.Document.GetElementsByTagName(tag);
            foreach (HtmlElement he in divCols)
            {
                string title = he.InnerText;
                Regex reg = new Regex(regTxt);
                
                if (title != null)
                {
                    Match m = reg.Match(title);
                    if (m.Success)
                    {
                       element = he;
                        break;
                    }

                }
            }
            return element;
        }
        // 获取元素通过ID
        public static HtmlElement GetHtmlElement(WebBrowser webBrowser, string eleId)
        {
            HtmlElement element = null;
            try
            {
                element = webBrowser.Document.GetElementById(eleId);
                if (element == null)
                {
                    //throw new Exception("注册邮箱元素ID：" + eleId + " 未找到!");
                }
            }
            catch
            {
                try
                {
                    while (webBrowser.ReadyState != WebBrowserReadyState.Complete)
                    {
                        Thread.Sleep(1000);
                    }
                }
                catch (System.Exception ex)
                {
                    return null;
                }

                element = webBrowser.Document.GetElementById(eleId);
            }
            return element;
        }

    }
}
