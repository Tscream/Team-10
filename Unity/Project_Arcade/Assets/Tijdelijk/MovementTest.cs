using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementTest : MonoBehaviour
{
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float horizontalInput = Input.GetAxis("Horizontal") * speed;
        float verticalInput = Input.GetAxis("Vertical") * speed;

        transform.Translate(horizontalInput * Time.deltaTime, verticalInput * Time.deltaTime, 0);
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -4.67f, 0), transform.position.z);

        float y = transform.position.y -4;

        float scale = 2 + (1 / 4.67f) * y;

        transform.localScale = new Vector3(scale, scale, scale);

    }
}
