using Cadabra.Attacks;
using Cadabra.Projectile;
using Cadabra.Util;
using EnemyRef;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ChildLocator))]
public class EnemyShoot : MonoBehaviour
{
    private float time;

    // Laser Behavior:
    public float laserDamage = 1f;
    public float laserForce = 1f;
    public bool laserIgnoreTeam = false;
    public float laserMaxDistance = 500f;
    public GameObject tracer;

    private BulletAttack bulletAttack;

    public GameObject projectile;

    private Transform muzzleTransform;
    //private Transform muzzleTransformLeft;
    //private Transform muzzleTransformRight;

    private EnemyRefrences enemyRefrences;

    private void Awake()
    {
        ChildLocator cd = gameObject.GetComponent<ChildLocator>();
        enemyRefrences = this.GetComponent<EnemyRefrences>();
        muzzleTransform = cd.FindTransform("Muzzle").transform;

    }

    private void ShootLaserLeft()
    {
        bulletAttack = new BulletAttack();
        bulletAttack.damage = laserDamage;
        bulletAttack.force = laserForce;
        bulletAttack.ignoreTeam = laserIgnoreTeam;
        bulletAttack.maxDistance = laserMaxDistance;
        bulletAttack.critsOnWeakPoints = false;
        bulletAttack.tracerPrefab = tracer;
        bulletAttack.origin = muzzleTransform.position;
        bulletAttack.aimVec = muzzleTransform.forward + enemyRefrences.navMeshagent.velocity;
        bulletAttack.muzzle = muzzleTransform;
        bulletAttack.Fire();
    }

    private void ShootLaserRight()
    {
        bulletAttack = new BulletAttack();
        bulletAttack.damage = laserDamage;
        bulletAttack.force = laserForce;
        bulletAttack.ignoreTeam = laserIgnoreTeam;
        bulletAttack.maxDistance = laserMaxDistance;
        bulletAttack.critsOnWeakPoints = false;
        bulletAttack.tracerPrefab = tracer;
        bulletAttack.origin = muzzleTransform.position;
        bulletAttack.aimVec = muzzleTransform.forward + enemyRefrences.navMeshagent.velocity;
        bulletAttack.muzzle = muzzleTransform;
        bulletAttack.Fire();
    }

    private void ShootProjectile()
    {
        GameObject instance = GameObject.Instantiate(projectile, muzzleTransform.position, muzzleTransform.rotation);
        instance.GetComponent<GenericProjectile>().aimDir = muzzleTransform.forward + enemyRefrences.navMeshagent.velocity;
    }
}
