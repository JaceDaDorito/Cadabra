using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace GDL_Tutorial
{
    [DisallowMultipleComponent]
    public class EnemyRefrences : MonoBehaviour
    {
        [HideInInspector] public NavMeshAgent navMeshagent;
        [HideInInspector] public Animator animator;

        [Header("Stats")]

        public float pathUpdateDelay = 0.2f;

        private void Awake()
        {
            navMeshagent = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();
        }
    }
}
