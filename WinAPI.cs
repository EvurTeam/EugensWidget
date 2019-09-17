using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace EugensWidget
{
    public static class WinAPI
    {
        [DllImport("useer32.dll")]
        public static extern IntPtr FindWindowEx(string clName, string title, IntPtr parent, IntPtr child);
    }
}
