using Cadabra.Attacks;
using Cadabra.Projectile;
using Cadabra.Util;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cadabra.Core;

namespace Cadabra.Enemies {
    [RequireComponent(typeof(ChildLocator))]
    public class EnemyShoot : MonoBehaviour
    {
        private float time;

        // Laser Behavior:
        public float laserDamage = 1f;
        public float laserForce = 1f;
        public bool laserIgnoreTeam = false;
        public float laserMaxDistance = 500f;
        public float predictionFactor = 0.5f;
        public GameObject tracer;

        private BulletAttack bulletAttack;

        public GameObject projectile;

        private Transform muzzleTransform;
        //private Transform muzzleTransformLeft;
        //private Transform muzzleTransformRight;

        private EnemyReferences enemyRefrences;
        private EnemySimple enemySimple;
        public PlayerBody target;
        public CharacterMotor targetMotor;
        public HurtBox targetCoreHurtBox;

        private void Awake()
        {
            ChildLocator cd = gameObject.GetComponent<ChildLocator>();
            enemyRefrences = this.GetComponent<EnemyReferences>();
            enemySimple = this.GetComponent<EnemySimple>();
            target = enemySimple.target.GetComponent<PlayerBody>();
            targetMotor = target._characterMotor;
            targetCoreHurtBox = target.GetComponent<HurtBoxGroup>().hurtBoxes[0];
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
            bulletAttack.aimVec = GetAimDirection();
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
            bulletAttack.aimVec = GetAimDirection();
            bulletAttack.muzzle = muzzleTransform;
            bulletAttack.Fire();
        }

        private void ShootProjectile()
        {
            GameObject instance = GameObject.Instantiate(projectile, muzzleTransform.position, muzzleTransform.rotation);

            GenericProjectile gpInstance = instance.GetComponent<GenericProjectile>();
            gpInstance.owner = enemyRefrences.characterBody;
            gpInstance.aimDir = GetAimDirection();
        }

        private Vector3 GetAimDirection()
        {
            if (target)
            {
                Vector3 predictedPos = targetMotor._motor.Velocity * predictionFactor + targetCoreHurtBox.transform.position;
                Vector3 predictedTrajectory = predictedPos - muzzleTransform.position;
                return predictedTrajectory;
            }

            return muzzleTransform.forward;
        }
    }
}