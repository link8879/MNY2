//  Copyright 2020 Jonguk Kim
//
//  Licensed under the Apache License, Version 2.0 (the "License");
//  you may not use this file except in compliance with the License.
//  You may obtain a copy of the License at
//
//      http://www.apache.org/licenses/LICENSE-2.0
//
//   Unless required by applicable law or agreed to in writing, software
//   distributed under the License is distributed on an "AS IS" BASIS,
//   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//   See the License for the specific language governing permissions and
//   limitations under the License.

using reWZ;
using System;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;

namespace MNY2
{
    static class Program
    {
        public static WZVariant WzVariant = WZVariant.BMS;
        public static string ClientPath = Environment.CurrentDirectory;
        public static bool IsEncrypted = true;

        /// <summary>
        /// 해당 애플리케이션의 주 진입점입니다.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            if(args.Length > 0) Thread.CurrentThread.CurrentUICulture = new CultureInfo(args[0]);
            switch (Thread.CurrentThread.CurrentUICulture.Name)
            {
                case "ko-KR":
                case "ja-JP":
                    break;
                default:
                    Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
                    break;
            }
            var one = new Mutex(true, "a", out var createdNew);
            if (createdNew)
            {
                one.ReleaseMutex();
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                if (!Environment.Is64BitOperatingSystem)
                {
                    MessageBox.Show(Strings._32Bit, Strings.Inform);
                    Environment.Exit(1);
                }

                if (new Microsoft.VisualBasic.Devices.ComputerInfo().TotalPhysicalMemory / (1024 * 1024) <= 6 * 1024)
                {
                    MessageBox.Show(Strings.LowMemory, Strings.Inform);
                }

                Application.Run(new MainForm());
            }
            else
            {
                Environment.Exit(1);
            }
        }
    }
}
