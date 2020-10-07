using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc2 : MonoBehaviour
{
    public bool idle;
    public bool attack;
    public bool weak;
    public bool retreat;
    public GameObject player;
    private float cooldown;

    // Start is called before the first frame update
    void Start()
    {
        idle = true;
        cooldown = 2;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) <= 10)
        {
            idle = false;
            attack = true;
        }
        if(weak == true && Time.time >= cooldown)
        {
            cooldown = Time.time + 2f;
        }
        
    }
}
