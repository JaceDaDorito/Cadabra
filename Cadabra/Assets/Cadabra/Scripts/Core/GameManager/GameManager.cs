using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cadabra.Util;
using UnityEngine.SceneManagement;
using Cadabra.Scripts.Core.Demo;
using Cadabra.ScriptableObjects;

namespace Cadabra.Core
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;
        public GameObject playerHolder;
        [HideInInspector]
        public PlayerBody playerBody;
        [HideInInspector]
        public GameObject instancedPlayerHolder;
        [HideInInspector]
        public CheckPoint currentCheckpoint;


        public delegate void ManagerStart();
        ManagerStart onManagerStart;

        public WeaponDef wdRifle;
        public WeaponDef wdRocketLauncher;

        private void Awake()
        {
            onManagerStart += InstantiatePlayerBody;

            wdRifle.IShootWandAssociation = new Rifle();
            wdRocketLauncher.IShootWandAssociation = new RocketLauncher();
        }

        private void Start()
        {
            if (onManagerStart != null) onManagerStart();
        }

        private void InstantiatePlayerBody()
        {
            if (playerHolder == null) return;

            if (playerBody)
            {
                playerBody.transform.position = currentCheckpoint.checkpointTransform.position;
                playerBody.transform.rotation = currentCheckpoint.checkpointTransform.rotation;
            }
            else
            {
                instancedPlayerHolder = GameObject.Instantiate(playerHolder, currentCheckpoint.checkpointTransform);
                playerBody = instancedPlayerHolder.GetComponent<ChildLocator>().FindTransform("Player").GetComponent<PlayerBody>();
                playerBody._healthController.bodyDeathBehavior.AddListener(PlayerDeathSequence);
                playerBody._team = Team.Player;
            }
        }

        private void PlayerDeathSequence(CharacterBody body)
        {
            SceneManager.LoadScene("DeathScene");
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }

        private void OnEnable()
        {
            if (instance)
            {
                Debug.LogError("Only one " + this.GetType() + " can exist at a time");
                return;
            }

            GameManager.instance = this;
        }

        private void OnDisable()
        {
            if (instance = this)
                GameManager.instance = null;
        }
    }
}

