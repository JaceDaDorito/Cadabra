using Codice.Client.Common.GameUI;
using EnemyRef;
using PlasticPipe.PlasticProtocol.Messages;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySimple : MonoBehaviour
{
    public Transform target;

    public List<Transform> wayPoint;

    public int currentWayPointIndex = 0;

    public float spotRange;

    private EnemyRefrences enemyRefrences;

    private float pathUpdateDeadline;

    private float shootingDistance;

    private bool isLost = true;

    private void Awake()
    {
        enemyRefrences = GetComponent<EnemyRefrences>();
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
        if(target != null)
        {
            
            float distance = Vector3.Distance(this.transform.position, target.position);

            bool inRange = Vector3.Distance(transform.position, target.position) <= shootingDistance;

            if (distance > spotRange && isLost == true)
            {
                if (Time.time >= pathUpdateDeadline)
                    Patrolling();
            }
            if (distance > spotRange && isLost == false)
            {
                if (Time.time >= pathUpdateDeadline)
                    Searching();
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
            }
            //enemyRefrences.animator.SetBool("shooting", inRange);
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
        if(Time.time >= pathUpdateDeadline) // Delay for updating path
        {
            // Debug.Log("Updating Path");
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
        if(wayPoint.Count == 0)
        {
            return;
        }

        float distanceToWayPoint = Vector3.Distance(wayPoint[currentWayPointIndex].position, transform.position);

        if( Time.time >= pathUpdateDeadline) // Delay for updating path
        {
            if (distanceToWayPoint <= enemyRefrences.navMeshagent.stoppingDistance)
            {
                currentWayPointIndex = (currentWayPointIndex + 1) % wayPoint.Count;
            }
            enemyRefrences.navMeshagent.SetDestination(wayPoint[currentWayPointIndex].position);
        }
    }
}
