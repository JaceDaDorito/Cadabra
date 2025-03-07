using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace Cadabra.VFX
{
    public class TracerComponent : MonoBehaviour
    {
        [HideInInspector]
        public Vector3 startPosition;
        [HideInInspector]
        public Vector3 target;
        [HideInInspector]
        public Vector3 direction;
        [HideInInspector]
        public float totalDistance;

        public Transform head;
        public Transform tail;
        [Min(10f)] //I have to set a min or else itll take forever for the tracer to reach its destination. Not great for performance.
        public float speed;
        public float headToTailOffset;
        public List<LineRenderer> lineRenderers;
        public UnityEvent onTracerHit;

        private float distanceTraveled;
        private bool headHit;


        public void Start()
        {
            distanceTraveled = 0;
            Vector3 delta = target - startPosition;
            direction = delta / totalDistance;

            head.position = startPosition;
            tail.position = startPosition;
            headHit = false;
        }

        public void Update()
        {
            //If the tracer hits its endpoint, call Hit
            if (distanceTraveled >= totalDistance) { Hit(); return; }

            //Moves tracer head and tail
            distanceTraveled += speed * Time.deltaTime;
            if (head) head.position = startPosition + Mathf.Clamp(distanceTraveled, 0f, totalDistance) * direction; //head movement
            if (tail) tail.position = startPosition + Mathf.Clamp(distanceTraveled - headToTailOffset, 0f, totalDistance) * direction; //trail movement
        }

        public void Hit()
        {
            onTracerHit.Invoke();
            Destroy(gameObject);
        }
    }
}
