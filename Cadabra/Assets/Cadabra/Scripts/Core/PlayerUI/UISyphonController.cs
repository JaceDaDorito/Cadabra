using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISyphonController : MonoBehaviour
{
    [SerializeField]
    private RectTransform _syphonForeground;

    private float maxPosX, maxPosY, maxPosZ, maxScaleX, maxScaleY, maxScaleZ;

    // Start is called before the first frame update
    void Start()
    {
        maxPosX = -85;
        maxPosY = -150;
        maxPosZ = 2;
        maxScaleX = 70;
        maxScaleY = 70;
        maxScaleZ = 1;
    }

    public void UpdateSyphonCooldown(float timeRemaining, float maxCooldown)
    {
        _syphonForeground.localScale = new Vector3(maxScaleX, maxScaleY*((maxCooldown-timeRemaining)/maxCooldown), maxScaleZ);
        _syphonForeground.localPosition = new Vector3(maxPosX, maxPosY - (0.5f*(maxScaleY*(timeRemaining/maxCooldown))), maxPosZ);
    }
}
