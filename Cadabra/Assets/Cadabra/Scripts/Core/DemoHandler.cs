using System;
using System.Collections;
using System.Text;
using UnityEngine;

namespace Cadabra.Core
{
    public class DemoHandler
    {
        
        private static ArrayList _times = new ArrayList();
        
        // Start time
        private static float _startTime;
        // End time
        private static float _endTime;
        
        private static bool _timerStarted;
        
        public static void StartTimer()
        {
            if(_timerStarted)
                return;
            _startTime = Time.time;
            _timerStarted = true;
        }
        
        public static void EndTimer()
        {
            _endTime = Time.time;
            _timerStarted = false;
            _times.Add(GetTime());
        }
        
        public static float GetTime()
        {
            return _endTime - _startTime;
        }

        public static void PrintAllTimes()
        {
            // Sort times in ascending order
            _times.Sort();
            // Print all times
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("All times:");
            foreach (float time in _times)
            {
                sb.AppendLine(time.ToString());
            }
            Debug.Log(sb.ToString());
        }
        
    }
}