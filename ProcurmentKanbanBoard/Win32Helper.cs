namespace ProcurmentKanbanBoard
{
    using System;
    using System.Runtime.InteropServices;

    internal class Win32Helper
    {
        public const int CBS_DROPDOWNLIST = 3;
        public const int CBS_OWNERDRAWFIXED = 0x10;
        public const int CBS_OWNERDRAWVARIABLE = 0x20;
        public const int EM_GETCUEBANNER = 0x1502;
        public const int EM_SETCUEBANNER = 0x1501;
        public const int HWND_BROADCAST = 0xffff;
        public const int LB_GETCOUNT = 0x18b;
        public const int LB_GETCURSEL = 0x188;
        public const int LB_SETCOUNT = 0x1a7;
        public const int LB_SETCURSEL = 390;
        public const int LBS_HASSTRINGS = 0x40;
        public const int LBS_NODATA = 0x2000;
        public const int LBS_OWNERDRAWFIXED = 0x10;
        public const int LBS_OWNERDRAWVARIABLE = 0x20;
        public const int LBS_SORT = 2;
        public const int WS_TABSTOP = 0x10000;
        public const int WS_VSCROLL = 0x200000;

        [DllImport("user32.dll", CharSet=CharSet.Auto)]
        public static extern int PostMessage(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);
        [DllImport("user32", CharSet=CharSet.Auto)]
        public static extern int RegisterWindowMessage([MarshalAs(UnmanagedType.LPWStr)] string message);
        [DllImport("user32.dll", CharSet=CharSet.Auto)]
        public static extern int SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);
        [DllImport("user32.dll", CharSet=CharSet.Auto)]
        public static extern int SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, [MarshalAs(UnmanagedType.LPWStr)] string lParam);
    }
}

