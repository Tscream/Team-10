using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.EventSystems;

public class Menu : MonoBehaviour
{
    public GameObject login;
    public GameObject play;
    public GameObject control;
    public GameObject button;
    public GameObject trein_vertrekt;
    public GameObject button_but_red;
    static public bool begin;
    
    private void FixedUpdate()
    {
        if (begin == true)
        {
            transform.Translate(0, -5, 0);
        }

        if (transform.position.y <= -500)
        {
            Destroy(this.gameObject);
        }
    }

    public void Play()
    {
        control.SetActive(true);
        button.SetActive(true);
        login.SetActive(false);
        play.SetActive(false);
    }

    public void Button()
    {
        control.SetActive(false);
        button.SetActive(false);
        trein_vertrekt.SetActive(true);
        Invoke("treinmelding", 3f);
    }

    void treinmelding()
    {
        begin = true;
    }

}
