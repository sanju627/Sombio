using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : MonoBehaviour
{
	public GameObject Roof;

    // Start is called before the first frame update
    void Start()
    {
        Roof.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Player")
    	Roof.SetActive(false);
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Player")
            Roof.SetActive(true);
    }
}
