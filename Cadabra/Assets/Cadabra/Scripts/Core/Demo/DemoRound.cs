using System.Collections;
using UnityEngine;

namespace Cadabra.Core
{
    public class DemoRound
    {
        // Time
        private ArrayList _times = new ArrayList();
        private float _startTime = Time.time;
        
        private int _numJumps;
        private int _primaryShotsFired;
        private int _secondaryShotsFired;
        
        private float _damageTaken;
        private float _healthGained;
        
        private float _manaLost;
        
        public void IncrementJumps()
        {
            _numJumps++;
        }
        
        public void IncrementPrimaryShotsFired()
        {
            _primaryShotsFired++;
        }
        
        public void IncrementSecondaryShotsFired()
        {
            _secondaryShotsFired++;
        }
        
        public void IncrementDamageTaken(float amount)
        {
            _damageTaken += amount;
        }
        
        public void IncrementHealthGained(float amount)
        {
            _healthGained += amount;
        }
        
        public void IncrementManaLost(float amount)
        {
            _manaLost += amount;
        }
        
        
        public int GetNumJumps()
        {
            return _numJumps;
        }
        
        public int GetPrimaryShotsFired()
        {
            return _primaryShotsFired;
        }
        
        public int GetSecondaryShotsFired()
        {
            return _secondaryShotsFired;
        }
        
        public float GetDamageTaken()
        {
            return _damageTaken;
        }
        
        public float GetHealthGained()
        {
            return _healthGained;
        }
        
        public float GetManaLost()
        {
            return _manaLost;
        }
        
        public void AddTime(float time)
        {
            Debug.Log("DemoRound: Adding time: " + (time - _startTime));
            _times.Add(time - _startTime);
        }
        
        public ArrayList GetTimes()
        {
            return _times;
        }
    }
}