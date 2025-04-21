using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cadabra.Core;
using Cadabra.Projectile;
using Cadabra.ScriptableObjects;
using UnityEngine;
using Cadabra.Attacks;

namespace Cadabra.Core
{
    public class Shotgun : IShootWand
    {
        private GameObject tracer;
        private GameObject tracerPower;
        private AudioSource sound;
        public WeaponDef weaponDef;

        static readonly float[] SPREAD_ANGLES = { 9f, 4.5f, 0f, -4.5f, -9f };
        static readonly float[] SPREAD_ANGLES_WIDE = { 30f, 15f, 0f, -15f, -30f };

        WeaponDef IShootWand.weaponDef
        {
            get
            {
                return weaponDef;
            }
            set
            {
                weaponDef = value;
            }
        }

        public void ShootBullets(WeaponStateMachine wsm, float[] angles, bool power)
        {
            Vector3 axisOfRotation = Vector3.Cross(wsm._cameraController.transform.forward, wsm._cameraController.transform.right);
            Vector3 muzzle = new Vector3(wsm._cameraController.transform.position.x, wsm._cameraController.transform.position.y - 0.8f, wsm._cameraController.transform.position.z);
            int index = -1;
            foreach (float angle in angles)
            {
                index++;
                BulletAttack bulletAttack = new BulletAttack();
                bulletAttack.damage = 7.5f;
                bulletAttack.critDamageMultiplier = 2f;
                bulletAttack.force = 0f;
                bulletAttack.ignoreTeam = false;
                bulletAttack.maxDistance = 15f;
                bulletAttack.critsOnWeakPoints = true;
                bulletAttack.tracerPrefab = power ? tracerPower : tracer;
                bulletAttack.origin = wsm._cameraController.transform.position;
                bulletAttack.aimVec = Quaternion.AngleAxis(angle, axisOfRotation) * wsm._cameraController.transform.forward;
                bulletAttack.overrideMuzzle = true;
                bulletAttack.muzzleOverride = new Vector3(wsm._cameraController.transform.position.x, wsm._cameraController.transform.position.y - 0.8f, wsm._cameraController.transform.position.z);
                bulletAttack.Fire();
            }

            if(sound) AudioSource.PlayClipAtPoint(sound.clip, muzzle, BulletAttack.soundVolume);
        }

        public void ShootPrimary(WeaponStateMachine wsm)
        {
            if (!tracer)
            {
                tracer = weaponDef.FindGameObject("Tracer");
                sound = tracer.GetComponent<AudioSource>();
            }

            ShootBullets(wsm, SPREAD_ANGLES, false);
        }

        public void ShootSecondary(WeaponStateMachine wsm)
        {
            if (!tracerPower)
            {
                tracerPower = weaponDef.FindGameObject("TracerPower");
                sound = tracer.GetComponent<AudioSource>();
            }

            ShootBullets(wsm, SPREAD_ANGLES_WIDE, true);

            if(!wsm.body._characterMotor._motor.GroundingStatus.IsStableOnGround)
                wsm.body._characterMotor.RequestImpulseForce(-wsm._cameraController.transform.forward * 26f);
        }
    }
}
