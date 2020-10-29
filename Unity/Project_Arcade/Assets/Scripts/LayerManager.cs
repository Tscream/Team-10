using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerManager : MonoBehaviour
{

    public List<GameObject> childObjects;

    // Start is called before the first frame update
    void Start()
    {
        foreach(Transform child in transform)
        {
            childObjects.Add(child.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y > GameObject.Find("Player").transform.position.y)
        {
            GetComponent<SpriteRenderer>().sortingOrder = -1;
            foreach(GameObject child in childObjects)
            {
                child.GetComponent<SpriteRenderer>().sortingOrder = -2;
            }
        }
        else
        {
            GetComponent<SpriteRenderer>().sortingOrder = 1;
            foreach (GameObject child in childObjects)
            {
                child.GetComponent<SpriteRenderer>().sortingOrder = 0;
            }
        }
    }
}
