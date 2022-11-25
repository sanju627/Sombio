using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AvatarBTN : MonoBehaviour
{
    public bool purchased;
    public TMP_Text buyTXT;
    public GameObject coin;
    public int index;
    [Space]
    public int prize;

    [Header("Vehicles Type")]
    public bool c_Chicken;
    public bool c_Dino;
    public bool c_Fry;
    public bool c_GreenAlien;
    public bool c_Poop;
    public bool c_Bacon;
    public bool c_Mad;
    public bool c_RedFox;
    public bool c_RomanCaeser;
    public bool c_RomanLegion;
    public bool c_Donald;
    public bool c_Biden;
    public bool c_RomanCenturian;
    public bool c_RomanPraetorian;
    public bool c_RomanScout;
    public bool c_Burger;
    public bool c_Donut;
    public bool c_Fries;
    public bool c_HotDog;
    public bool c_MilitaryCamo;
    public bool c_Military;
    public bool c_MilitaryGreen;
    public bool c_MilkShake;
    public bool c_Pickle;
    public bool c_StormTrooper;
    public bool c_STPFemale;
    public bool c_STPMale;
    public bool c_SoldierBlue;
    public bool c_SoldierRed;
    public bool c_SoldierYellow;
    public bool c_Cowboy;
    public bool c_Pirate;
    public bool c_SWAT;

    [Header("Vehicles Type")]
    public bool c_Chicken_B;
    public bool c_Dino_B;
    public bool c_Fry_B;
    public bool c_GreenAlien_B;
    public bool c_Poop_B;
    public bool c_Bacon_B;
    public bool c_Mad_B;
    public bool c_RedFox_B;
    public bool c_RomanCaeser_B;
    public bool c_RomanLegion_B;
    public bool c_Donald_B;
    public bool c_Biden_B;
    public bool c_RomanCenturian_B;
    public bool c_RomanPraetorian_B;
    public bool c_RomanScout_B;
    public bool c_Burger_B;
    public bool c_Donut_B;
    public bool c_Fries_B;
    public bool c_HotDog_B;
    public bool c_MilitaryCamo_B;
    public bool c_Military_B;
    public bool c_MilitaryGreen_B;
    public bool c_MilkShake_B;
    public bool c_Pickle_B;
    public bool c_StormTrooper_B;
    public bool c_STPFemale_B;
    public bool c_STPMale_B;
    public bool c_SoldierBlue_B;
    public bool c_SoldierRed_B;
    public bool c_SoldierYellow_B;
    public bool c_SelectedCostume_B;
    public bool c_Cowboy_B;
    public bool c_Pirate_B;
    public bool c_SWAT_B;

    PlayfabManager database;
    MenuImporter menuImporter;

    // Start is called before the first frame update
    void Start()
    {
        database = GameObject.FindGameObjectWithTag("Database").GetComponent<PlayfabManager>();
        menuImporter = GameObject.FindGameObjectWithTag("Menu").GetComponent<MenuImporter>();


        CheckPurchasedItem();
    }

    // Update is called once per frame
    void Update()
    {
        Initialize();

        if(!purchased)
        {
            coin.SetActive(true);
        }else
        {
            coin.SetActive(false);
        }
    }

    void PurchasedItem()
    {
        buyTXT.text = "Equip";
        purchased = true;
    }

    public void BuyBTN()
    {
        if(!purchased)
        {
            

            if(prize <= menuImporter.money)
            {
                GetAvatarData();

                PlayerPrefs.SetInt("Selected Costume", index);

                menuImporter.money -= prize;
                database.SendData("Coins", menuImporter.money.ToString());
            }
        }else if(purchased)
        {
            PlayerPrefs.SetInt("Selected Costume", index);
        }
        
    }

    void Initialize()
    {
        if (c_Chicken && c_Chicken_B)
        {
            PurchasedItem();
        }

        if (c_Dino && c_Dino_B)
        {
            PurchasedItem();
        }

        if (c_Fry && c_Fry_B)
        {
            PurchasedItem();
        }

        if (c_GreenAlien && c_GreenAlien_B)
        {
            PurchasedItem();
        }

        if (c_Poop && c_Poop_B)
        {
            PurchasedItem();
        }

        if (c_Bacon && c_Bacon_B)
        {
            PurchasedItem();
        }

        if (c_Mad && c_Mad_B)
        {
            PurchasedItem();
        }

        if (c_RedFox && c_RedFox_B)
        {
            PurchasedItem();
        }

        if (c_RomanCaeser && c_RomanCaeser_B)
        {
            PurchasedItem();
        }

        if (c_RomanLegion && c_RomanLegion_B)
        {
            PurchasedItem();
        }

        if (c_Donald && c_Donald_B)
        {
            PurchasedItem();
        }

        if (c_Biden && c_Biden_B)
        {
            PurchasedItem();
        }

        if (c_RomanCenturian && c_RomanCenturian_B)
        {
            PurchasedItem();
        }

        if (c_RomanPraetorian && c_RomanPraetorian_B)
        {
            PurchasedItem();
        }

        if (c_RomanScout && c_RomanScout_B)
        {
            PurchasedItem();
        }

        if (c_Burger && c_Burger_B)
        {
            PurchasedItem();
        }

        if (c_Donut && c_Donut_B)
        {
            PurchasedItem();
        }

        if (c_Fries && c_Fries_B)
        {
            PurchasedItem();
        }

        if (c_HotDog && c_HotDog_B)
        {
            PurchasedItem();
        }

        if (c_MilitaryCamo && c_MilitaryCamo_B)
        {
            PurchasedItem();
        }

        if (c_Military && c_Military_B)
        {
            PurchasedItem();
        }

        if (c_MilitaryGreen && c_MilitaryGreen_B)
        {
            PurchasedItem();
        }

        if (c_MilkShake && c_MilkShake_B)
        {
            PurchasedItem();
        }

        if (c_Pickle && c_Pickle_B)
        {
            PurchasedItem();
        }

        if (c_StormTrooper && c_StormTrooper_B)
        {
            PurchasedItem();
        }

        if (c_STPFemale && c_STPFemale_B)
        {
            PurchasedItem();
        }

        if (c_STPMale && c_STPMale_B)
        {
            PurchasedItem();
        }

        if (c_SoldierBlue && c_SoldierBlue_B)
        {
            PurchasedItem();
        }

        if (c_SoldierRed && c_SoldierRed_B)
        {
            PurchasedItem();
        }

        if (c_SoldierYellow && c_SoldierYellow)
        {
            PurchasedItem();
        }

        if (c_Cowboy && c_Cowboy_B)
        {
            PurchasedItem();
        }

        if (c_SWAT && c_SWAT_B)
        {
            PurchasedItem();
        }

        if (c_Pirate && c_Pirate_B)
        {
            PurchasedItem();
        }

    }

    void CheckPurchasedItem()
    {
        if (database.c_Chicken > 0)
        {
            c_Chicken_B = true;
        }

        if (database.c_Dino > 0)
        {
            c_Dino_B = true;
        }

        if (database.c_Fry > 0)
        {
            c_Fry_B = true;
        }

        if (database.c_GreenAlien > 0)
        {
            c_GreenAlien_B = true;
        }

        if (database.c_Poop > 0)
        {
            c_Poop_B = true;
        }

        if (database.c_Bacon > 0)
        {
            c_Bacon_B = true;
        }

        if (database.c_Mad > 0)
        {
            c_Mad_B = true;
        }

        if (database.c_RedFox > 0)
        {
            c_RedFox_B = true;
        }

        if (database.c_RomanCaeser > 0)
        {
            c_RomanCaeser_B = true;
        }

        if (database.c_RomanLegion > 0)
        {
            c_RomanLegion_B = true;
        }

        if (database.c_Donald > 0)
        {
            c_Donald_B = true;
        }

        if (database.c_Biden > 0)
        {
            c_Biden_B = true;
        }

        if (database.c_RomanCenturian > 0)
        {
            c_RomanCenturian_B = true;
        }

        if (database.c_RomanPraetorian > 0)
        {
            c_RomanPraetorian_B = true;
        }

        if (database.c_RomanScout > 0)
        {
            c_RomanScout_B = true;
        }

        if (database.c_Burger > 0)
        {
            c_Burger_B = true;
        }

        if (database.c_Donut > 0)
        {
            c_Donut_B = true;
        }

        if (database.c_Fries > 0)
        {
            c_Fries_B = true;
        }

        if (database.c_HotDog > 0)
        {
            c_HotDog_B = true;
        }

        if (database.c_MilitaryCamo > 0)
        {
            c_MilitaryCamo_B = true;
        }

        if (database.c_Military > 0)
        {
            c_Military = true;
        }

        if (database.c_MilitaryGreen > 0)
        {
            c_MilitaryGreen_B = true;
        }

        if (database.c_MilkShake > 0)
        {
            c_MilkShake_B = true;
        }

        if (database.c_Pickle > 0)
        {
            c_Pickle_B = true;
        }

        if (database.c_StormTrooper > 0)
        {
            c_StormTrooper_B = true;
        }

        if (database.c_STPFemale > 0)
        {
            c_STPFemale_B = true;
        }

        if (database.c_STPMale > 0)
        {
            c_STPMale_B = true;
        }

        if (database.c_SoldierBlue > 0)
        {
            c_SoldierBlue_B = true;
        }

        if (database.c_SoldierRed > 0)
        {
            c_SoldierRed_B = true;
        }

        if (database.c_SoldierYellow > 0)
        {
            c_SoldierYellow_B = true;
        }

        if (database.c_Cowboy > 0)
        {
            c_Cowboy_B = true;
        }

        if (database.c_SWAT > 0)
        {
            c_SWAT_B = true;
        }

        if (database.c_Pirate > 0)
        {
            c_Pirate_B = true;
        }
    }

    void GetAvatarData()
    {
        if (c_Chicken)
        {
            database.SendData("Cloth Chicken", 1.ToString());
            c_Chicken_B = true;
        }

        if (c_Dino)
        {
            database.SendData("Cloth Dino", 1.ToString());
            c_Dino_B = true;
        }

        if (c_Fry)
        {
            database.SendData("Cloth Fry", 1.ToString());
            c_Fry_B = true;
        }
        
        if (c_GreenAlien)
        {
            database.SendData("Cloth GreenAlien", 1.ToString());
            c_GreenAlien_B = true;
        }
        
        if (c_Poop)
        {
            database.SendData("Cloth Poop", 1.ToString());
            c_Poop_B = true;
        }
        
        if (c_Bacon)
        {
            database.SendData("Cloth Bacon", 1.ToString());
            c_Bacon_B = true;
        }
        
        if (c_Mad)
        {
            database.SendData("Cloth MadScientist", 1.ToString());
            c_Mad_B = true;
        }
        
        if (c_RedFox)
        {
            database.SendData("Cloth RedFox", 1.ToString());
            c_RedFox_B = true;
        }
        
        if (c_RomanCaeser)
        {
            database.SendData("Cloth RomanCaeser", 1.ToString());
            c_RomanCaeser_B = true;
        }
        
        if (c_RomanLegion)
        {
            database.SendData("Cloth RomanLegion", 1.ToString());
            c_RomanLegion_B = true;
        }
        
        if (c_Donald)
        {
            database.SendData("Cloth DonaldTrump", 1.ToString());
            c_Donald_B = true;
        }
        
        if (c_Biden)
        {
            database.SendData("Cloth JoeBidon", 1.ToString());
            c_Biden_B = true;
        }
        
        if (c_RomanCenturian)
        {
            database.SendData("Cloth RomanCenturion", 1.ToString());
            c_RomanCenturian_B = true;
        }
        
        if (c_RomanPraetorian)
        {
            database.SendData("Cloth RomanPraetorion", 1.ToString());
            c_RomanPraetorian_B = true;
        }
        
        if (c_RomanScout)
        {
            database.SendData("Cloth RomanScout", 1.ToString());
            c_RomanScout_B = true;
        }
        
        if (c_Burger)
        {
            database.SendData("Cloth Burger", 1.ToString());
            c_Burger_B = true;
        }
        
        if (c_Donut)
        {
            database.SendData("Cloth Donut", 1.ToString());
            c_Donut_B = true;
        }
        
        if (c_Fries)
        {
            database.SendData("Cloth Fries", 1.ToString());
            c_Fries_B = true;
        }
        
        if (c_HotDog)
        {
            database.SendData("Cloth Hotdog", 1.ToString());
            c_HotDog_B = true;
        }
        
        if (c_MilitaryCamo)
        {
            database.SendData("Cloth MilitaryCamo", 1.ToString());
            c_MilitaryCamo_B = true;
        }
        
        if (c_MilkShake)
        {
            database.SendData("Cloth MilkShake", 1.ToString());
            c_MilkShake_B = true;
        }
        
        if (c_Pickle)
        {
            database.SendData("Cloth Pickle", 1.ToString());
            c_Pickle_B = true;
        }
        
        if (c_StormTrooper)
        {
            database.SendData("Cloth StormTrooper", 1.ToString());
            c_StormTrooper_B = true;
        }
        
        if (c_STPFemale)
        {
            database.SendData("Cloth STPFemale", 1.ToString());
            c_STPFemale_B = true;
        }
        
        if (c_STPMale)
        {
            database.SendData("Cloth STPMale", 1.ToString());
            c_STPMale_B = true;
        }
        
        if (c_SoldierBlue)
        {
            database.SendData("Cloth SoldierBlue", 1.ToString());
            c_SoldierBlue_B = true;
        }
        
        if (c_SoldierRed)
        {
            database.SendData("Cloth SoldierRed", 1.ToString());
            c_SoldierRed_B = true;
        }
        
        if (c_SoldierYellow)
        {
            database.SendData("Cloth SoldierYellow", 1.ToString());
            c_SoldierYellow_B = true;
        }

        if (c_Cowboy)
        {
            database.SendData("Cloth CowBoy", 1.ToString());
            c_Cowboy_B = true;
        }

        if (c_SWAT)
        {
            database.SendData("Cloth SWAT", 1.ToString());
            c_SoldierYellow_B = true;
        }

        if (c_Pirate)
        {
            database.SendData("Cloth Pirate", 1.ToString());
            c_Pirate_B = true;
        }

    }
}

