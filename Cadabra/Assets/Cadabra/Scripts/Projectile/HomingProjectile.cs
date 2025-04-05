using UnityEngine;
using Cadabra.Core; // Make sure to import the namespace where PlayerBody exists

namespace Cadabra.Projectile
{
    public class HomingProjectile : GenericProjectile
    {
        public Transform target;
        public float rotationSpeed = 5f;
        
        public override void Start()
        {
            base.Start();
            
            // Find the player by looking for the PlayerBody component
            PlayerBody playerBody = FindObjectOfType<PlayerBody>();
            if (playerBody != null)
            {
                target = playerBody.transform;
            }
            else
            {
                Debug.LogWarning("HomingProjectile: Could not find PlayerBody component.");
            }
        }
        
        public override void FixedUpdate()
        {
            base.FixedUpdate();
            if (target != null)
            {
                // Calculate direction to target
                Vector3 directionToTarget = (target.position - transform.position).normalized;
                
                // Smoothly rotate toward the target
                Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
                
                // Move forward in the direction we're facing
                SetVelocity(speedCoefficient, transform.forward);
            }
        }
    }
}