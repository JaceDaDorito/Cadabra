using Cadabra.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Cadabra.Enemies
{
    [DisallowMultipleComponent]
    public class EnemyReferences : MonoBehaviour
    {
        [HideInInspector] public NavMeshAgent navMeshagent;
        [HideInInspector] public Animator animator;
        public CharacterBody characterBody;

        [Header("Stats")]

        public float pathUpdateDelay = 0.2f;

        private void Awake()
        {
            navMeshagent = this.GetComponent<NavMeshAgent>();
            animator = this.GetComponent<Animator>();
            characterBody = this.GetComponent<CharacterBody>();
        }
    }
}
