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
    public bool godMode;

    private void Update()
    {
        if(sliderhealth.value == 0 && godMode == false)
        {
            SceneManager.LoadScene("Deadth"); //dit moet veranderen naar wat we gaan doen voor de death scene, maar laat het hier staan want dit werkt. 
        }
        if (Input.GetKey(KeyCode.G))
        {
            godMode = true;
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
