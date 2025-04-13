using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cadabra.Core
{
    public enum Team
    {
        None,
        Player,
        Enemy
    }
    [DisallowMultipleComponent]
    public class CharacterBody : MonoBehaviour
    {
        [SerializeField]
        public Team _team;
        [SerializeField]
        public CharacterMotor _characterMotor;
        [SerializeField]
        public HealthController _healthController;
        [SerializeField]
        public ManaController _manaController;
        [SerializeField]
        public WeaponStateMachine _weaponStateMachine;
        [SerializeField]
        public SyphonController _syphonController;
    }
}

