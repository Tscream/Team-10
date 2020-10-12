using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class _AI : MonoBehaviour
{
    Animator aiAnim;
    GameObject player;
    private float loop_cooldown;
    private bool retreat;
    private bool followY;
    private bool follow_Y;
    public int maxHealth = 6;
    public int currentHealth;
    public Healthbar healthbar;
    
    // Start is called before the first frame update
    void Start()
    {
        aiAnim = gameObject.GetComponent<Animator>();
        player = GameObject.Find("Player");
        loop_cooldown = 2f;
        currentHealth = maxHealth;
        
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(player.transform.position, transform.position) <= 10 && Vector3.Distance(player.transform.position, transform.position) >= 2.01f) //&& aiAnim.GetBool("Idle") == true) 
        {
            Debug.Log("Walk");
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


            if (aiAnim.GetBool("Attack") == false && aiAnim.GetBool("Walk") == true) //de movement om naar de speler toe te komen
            {
                transform.Translate(-0.01f, 0, 0);
            }

        }

        if (Vector3.Distance(player.transform.position, transform.position) <= 2 && aiAnim.GetBool("Walk") == true) // als die eenmaal bij de speler is en wilt aanvallen.
        {
            Debug.Log("Attack");
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
            //TakeDamage(1);  //deze moet van de // af als de attack functie van de ai werkt en het testje hieronder moet dan juist // worden.
            Retreat();
        }

        if (Input.GetKeyDown(KeyCode.Space)) // testje voor de healthbar
        {
            TakeDamage(1);
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


    void TakeDamage(int damage) // dit is voor de healthbar van de speler. Dit staat in dit script zodat het makkelijk doorgevoerd kan worden naar de healthbar die boven staat. Er komen nog losse healthbars voor de AI.
    {
        currentHealth -= damage;
        healthbar.SetHealth(currentHealth);
    }

    private void FixedUpdate()
    {
        float y = transform.position.y; // dezelfde AI scaling als de speler heeft zodat die niet de enige is die groter en kleiner wordt.

        float scale = 2 + (1 / 4.9f) * -y;

        transform.localScale = new Vector3(scale, scale, scale);
    }

    

}
