﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam : MonoBehaviour
{

    public GameObject Camleft;
    public GameObject Camright;
    public GameObject player;
    public AudioSource gameplay;
    bool done;
    
    
    // Start is called before the first frame update
    void Start()
    {
      
    }


   

    // Update is called once per frame
    void FixedUpdate()
    {
       if(PlayerMovement.rechts == true && transform.position.x <= 30)
        {
            transform.Translate(0.06f, 0, 0);
        }

       if(PlayerMovement.links == true && transform.position.x >= -30.84)
        {
            transform.Translate(-0.06f, 0, 0);
        }
    }

    private void Update()
    {
        if(Menu.begin == true && done == false)
        {
            gameplay.Play(0);
            done = true;
        }
    }


}
