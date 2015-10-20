using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace Utilities
{
    public class ProcessUtility
    {
         public static void KillProcess(string name)
        {
            string killCmm = "tasklist | findstr \""+name+"\"";
            string allListenning = execAndWait("cmd.exe", killCmm);
            string[] lines = allListenning.Split(new string[] { "\r\n" }, StringSplitOptions.None);

            for (int i = 1; i < lines.Length; i++)
            {
                if (lines[i].Contains(".exe"))
                {
                    string[] cols = lines[i].Split(new string[] { "\t", "      ", "     ", "    ", "   ", "  ", " " }, StringSplitOptions.None);

                    string pid = "";
                    int cnt = 0;
                    foreach (string str in cols)
                    {
                        if (str != "")
                        {
                            cnt++;
                            if (cnt == 2)
                            {
                                pid = str;
                            }
                        }
                    }
                    execAndWait("cmd.exe", "taskkill /f /pid " + pid);
                }
            }
        }

         /************************************************************************/
         /* 执行命令                                                             */
         /************************************************************************/
         public static string execAndWait(string exePath, string cmdLines)
         {

             Process process = new System.Diagnostics.Process();
             process.StartInfo.FileName = exePath;
             process.StartInfo.UseShellExecute = false;
             process.StartInfo.CreateNoWindow = true;
             process.StartInfo.RedirectStandardOutput = true;
             process.StartInfo.RedirectStandardInput = true;
             process.StartInfo.RedirectStandardError = true;
             process.Start();
             process.StandardInput.WriteLine(cmdLines);
             process.StandardInput.WriteLine("exit");
             process.StandardInput.AutoFlush = true;
             string output = process.StandardOutput.ReadToEnd();
             process.WaitForExit();
             process.Close();
             return output;
         }
    }
}
