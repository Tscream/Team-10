﻿using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    float speed = 3;
    float horizontalInput;
    float verticalInput;
    public static float staminaFill = 1; // de float die de stamina bar regelt
    bool defend;
    public static bool doneAttack;
    RectTransform staminaBar; // te recttransform van de stamina bar
    public static Animator plAnim;
    SpriteRenderer sr;
    public static bool rechts;
    public static bool links;
    public static bool damage; //zelfde verhaal hier. hier is het de bedoeling dat door deze static bool de ai damage kan krijgen.



    void Start()
    {
        plAnim = gameObject.GetComponent<Animator>();
        sr = gameObject.GetComponent<SpriteRenderer>();
        staminaBar = GameObject.Find("Canvas/Bars/Staminabar/Fill").GetComponent<RectTransform>();
    }

    void FixedUpdate()
    {

        if (defend != true && Menu.begin == true)
        {
            horizontalInput = Input.GetAxis("Horizontal") * speed;
            verticalInput = Input.GetAxis("Vertical") * speed;

            transform.Translate(horizontalInput * Time.deltaTime, verticalInput * Time.deltaTime, 0);
            transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -4.9f, -0.3f), transform.position.z);

            float y = transform.position.y;

            float scale = 2 + (1 / 4.9f) * -y;

            transform.localScale = new Vector3(scale, scale, scale);
        }

       
    }

    void Update()
    {

        if (horizontalInput != 0 || verticalInput != 0)
        {
            plAnim.SetBool("Idle", false);
            plAnim.SetBool("Walk", true);
        }
        else
        {
            plAnim.SetBool("Idle", true);
            plAnim.SetBool("Walk", false);
        }

        if(horizontalInput < 0)
        {
            sr.flipX = true;
        }

        if(horizontalInput > 0)
        {
            sr.flipX = false;
        }

        if (Input.GetKey(KeyCode.R) && staminaFill > 0)
        {
            staminaFill -= 0.1f * Time.deltaTime; // als player defend gaat er iedere milisec 0.1 van zijn stamina af
        }
        else if (staminaFill < 1)
        {
            staminaFill += 0.05f * Time.deltaTime; // telt iedere miliseconde 0.05 op bij staminafill tot een max van 1
        }

        if (Input.GetKey(KeyCode.R) && staminaFill > 0.01f)
        {
            plAnim.SetBool("Idle", false);
            plAnim.SetBool("Defend", true);
            defend = true;
        }
        else
        {
            plAnim.SetBool("Idle", true);
            plAnim.SetBool("Defend", false);
            defend = false;
        }

        if (Input.GetKeyDown(KeyCode.Space) && staminaFill > 0.20f && doneAttack == false)
        {
            plAnim.SetBool("Idle", false);
            plAnim.SetBool("Defend", false);
            plAnim.SetBool("Attack", true);
            staminaFill -= 0.20f; //haalt 0.20 van de stamina fill af (1 is max) als hij aanvalt
            Invoke("Attack", 0.3f);
            doneAttack = true;
            damage = true;  // door de damage mogelijkheid heel kort (0.1f) mogelijk te maken kan damage niet gespamd worden zoals ik al in een vid heb laten zien.
        }

        staminaBar.localScale = new Vector3(staminaFill, 1, 1); // pakt de scale van de stamina bar en maakt daar de stamina float van
    }

    void Attack()
    {
        plAnim.SetBool("Attack", false);
        doneAttack = false;
        damage = false; // en gelijk na het damage doen word die weer false gemaakt.

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "CamR")
        {
            rechts = true;
        }
        

        if (collision.gameObject.tag == "CamL")
        {
            links = true;
        }
       
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "CamR")
        {
            rechts = false;
        }


        if (collision.gameObject.tag == "CamL")
        {
            links = false;
        }
    }
}
