using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    //voor de healthbar
    public Slider sliderhealth;
    public Gradient gradient;
    public Image fill;
   


    public void SetMaxHealth(int health)
    {
        sliderhealth.maxValue = health;
        sliderhealth.value = health;

        fill.color = gradient.Evaluate(1f);

    }

    public void SetHealth(int health)
    {
        sliderhealth.value = health;
        fill.color = gradient.Evaluate(sliderhealth.normalizedValue);
    }


    
}
