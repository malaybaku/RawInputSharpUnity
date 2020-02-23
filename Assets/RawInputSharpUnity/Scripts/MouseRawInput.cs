using System;
using UnityEngine;
using Linearstar.Windows.RawInput;

namespace RawInputSharpUnity
{
    /// <summary>
    /// Read all mouse input event from RawInput.
    /// </summary>
    public class MouseRawInput : MonoBehaviour
    {
        [Tooltip("Set true if you want to read input during the application is in background")]
        [SerializeField] private bool readWhenBackground = true;
        
        /// <summary>
        /// Fire when receive raw input keyboard data.
        /// </summary>
        public event Action<RawInputMouseData> ReceiveRawInputMouseData;

        private void OnEnable()
        {
            RawInputWindowProcedure.Instance.ReceiveRawInput += OnReceiveRawInput;
#if UNITY_EDITOR
            Debug.Log("RawInputDevice.RegisterDevice was skipped in editor, readWhenBackground = " + readWhenBackground);
#else
            RawInputDevice.RegisterDevice(
                HidUsageAndPage.Mouse, 
                readWhenBackground 
                    ? RawInputDeviceFlags.InputSink
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
            RawInputDevice.UnregisterDevice(HidUsageAndPage.Mouse);
#endif
        }

        private void OnReceiveRawInput(RawInputEventData data)
        {
            if (RawInputData.FromHandle(data.LParam) is RawInputMouseData mouseData)
            {
                ReceiveRawInputMouseData?.Invoke(mouseData);
            }
        }
    }
}