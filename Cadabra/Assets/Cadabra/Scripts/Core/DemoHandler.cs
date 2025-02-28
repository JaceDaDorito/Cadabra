using UnityEngine;

namespace Cadabra.Core
{
    public class DemoHandler
    {
        private static int _JumpCount = 1;
        
        public static void incrementJumpCount()
        {
            _JumpCount++;
            Debug.Log("Jump count is now: " + _JumpCount);
        }
        
        
        
        
    }
}