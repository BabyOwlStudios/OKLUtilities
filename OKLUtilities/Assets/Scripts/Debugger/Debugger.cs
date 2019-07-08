using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Collections.Generic;

namespace Daikin
{
    public class Debugger : MonoBehaviour
    {
        public GameObject DebuggerGO;
        public GameObject ButtonsGO;

        public Toggle DebuggerToggle;


        public GameObject LogObjectPrefab;
        public Transform LogObjectsHolder;

        public List<GameObject> debugs;

        public TextMeshProUGUI Log, Warning, Error, Assert, Exception;



        private int errorCount;
        public int ErrorCount
        {
            get => errorCount;
            set
            {
                errorCount = value;
                Error.text = errorCount.ToString();
            }
        }

        private int warningCount;
        public int WarningCount
        {
            get => warningCount;
            set
            {
                warningCount = value;
                Warning.text = warningCount.ToString();
            }
        }

        private int logCount;
        public int LogCount
        {
            get => logCount;
            set
            {
                logCount = value;
                Log.text = logCount.ToString();
            }
        }

        private int assertCount;
        public int AssertCount
        {
            get => assertCount;
            set
            {
                assertCount = value;
                Assert.text = assertCount.ToString();
            }
        }

        private int exceptionCount;
        public int ExceptionCount
        {
            get => exceptionCount;
            set
            {
                exceptionCount = value;
                Exception.text = exceptionCount.ToString();
            }
        }


        [TextArea]
        public string TextHolder;

        public List<Sprite> sprites;

        public TextMeshProUGUI StackTraceText;

        public bool[] IsLogsEnabled = new bool[5];

        //public bool IsLogEnabled = true, IsWarningEnabled = true, IsErrorEnabled = true, IsAssertEnabled = true, IsExceptionEnabled = true;

        private void Start()
        {
            IsLogsEnabled = new bool[5];

            for (int i = 0; i < IsLogsEnabled.Length; i++)
            {
                IsLogsEnabled[i] = true;
            }

            debugs = new List<GameObject>();
            Application.logMessageReceived += HandleLog;
        }

        private void OnDestroy()
        {
            Application.logMessageReceived -= HandleLog;
        }


        public void DebuggerOnClick(bool state)
        {
            bool toggleState = DebuggerToggle.isOn;
            DebuggerGO.SetActive(toggleState);
            ButtonsGO.SetActive(toggleState);
        }

        public void ChangeFilterState(int type)
        {
            IsLogsEnabled[type] = IsLogsEnabled[type] == false ? true : false;
            FilterLogs();
        }

        private void FilterLogs()
        {
            if(debugs.Count > 0)
            {
                foreach (GameObject debug in debugs)
                {
                    for(int i=0; i<Enum.GetValues(typeof(LogType)).Length; i++)
                    {
                        if(debug.tag == ((LogType)i).ToString())
                        {
                            debug.SetActive(IsLogsEnabled[i]);
                            break;
                        }
                    }
                }
            }
        }

        private void HandleLog(string logString, string stackTrace, LogType type)
        {
            switch (type)
            {
                case LogType.Error:
                    ErrorCount += 1;
                    break;
                case LogType.Assert:
                    AssertCount += 1;
                    break;
                case LogType.Warning:
                    WarningCount += 1;
                    break;
                case LogType.Log:
                    LogCount += 1;
                    break;
                case LogType.Exception:
                    ExceptionCount += 1;
                    break;
            }

            GameObject obj = GameObject.Instantiate(LogObjectPrefab, LogObjectsHolder);
            LogObject myLog = obj.GetComponent<LogObject>();
            debugs.Add(obj);
            obj.tag = type.ToString();
            myLog.Setup(DateTime.Now.ToString("[HH:mm:ss]"), logString, stackTrace, sprites[(int)type], StackTraceText);
            FilterLogs();
        }

        public void ButtonClick(int index)
        {
            switch(index)
            {
                case 0:
                    Debug.LogError("Error!!");
                break;
                case 1:
                    Debug.Assert(DebuggerGO.activeSelf == false, "Assert - Debugger está inativo");
                break;
                case 2:
                    Debug.LogWarning("Warning!!");                                      
                break;
                case 3:
                    Debug.Log("Log!!");
                break;
                case 4:
                    GameObject obj = null;
                    obj.SetActive(false);
                break;
            }
        }

        public void ClearLogs()
        {
            if(debugs.Count > 0)
            {
                for (int i = 0; i < debugs.Count; i++)
                {
                    Destroy(debugs[i]);
                }

                LogCount = 0;
                ErrorCount = 0;
                WarningCount = 0;
                ExceptionCount = 0;
                AssertCount = 0;
                debugs.Clear();

                StackTraceText.text = string.Empty;
            }
        }
    }
}

