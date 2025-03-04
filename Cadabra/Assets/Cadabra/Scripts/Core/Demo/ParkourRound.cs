using UnityEngine;

namespace Cadabra.Core
{
    public class ParkourRound
    {
        // Start time
        private float _startTime;
        // End time
        private float _endTime;
        
        // Timer started boolean
        private bool _timerStarted;
        
        public void StartTimer()
        {
            if(_timerStarted) return;
            _startTime = Time.time;
            _timerStarted = true;
        }
        
        public void EndTimer()
        {
            if (!_timerStarted) return;
            
            _endTime = Time.time;
            _timerStarted = false;
        }
        
        public float GetTime()
        {
            return _endTime - _startTime;
        }
    }
}