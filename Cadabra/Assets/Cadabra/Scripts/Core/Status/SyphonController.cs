using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyphonController : MonoBehaviour
{

    public float syphonCooldownAmount;

    [HideInInspector]
    public float timer;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
    }

    public void Update()
    {
        if (timer > 0) {
            timer -= Time.deltaTime;
        }
    }

    public void UseSyphon()
    {
        if (timer <= 0) timer = syphonCooldownAmount;
    }

}
