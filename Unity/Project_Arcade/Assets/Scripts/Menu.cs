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
    
     
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        
    }

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
        //this.gameObject.GetComponent<RectTransform>().localPosition = Vector2.MoveTowards(transform.position, new Vector2(0, -1200), 5f);
        begin = true;
    }


    
}
