using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject login;
    public GameObject play;
    public GameObject control;
    public GameObject button;
    public GameObject trein_vertrekt;
    public GameObject button_but_red;
    public GameObject start_menu;
    public GameObject pauzescherm;
    public GameObject resumeknop;
    public GameObject main_menuknop;
    static public bool begin;
    public static bool pauze;




    void Start()
    {
        
    }

    private void FixedUpdate()
    {
        if (begin == true)
        {
            start_menu.transform.Translate(0, -5, 0);
        }

        if (transform.position.y <= -500)
        {
            Destroy(this.gameObject);
        }
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && pauze == false && begin == true)
        {
            pauze = true;
            pauzescherm.SetActive(true);
            resumeknop.SetActive(true);
            main_menuknop.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && pauze == true && begin == true)
        {
            pauze = false;
            pauzescherm.SetActive(false);
            resumeknop.SetActive(false);
            main_menuknop.SetActive(false);
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
        Invoke("Treinmelding", 3f);
    }



    void Treinmelding()
    {
        begin = true;
    }

    public void resume()
    {
        pauze = false;
        pauzescherm.SetActive(false);
        resumeknop.SetActive(false);
        main_menuknop.SetActive(false);
    }

    public void Main_menu()
    {
        SceneManager.LoadScene("Team_10");
        begin = false;
        pauze = false;
        pauzescherm.SetActive(false);
        resumeknop.SetActive(false);
        main_menuknop.SetActive(false);
    }

}
