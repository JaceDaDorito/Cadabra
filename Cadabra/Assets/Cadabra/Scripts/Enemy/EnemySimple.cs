using Cadabra.Core;
using Codice.Client.Common.GameUI;
using PlasticPipe.PlasticProtocol.Messages;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
namespace Cadabra.Enemies
{
    public class EnemySimple : MonoBehaviour
    {
        public Transform target;

        private Transform[] wayPoint;

        public int currentWayPointIndex = 0;

        public float spotRange;

        private EnemyReferences enemyRefrences;

        private float pathUpdateDeadline;

        private float shootingDistance;

        public bool isLost = true;

        public EnemyShoot enemyShoot;

        private float shootingDelay = 3f;
        private float shootingDeadline;
        private bool shootingState;

        public bool isOneArmLaser = false;
        public bool isTwoArmLaser = false;
        public bool isProjectile = false;
        public bool inRange;

        //public EnemyDeath enemyDeath;

        // For EnemyManager
        public delegate void EnemyKilled();
        public static event EnemyKilled OnEnemyKilled;

        private void Awake()
        {
            // Used to get target and waypoints
            target = GameManager.instance.playerBody.transform;
            GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Waypoints");
            wayPoint = new Transform[gameObjects.Length];
            for (int i = 0; i < gameObjects.Length; i++)
            {
                wayPoint[i] = gameObjects[i].transform;
            }
            enemyRefrences = this.GetComponent<EnemyReferences>();
            enemyShoot = this.GetComponent<EnemyShoot>();
        }

        // Start is called before the first frame update
        void Start()
        {
            shootingDistance = enemyRefrences.navMeshagent.stoppingDistance;
            isLost = true;
        }

        // Update is called once per frame
        void Update()
        {
            if (target != null)
            {
                enemyRefrences.animator.SetBool("isOneArmLaser", isOneArmLaser);
                enemyRefrences.animator.SetBool("isTwoArmLaser", isTwoArmLaser);
                enemyRefrences.animator.SetBool("isProjectile", isProjectile);

                float distance = Vector3.Distance(this.transform.position, target.position);

                inRange = Vector3.Distance(transform.position, target.position) <= shootingDistance;

                if (distance > spotRange && isLost == true)
                {
                    if (Time.time >= pathUpdateDeadline)
                    {
                        Patrolling();
                        enemyRefrences.animator.SetBool("shooting", false);
                    }
                }
                if (distance > spotRange && isLost == false)
                {
                    if (Time.time >= pathUpdateDeadline)
                    {
                        Searching();
                        enemyRefrences.animator.SetBool("shooting", false);
                    }
                }
                if (distance < spotRange)
                {
                    // Chasing
                    if (inRange)
                    {
                        LookAtTarget();
                        enemyRefrences.animator.SetBool("shooting", true);
                        isLost = false;
                    }
                    else
                    {
                        UpdatePath();
                        isLost = false;
                        // Toggles on and off shooting after shootingDelay
                        if (Time.time >= shootingDeadline)
                        {
                            shootingState = !shootingState;
                            enemyRefrences.animator.SetBool("shooting", shootingState);
                            shootingDeadline = Time.time + shootingDelay;
                        }
                    }

                }
            }
            // Get speed for animation
            enemyRefrences.animator.SetFloat("speed", enemyRefrences.navMeshagent.velocity.sqrMagnitude);
        }

        private void StartShooting()
        {
            enemyRefrences.animator.SetBool("shooting", true);
        }

        private void StopShooting()
        {
            enemyRefrences.animator.SetBool("shooting", false);
        }

        private void LookAtTarget()
        {
            Vector3 lookPos = target.position - transform.position;
            lookPos.y = 0;
            Quaternion rotation = Quaternion.LookRotation(lookPos);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 0.2f);
        }

        private void UpdatePath()
        {
            if (Time.time >= pathUpdateDeadline) // Delay for updating path
            {
                Debug.Log("Updating Path");
                pathUpdateDeadline = Time.time + enemyRefrences.pathUpdateDelay;
                enemyRefrences.navMeshagent.SetDestination(target.position);
            }
        }

        private void Searching()
        {
            float distanceToTarget = Vector3.Distance(enemyRefrences.navMeshagent.pathEndPosition, transform.position);
            if (distanceToTarget > enemyRefrences.navMeshagent.stoppingDistance)
            {
                enemyRefrences.navMeshagent.SetDestination(enemyRefrences.navMeshagent.pathEndPosition);
                isLost = false;
            }
            else
                isLost = true;
        }

        private void Patrolling()
        {
            if (wayPoint.Length == 0)
            {
                return;
            }

            float distanceToWayPoint = Vector3.Distance(wayPoint[currentWayPointIndex].position, transform.position);

            if (Time.time >= pathUpdateDeadline) // Delay for updating path
            {
                if (distanceToWayPoint <= enemyRefrences.navMeshagent.stoppingDistance)
                {
                    currentWayPointIndex = (currentWayPointIndex + 1) % wayPoint.Length;
                }
                enemyRefrences.navMeshagent.SetDestination(wayPoint[currentWayPointIndex].position);
            }
        }
        public void Die()
        {
            Debug.Log("SimpleDeath");
            if (gameObject != null)
            {
                Destroy(this.gameObject, 0.1f);
            }
            if (OnEnemyKilled != null)
            {
                OnEnemyKilled();
            }

        }
    }
}
