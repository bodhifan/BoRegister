using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using Microsoft.Win32;
using System.Diagnostics;

namespace Utilities
{
    public struct RASCONN
    {
        public int dwSize;
        public IntPtr hrasconn;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 257)]
        public string szEntryName;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 17)]
        public string szDeviceType;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 129)]
        public string szDeviceName;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    public struct RasStats
    {
        public int dwSize;
        public int dwBytesXmited;
        public int dwBytesRcved;
        public int dwFramesXmited;
        public int dwFramesRcved;
        public int dwCrcErr;
        public int dwTimeoutErr;
        public int dwAlignmentErr;
        public int dwHardwareOverrunErr;
        public int dwFramingErr;
        public int dwBufferOverrunErr;
        public int dwCompressionRatioIn;
        public int dwCompressionRatioOut;
        public int dwBps;
        public int dwConnectionDuration;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    public struct RasEntryName
    {
        public int dwSize;
        //[MarshalAs(UnmanagedType.ByValTStr,SizeConst=(int)RasFieldSizeConstants.RAS_MaxEntryName + 1)]
        public string szEntryName;
        //#if WINVER5
        //  public int dwFlags;
        //  [MarshalAs(UnmanagedType.ByValTStr,SizeConst=260+1)]
        //  public string szPhonebookPath;
        //#endif
    }
    public class RAS
    {
        [DllImport("Rasapi32.dll", EntryPoint = "RasEnumConnectionsA",
             SetLastError = true)]

        internal static extern int RasEnumConnections
            (
            ref RASCONN lprasconn, // buffer to receive connections data
            ref int lpcb, // size in bytes of buffer
            ref int lpcConnections // number of connections written to buffer
            );


        [DllImport("rasapi32.dll", CharSet = CharSet.Auto)]
        internal static extern uint RasGetConnectionStatistics(
            IntPtr hRasConn,       // handle to the connection
            [In, Out]RasStats lpStatistics  // buffer to receive statistics
            );
        [DllImport("rasapi32.dll", CharSet = CharSet.Auto)]
        public extern static uint RasHangUp(
            IntPtr hrasconn  // handle to the RAS connection to hang up
            );

        [DllImport("rasapi32.dll", CharSet = CharSet.Auto)]
        public extern static uint RasEnumEntries(
            string reserved,              // reserved, must be NULL
            string lpszPhonebook,         // pointer to full path and
            //  file name of phone-book file
            [In, Out]RasEntryName[] lprasentryname, // buffer to receive
            //  phone-book entries
            ref int lpcb,                  // size in bytes of buffer
            out int lpcEntries             // number of entries written
            //  to buffer
            );

        [DllImport("wininet.dll", CharSet = CharSet.Auto)]
        public extern static int InternetDial(
            IntPtr hwnd,
            [In]string lpszConnectoid,
            uint dwFlags,
            ref int lpdwConnection,
            uint dwReserved
            );

        public RAS()
        {
        }
    }
    public enum DEL_CACHE_TYPE //要删除的类型。
    {
        File,//表示internet临时文件
        Cookie //表示Cookie
    }

    public class RASDisplay
    {
        [DllImport("wininet.dll", CharSet = CharSet.Auto)]
        public static extern bool DeleteUrlCacheEntry(
            DEL_CACHE_TYPE type
            );
        private string m_duration;
        private string m_ConnectionName;
        private string[] m_ConnectionNames;
        private double m_TX;
        private double m_RX;
        private bool m_connected;
        private IntPtr m_ConnectedRasHandle;

        RasStats status = new RasStats();
        public RASDisplay()
        {
            m_connected = true;

            RAS lpras = new RAS();
            RASCONN lprasConn = new RASCONN();

            lprasConn.dwSize = Marshal.SizeOf(typeof(RASCONN));
            lprasConn.hrasconn = IntPtr.Zero;

            int lpcb = 0;
            int lpcConnections = 0;
            int nRet = 0;
            lpcb = Marshal.SizeOf(typeof(RASCONN));

            nRet = RAS.RasEnumConnections(ref lprasConn, ref lpcb, ref
                lpcConnections);

            if (nRet != 0)
            {
                m_connected = false;
                return;

            }

            if (lpcConnections > 0)
            {
                //for (int i = 0; i < lpcConnections; i++)

                //{
                RasStats stats = new RasStats();

                m_ConnectedRasHandle = lprasConn.hrasconn;
                RAS.RasGetConnectionStatistics(lprasConn.hrasconn, stats);


                m_ConnectionName = lprasConn.szEntryName;

                int Hours = 0;
                int Minutes = 0;
                int Seconds = 0;

                Hours = ((stats.dwConnectionDuration / 1000) / 3600);
                Minutes = ((stats.dwConnectionDuration / 1000) / 60) - (Hours * 60);
                Seconds = ((stats.dwConnectionDuration / 1000)) - (Minutes * 60) - (Hours * 3600);


                m_duration = Hours + " hours " + Minutes + " minutes " + Seconds + " secs";
                m_TX = stats.dwBytesXmited;
                m_RX = stats.dwBytesRcved;
                //}
            }
            else
            {
                m_connected = false;
            }


            int lpNames = 1;
            int entryNameSize = 0;
            int lpSize = 0;
            RasEntryName[] names = null;

            entryNameSize = Marshal.SizeOf(typeof(RasEntryName));
            lpSize = lpNames * entryNameSize;

            names = new RasEntryName[lpNames];
            names[0].dwSize = entryNameSize;

            uint retval = RAS.RasEnumEntries(null, null, names, ref lpSize, out lpNames);

            //if we have more than one connection, we need to do it again
            if (lpNames > 1)
            {
                names = new RasEntryName[lpNames];
                for (int i = 0; i < names.Length; i++)
                {
                    names[i].dwSize = entryNameSize;
                }

                retval = RAS.RasEnumEntries(null, null, names, ref lpSize, out lpNames);

            }
            m_ConnectionNames = new string[names.Length];


            if (lpNames > 0)
            {
                for (int i = 0; i < names.Length; i++)
                {
                    m_ConnectionNames[i] = names[i].szEntryName;
                }
            }
        }

        public string Duration
        {
            get
            {
                return m_connected ? m_duration : "";
            }
        }

        public string[] Connections
        {
            get
            {
                return m_ConnectionNames;
            }
        }

        public double BytesTransmitted
        {
            get
            {
                return m_connected ? m_TX : 0;
            }
        }
        public double BytesReceived
        {
            get
            {
                return m_connected ? m_RX : 0;

            }
        }
        public string ConnectionName
        {
            get
            {
                return m_connected ? m_ConnectionName : "";
            }
        }
        public bool IsConnected
        {
            get
            {
                return m_connected;
            }
        }

        public int Connect(string Connection)
        {
            int temp = 0;
            uint INTERNET_AUTO_DIAL_UNATTENDED = 2;
            int retVal = RAS.InternetDial(IntPtr.Zero, Connection, INTERNET_AUTO_DIAL_UNATTENDED, ref temp, 0);
            return retVal;
        }
        public void Disconnect()
        {
            RAS.RasHangUp(m_ConnectedRasHandle);
        }
    }


    public class SinASDL
    {
        //ASDL在注册表中的存放位置，这个是针对WinXP的，不知道Win7是否是这个，待验证
        private static String _adlskeys = @"RemoteAccess\Profile";
        public static String adlskeys
        {
            get
            {
                return _adlskeys;
            }
        }

        /// <summary>
        /// 获取本机的拨号名称，也就是本机上所有的拨号
        /// </summary>
        /// <returns></returns>
        public static String[] GetASDLNames()
        {
            RegistryKey RegKey = Registry.CurrentUser.OpenSubKey(adlskeys);
            if (RegKey != null)
                return RegKey.GetSubKeyNames();
            else
                return null;
        }


        private String _asdlname = null;
        private ProcessWindowStyle _windowstyle = ProcessWindowStyle.Hidden;


        /// <summary>
        /// 实例化一个ASDL连接
        /// </summary>
        /// <param name="asdlname">ASDL名称，如“宽带连接”</param>
        /// <param name="username">用户名</param>
        /// <param name="password">密码</param>
        /// <param name="windowstyle">窗口显示方式，默认为因此拨号过程</param>
        public SinASDL(String asdlname, String username = null, String password = null, ProcessWindowStyle windowstyle = ProcessWindowStyle.Hidden)
        {
            this.ASDLName = asdlname;
            this.Username = username;
            this.Password = password;
            this.WindowStyle = windowstyle;
        }

        /// <summary>
        /// 拨号名称
        /// </summary>
        public String ASDLName
        {
            get
            {
                return this._asdlname;
            }
            set
            {
                this._asdlname = value;
            }
        }

        /// <summary>
        /// 拨号进程的窗口方式
        /// </summary>
        public ProcessWindowStyle WindowStyle
        {
            get
            {
                return this._windowstyle;
            }
            set
            {
                this._windowstyle = value;
            }
        }

        private String _username = null;	//用户名
        private String _password = null;	//密码
        /// <summary>
        /// 用户名
        /// </summary>
        public String Username
        {
            get
            {
                return this._username;
            }
            set
            {
                this._username = value;
            }
        }
        /// <summary>
        /// 密码
        /// </summary>
        public String Password
        {
            get
            {
                return this._password;
            }
            set
            {
                this._password = value;
            }
        }



        /// <summary>
        /// 开始拨号
        /// </summary>
        /// <returns>返回拨号进程的返回值</returns>
        public int Connect()
        {
            Process pro = new Process();
            pro.StartInfo.FileName = "rasdial.exe";
            pro.StartInfo.Arguments = this.ASDLName + " " + this.Username + " " + this.Password;
            pro.StartInfo.WindowStyle = this.WindowStyle;
            pro.Start();
            pro.WaitForExit();
            return pro.ExitCode;
        }

        /// <summary>
        /// 端口连接
        /// </summary>
        /// <returns></returns>
        public int Disconnect()
        {
            Process pro = new Process();
            pro.StartInfo.FileName = "rasdial.exe";
            pro.StartInfo.Arguments = this.ASDLName + " /DISCONNECT";
            pro.StartInfo.WindowStyle = this.WindowStyle;
            pro.Start();
            pro.WaitForExit();
            return pro.ExitCode;
        }
    }

}
