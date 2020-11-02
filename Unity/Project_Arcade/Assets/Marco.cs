using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Marco : MonoBehaviour
{
    private GameObject spawner;
    private GameObject player;
    public GameObject marco;
    private bool rnd;
    private float Yas;
    public bool spawn;
    public int cooldown;
    
    

    
    // Start is called before the first frame update
    void Start()
    {
        spawner = GameObject.Find("Spawner");
        player = GameObject.Find("Player");
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(marco != null)
        {
            marco.transform.Translate(-1f, 0, 0);
        }
        
    }

    void Update()
    {
        if (player.transform.position.x >= -6)
        {
            spawn = true;
        }
        else if (player.transform.position.x < -6)
        {
            spawn = false;
        }

        if(spawn == true)
        {
            Cooldown();
        }

        if(cooldown < Time.time) 
        {
            Instantiate(marco, new Vector3(45, Yas, 0), Quaternion.identity);
        }

        Yas = player.transform.position.y;
    }

    void Cooldown()
    {
        if (rnd == false)
        {
            cooldown = Random.Range(5, 10);
            rnd = true;
        }
       
    }
}
