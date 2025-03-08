using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Cadabra.Core
{
    public class HurtBoxGroup : MonoBehaviour
    {
        public HurtBox[] hurtBoxes;
        public HealthController healthController;

        private void OnValidate()
        {
            foreach(HurtBox hb in hurtBoxes)
            {
                if (hb.healthController)
                {
                    Debug.LogWarningFormat("Cannot populate {0} into {1}. Do not use one HurtBox in multiple HurtBoxGroups.", new object[]
                    {
                        healthController,
                        hb
                    });
                    continue;
                }

                hb.healthController = healthController;
            }
        }

        public void ToggleHurtboxes(bool active)
        {
            foreach(HurtBox hb in hurtBoxes)
            {
                hb.gameObject.SetActive(active);
            }
        }
    }
}
