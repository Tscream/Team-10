using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc : MonoBehaviour
{
    public bool attack;
    public bool defend;
    private float state;
    public bool nothing;
    public float stamina;
    [SerializeField]
    private Sprite hit;
    private SpriteRenderer sr;
    // Start is called before the first frame update
    void Start()
    {
        stamina = 10;
    }

    // Update is called once per frame
    void Update()
    {
        if(attack == true)
        {
            if(gameObject.tag == "Luc" && stamina >= 5)
            {
                sr.sprite = hit;
                stamina -= 5;
                transform.Translate(new Vector2(-1, 0));
            }
            else
            {
               state = Random.Range(0, 3);
            }
            
            
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
      ; state = Random.Range(0, 3);

        if(state == 0)
        {
            attack = true;
        }
        if (state == 1)
        {
            defend = true;
        }
        if (state == 2)
        {
            nothing = true;
        }
    }


    private void OnCollisionExit2D(Collision2D collision)
    {
        if (state == 0)
        {
            attack = false;
            
        }
        if (state == 1)
        {
            defend = false;
        }
        if (state == 2)
        {
            nothing = false ;
        }
    }
}
