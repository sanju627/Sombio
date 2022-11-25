using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bed : MonoBehaviour
{
    public GameObject[] restingSRV;
    public bool isFull;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < restingSRV.Length; i++)
        {
            restingSRV[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetSRV(int index)
    {
        for (int i = 0; i < restingSRV.Length; i++)
        {
            restingSRV[i].SetActive(false);
        }

        restingSRV[index].SetActive(true);
    }
}
