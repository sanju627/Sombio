using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class shopItem : MonoBehaviour
{
    public bool purchased;
    public WeaponManger weaponManger;
    [Space]
	public Shop shop;
	public int Prize;
    [Space]
    public bool isVehicle;
    public bool isWeapon;
    public bool isSRV;
    public bool isDog;
    public bool isAkira;
    public bool isHusky;
    [Space]
    public bool fuel;

    [Header("Item")]
    public Item item;
    public int vehicleIndex;
    public int dogsIndex;
    public int SRVIndex;

    [Header("Vehicles Type")]
    public bool C_1940;
    public bool C_BubbleCar;
    public bool Hotrod;
    public bool IceCreamTruck;
    public bool MiniVan;
    public bool MonsterTruck;
    public bool MuscleTruck;
    public bool PickupTruck;
    public bool PoopTruck;
    public bool PorkTruck;
    public bool PrisonTruck;
    public bool WaterTruck;
    public bool WienerTruck;
    public bool BlackHawk;
    public bool Semi;
    public bool Airship;

    [Header("SRV")]
    public bool madScientist;
    public bool redFox;
    public bool romanCaeser;
    public bool romanLegion;
    public bool Biden;
    public bool Trump;
    public bool BlueSoldier;
    public bool RedSoldier;
    public bool STPMale;
    public bool STPFemale;
    public bool FrenchFries;
    public bool Pickle;
    public bool Cowboy;
    public bool Pirate;
    public bool SWAT;

    [Header("GFX")]
    public Image icon;
    public Button clickBTN;
    public Color disableColor;

    [Header("Melee")]
    public bool isMelee;
    public bool isAxe;
    public bool isBasebat;
    public bool isKatana;

    [Header("UI")]
    public TMP_Text PrizeText;

    
    PlayfabManager database;

    // Start is called before the first frame update
    void Start()
    {
        clickBTN = GetComponent<Button>();


        

        weaponManger = FindObjectOfType<WeaponManger>();

        Initialize();
        
    }

    // Update is called once per frame
    void Update()
    {
        database = PlayfabManager.database;
        Initialize();
    }

    public void itemClick()
    {
        if(!purchased)
        {
            shop.prize = Prize;

            if(isWeapon)
            {
                CancelBools();
                shop.isWeapon = true;
                shop.item = item;
            }

            if(isVehicle)
            {
                CancelBools();
                shop.isVehicle = true;

                //-------------------------Vehicle Types--------------------------//

                shop.vehicleIndex = vehicleIndex;

                if(C_1940)
                {
                    shop.C_1940 = true;
                }
                if(C_BubbleCar)
                {
                    shop.C_BubbleCar = true;
                }
                if (Hotrod)
                {
                    shop.Hotrod = true;
                }
                if (IceCreamTruck)
                {
                    shop.IceCreamTruck = true;
                }
                if (MiniVan)
                {
                    shop.MiniVan = true;
                }
                if (MonsterTruck)
                {
                    shop.MonsterTruck = true;
                }
                if (MuscleTruck)
                {
                    shop.MuscleTruck = true;
                }
                if (PickupTruck)
                {
                    shop.PickupTruck = true;
                }
                if (PoopTruck)
                {
                    shop.PoopTruck = true;
                }
                if (PorkTruck)
                {
                    shop.PorkTruck = true;
                }
                if (PrisonTruck)
                {
                    shop.PrisonTruck = true;
                }
                if (WaterTruck)
                {
                    shop.WaterTruck = true;
                }
                if (WienerTruck)
                {
                    shop.WienerTruck = true;
                }
                if(BlackHawk)
                {
                    shop.BlackHawk = true;
                }
                if (Semi)
                {
                    shop.Semi = true;
                }

                if (Airship)
                {
                    shop.Airship = true;
                }
            }

            if(isDog)
            {
                CancelBools();
                shop.isDog = true;
                shop.dogsIndex = dogsIndex;

                if (isHusky)
                {
                    shop.isHusky = true;
                }

                if (isAkira)
                {
                    shop.isAkira = true;
                }
            }
            

            if (isSRV)
            {
                CancelBools();
                shop.SRVIndex = SRVIndex;
                shop.isSRV = true;
            }

            if(fuel)
            {
                shop.isFuel = true;
            }

            if(isMelee)
            {
                shop.isMelee = true;

                if(isKatana)
                {
                    shop.isKatana = true;
                }

                if (isBasebat)
                {
                    shop.isBasebat = true;
                }
            }

        }
        else if (purchased)
        {
            if(isDog)
            {
                shop.dogsIndex = dogsIndex;
                shop.SpawnDog();
            }
            
            if(isVehicle)
            {
                shop.vehicleIndex = vehicleIndex;
                shop.Spawn();
            }

            if (isAxe)
            {
                weaponManger.SwitchMelee(0);
                PlayerPrefs.SetInt("Melee", 0);
            }

            if (isBasebat)
            {
                weaponManger.SwitchMelee(1);
                PlayerPrefs.SetInt("Melee", 1);
            }

            if (isKatana)
            {
                weaponManger.SwitchMelee(2);
                PlayerPrefs.SetInt("Melee", 2);
            }
        }
    }

    void CancelBools()
    {
        shop.C_1940 = false;
        shop.C_BubbleCar = false;
        shop.Hotrod = false;
        shop.IceCreamTruck = false;
        shop.MiniVan = false;
        shop.MonsterTruck = false;
        shop.MuscleTruck = false;
        shop.PickupTruck = false;
        shop.PoopTruck = false;
        shop.PorkTruck = false;
        shop.PrisonTruck = false;
        shop.WaterTruck = false;
        shop.WienerTruck = false;
        shop.BlackHawk = false;
        shop.isWeapon = false;
        shop.isVehicle = false;
        shop.isSRV = false;
        shop.isDog = false;
        shop.isHusky = false;
        shop.isAkira = false;

        shop.isMelee = false;
        shop.isAxe = false;
        shop.isKatana = false;
        shop.isBasebat = false;
    }

    void PurchasedItem()
    {
        PrizeText.text = "Click To Park";
        purchased = true;
    }

    void Initialize()
    {
        if(C_1940 && shop.C_1940_B)
        {
            PurchasedItem();
        }

        if (C_BubbleCar && shop.C_BubbleCar_B)
        {
            PurchasedItem();
        }

        if (Hotrod && shop.Hotrod_B)
        {
            PurchasedItem();
        }

        if (IceCreamTruck && shop.IceCreamTruck_B)
        {
            PurchasedItem();
        }

        if (MiniVan && shop.MiniVan_B)
        {
            PurchasedItem();
        }

        if(MonsterTruck && shop.MonsterTruck_B)
        {
            PurchasedItem();
        }

        if (MuscleTruck && shop.MuscleTruck_B)
        {
            PurchasedItem();
        }

        if (PickupTruck && shop.PickupTruck_B)
        {
            PurchasedItem();
        }

        if (PoopTruck && shop.PoopTruck_B)
        {
            PurchasedItem();
        }

        if (PorkTruck && shop.PorkTruck_B)
        {
            PurchasedItem();
        }

        if (PrisonTruck && shop.PrisonTruck_B)
        {
            PurchasedItem();
        }

        if (WaterTruck && shop.WaterTruck_B)
        {
            PurchasedItem();
        }

        if (WienerTruck && shop.WienerTruck_B)
        {
            PurchasedItem();
        }

        if (BlackHawk && shop.BlackHawk_B)
        {
            PurchasedItem();
        }

        if (Semi && shop.Semi_B)
        {
            PurchasedItem();
        }

        if (Airship && shop.Airship_B)
        {
            PurchasedItem();
        }

        if (isAkira && shop.isAkira_B)
        {
            purchasedDog();
        }
        
        if(isHusky && shop.isHusky_B)
        {
            purchasedDog();
        }

        if (isAxe && shop.isAxe_B)
        {
            purchasedDog();
        }

        if (isBasebat && shop.isBasebat_B)
        {
            purchasedDog();
        }

        if (isKatana && shop.isKatana_B)
        {
            purchasedDog();
        }

        //-----------------------------------------------------------------SRV-------------------------------------------//

        if(shop.totalSRV >= 10 && isSRV)
        {
            DisableBTN();
        }
/*
        if(madScientist)
        {
            if(database.srvAmount > 0 || database.srvRest > 0)
            DisableBTN();
        }

        if (redFox)
        {
            if(database.srvRedFox > 0 || database.srvRedFox_Rest > 0)
            DisableBTN();
        }

        if (romanCaeser)
        {
            if(database.srvRomanCaeser > 0 || database.srvRomanCaeser_Rest > 0)
            DisableBTN();
        }

        if (romanLegion)
        {
            if(database.srvRomanLegion > 0 || database.srvRomanLegion_Rest > 0)
            DisableBTN();
        }

        if (Biden)
        {
            if(database.srvBiden > 0 || database.srvBiden_Rest > 0)
            DisableBTN();
        }

        if (Trump)
        {
            if(database.srvTrump > 0 || database.srvTrump_Rest > 0)
            DisableBTN();
        }

        if (RedSoldier)
        {
            if(database.srvSoldierRed > 0 || database.srvSoldierRed_Rest > 0)
            DisableBTN();
        }

        if (BlueSoldier)
        {
            if(database.srvSoldierBlue > 0 || database.srvSoldierBlue_Rest > 0)
            DisableBTN();
        }

        if (STPMale)
        {
            if(database.srvSTPMale > 0 || database.srvSTPMale_Rest > 0)
            DisableBTN();
        }

        if (STPFemale)
        {
            if(database.srvSTPFemale > 0 || database.srvSTPFemale_Rest > 0)
            DisableBTN();
        }

        if (FrenchFries)
        {
            if(database.srvFrenchFry > 0 || database.srvFrenchFry_Rest > 0)
            DisableBTN();
        }

        if (Pickle)
        {
            if(database.srvPickle > 0 || database.srvPickle_Rest > 0)
            DisableBTN();
        }

        if (Cowboy)
        {
            if(database.srvCowboy > 0 || database.srvCowboy_Rest > 0)
            DisableBTN();
        }

        if (Pirate)
        {
            if(database.srvPirate > 0 || database.srvPirate_Rest > 0)
            DisableBTN();
        }

        if (SWAT)
        {
            if(database.srvSWAT > 0 || database.srvSWAT_Rest > 0)
            DisableBTN();
        }*/
    }

    public void DisableBTN()
    {
        icon.color = disableColor;
        clickBTN.interactable = false;
    }

    public void purchasedDog()
    {
        PrizeText.text = "Click To Equip";
        purchased = true;
    }

}
