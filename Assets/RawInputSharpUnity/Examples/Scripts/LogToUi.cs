using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RawInputSharpUnity.Example
{
    /// <summary>
    /// Output Log text to UI and Debug.Log
    /// </summary>
    public class LogToUi : MonoBehaviour
    {
        [SerializeField] private Text text = null;
        [SerializeField] private int maxLines = 20;
        
        private readonly List<string> _lines = new List<string>();

        private void Start()
        {
            AddLog("RawInput message will come here...");
        }

        public void AddLog(string line)
        {
            _lines.Add(line);
            if (_lines.Count > maxLines)
            {
                _lines.RemoveAt(0);
            }
            text.text = string.Join("\n", _lines);
        }
    }
}
