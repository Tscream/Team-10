using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    float speed = 3;
    float horizontalInput;
    float verticalInput;
    public static float staminaFill = 1;
    bool defend;
    bool doneAttack;
    RectTransform staminaBar;
    Animator plAnim;
    SpriteRenderer sr;


    void Start()
    {
        plAnim = gameObject.GetComponent<Animator>();
        sr = gameObject.GetComponent<SpriteRenderer>();
        staminaBar = GameObject.Find("Canvas/Staminabar/Fill").GetComponent<RectTransform>();
    }

    void FixedUpdate()
    {
        if (defend != true)
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
            staminaFill -= 0.1f * Time.deltaTime;
        }
        else if (staminaFill < 1)
        {
            staminaFill += 0.05f * Time.deltaTime;
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
            staminaFill -= 0.20f;
            Invoke("Attack", 0.1f);
            doneAttack = true;
        }

        staminaBar.localScale = new Vector3(staminaFill, 1, 1);
    }

    void Attack()
    {
        plAnim.SetBool("Attack", false);
        doneAttack = false;

    }
}
