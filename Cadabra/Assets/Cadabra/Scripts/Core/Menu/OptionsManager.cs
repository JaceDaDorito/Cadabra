using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cadabra.Attacks;
using UnityEngine.UI;

public class OptionsManager : MonoBehaviour
{
    private static float sliderAmount = 0.7f;

    [SerializeField]
    private Slider slider;
    
    // Start is called before the first frame update
    void Start()
    {
        slider.value = sliderAmount;
        BulletAttack.soundVolume = sliderAmount;
    }

    // Update is called once per frame
    void Update()
    {
        sliderAmount = slider.value;
        BulletAttack.soundVolume = sliderAmount;
    }

    public void QuitButton()
    {
        Application.Quit();
    }
}
