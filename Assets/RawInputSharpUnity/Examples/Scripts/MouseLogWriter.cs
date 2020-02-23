using UnityEngine;
using Linearstar.Windows.RawInput;
using Linearstar.Windows.RawInput.Native;

namespace RawInputSharpUnity.Example
{
    [RequireComponent(typeof(MouseRawInput))]
    public class MouseLogWriter : MonoBehaviour
    {
        [SerializeField] private LogToUi logToUi = null;

        private void Start()
        {
            GetComponent<MouseRawInput>().ReceiveRawInputMouseData += OnReceiveMouseData;
        }

        private void OnReceiveMouseData(RawInputMouseData data)
        {
            // var m = data.Mouse;
            // if (!m.Buttons.HasFlag(RawMouseButtonFlags.MiddleButtonDown))
            // {
            //     logToUi.AddLog(
            //         $"Mouse: {data.Device.ProductName}, flag={m.Flags}, buttons={m.Buttons}, X={m.LastX}, Y={m.LastY}"
            //     );
            // }
        }
    }
}
