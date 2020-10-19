using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    float horizontalInput;
    float verticalInput;
    Animator plAnim;
    private SpriteRenderer sr;

    void Start()
    {
        plAnim = gameObject.GetComponent<Animator>();
        sr = gameObject.GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        horizontalInput = Input.GetAxis("Horizontal") * speed;
        verticalInput = Input.GetAxis("Vertical") * speed;

        transform.Translate(horizontalInput * Time.deltaTime, verticalInput * Time.deltaTime, 0);
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -4.9f, -0.3f), transform.position.z);

        float y = transform.position.y;

        float scale = 2 + (1 / 4.9f) * -y;

        transform.localScale = new Vector3(scale, scale, scale);
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

        if (Input.GetKeyDown(KeyCode.R))
        {
            plAnim.SetBool("Idle", false);
            plAnim.SetBool("Defend", true);
        }
        if (Input.GetKeyUp(KeyCode.R))
        {
            plAnim.SetBool("Idle", true);
            plAnim.SetBool("Defend", false);
        }

        if(horizontalInput < 0)
        {
            sr.flipX = true;
        }

        if(horizontalInput > 0)
        {
            sr.flipX = false;
        }

       

    }
}
