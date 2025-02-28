using Codice.Client.Common.GameUI;
using EnemyRef;
using PlasticPipe.PlasticProtocol.Messages;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySimple : MonoBehaviour
{
    public Transform target;

    private EnemyRefrences enemyRefrences;

    private float pathUpdateDeadline;

    private float shootingDistance;

    private void Awake()
    {
        enemyRefrences = GetComponent<EnemyRefrences>();
    }

    // Start is called before the first frame update
    void Start()
    {
        shootingDistance = enemyRefrences.navMeshagent.stoppingDistance;
    }

    // Update is called once per frame
    void Update()
    {
        if(target != null)
        {
            bool inRange = Vector3.Distance(transform.position, target.position) <= shootingDistance;
            
            if(inRange)
            {
                LookAtTarget();
            }
            else
            {
                UpdatePath();
            }

            //enemyRefrences.animator.SetBool("shooting", inRange);
        }
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
        if(Time.time >= pathUpdateDeadline)
        {
            Debug.Log("Updating Path");
            pathUpdateDeadline = Time.time + enemyRefrences.pathUpdateDelay;
            enemyRefrences.navMeshagent.SetDestination(target.position);
        }
    }
}
