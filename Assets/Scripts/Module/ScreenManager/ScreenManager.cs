namespace Assets.Scripts.Module.ScreenManager
{
    using System;
    using System.Runtime.InteropServices;
    using Assets.Scripts.Base;
    using UnityEngine;

    /// <summary>
    /// 屏幕管理类
    /// </summary>
    internal class ScreenManager : Singleton<ScreenManager>
    {
        /// <summary>
        /// 窗口风格
        /// </summary>
        const int GWL_STYLE = -16;

        /// <summary>
        /// 标题栏
        /// </summary>
        const int WS_CAPTION = 0x00c00000;

        /// <summary>
        /// 标题栏关闭按钮
        /// </summary>
        const int WS_SYSMENU = 0x00080000;

        /// <summary>
        /// 标题栏最小化按钮
        /// </summary>  
        const int WS_MINIMIZEBOX = 0x00020000;

        /// <summary>
        /// 标题栏最大化按钮
        /// </summary>
        private const int WS_MAXIMIZEBOX = 0x00010000;

        /// <summary>
        /// The w m_ syscommand.
        /// </summary>
        private const int WM_SYSCOMMAND = 0x112;

        /// <summary>
        /// The sw p_ showwindow.
        /// </summary>
        private const uint SWP_SHOWWINDOW = 0x0040;

        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        public static extern bool ShowWindow(IntPtr hwd, int cmdShow);

        [DllImport("user32.dll")]
        public static extern long GetWindowLong(IntPtr hwd, int nIndex);

        [DllImport("user32.dll")]
        public static extern void SetWindowLong(IntPtr hwd, int nIndex, long dwNewLong);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr LoadIcon(IntPtr hInstance, IntPtr lpIconName);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll")]
        private static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

        [DllImport("user32.dll")]
        private static extern IntPtr GetActiveWindow();

        [DllImport("user32.dll")]
        private static extern bool ReleaseCapture();

        [DllImport("user32.dll")]
        private static extern bool SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

        /// <summary>
        /// The rect.
        /// </summary>
        [Serializable]
        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            /// <summary>
            /// The left.
            /// </summary>
            public int Left;

            /// <summary>
            /// The top.
            /// </summary>
            public int Top;

            /// <summary>
            /// The right.
            /// </summary>
            public int Right;

            /// <summary>
            /// The bottom.
            /// </summary>
            public int Bottom;
        }

        /// <summary>
        /// The window rect.
        /// </summary>
        private RECT windowRect;

        /// <summary>
        /// 初始化
        /// </summary>
        public void Init()
        {
            var hwd = GetForegroundWindow();

            //初始化屏幕大小
            //Screen.SetResolution(1366, 768, false);

            /*//显示标题栏
            var wl = GetWindowLong(hwd, GWL_STYLE);
            wl |= WS_CAPTION | WS_SYSMENU | WS_MINIMIZEBOX | WS_MAXIMIZEBOX;
            SetWindowLong(hwd, GWL_STYLE, wl);*/
        }

        /// <summary>
        /// 每帧更新
        /// </summary>
        public void Update()
        {
            // 获得窗口句柄
            var hwd = GetForegroundWindow();

            //按下空格+R隐藏标题栏
            if (Input.GetKey(KeyCode.Space) && Input.GetKey(KeyCode.H))
            {
                var wl = GetWindowLong(hwd, GWL_STYLE);
                wl &= ~WS_CAPTION;
                SetWindowLong(hwd, GWL_STYLE, wl);
            }

            //按下空格+T显示标题栏
            if (Input.GetKey(KeyCode.Space) && Input.GetKey(KeyCode.S))
            {
                var wl = GetWindowLong(hwd, GWL_STYLE);
                wl |= WS_CAPTION | WS_SYSMENU | WS_MINIMIZEBOX | WS_MAXIMIZEBOX;
                SetWindowLong(hwd, GWL_STYLE, wl);
            }

            //按下空格+X窗口化
            if (Input.GetKey(KeyCode.Space) && Input.GetKey(KeyCode.X))
            {
                Screen.SetResolution(1366, 768, false);
            }

            //按下空格+F满屏
            if (Input.GetKey(KeyCode.Space) && Input.GetKey(KeyCode.F))
            {
                Screen.SetResolution(1920, 1080, true);
            }

            //TODO 实现拖动鼠标改变窗口大小
            /*Vector3 mousePosition = Input.mousePosition;
            Rect windowRect = GetWindowRect();

            if (mousePosition.x >= windowRect.xMax - 5 && mousePosition.x <= windowRect.xMax &&
                mousePosition.y >= windowRect.yMin && mousePosition.y <= windowRect.yMax ||
                mousePosition.x >= windowRect.xMin && mousePosition.x <= windowRect.xMax &&
                mousePosition.y >= windowRect.yMax - 5 && mousePosition.y <= windowRect.yMax)
            {
                Cursor.SetCursor(Texture2D.whiteTexture, Vector2.zero, CursorMode.Auto);
            }
            else
            {
                Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
            }
                
            if (Input.GetMouseButton(0))
            {
                mousePosition = Input.mousePosition;
                windowRect = GetWindowRect();

                if (mousePosition.x >= windowRect.xMax - 5 && mousePosition.x <= windowRect.xMax &&
                    mousePosition.y >= windowRect.yMin && mousePosition.y <= windowRect.yMax)
                {
                    ResizeWindow(SC_SIZE + 2);
                }
                else if (mousePosition.x >= windowRect.xMin && mousePosition.x <= windowRect.xMax &&
                         mousePosition.y >= windowRect.yMax - 5 && mousePosition.y <= windowRect.yMax)
                {
                    ResizeWindow(SC_SIZE + 6);
                }
            }*/
        }

        /// <summary>
        /// The resize window.
        /// </summary>
        /// <param name="command">
        /// The command.
        /// </param>
        private void ResizeWindow(int command)
        {
            IntPtr windowHandle = GetActiveWindow();
            ReleaseCapture();
            SendMessage(windowHandle, WM_SYSCOMMAND, new IntPtr(command), IntPtr.Zero);
            SetWindowPos(windowHandle, 0, 0, 0, 0, 0, SWP_SHOWWINDOW);
        }

        /// <summary>
        /// The get window rect.
        /// </summary>
        /// <returns>
        /// The <see cref="Rect"/>.
        /// </returns>
        private Rect GetWindowRect()
        {
            IntPtr windowHandle = GetActiveWindow();
            RECT rect;
            GetWindowRect(windowHandle, out rect);
            return new Rect(rect.Left, rect.Top, rect.Right - rect.Left, rect.Bottom - rect.Top);
        }
    }
}
