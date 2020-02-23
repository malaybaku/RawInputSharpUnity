using System;
using UnityEngine;
using Linearstar.Windows.RawInput;

namespace RawInputSharpUnity
{
    /// <summary>
    /// Read all keyboard input event from RawInput.
    /// </summary>
    public class KeyboardRawInput : MonoBehaviour
    {
        [Tooltip("Set true if you want to read input during the application is in background")]
        [SerializeField] private bool readWhenBackground = true;

        /// <summary>
        /// Fire when receive raw input keyboard data.
        /// </summary>
        public event Action<RawInputKeyboardData> ReceiveRawInputKeyboardData;
        
        private void OnEnable()
        {
            RawInputWindowProcedure.Instance.ReceiveRawInput += OnReceiveRawInput;
#if UNITY_EDITOR
            Debug.Log("RawInputDevice.RegisterDevice was skipped in editor, readWhenBackground = " + readWhenBackground);
#else
            RawInputDevice.RegisterDevice(
                HidUsageAndPage.Keyboard, 
                readWhenBackground 
                    ? RawInputDeviceFlags.InputSink | RawInputDeviceFlags.None
                    : RawInputDeviceFlags.None,
                WinApiUtils.GetUnityWindowHandle()
            );            
#endif
        }

        private void OnDisable()
        {
            RawInputWindowProcedure.Instance.ReceiveRawInput -= OnReceiveRawInput;
#if UNITY_EDITOR
            Debug.Log("RawInputDevice.UnregisterDevice was skipped in editor");
#else
            RawInputDevice.UnregisterDevice(HidUsageAndPage.Keyboard);
#endif
        }
        private void OnReceiveRawInput(RawInputEventData data)
        {
            if (RawInputData.FromHandle(data.LParam) is RawInputKeyboardData keyboardData)
            {
                ReceiveRawInputKeyboardData?.Invoke(keyboardData);
            }
        }
    }
}
