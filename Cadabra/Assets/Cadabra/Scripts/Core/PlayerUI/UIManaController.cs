using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManaController : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _manaText;
    [SerializeField]
    private RectTransform _manaForeground;

    private float maxPosX, maxPosY, maxPosZ, maxScaleX, maxScaleY, maxScaleZ;

    // Inherited
    private void Start()
    {
        maxPosX = _manaForeground.localPosition.x;
        maxPosY = _manaForeground.localPosition.y;
        maxPosZ = _manaForeground.localPosition.z;
        maxScaleX = _manaForeground.localScale.x;
        maxScaleY = _manaForeground.localScale.y;
        maxScaleZ = _manaForeground.localScale.z;
    }

    public void UpdateMana(float currentMana, float maxMana)
    {
        _manaText.text = "Mana: " + currentMana + "/" + maxMana;
        _manaForeground.localScale = new Vector3(maxScaleX, maxScaleY*(currentMana/maxMana), maxScaleZ);
        _manaForeground.localPosition = new Vector3(maxPosX, maxPosY - (0.5f*(maxScaleY*((maxMana-currentMana)/maxMana))), maxScaleZ);
    }
}
