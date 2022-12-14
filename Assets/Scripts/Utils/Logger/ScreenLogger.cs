using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utils.Logger {
    public class ScreenLogger : MonoBehaviour {
        string myLog;
        Queue myLogQueue = new Queue();
        int maxQueue = 20;

        void OnEnable() {
            Application.logMessageReceived += HandleLog;
        }

        void OnDisable() {
            Application.logMessageReceived -= HandleLog;
        }

        void HandleLog(string logString, string stackTrace, LogType type) {
            myLog = logString;
            string newString = "\n [" + type + "] : " + myLog;
            myLogQueue.Enqueue(newString);
            if (type == LogType.Exception) {
                newString = "\n" + stackTrace;
                myLogQueue.Enqueue(newString);
            }
            if(myLogQueue.Count > maxQueue)
                myLogQueue.Dequeue();

            myLog = string.Empty;
            foreach (string mylog in myLogQueue) {
                myLog += mylog;
            }
        }

        void OnGUI() {
            GUILayout.Label(myLog);
        }
    }
}