using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Timers;
using Microsoft.Win32;
using DotRas;
using System.Collections.ObjectModel;
using System.Net;
using System.Threading;
namespace Utilities
{
    public class ADSLConnetion
    {
        static bool rasDone = false;

        public string user;
        public string password;

        public string connectName;
        /// <summary>
        /// 创建或更新一个PPPOE连接(指定PPPOE名称)
        /// </summary>
        public void CreateOrUpdatePPPOE(string updatePPPOEname)
        {
            RasDialer dialer = new RasDialer();
            RasPhoneBook allUsersPhoneBook = new RasPhoneBook();
            string path = RasPhoneBook.GetPhoneBookPath(RasPhoneBookType.AllUsers);
            allUsersPhoneBook.Open(path);
            // 如果已经该名称的PPPOE已经存在，则更新这个PPPOE服务器地址
            if (allUsersPhoneBook.Entries.Contains(updatePPPOEname))
            {
                allUsersPhoneBook.Entries[updatePPPOEname].PhoneNumber = " ";
                // 不管当前PPPOE是否连接，服务器地址的更新总能成功，如果正在连接，则需要PPPOE重启后才能起作用
                allUsersPhoneBook.Entries[updatePPPOEname].Update();
            }
            // 创建一个新PPPOE
            else
            {
                string adds = string.Empty;
                ReadOnlyCollection<RasDevice> readOnlyCollection = RasDevice.GetDevices();
                //                foreach (var col in readOnlyCollection)
                //                {
                //                    adds += col.Name + ":" + col.DeviceType.ToString() + "|||";
                //                }
                //                _log.Info("Devices are : " + adds);
                // Find the device that will be used to dial the connection.
                RasDevice device = null;
                
                foreach (RasDevice dev in RasDevice.GetDevices())
                {
                    if (dev.DeviceType == RasDeviceType.PPPoE)
                    {
                        device = dev;
                        break;
                    }
                }
              //  = RasDevice.GetDevices().Where(o => o.DeviceType == RasDeviceType.PPPoE).First();
                RasEntry entry = RasEntry.CreateBroadbandEntry(updatePPPOEname, device);    //建立宽带连接Entry
                entry.PhoneNumber = " ";
                allUsersPhoneBook.Entries.Add(entry);
            }
        }

        public int Connect(string Connection)
        {
            int flag = 0;
            try
            {
                CreateOrUpdatePPPOE(Connection);
                RasDialer dialer = new RasDialer();
                dialer.EntryName = Connection;
                dialer.PhoneNumber = " ";
                dialer.AllowUseStoredCredentials = true;
                dialer.PhoneBookPath = RasPhoneBook.GetPhoneBookPath(RasPhoneBookType.AllUsers);
                dialer.Credentials = new NetworkCredential(user, password);
                dialer.Timeout = 1000;
                RasHandle myras = dialer.Dial();
                while (myras.IsInvalid)
                {
                    Thread.Sleep(1000);
                    myras = dialer.Dial();
                }
                if (!myras.IsInvalid)
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                flag = -1;
            }
            return flag;
        }
        static public bool ChangeIP()
        {
            try
            {
                string mypbk = RasPhoneBook.GetPhoneBookPath(RasPhoneBookType.AllUsers).ToString();
                RasConnection myconn = RasConnection.GetActiveConnectionByName("adsl", mypbk);
                myconn.HangUp();
                RasDialer dialer = new RasDialer();
               // dialer.DialCompleted += new EventHandler<DialCompletedEventArgs>(dialer_DialCompleted);
                dialer.DialCompleted += new EventHandler<DialCompletedEventArgs>(dialer_DialCompleted);
                dialer.EntryName = "adsl";
                dialer.PhoneNumber = "";
                dialer.AllowUseStoredCredentials = true;
                dialer.PhoneBookPath = RasPhoneBook.GetPhoneBookPath(RasPhoneBookType.AllUsers);
                dialer.Timeout = 5000;
                rasDone = false;
                RasHandle myras = dialer.Dial();
                while (myras.IsInvalid)
                {
                    System.Threading.Thread.Sleep(3000);
                    myras = dialer.Dial();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        static void dialer_DialCompleted(object sender, DialCompletedEventArgs e)
        {
            rasDone = true;
        }
        static bool IsCompleted()
        {
            return rasDone;
        }
    }

}
