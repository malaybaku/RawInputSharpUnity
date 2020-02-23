using System;
using System.Runtime.InteropServices;
using UnityEngine;

namespace RawInputSharpUnity
{
    /// <summary>
    /// Provides window util functions.
    /// </summary>
    public static class WinApiUtils
    {
        private static IntPtr _currentWindowHandle = IntPtr.Zero;
        
        /// <summary>
        /// Get Unity Window Handle.
        /// </summary>
        /// <returns></returns>
        public static IntPtr GetUnityWindowHandle() 
            => _currentWindowHandle == IntPtr.Zero 
                ? _currentWindowHandle = FindWindow(null, Application.productName)
                : _currentWindowHandle;

        [DllImport("user32.dll")]
        private static extern IntPtr FindWindow(string className, string windowName);
    }
}
