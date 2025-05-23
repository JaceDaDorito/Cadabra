﻿using System;
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
    public class Rifle : IShootWand
    {
        private GameObject tracer;
        private AudioSource sound;
        public WeaponDef weaponDef;

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

        public void ShootPrimary(WeaponStateMachine wsm)
        {
            if (!tracer)
            {
                tracer = weaponDef.FindGameObject("Tracer");
                sound = tracer.GetComponent<AudioSource>();
            }

            BulletAttack bulletAttack = new BulletAttack();
            bulletAttack.damage = 7.5f;
            bulletAttack.force = 0f;
            bulletAttack.ignoreTeam = false;
            bulletAttack.maxDistance = 50f;
            bulletAttack.critDamageMultiplier = 2f;
            bulletAttack.critsOnWeakPoints = true;
            bulletAttack.tracerPrefab = tracer;
            bulletAttack.origin = wsm._cameraController.transform.position;
            bulletAttack.aimVec = wsm._cameraController.transform.forward;
            bulletAttack.overrideMuzzle = true;
            bulletAttack.muzzleOverride = new Vector3(wsm._cameraController.transform.position.x, wsm._cameraController.transform.position.y - 0.8f, wsm._cameraController.transform.position.z);
            bulletAttack.Fire();
            AudioSource.PlayClipAtPoint(sound.clip, bulletAttack.muzzleOverride, BulletAttack.soundVolume);
        }

        public void ShootSecondary(WeaponStateMachine wsm)
        {
            return;
        }
    }
}
