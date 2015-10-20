using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace Utilities.Utility
{
    struct Struct_INTERNET_PROXY_INFO
    {
        public int dwAccessType;
        public IntPtr proxy;
        public IntPtr proxyBypass;
    };

    public class WebProxyUtility
    {
        private static int INTERNET_OPTION_PROXY = 38;
        private static int INTERNET_OPEN_TYPE_PROXY = 3;
        private static int INTERNET_OPEN_TYPE_DIRECT = 1;
        [DllImport("wininet.dll", SetLastError = true)]
        private static extern bool InternetSetOption(IntPtr hInternet, int dwOption, IntPtr lpBuffer, int lpdwBufferLength);
        /// <summary>
        /// demo:
        /// RefreshIESettings(”221.4.155.51:3128″);
        /// </summary>
        /// <param name="strProxy"></param>
        public static bool RefreshIESettings(string strProxy)
        {
            int bufferLength;
            IntPtr intptrStruct;
            Struct_INTERNET_PROXY_INFO struct_IPI;

            if (string.IsNullOrEmpty(strProxy) || strProxy.Trim().Length == 0)
            {
                strProxy = string.Empty;
                struct_IPI.dwAccessType = INTERNET_OPEN_TYPE_DIRECT;

            }
            else
            {
                struct_IPI.dwAccessType = INTERNET_OPEN_TYPE_PROXY;
            }
            struct_IPI.proxy = Marshal.StringToHGlobalAnsi(strProxy);
            struct_IPI.proxyBypass = Marshal.StringToHGlobalAnsi("local");
            bufferLength = Marshal.SizeOf(struct_IPI);
            intptrStruct = Marshal.AllocCoTaskMem(bufferLength);
            Marshal.StructureToPtr(struct_IPI, intptrStruct, true);
            return InternetSetOption(IntPtr.Zero, INTERNET_OPTION_PROXY, intptrStruct, bufferLength);
        }
    }
}
