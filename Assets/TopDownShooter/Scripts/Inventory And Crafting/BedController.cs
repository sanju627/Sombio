using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BedController : MonoBehaviour
{
    public Bed[] beds;

    PlayfabManager database;
    DataImporter dataImporter;

    // Start is called before the first frame update
    void Start()
    {
        database = PlayfabManager.database;
        dataImporter = FindObjectOfType<DataImporter>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetBed()
    {
        if(database.srvBiden_Rest > 0)
        {
            fullFillSlots(0);
        }

        if (database.srvSoldierBlue_Rest > 0)
        {
            fullFillSlots(1);
        }

        if (database.srvCowboy_Rest > 0)
        {
            fullFillSlots(2);
        }

        if (database.srvRedFox_Rest > 0)
        {
            fullFillSlots(8);
        }
    }

    public void fullFillSlots(int index)
    {
        for (int i = 0; i < beds.Length; i++)
        {
            if(beds[i].isFull == false)
            {
                beds[i].isFull = true;
                beds[i].SetSRV(index);
                break;
            }
        }
    }

    public void LoadSRV()
    {
        dataImporter.LoadSRV();
    }
}
