using System.Collections;
using System.Collections.Generic;
using Cadabra.Projectile;
using UnityEngine;

public class EnemyRainbowProjectile : GenericProjectile

{

    public float roatationSpeed = 360f;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up, roatationSpeed * Time.deltaTime);
        transform.Rotate(Vector3.right, roatationSpeed * Time.deltaTime);
    }
}
