using UnityEngine;
using Linearstar.Windows.RawInput;

namespace RawInputSharpUnity.Example
{
    /// <summary>
    /// Analyzer sample to parse RawInput data buffer.
    /// </summary>
    public class RawInputDataAnalyzerSample : MonoBehaviour
    {
        [SerializeField] private LogToUi logToUi = null;
        
        private void OnEnable() 
            => RawInputWindowProcedure.Instance.ReceiveRawInput += OnReceiveRawInput;

        private void OnDisable()
            => RawInputWindowProcedure.Instance.ReceiveRawInput -= OnReceiveRawInput;

        private void OnReceiveRawInput(RawInputEventData data)
        {
            var buf = RawInputData.GetBufferedData();
            for (int i = 0; i < buf.Length; i++)
            {
                logToUi.AddLog($"Buffer[{i}]: Type = {buf[i].Header.Type}");    
            }
        }
    }
}
