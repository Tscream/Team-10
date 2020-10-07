using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _AI : MonoBehaviour
{
    Animator aiAnim;
    GameObject player;
    private float loop_cooldown;
    private bool retreat;

    // Start is called before the first frame update
    void Start()
    {
        aiAnim = gameObject.GetComponent<Animator>();
        player = GameObject.Find("Player");
        loop_cooldown = 2f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(player.transform.position, transform.position) <= 10 && aiAnim.GetBool("Idle") == true) 
        {
            aiAnim.SetBool("Idle", false);
            aiAnim.SetBool("Walk", true);
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }

        if (Vector3.Distance(player.transform.position, transform.position) <= 0.1f && aiAnim.GetBool("Walk") == true)
        {

            aiAnim.SetBool("Walk", false);
            aiAnim.SetBool("Attack", true);
        }

        if (aiAnim.GetBool("Attack") == true && Input.GetKey(KeyCode.R))
        {
            //defend
            aiAnim.SetBool("Attack", false);
            aiAnim.SetBool("Weak", true);
        } 

        if(aiAnim.GetBool("Attack") == true)
        {
            //hit
            aiAnim.SetBool("Attack", false);
            Retreat();
        }

        if(aiAnim.GetBool("Weak") == true)
        {
            Invoke("Retreat", 1f);
        }
   
        if(retreat == true)
        {
            transform.Translate(0.1f, 0, 0);
            if (Time.time >= loop_cooldown)
            {
                aiAnim.SetBool("Walk", true);
                retreat = false;
            }
        }
    }


    void Retreat()
    {
        //retreat
        aiAnim.SetBool("Weak", false);
        retreat = true;
       // aiAnim.SetBool("Walk", true);

        
    }

}
