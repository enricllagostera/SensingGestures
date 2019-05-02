using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

namespace SensingGestures.Utils
{
    /// <summary>
    /// This UnityEvent-based component is helpful when setting up OSC and other wireless communication between a mobile and the host PC.
    /// </summary>
    public class DetectDeviceIPAddress : MonoBehaviour
    {
        [SerializeField] public StringEvent OnDetectedIPAddress;

        private void Start()
        {
            string IP = GetIP();
            if (!string.IsNullOrWhiteSpace(IP))
            {
                if (OnDetectedIPAddress != null)
                {
                    OnDetectedIPAddress.Invoke(IP);
                }
            }
        }

        private string GetIP()
        {
            var host = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new System.Exception("No network adapters with an IPv4 address in the system!");
        }
    }
}