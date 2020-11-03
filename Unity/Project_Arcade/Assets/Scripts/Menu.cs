using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public GameObject login;
    public GameObject play;
    public GameObject quitButton;
    public GameObject control;
    public GameObject button;
    public GameObject trein_vertrekt;
    public GameObject button_but_red;
    public GameObject start_menu;
    public GameObject pauzescherm;
    public GameObject resumeknop;
    public GameObject main_menuknop;
    public Text tijdObject;
    static public bool begin;
    public static bool pauze;
    public static int tijd; 

    void Start()
    {
        pauze = true;
        begin = false;
        tijd = 10; // je hebt 120 seconde (2 minuten) de tijd
    }

    private void FixedUpdate()
    {
        if (begin == true && start_menu != null)
        {
            start_menu.transform.Translate(0, -5, 0);
        }

        if (start_menu != null && start_menu.transform.position.y <= -225)
        {
            Destroy(start_menu);
            pauze = false;
            StartCoroutine(Timer());
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
            StartCoroutine(Timer());
            pauzescherm.SetActive(false);
            resumeknop.SetActive(false);
            main_menuknop.SetActive(false);
        }

        string minutes = ((int)tijd / 60).ToString("f0").PadLeft(2,'0'); // format de string van een decimaal getal naar een die je op een klok ziet
        string seconds = (tijd % 60).ToString("f0").PadLeft(2, '0');

        tijdObject.text = minutes + ":" + seconds; //update te text met de tijd

    }

    public void Play()
    {
        control.SetActive(true);
        button.SetActive(true);
        login.SetActive(false);
        play.SetActive(false);
        quitButton.SetActive(false);
    }

    public void Button()
    {
        control.SetActive(false);
        button.SetActive(false);
        trein_vertrekt.SetActive(true);
        button_but_red.SetActive(true);
        Invoke("Treinmelding", 3f);
    }

    void Treinmelding()
    {
        begin = true;
    }

    public void resume()
    {
        pauze = false;
        StartCoroutine(Timer());
        pauzescherm.SetActive(false);
        resumeknop.SetActive(false);
        main_menuknop.SetActive(false);
    }

    public void Main_menu()
    {
        SceneManager.LoadScene("Team_10");
        begin = false;
        pauze = false;
        StartCoroutine(Timer());
        pauzescherm.SetActive(false);
        resumeknop.SetActive(false);
        main_menuknop.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }


    private IEnumerator Timer()
    {
        while (pauze == false && tijd > 0)
        {
            yield return new WaitForSeconds(1f); // zo lang als dat pauze false is haalt hij iedere seconde 1 van de tijd af
            tijd--;
        }

        while (pauze == false && tijd == 0)
        {
            tijdObject.gameObject.SetActive(false);
            yield return new WaitForSeconds(1f);
            tijdObject.gameObject.SetActive(true);
            yield return new WaitForSeconds(1f);
        }
    }
}
