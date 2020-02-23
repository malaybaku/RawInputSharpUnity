using System;
using UnityEngine;
using Linearstar.Windows.RawInput;

namespace RawInputSharpUnity
{
    /// <summary>
    /// Read generic input event from RawInput.
    /// </summary>
    public class HidRawInput : MonoBehaviour
    {
        /// <summary>
        /// Device types to read.
        /// </summary>
        [Flags]
        private enum DeviceType
        {
            JoyStick,
            Pen,
            Gamepad,
            TouchPad,
            TouchScreen,
        }
        
        [Tooltip("Set true if you want to read input during the application is in background")]
        [SerializeField] private bool readWhenBackground = true;

        [Tooltip("Device to read.")] [SerializeField]
        private DeviceType[] devices = new[] { DeviceType.JoyStick }; 
        /// <summary>
        /// Fire when receive raw input keyboard data.
        /// </summary>
        public event Action<RawInputHidData> ReceiveRawInputHidData;
        
        private void OnEnable()
        {
#if UNITY_EDITOR
            Debug.Log("RawInputDevice.RegisterDevice was skipped in editor, readWhenBackground = " + readWhenBackground);
#else
            foreach (var device in devices)
            {
                RawInputDevice.RegisterDevice(
                    GetHidUsageAndPage(device),
                    readWhenBackground ? RawInputDeviceFlags.InputSink : RawInputDeviceFlags.None,  
                    WinApiUtils.GetUnityWindowHandle()
                );
            }
#endif
            RawInputWindowProcedure.Instance.ReceiveRawInput += OnReceiveRawInput;
        }

        private void OnDisable()
        {
            RawInputWindowProcedure.Instance.ReceiveRawInput -= OnReceiveRawInput;
#if UNITY_EDITOR
            Debug.Log("RawInputDevice.UnregisterDevice was skipped in editor");
#else
            RawInputDevice.UnregisterDevice(HidUsageAndPage.Mouse);
#endif
        }

        private HidUsageAndPage GetHidUsageAndPage(DeviceType deviceType)
        {
            switch (deviceType)
            {
                case DeviceType.JoyStick: return HidUsageAndPage.Joystick;
                case DeviceType.Pen : return HidUsageAndPage.Pen;
                case DeviceType.Gamepad : return HidUsageAndPage.GamePad;
                case DeviceType.TouchPad : return HidUsageAndPage.TouchPad;
                case DeviceType.TouchScreen : return HidUsageAndPage.TouchScreen;
                default: return HidUsageAndPage.Joystick;
            }
        }
        
        private void OnReceiveRawInput(RawInputEventData data)
        {
            if (RawInputData.FromHandle(data.LParam) is RawInputHidData keyboardData)
            {
                ReceiveRawInputHidData?.Invoke(keyboardData);
            }
        }
    }
}
