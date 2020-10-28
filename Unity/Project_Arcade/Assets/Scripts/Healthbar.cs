using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Healthbar : MonoBehaviour
{
    //voor de healthbar
    public Slider sliderhealth;
    public Gradient gradient;
    public Image fill;



    private void Update()
    {
        if(sliderhealth.value == 0)
        {
            Debug.Log("No health");
            SceneManager.LoadScene("Luuk_Test"); //dit moet veranderen naar wat we gaan doen voor de death scene, maar laat het hier staan want dit werkt. 
        }
    }

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
