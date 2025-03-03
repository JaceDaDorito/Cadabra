using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cadabra.Util;

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


        public delegate void ManagerStart();
        ManagerStart onManagerStart;

        private void Awake()
        {
            onManagerStart += InstantiatePlayerBody;
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
                playerBody.transform.position = PlayerSpawnPoint.instance.transform.position;
                playerBody.transform.rotation = PlayerSpawnPoint.instance.transform.rotation;
            }
            else
            {
                instancedPlayerHolder = GameObject.Instantiate(playerHolder, PlayerSpawnPoint.instance.transform);
                playerBody = instancedPlayerHolder.GetComponent<ChildLocator>().FindTransform(0).GetComponent<PlayerBody>();
                playerBody._healthController.bodyDeathBehavior.AddListener(PlayerDeathSequence);
            }
        }

        private void PlayerDeathSequence(CharacterBody body)
        {
            Debug.Log("oooo you died");
        }

        private void OnEnable()
        {
            if (instance)
            {
                Destroy(this);
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

