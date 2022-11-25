using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StorageBTN : MonoBehaviour
{
    public Storage storage;

    
    public bool kriss;
    public bool mp7;
    public bool mp5;
    public bool ump45;
    public bool tec9;
    public bool uzi;
    public bool ak12;
    public bool ak74;
    public bool g3a4;
    public bool g36c;
    public bool flamethrower;
    public bool glock17;
    public bool minigun;
    [Space]
    public bool ammo;
    public bool medkit;
    public bool bandage;
    public bool grenade;
    public bool molotov;
    public bool smoke;
    public bool landmine;
    public bool chicken;
    public bool wood;
    public bool stone;
    public bool wall;
    public bool metalWall;
    public bool woodDoor;
    public bool metalDoor;
    public bool fuel;
    public TMP_Text amountTXT;

    // Start is called before the first frame update
    void Start()
    {
        if(ammo)
        {
            amountTXT.text = storage.ammoAmount.ToString();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (ammo)
        {
            amountTXT.text = storage.ammoAmount.ToString();
        }

        if (medkit)
        {
            amountTXT.text = storage.medkitAmount.ToString();
        }

        if (bandage)
        {
            amountTXT.text = storage.bandageAmount.ToString();
        }

        if (grenade)
        {
            amountTXT.text = storage.grenadeAmount.ToString();
        }

        if (molotov)
        {
            amountTXT.text = storage.molotovAmount.ToString();
        }

        if (smoke)
        {
            amountTXT.text = storage.smokeAmount.ToString();
        }

        if (landmine)
        {
            amountTXT.text = storage.landmineAmount.ToString();
        }

        if (chicken)
        {
            amountTXT.text = storage.chickenAmount.ToString();
        }

        if (wood)
        {
            amountTXT.text = storage.woodAmount.ToString();
        }

        if (stone)
        {
            amountTXT.text = storage.stoneAmount.ToString();
        }

        if (wall)
        {
            amountTXT.text = storage.wallAmount.ToString();
        }

        if (metalWall)
        {
            amountTXT.text = storage.metalWall.ToString();
        }

        if (woodDoor)
        {
            amountTXT.text = storage.woodDoorAmount.ToString();
        }

        if (metalDoor)
        {
            amountTXT.text = storage.metalDoorAmount.ToString();
        }

        if (fuel)
        {
            amountTXT.text = storage.fuelAmount.ToString();
        }

        if (minigun)
        {
            amountTXT.text = storage.minigunAmount.ToString();
        }
    }
}
