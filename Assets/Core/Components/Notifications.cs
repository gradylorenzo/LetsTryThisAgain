using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Core.Notify
{
    public class Notifications : MonoBehaviour
    {
        public string[] colors;

        public Text notificationBox;
        public Text specsBox;
        private string notifications = "";
        private string specs;
        private string GetSystemInfo()
        {
            string cpu;
            string cpuSpeed;
            string gpu;
            string gpuDriver;
            string gpuMem;
            string os;
            string res;
            string messageToPlayer = "Main Menu is still being built out, not all functionality is ready. Load Game and New Game are working. Controls for ship are W and S to" +
                "move longitudinally, Q and E to move laterally, R and F OR Space and Left Shift to move vertically, and A and D to rotate. Control system is still being prototyped.";

            cpu = "CPU Type: " + SystemInfo.processorType + "\n";
            cpuSpeed = "CPU Speed: " + SystemInfo.processorFrequency.ToString() + " MHz\n";
            gpu = "GPU Vendor: " + SystemInfo.graphicsDeviceName + "\n";
            gpuDriver = "GPU API: " + SystemInfo.graphicsDeviceVersion + "\n";
            gpuMem = "GPU Memory: " + SystemInfo.graphicsMemorySize + " MB\n";
            os = "OS: " + SystemInfo.operatingSystem + "\n";
            res = "RESOLUTION: " + Screen.width + " x " + Screen.height + "\n\n";
            return "Created by Grady Lorenzo\nDiscord: Nyxton#6759\n" + cpu + cpuSpeed + gpu + gpuDriver + gpuMem + os + res + messageToPlayer;
        }

        public void Awake()
        {
            Notify.ENotifyLog += ENotifyLog;
            Notify.Success("Notify.Log Started!");

            specs = GetSystemInfo();
            specsBox.text = specs;
        }

        private void ENotifyLog(Notify.Intent intent, string text)
        {
            string prefix = "<color=" + colors[(int)intent] + ">";
            string main = text;

            string final = prefix + main + "</color>\n";
            notifications += final;

            string currentText = notifications;
            while (currentText.Length > 1000)
            {
                currentText = currentText.Substring(currentText.IndexOf("</color>") + 8);
            }
            notifications = currentText;
        }

        private void FixedUpdate()
        {
            notificationBox.text = notifications;
            specsBox.gameObject.SetActive(Notify.useDebugLog);
            notificationBox.gameObject.SetActive(Notify.useDebugLog);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Home))
            {
                Notify.ToggleDebug();
            }
        }
    }
}