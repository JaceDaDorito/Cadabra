using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public Transform[] SpawnPoint;
    
    public GameObject EnemyPrefab;

    void Awake()
    {
        SpawnNewEnemy();
    }

    void OnEnable()
    {
        Debug.Log("Respawning");
        EnemySimple.OnEnemyKilled += SpawnNewEnemy;
    }

    void SpawnNewEnemy()
    {
        Instantiate(EnemyPrefab, SpawnPoint[0].transform.position, Quaternion.identity);
    }
}
