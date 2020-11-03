using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Marco : MonoBehaviour
{

    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate(-0.15f, 0, 0);

        float y = transform.position.y;

        float scale = 2 + (1 / 4.9f) * -y;

        
        transform.localScale = new Vector3(scale, scale, scale);

        if (player.transform.position.y > transform.position.y)
        {
            GetComponent<SpriteRenderer>().sortingOrder = 1;
        }
        else
        {
            GetComponent<SpriteRenderer>().sortingOrder = -1;
        }

        if(transform.position.x <= -45)
        {
            Destroy(this.gameObject);
        }
    }
}
