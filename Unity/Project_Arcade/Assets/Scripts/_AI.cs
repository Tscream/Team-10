using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _AI : MonoBehaviour
{
    Animator aiAnim;
    GameObject player;
    private float loop_cooldown;
    private bool retreat;
    public bool followY;
    public bool follow_Y;
    
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
        if (Vector3.Distance(player.transform.position, transform.position) <= 10)  //&& aiAnim.GetBool("Idle") == true) 
        {
            aiAnim.SetBool("Idle", false);
            aiAnim.SetBool("Walk", true);
            //transform.position = (new Vector3(transform.position.x, otherTransform.position.y, transform.position.z));
            gameObject.GetComponent<SpriteRenderer>().flipX = true;

            if (transform.position.y < player.transform.position.y) // hiermee kan de ai met de speler op dezelfde Y as komen. 
            {
                followY = true;
                follow_Y = false;
            }
            else if (transform.position.y > player.transform.position.y)
            {
                follow_Y = true;
                followY = false;
            }


            if(followY == true) // de ai is lager dan de speler in het scherm
            {
                transform.Translate(0, 0.01f, 0);
            }
            else if (follow_Y == true) // de ai is hoger in dan de speler in het scherm
            {
                transform.Translate(0, -0.01f, 0);
            }


            if (aiAnim.GetBool("Attack") == false && aiAnim.GetBool("Walk") == true) 
            {
                transform.Translate(-0.01f, 0, 0);
            }

        }

        if (Vector3.Distance(player.transform.position, transform.position) <= 2 && aiAnim.GetBool("Walk") == true) // als die eenmaal bij de speler is en wilt aanvallen.
        {

            aiAnim.SetBool("Walk", false);
            aiAnim.SetBool("Attack", true);
        } 

        if (aiAnim.GetBool("Attack") == true && Input.GetKey(KeyCode.R)) // de speler verdedigd en zo komt de ai open voor een tegenaanval
        {
            
            aiAnim.SetBool("Attack", false);
            aiAnim.SetBool("Weak", true);
        } 

        if(aiAnim.GetBool("Attack") == true)
        {
            
            aiAnim.SetBool("Attack", false); // de ai raakt de speler met zijn aanval
            Retreat();
        }

        if(aiAnim.GetBool("Weak") == true)
        {
            Invoke("Retreat", 1f);
        }
   
        if(retreat == true)
        {
            transform.Translate(0.01f, 0, 0);
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

    private void FixedUpdate()
    {
        float y = transform.position.y;

        float scale = 2 + (1 / 4.9f) * -y;

        transform.localScale = new Vector3(scale, scale, scale);
    }

    

}
