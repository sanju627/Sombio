using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SRV : MonoBehaviour
{
    [Header("SRV")]
    public bool srvBiden;
    public bool srvSoldierBlue;
    public bool srvCowboy;
    public bool srvRedFox;
    public bool srvFrenchFry;
    public bool srvMadScientist;
    public bool srvPickle;
    public bool srvPirate;
    public bool srvSoldierRed;
    public bool srvRomanCaeser;
    public bool srvRomanLegion;
    public bool srvSTPFemale;
    public bool srvSTPMale;
    public bool srvSWAT;
    public bool srvTrump;

    PlayfabManager database;    

    // Start is called before the first frame update
    void Start()
    {
        database = PlayfabManager.database;
    }

    // Update is called once per frame
    void Update()
    {
        if(srvBiden && database.srvBiden_Rest == 0)
        {
            gameObject.SetActive(false);
        }

        if (srvSoldierBlue && database.srvSoldierBlue_Rest == 0)
        {
            gameObject.SetActive(false);
        }

        if (srvCowboy && database.srvCowboy_Rest == 0)
        {
            gameObject.SetActive(false);
        }

        if (srvRedFox && database.srvRedFox_Rest == 0)
        {
            gameObject.SetActive(false);
        }

        if (srvFrenchFry && database.srvFrenchFry_Rest == 0)
        {
            gameObject.SetActive(false);
        }

        if (srvMadScientist && database.srvRest == 0)
        {
            gameObject.SetActive(false);
        }

        if (srvPickle && database.srvPickle_Rest == 0)
        {
            gameObject.SetActive(false);
        }

        if (srvPirate && database.srvPirate_Rest == 0)
        {
            gameObject.SetActive(false);
        }

        if (srvSoldierRed && database.srvSoldierRed_Rest == 0)
        {
            gameObject.SetActive(false);
        }

        if (srvRomanCaeser && database.srvRomanCaeser_Rest == 0)
        {
            gameObject.SetActive(false);
        }

        if (srvRomanLegion && database.srvRomanLegion_Rest == 0)
        {
            gameObject.SetActive(false);
        }

        if (srvSTPFemale && database.srvSTPFemale_Rest == 0)
        {
            gameObject.SetActive(false);
        }

        if (srvSTPMale && database.srvSTPMale_Rest == 0)
        {
            gameObject.SetActive(false);
        }

        if (srvSWAT && database.srvSWAT_Rest == 0)
        {
            gameObject.SetActive(false);
        }

        if (srvTrump && database.srvTrump_Rest == 0)
        {
            gameObject.SetActive(false);
        }
    }
}
