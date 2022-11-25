using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public GameObject[] items;

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(items[Random.Range(0, items.Length)], transform.position, transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
