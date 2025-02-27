using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cadabra.Core
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;
        public static GameObject playerHolder;
        public PlayerBody playerbody;
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

            instancedPlayerHolder = GameObject.Instantiate(playerHolder, PlayerSpawnPoint.instance.transform);
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

