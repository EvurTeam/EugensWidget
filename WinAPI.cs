using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Drawing;

namespace EugensWidget
{
    public static class WinAPI
    {
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern IntPtr FindWindowEx(IntPtr parent, IntPtr nextChild, string clName, string title);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, StringBuilder lParam);

        public const UInt32 WM_CLOSE = 0x0010;
        public const UInt32 WM_GETTEXTLENGTH = 0x000E;
        public const UInt32 WM_GETTEXT = 0x000D;

        [DllImport("user32.dll")]
        public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        public const int SW_HIDE = 0;
        public const int SW_SHOW = 1;

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left;        // x position of upper-left corner
            public int Top;         // y position of upper-left corner
            public int Right;       // x position of lower-right corner
            public int Bottom;      // y position of lower-right corner
        }

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern int GetWindowTextLength(IntPtr hWnd);

        public static string GetWindowTextRaw(IntPtr hwnd)
        {
            // Allocate correct string length first
            int length = (int)SendMessage(hwnd, WM_GETTEXTLENGTH, IntPtr.Zero, IntPtr.Zero);
            StringBuilder sb = new StringBuilder(length + 1);
            SendMessage(hwnd, WM_GETTEXT, (IntPtr)sb.Capacity, sb);
            return sb.ToString();
        }

        public static IntPtr SetWindowLongPtr(IntPtr hWnd, int nIndex, IntPtr dwNewLong)
        {
            if (IntPtr.Size == 8)
                return SetWindowLongPtr64(hWnd, nIndex, dwNewLong);
            else
                return new IntPtr(SetWindowLong32(hWnd, nIndex, dwNewLong.ToInt32()));
        }

        [DllImport("user32.dll", EntryPoint = "SetWindowLong")]
        private static extern int SetWindowLong32(IntPtr hWnd, int nIndex, int dwNewLong);

        [DllImport("user32.dll", EntryPoint = "SetWindowLongPtr")]
        private static extern IntPtr SetWindowLongPtr64(IntPtr hWnd, int nIndex, IntPtr dwNewLong);

        [DllImport("user32.dll")]
        public static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        public static uint MF_BYPOSITION = 0x400;
        public static uint MF_REMOVE = 0x1000;
        public static int GWL_STYLE = -16;
        public static int WS_CHILD = 0x40000000; //child window
        public static int WS_BORDER = 0x00800000; //window with border
        public static int WS_DLGFRAME = 0x00400000; //window with double border but no title
        public static int WS_CAPTION = WS_BORDER | WS_DLGFRAME; //window with a title bar 
        public static int WS_SYSMENU = 0x00080000; //window menu  

        [DllImport("user32.dll")]
        public static extern void SetWindowText(IntPtr hWnd, string text);
    }
}
