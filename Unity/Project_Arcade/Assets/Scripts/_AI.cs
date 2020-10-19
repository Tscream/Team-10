using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class _AI : MonoBehaviour
{
    Animator aiAnim;
    GameObject player;
    private float cooldown;
    private bool done_Attack;
    private bool done_Retreat;
    private bool retreat;
    public int maxHealth = 100;
    public int currentHealth;
    public Healthbar healthbar;
    float speed = 3f;
    
    void Start()
    {
        aiAnim = gameObject.GetComponent<Animator>();
        player = GameObject.Find("Player");
        currentHealth = maxHealth;
    }

    void FixedUpdate()
    {
        float y = transform.position.y;

        float scale = 2 + (1 / 4.9f) * -y;

        transform.localScale = new Vector3(scale, scale, scale);
    }

    void Update()
    {
        if (Vector3.Distance(player.transform.position, transform.position) <= 10 && Vector3.Distance(player.transform.position, transform.position) >= 3 && aiAnim.GetBool("Retreat") != true) 
        {
            aiAnim.SetBool("Idle", false);
            aiAnim.SetBool("Walk", true);
            done_Retreat = false;
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }

        if (aiAnim.GetBool("Walk") == true ) 
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        }

        if (Vector3.Distance(player.transform.position, transform.position) < 2 && aiAnim.GetBool("Retreat") == false)
        {
            Debug.Log("stop walk");
            aiAnim.SetBool("Walk", false);
            aiAnim.SetBool("Attack", true);
        } 

        if (aiAnim.GetBool("Attack") == true && Input.GetKey(KeyCode.R)) 
        {
            Debug.Log("weak");
            aiAnim.SetBool("Attack", false);
            aiAnim.SetBool("Weak", true);
        } 

        if(aiAnim.GetBool("Attack") == true && done_Attack == false)
        {
            aiAnim.SetBool("Attack", false); 
            TakeDamage(1);
            Invoke("Retreat", 1f);
            done_Attack = true;
        }

        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            TakeDamage(1);
        }

        if(aiAnim.GetBool("Weak") == true)
        {
            Invoke("Retreat", 3f);
        }

        if (retreat == true)
        {
            if (Vector3.Distance(transform.position, player.transform.position) > 8)
            {

                if(done_Retreat == false)
                {
                    cooldown = Time.time + 1f;
                    done_Retreat = true;
                    aiAnim.SetBool("Idle", true);
                }


                if (Time.time >= cooldown)
                {
                    retreat = false;
                    aiAnim.SetBool("Retreat", false);
                }

            }
            else
            {
                transform.Translate(speed * Time.deltaTime, 0, 0);
                gameObject.GetComponent<SpriteRenderer>().flipX = false;
            }
        }
    }

    void Retreat()
    {
        retreat = true;
        done_Attack = false;
        aiAnim.SetBool("Retreat", true);
        aiAnim.SetBool("Weak", false);
        aiAnim.SetBool("Attack", false);
        aiAnim.SetBool("Walk", false);
        aiAnim.SetBool("Idle", false);
    }

    void TakeDamage(int damage) 
    {
        currentHealth -= damage;
        healthbar.SetHealth(currentHealth);
    }
}
