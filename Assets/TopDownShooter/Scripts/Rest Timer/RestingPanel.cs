using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestingPanel : MonoBehaviour
{
    PlayfabManager database;

    public GameObject[] srvPanels;

    // Start is called before the first frame update
    void Start()
    {
        database = PlayfabManager.database;
    }

    // Update is called once per frame
    void Update()
    {
        database = PlayfabManager.database;

        if (database.srvBiden_Rest > 0)
        {
            srvPanels[0].SetActive(true);
        }else
        {
            srvPanels[0].SetActive(false);
        }

        if (database.srvSoldierBlue_Rest > 0)
        {
            srvPanels[1].SetActive(true);
        }else
        {
            srvPanels[1].SetActive(false);
        }

        if (database.srvCowboy_Rest > 0)
        {
            srvPanels[2].SetActive(true);
        }
        else
        {
            srvPanels[2].SetActive(false);
        }

        if (database.srvRedFox_Rest > 0)
        {
            srvPanels[3].SetActive(true);
        }
        else
        {
            srvPanels[3].SetActive(false);
        }

        if (database.srvFrenchFry_Rest > 0)
        {
            srvPanels[4].SetActive(true);
        }
        else
        {
            srvPanels[4].SetActive(false);
        }

        if (database.srvRest > 0)
        {
            srvPanels[5].SetActive(true);
        }
        else
        {
            srvPanels[5].SetActive(false);
        }

        if (database.srvPickle > 0)
        {
            srvPanels[6].SetActive(true);
        }
        else
        {
            srvPanels[6].SetActive(false);
        }

        if (database.srvPirate_Rest > 0)
        {
            srvPanels[7].SetActive(true);
        }
        else
        {
            srvPanels[7].SetActive(false);
        }

        if (database.srvSoldierRed_Rest > 0)
        {
            srvPanels[8].SetActive(true);
        }
        else
        {
            srvPanels[8].SetActive(false);
        }

        if (database.srvRomanCaeser_Rest > 0)
        {
            srvPanels[9].SetActive(true);
        }
        else
        {
            srvPanels[9].SetActive(false);
        }

        if (database.srvRomanLegion_Rest > 0)
        {
            srvPanels[10].SetActive(true);
        }
        else
        {
            srvPanels[10].SetActive(false);
        }

        if (database.srvSTPFemale_Rest > 0)
        {
            srvPanels[11].SetActive(true);
        }
        else
        {
            srvPanels[11].SetActive(false);
        }

        if (database.srvSTPMale_Rest > 0)
        {
            srvPanels[12].SetActive(true);
        }
        else
        {
            srvPanels[12].SetActive(false);
        }

        if (database.srvSWAT_Rest > 0)
        {
            srvPanels[13].SetActive(true);
        }
        else
        {
            srvPanels[13].SetActive(false);
        }

        if (database.srvTrump_Rest > 0)
        {
            srvPanels[14].SetActive(true);
        }
        else
        {
            srvPanels[14].SetActive(false);
        }
    }
}
