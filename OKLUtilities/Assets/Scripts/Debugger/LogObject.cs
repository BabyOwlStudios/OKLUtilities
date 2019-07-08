using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Daikin
{
    public class LogObject : MonoBehaviour
    {
        private string logString;
        private string stackTrace;

        private Button button;
        public Image Icon;

        private TextMeshProUGUI stackTraceText;
        public TextMeshProUGUI HourText;
        public TextMeshProUGUI LogStringText;
        
        
    
        public void Setup(string hour, string logString, string stackTrace, Sprite spriteIcon, TextMeshProUGUI stackTraceText)
        {
            button = GetComponent<Button>();
            HourText.text = hour;
            this.stackTraceText = stackTraceText;
            this.logString = logString;
            this.stackTrace = stackTrace;

            Icon.sprite = spriteIcon;
            LogStringText.text = logString;

            button.onClick.AddListener(OnButtonClick);
        }

        private void OnButtonClick()
        {
            stackTraceText.text = stackTrace;
        }
    }
}
