using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SRV_Inventory
{
    public int ammo;
    public int srv_Biden;
    public int srv_Cowboy;
    public int srv_Fox;
    public int srv_FrenchFries;
    public int srv_MadScientist;
    public int srv_Pickle;
    public int srv_Pirate;
    public int srv_RomanCaeser;
    public int srv_RomanLegion;
    public int srv_SoldierBlue;
    public int srv_SoldierRed;
    public int srv_STPFemale;
    public int srv_STPMale;
    public int srv_SWAT;
    public int srv_Trump;
    public int BagIndex;


    public SRV_Inventory(int ammo, int srv_Biden, int srv_Cowboy, int srv_Fox, int srv_FrenchFries, int srv_MadScientist, int srv_Pickle, int srv_Pirate, int srv_RomanCaeser, int srv_RomanLegion, int srv_SoldierBlue, int srv_SoldierRed, int srv_STPFemale, int srv_STPMale, int srv_SWAT, int srv_Trump, int BagIndex)
    {
        this.ammo = ammo;
        this.srv_Biden = srv_Biden;
        this.srv_Cowboy = srv_Cowboy;
        this.srv_Fox = srv_Fox;
        this.srv_FrenchFries = srv_FrenchFries;
        this.srv_MadScientist = srv_MadScientist;
        this.srv_Pickle = srv_Pickle;
        this.srv_Pirate = srv_Pirate;
        this.srv_RomanCaeser = srv_RomanCaeser;
        this.srv_RomanLegion = srv_RomanLegion;
        this.srv_SoldierBlue = srv_SoldierBlue;
        this.srv_SoldierRed = srv_SoldierRed;
        this.srv_STPFemale = srv_STPFemale;
        this.srv_STPMale = srv_STPMale;
        this.srv_SWAT = srv_SWAT;
        this.srv_Trump = srv_Trump;
        this.BagIndex = BagIndex;
    }
}

public class SRV_Items : MonoBehaviour
{
    public int ammo;
    public int srv_Biden;
    public int srv_Cowboy;
    public int srv_Fox;
    public int srv_FrenchFries;
    public int srv_MadScientist;
    public int srv_Pickle;
    public int srv_Pirate;
    public int srv_RomanCaeser;
    public int srv_RomanLegion;
    public int srv_SoldierBlue;
    public int srv_SoldierRed;
    public int srv_STPFemale;
    public int srv_STPMale;
    public int srv_SWAT;
    public int srv_Trump;
    public int BagIndex;

    public SRV_Inventory ReturnClass()
    {
        return new SRV_Inventory(ammo, srv_Biden, srv_Cowboy, srv_Fox, srv_FrenchFries, srv_MadScientist, srv_Pickle, srv_Pirate, srv_RomanCaeser, srv_RomanLegion, srv_SoldierBlue, srv_SoldierRed, srv_STPFemale,  srv_STPMale,  srv_SWAT,  srv_Trump, BagIndex);
    }

    public void SetUI(SRV_Inventory inventory)
    {
        ammo = inventory.ammo;
        srv_Biden = inventory.srv_Biden;
        srv_Cowboy = inventory.srv_Cowboy;
        srv_Fox = inventory.srv_Fox;
        srv_FrenchFries = inventory.srv_FrenchFries;
        srv_MadScientist = inventory.srv_MadScientist;
        srv_Pickle = inventory.srv_Pickle;
        srv_Pirate = inventory.srv_Pirate;
        srv_RomanCaeser = inventory.srv_RomanCaeser;
        srv_RomanLegion = inventory.srv_RomanLegion;
        srv_SoldierBlue = inventory.srv_SoldierBlue;
        srv_SoldierRed = inventory.srv_SoldierRed;
        srv_STPFemale = inventory.srv_STPFemale;
        srv_STPMale = inventory.srv_STPMale;
        srv_SWAT = inventory.srv_SWAT;
        srv_Trump = inventory.srv_Trump;
        BagIndex = inventory.BagIndex;
    }
}
