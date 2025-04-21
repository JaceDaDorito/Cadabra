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
        private float pathTimer;

        private float shootingDistance;

        public bool isLost = true;

        public EnemyShoot enemyShoot;

        private float shootingTimer;
        public float shootingDelay = 2f;
        private float animationTimer;
        private float animationDelay = 1f;

        public bool isOneArmLaser = false;
        public bool isTwoArmLaser = false;
        public bool isProjectile = false;
        public bool inRange;

        private bool firstDetect;

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
            pathTimer = 0;
            shootingTimer = 0;
            animationTimer = 0;
            firstDetect = true;
        }

        // Update is called once per frame
        void Update()
        {
            if (pathTimer > 0)
            {
                pathTimer -= Time.deltaTime;
            }
            if (shootingTimer > 0)
            {
                shootingTimer -= Time.deltaTime;
            }
            if (animationTimer > 0)
            {
                animationTimer -= Time.deltaTime;
            }
            if (target != null)
            {
                enemyRefrences.animator.SetBool("isOneArmLaser", isOneArmLaser);
                enemyRefrences.animator.SetBool("isTwoArmLaser", isTwoArmLaser);
                enemyRefrences.animator.SetBool("isProjectile", isProjectile);

                float distance = Vector3.Distance(this.transform.position, target.position);

                inRange = Vector3.Distance(transform.position, target.position) <= shootingDistance;

                if (distance > spotRange)
                {
                    if (isLost)
                    {
                        if (pathTimer <= 0)
                        {
                            Patrolling();
                            enemyRefrences.animator.SetBool("shooting", false);
                            pathTimer = enemyRefrences.pathUpdateDelay;
                        }
                    }
                    else
                    {
                        if (pathTimer <= 0)
                        {
                            Searching();
                            enemyRefrences.animator.SetBool("shooting", false);
                            pathTimer = enemyRefrences.pathUpdateDelay;
                        }
                    }
                }
                if (distance < spotRange)
                {
                    // Chasing
                    if (inRange)
                    {
                        LookAtTarget();
                        isLost = false;
                    }
                    else
                    {
                        UpdatePath();
                        isLost = false;
                    }
                    if (shootingTimer <= 0)
                    {
                        if (firstDetect)
                        {
                            enemyRefrences.animator.SetBool("shooting", true);
                            shootingTimer = 0.5f;
                            firstDetect = false;
                        }
                        else
                        {
                            enemyRefrences.animator.SetBool("shooting", true);
                            shootingTimer = shootingDelay;
                        }
                    }
                    if (animationTimer <= 0)
                    {
                        enemyRefrences.animator.SetBool("shooting", false);
                        animationTimer = shootingTimer + animationDelay;
                    }
                }
            }
            // Get speed for animation
            enemyRefrences.animator.SetFloat("speed", enemyRefrences.navMeshagent.velocity.sqrMagnitude);
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
            if (pathTimer <= 0) // Delay for updating path
            {
                Debug.Log("Updating Path");
                pathTimer = enemyRefrences.pathUpdateDelay;
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

            if (pathTimer <= 0) // Delay for updating path
            {
                if (distanceToWayPoint <= enemyRefrences.navMeshagent.stoppingDistance)
                {
                    currentWayPointIndex = (currentWayPointIndex + 1) % wayPoint.Length;
                }
                enemyRefrences.navMeshagent.SetDestination(wayPoint[currentWayPointIndex].position);
                pathTimer = enemyRefrences.pathUpdateDelay;
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
