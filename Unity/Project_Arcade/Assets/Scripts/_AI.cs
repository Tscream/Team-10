using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class _AI : MonoBehaviour
{
    Animator aiAnim;
    GameObject player;
    GameObject hp;
    GameObject nametag;
    Vector3 oldPlayerPos;
    float speed = 3f;
    float cooldown;
    float healthFill = 0.6f;
    bool done_Attack;
    bool done_Retreat;
    bool retreat;

    void Start()
    {
        aiAnim = gameObject.GetComponent<Animator>();
        player = GameObject.Find("Player");
        hp = gameObject.transform.Find("Fill").gameObject;
        nametag = gameObject.transform.Find("Nametag").gameObject;
    }

    void FixedUpdate()
    {
        float y = transform.position.y;

        float scale = 2 + (1 / 4.9f) * -y;

        transform.localScale = new Vector3(scale, scale, scale);

        if(transform.position.x < player.transform.position.x)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
            hp.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            hp.transform.localPosition = new Vector3(0.3f, 1.2f, 0);
            nametag.GetComponent<RectTransform>().rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        }
        else
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            hp.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            hp.transform.localPosition = new Vector3(-0.3f, 1.2f, 0);
            nametag.GetComponent<RectTransform>().rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        }
    }

    void Update()
    {
        if (Menu.pauze == false && Menu.begin == true)
        {
            if (Vector3.Distance(player.transform.position, transform.position) <= 7 && Vector3.Distance(player.transform.position, transform.position) >= 3 && aiAnim.GetBool("Retreat") != true)
            {
                aiAnim.SetBool("Idle", false);
                aiAnim.SetBool("Walk", true);
                done_Retreat = false;
                gameObject.GetComponent<SpriteRenderer>().flipX = true;
            }

            if (aiAnim.GetBool("Walk") == true)
            {
                transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
            }

            if (Vector3.Distance(player.transform.position, transform.position) < 2 && aiAnim.GetBool("Retreat") == false && aiAnim.GetBool("Attack") == false)
            {
                aiAnim.SetBool("Walk", false);
                aiAnim.SetBool("Attack", true);
            }

            if (aiAnim.GetBool("Attack") == true && Input.GetKey(KeyCode.R) && PlayerMovement.staminaFill > 0.01f)
            {
                aiAnim.SetBool("Weak", true);
            }

            if (aiAnim.GetBool("Attack") == true && done_Attack == false && aiAnim.GetBool("Weak") == false)
            {
                aiAnim.SetBool("Attack", false);
                PlayerMovement.TakeDamage(1);
                oldPlayerPos = player.transform.position;
                Invoke("Retreat", 1f);
                done_Attack = true;
            }

            if (aiAnim.GetBool("Weak") == true && Input.GetKeyDown(KeyCode.Space) && PlayerMovement.staminaFill >= 0.20f)
            {
                healthFill -= 0.2f;
                Invoke("Retreat", 0.2f);
            }

            if (aiAnim.GetBool("Weak") == true)
            {
                Invoke("Retreat", 3f);
            }

            if (retreat == true)
            {
                if (Vector3.Distance(transform.position, oldPlayerPos) > 5)
                {
                    if (done_Retreat == false)
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

            hp.transform.localScale = new Vector3(healthFill, 0.05f, 1);

            if (transform.position.y > player.transform.position.y)
            {
                GetComponent<SpriteRenderer>().sortingOrder = -1;
            }
            else
            {
                GetComponent<SpriteRenderer>().sortingOrder = 1;
            }

            if(hp.transform.localScale.x <= 0.1f)
            {
                Destroy(this.gameObject);
            }


        }
    }

    void Retreat()
    {
        retreat = true;
        done_Attack = false;
        aiAnim.SetBool("Retreat", true);
        aiAnim.SetBool("Weak", false);
        aiAnim.SetBool("Walk", false);
        aiAnim.SetBool("Attack", false);
    }

}
