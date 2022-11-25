using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuBuy : MonoBehaviour
{
    public int prize;
    public MenuImporter menuImporter;
    PlayfabManager database;
    [Space]
    public string databaseItem;
    public int value;
    public int SRVIndex;

    // Start is called before the first frame update
    void Start()
    {
        database = GameObject.FindGameObjectWithTag("Database").GetComponent<PlayfabManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BuyBTN()
    {
        if(prize <= menuImporter.money)
        {
            database.SendData(databaseItem, value.ToString());

            menuImporter.money -= prize;
            database.SendData("Coins", menuImporter.money.ToString());
        }
    }

    public void BuyStat()
    {
        if (prize <= menuImporter.money)
        {
            if (SRVIndex == 1)
            {
                database.srvRedFox += 1;
                database.SendStats();
            }

            if (SRVIndex == 2)
            {
                database.srvRomanCaeser += 1;
                database.SendStats();
            }

            if (SRVIndex == 3)
            {
                database.srvRomanLegion += 1;
                database.SendStats();
            }

            if (SRVIndex == 4)
            {
                database.srvBiden += 1;
                database.SendStats();
            }

            if (SRVIndex == 5)
            {
                database.srvTrump += 1;
                database.SendStats();
            }

            if (SRVIndex == 6)
            {
                database.srvSoldierBlue += 1;
                database.SendStats();
            }

            if (SRVIndex == 7)
            {
                database.srvSoldierRed += 1;
                database.SendStats();
            }

            if (SRVIndex == 8)
            {
                database.c_STPMale += 1;
                database.SendStats();
            }

            if (SRVIndex == 9)
            {
                database.srvSTPFemale += 1;
                database.SendStats();
            }

            if (SRVIndex == 10)
            {
                database.srvPickle += 1;
                database.SendStats();
            }

            if (SRVIndex == 11)
            {
                database.srvFrenchFry += 1;
                database.SendStats();
            }

            menuImporter.money -= prize;
            database.SendData("Coins", menuImporter.money.ToString());
        }
    }
}
