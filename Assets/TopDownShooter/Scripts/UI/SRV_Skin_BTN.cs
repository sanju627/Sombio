using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SRV_Skin_BTN : MonoBehaviour
{
    public Image iconIMG;
    public Sprite purchasedSprite;
    public Sprite normalSprite;
    [Space]
    public bool srv_Biden;
    public bool srv_Cowboy;
    public bool srv_Fox;
    public bool srv_FrenchFries;
    public bool srv_MadScientist;
    public bool srv_Pickle;
    public bool srv_Pirate;
    public bool srv_RomanCaeser;
    public bool srv_RomanLegion;
    public bool srv_SoldierBlue;
    public bool srv_SoldierRed;
    public bool srv_STPFemale;
    public bool srv_STPMale;
    public bool srv_SWAT;
    public bool srv_Trump;

    PlayfabManager database;

    // Start is called before the first frame update
    void Start()
    {
        iconIMG = GetComponent<Image>();
        database = GameObject.FindGameObjectWithTag("Database").GetComponent<PlayfabManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(srv_Biden)
        {
            if(database.srvs_items[0].srv_Biden > 0)
            {
                PurchasedChange();
            }
            else
            {
                NotPurchasedChange();
            }
        }

        if (srv_Cowboy)
        {
            if (database.srvs_items[0].srv_Cowboy > 0)
            {
                PurchasedChange();
            }
            else
            {
                NotPurchasedChange();
            }
        }

        if (srv_Fox)
        {
            if (database.srvs_items[0].srv_Fox > 0)
            {
                PurchasedChange();
            }
            else
            {
                NotPurchasedChange();
            }
        }

        if (srv_FrenchFries)
        {
            if (database.srvs_items[0].srv_FrenchFries > 0)
            {
                PurchasedChange();
            }
            else
            {
                NotPurchasedChange();
            }
        }

        if (srv_MadScientist)
        {
            if (database.srvs_items[0].srv_MadScientist > 0)
            {
                PurchasedChange();
            }
            else
            {
                NotPurchasedChange();
            }
        }

        if (srv_Pickle)
        {
            if (database.srvs_items[0].srv_Pickle > 0)
            {
                PurchasedChange();
            }
            else
            {
                NotPurchasedChange();
            }
        }


        if (srv_Pirate)
        {
            if (database.srvs_items[0].srv_Pirate > 0)
            {
                PurchasedChange();
            }
            else
            {
                NotPurchasedChange();
            }
        }

        if (srv_RomanCaeser)
        {
            if (database.srvs_items[0].srv_RomanCaeser > 0)
            {
                PurchasedChange();
            }
            else
            {
                NotPurchasedChange();
            }
        }

        if (srv_RomanLegion)
        {
            if (database.srvs_items[0].srv_RomanLegion > 0)
            {
                PurchasedChange();
            }
            else
            {
                NotPurchasedChange();
            }
        }

        if (srv_SoldierBlue)
        {
            if (database.srvs_items[0].srv_SoldierBlue > 0)
            {
                PurchasedChange();
            }
            else
            {
                NotPurchasedChange();
            }
        }

        if (srv_SoldierRed)
        {
            if (database.srvs_items[0].srv_SoldierRed > 0)
            {
                PurchasedChange();
            }
            else
            {
                NotPurchasedChange();
            }
        }

        if (srv_STPFemale)
        {
            if (database.srvs_items[0].srv_STPFemale > 0)
            {
                PurchasedChange();
            }
            else
            {
                NotPurchasedChange();
            }
        }

        if (srv_STPMale)
        {
            if (database.srvs_items[0].srv_STPMale > 0)
            {
                PurchasedChange();
            }
            else
            {
                NotPurchasedChange();
            }
        }

        if (srv_SWAT)
        {
            if (database.srvs_items[0].srv_SWAT > 0)
            {
                PurchasedChange();
            }
            else
            {
                NotPurchasedChange();
            }
        }

        if (srv_Trump)
        {
            if (database.srvs_items[0].srv_Trump > 0)
            {
                PurchasedChange();
            }
            else
            {
                NotPurchasedChange();
            }
        }

    }

    public void PurchasedChange()
    {
        iconIMG.sprite = purchasedSprite;
    }

    public void NotPurchasedChange()
    {
        iconIMG.sprite = normalSprite;
    }
}
