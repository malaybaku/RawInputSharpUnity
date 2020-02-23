using System;
using System.Runtime.InteropServices;
using UnityEngine;

namespace RawInputSharpUnity
{
    /// <summary>
    /// Procedure Hook to read RawInput windows message.
    /// </summary>
    public class RawInputWindowProcedure
    {
        private RawInputWindowProcedure() { }
        private static RawInputWindowProcedure _instance;
        /// <summary>
        /// Get Singleton Instance.
        /// </summary>
        public static RawInputWindowProcedure Instance
            => _instance ?? (_instance = new RawInputWindowProcedure());

        private Action<RawInputEventData> _receiveRawInput = null;
        /// <summary>
        /// Fire when received RawInput message.
        /// </summary>
        public event Action<RawInputEventData> ReceiveRawInput
        {
            add
            {
                _receiveRawInput += value;
                if (_receiveRawInput != null)
                {
                    EnableWindowHook();
                }
            }
            remove
            {
                _receiveRawInput -= value;
                if (_receiveRawInput == null)
                {
                    DisableWindowHook();
                }
            }
        }

        private bool _isWindowHooked;
        private IntPtr _oldWndProcPtr;
        private IntPtr _newWndProcPtr;
        private WndProcDelegate _newWndProc;
        
        private void EnableWindowHook()
        {
            if (_isWindowHooked)
            {
                return;
            }
            
            // Hook window procedure
            _newWndProc = WndProc;
            _newWndProcPtr = Marshal.GetFunctionPointerForDelegate(_newWndProc);
#if UNITY_EDITOR
            Debug.Log("Do not start RawInputWindowProcedure, because function '" + nameof(SetWindowLongPtr) + "' might leads editor crash.");
            Debug.Log(
                $"{nameof(_newWndProcPtr)} remains to IntPtr.Zero, and param {nameof(GWLP_WNDPROC)} is not used.");
#else
            _oldWndProcPtr = SetWindowLongPtr(
                WinApiUtils.GetUnityWindowHandle(), GWLP_WNDPROC, _newWndProcPtr
                );
#endif
            _isWindowHooked = true;
        }

        /// <summary>
        /// Force to stop window hook. Normally you do not have to call it directly.
        /// </summary>
        public void DisableWindowHook()
        {
            // Avoid to assign invalid window procedure.
            if (_oldWndProcPtr == IntPtr.Zero)
            {
                return;
            }
            
            // Reset Window Procedure to original one.
#if !UNITY_EDITOR            
            SetWindowLongPtr(WinApiUtils.GetUnityWindowHandle(), GWLP_WNDPROC, _oldWndProcPtr);
#endif
            _oldWndProcPtr = IntPtr.Zero;
            _newWndProcPtr = IntPtr.Zero;
            _newWndProc = null;
            _isWindowHooked = false;
            _receiveRawInput = null;
        }
        
        private IntPtr WndProc(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam)
        {
            if (msg == WM_INPUT)
            {
                _receiveRawInput?.Invoke(new RawInputEventData(wParam, lParam));
            }
            return CallWindowProc(_oldWndProcPtr, hWnd, msg, wParam, lParam);
        }
     
        
        delegate IntPtr WndProcDelegate(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);
        
        private const uint WM_INPUT = 0x00FF;
        private const int GWLP_WNDPROC = -4;
        
        [DllImport("user32.dll")]
        private static extern IntPtr SetWindowLongPtr(IntPtr hWnd, int nIndex, IntPtr dwNewLong);

        [DllImport("user32.dll")]
        private static extern IntPtr CallWindowProc(IntPtr lpPrevWndFunc, IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);
   }

    public struct RawInputEventData
    {
        public RawInputEventData(IntPtr wParam, IntPtr lParam) : this()
        {
            WParam = wParam;
            LParam = lParam;
        }
        
        /// <summary>
        /// wParam data when receive RawInput message.
        /// </summary>
        public IntPtr WParam { get; }
        
        /// <summary>
        /// lParam data when receive RawInput message.
        /// </summary>
        public IntPtr LParam { get; }
    }
}
