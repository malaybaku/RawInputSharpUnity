using UnityEngine;
using Linearstar.Windows.RawInput;

namespace RawInputSharpUnity.Example
{
    [RequireComponent(typeof(MouseRawInput))]
    public class MouseLogWriter : MonoBehaviour
    {
        [SerializeField] private LogToUi logToUi = null;

        private int _count = 0;

        private void Start()
        {
            GetComponent<MouseRawInput>().ReceiveRawInputMouseData += OnReceiveMouseData;
        }

        private void OnReceiveMouseData(RawInputMouseData data)
        {
            _count++;
            var mouse = data.Mouse;
            logToUi.AddLog(
                $"Mouse: {_count}, flags={mouse.Flags}, x={mouse.LastX}, y={mouse.LastY}, buttons={mouse.Buttons}"
                );
        }
    }
}
