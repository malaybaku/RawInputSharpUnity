using System.Windows.Forms;
using UnityEngine;
using Linearstar.Windows.RawInput;

namespace RawInputSharpUnity.Example
{
    [RequireComponent(typeof(KeyboardRawInput))]
    public class KeyboardLogWriter : MonoBehaviour
    {
        [SerializeField] private LogToUi logToUi = null;

        private void Start()
        {
            GetComponent<KeyboardRawInput>().ReceiveRawInputKeyboardData += OnReceiveKeyboardData;
        }

        private void OnReceiveKeyboardData(RawInputKeyboardData data)
        {
            Keys key = (Keys) data.Keyboard.VirutalKey;
            logToUi.AddLog(
                $"Keyboard: {data.Device.ProductName}, keyCode={data.Keyboard.VirutalKey}, key={key}, flag={data.Keyboard.Flags}"
            );
        }
    }
}
