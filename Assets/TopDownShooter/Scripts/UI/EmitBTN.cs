using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmitBTN : MonoBehaviour
{
    public Player player;
    public float currentPercent;
    public GameObject emitGFX;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currentPercent = player.ZombieRemovalValue;

        if(currentPercent <= 20)
        {
            emitGFX.SetActive(true);
        }else
        {
            emitGFX.SetActive(false);
        }
    }
}
