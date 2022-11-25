using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_NPC_Container : MonoBehaviour
{
    public GameObject[] srvPanels;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        srvPanels = GetComponentsInChildren<GameObject>();
    }

    public void Reset()
    {
        for (int i = 0; i < srvPanels.Length; i++)
        {
            Destroy(srvPanels[i]);
        }

        
    }
}
