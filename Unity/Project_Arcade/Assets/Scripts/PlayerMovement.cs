using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    Animator plAnim;

    void Start()
    {
        plAnim = gameObject.GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        float horizontalInput = Input.GetAxis("Horizontal") * speed;
        float verticalInput = Input.GetAxis("Vertical") * speed;

        transform.Translate(horizontalInput * Time.deltaTime, verticalInput * Time.deltaTime, 0);
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -4.9f, -0.3f), transform.position.z);

        float y = transform.position.y;

        float scale = 2 + (1 / 4.9f) * -y;

        transform.localScale = new Vector3(scale, scale, scale);
    }

    void Update()
    {
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
    }
}
