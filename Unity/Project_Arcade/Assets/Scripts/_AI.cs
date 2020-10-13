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
    private bool done;
    private bool retreat;
    public int maxHealth = 4;
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
        if (Vector3.Distance(player.transform.position, transform.position) <= 10 && Vector3.Distance(player.transform.position, transform.position) >= 3 && retreat != true) //&& aiAnim.GetBool("Idle") == true) 
        {
            
            aiAnim.SetBool("Idle", false);
            aiAnim.SetBool("Walk", true);
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }


        if (aiAnim.GetBool("Walk") == true ) 
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, 3f * Time.deltaTime);
            Debug.Log("feest");
        }

        if (Vector3.Distance(player.transform.position, transform.position) < 2)// && aiAnim.GetBool("Walk") == true)
        {
            aiAnim.SetBool("Walk", false);
            aiAnim.SetBool("Attack", true);
        } 

        if (aiAnim.GetBool("Attack") == true && Input.GetKey(KeyCode.R)) 
        {
            
            aiAnim.SetBool("Attack", false);
            aiAnim.SetBool("Weak", true);
        } 

        if(aiAnim.GetBool("Attack") == true && done == false)
        {
            Debug.Log("Attack");
            aiAnim.SetBool("Attack", false); 
            TakeDamage(1);
            Invoke("Retreat", 1f);
            done = true;
        }

        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            TakeDamage(1);
        }

        if(aiAnim.GetBool("Weak") == true)
        {
            Invoke("Retreat", 1f);
        }

        if (retreat == true)
        {
            if(Vector3.Distance(transform.position, player.transform.position) > 8)
            {
                retreat = false;
                aiAnim.SetBool("Walk", true);
            }
            else
            {
                transform.Translate(0.1f, 0, 0);
                gameObject.GetComponent<SpriteRenderer>().flipX = false;
            }

        }

    }


    void Retreat()
    {
        Debug.Log("retreat");
        retreat = true;
        

        //done = false;

        //aiAnim.SetBool("Weak", false);
        // aiAnim.SetBool("Walk", true);



        
        

        
    }


    void TakeDamage(int damage) 
    {
        currentHealth -= damage;
        healthbar.SetHealth(currentHealth);
    }

    private void FixedUpdate()
    {
        float y = transform.position.y;

        float scale = 2 + (1 / 4.9f) * -y;

        transform.localScale = new Vector3(scale, scale, scale);
    }

    

}
