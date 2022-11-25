using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Storage : MonoBehaviour
{
    public Player player;
    public GameObject[] borders;
    public GameObject[] Weapons;
    public int selectedWeapon;
    public int totalSRV;
    [Space]
    public GameObject setSRVWeapon;

    Inventory inventory;
    PlayfabManager database;
    SurvivalShop srvShop;

    [Header("Types Of weapon")]
    public bool glock17;
    public bool kriss;
    public bool mp7;
    public bool mp5;
    public bool ump;
    public bool tec9;
    public bool uzi;
    public bool ak12;
    public bool ak74;
    public bool g3a4;
    public bool g36c;
    public bool flamethrower;
    public bool minigun;
    //-------------------------------------------------Items---------------------------------//
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

    [Header("Amount")]
    public int ammoAmount;
    public int medkitAmount;
    public int bandageAmount;
    public int grenadeAmount;
    public int molotovAmount;
    public int smokeAmount;
    public int landmineAmount;
    public int chickenAmount;
    public int woodAmount;
    public int stoneAmount;
    public int wallAmount;
    public int metalWallAmount;
    public int woodDoorAmount;
    public int metalDoorAmount;
    public int fuelAmount;

    public int krissAmount;
    public int mp7Amount;
    public int mp5Amount;
    public int ump45Amount;
    public int tec9Amount;
    public int uziAmount;
    public int ak12Amount;
    public int ak74Amount;
    public int g3a4Amount;
    public int g36cAmount;
    public int flamethrowerAmount;
    public int glock17Amount;
    public int minigunAmount;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < borders.Length; i++)
        {
            borders[i].SetActive(false);
        }

        for (int i = 0; i < Weapons.Length; i++)
        {
            Weapons[i].SetActive(false);
        }

        database = GameObject.FindGameObjectWithTag("Database").GetComponent<PlayfabManager>();
        inventory = player.inventory;
        srvShop = GetComponent<SurvivalShop>();
        //EnableWeapon(0);

        GetData();
    }

    // Update is called once per frame
    void Update()
    {
        totalSRV = database.srvAmount + database.srvRedFox + database.srvRomanCaeser + database.srvRomanLegion +
                    database.srvSoldierBlue + database.srvSoldierRed + database.srvSTPMale + database.srvSTPFemale +
                    database.srvTrump + database.srvBiden + database.srvPickle + database.srvFrenchFry + database.srvCowboy + database.srvPirate + database.srvSWAT;

        glock17Amount = PlayerPrefs.GetInt("glock17 amt");
        krissAmount = PlayerPrefs.GetInt("kriss amt");
        mp7Amount = PlayerPrefs.GetInt("mp7 amt");
        mp5Amount = PlayerPrefs.GetInt("mp5 amt");
        ump45Amount = PlayerPrefs.GetInt("ump45 amt");
        tec9Amount = PlayerPrefs.GetInt("tec9 amt");
        uziAmount = PlayerPrefs.GetInt("uzi amt");
        ak12Amount = PlayerPrefs.GetInt("ak12 amt");
        ak74Amount = PlayerPrefs.GetInt("ak74 amt");
        g3a4Amount = PlayerPrefs.GetInt("g3a4 amt");
        g36cAmount = PlayerPrefs.GetInt("g36c amt");
        flamethrowerAmount = PlayerPrefs.GetInt("flamethrower amt");

        ammoAmount = PlayerPrefs.GetInt("ammo amt");
        medkitAmount = PlayerPrefs.GetInt("medkit amt");
        bandageAmount = PlayerPrefs.GetInt("bandage amt");
        grenadeAmount = PlayerPrefs.GetInt("grenade amt");
        molotovAmount = PlayerPrefs.GetInt("molotov amt");
        smokeAmount = PlayerPrefs.GetInt("smoke amt");
        landmineAmount = PlayerPrefs.GetInt("landmine amt");
        chickenAmount = PlayerPrefs.GetInt("chicken amt");
        woodAmount = PlayerPrefs.GetInt("wood amt");
        stoneAmount = PlayerPrefs.GetInt("stone amt");
        wallAmount = PlayerPrefs.GetInt("wall amt");
        metalWallAmount = PlayerPrefs.GetInt("metalWall amt");
        woodDoorAmount = PlayerPrefs.GetInt("woodDoor amt");
        metalDoorAmount = PlayerPrefs.GetInt("metalDoor amt");
        fuelAmount = PlayerPrefs.GetInt("fuel amt");
        minigunAmount = PlayerPrefs.GetInt("minigun amt");

        if(glock17Amount <= 0)
        {
            DisableWeapon(0);
        }

        if (krissAmount <= 0)
        {
            DisableWeapon(1);
        }

        if (mp7Amount <= 0)
        {
            DisableWeapon(2);
        }

        if (mp5Amount <= 0)
        {
            DisableWeapon(3);
        }

        if (ump45Amount <= 0)
        {
            DisableWeapon(4);
        }

        if (tec9Amount <= 0)
        {
            DisableWeapon(5);
        }

        if (uziAmount <= 0)
        {
            DisableWeapon(6);
        }

        if (ak12Amount <= 0)
        {
            DisableWeapon(7);
        }

        if (ak74Amount <= 0)
        {
            DisableWeapon(8);
        }

        if (g3a4Amount <= 0)
        {
            DisableWeapon(9);
        }

        if (g36cAmount <= 0)
        {
            DisableWeapon(10);
        }

        if (flamethrowerAmount <= 0)
        {
            DisableWeapon(11);
        }

        ///---------------------------------------------------------ITems----------------------------------------//

        if (ammoAmount <= 0)
        {
            DisableWeapon(12);
        }

        if (medkitAmount <= 0)
        {
            DisableWeapon(13);
        }

        if (bandageAmount <= 0)
        {
            DisableWeapon(14);
        }

        if (grenadeAmount <= 0)
        {
            DisableWeapon(15);
        }

        if (molotovAmount <= 0)
        {
            DisableWeapon(16);
        }

        if (smokeAmount <= 0)
        {
            DisableWeapon(17);
        }

        if (landmineAmount <= 0)
        {
            DisableWeapon(18);
        }

        if (chickenAmount <= 0)
        {
            DisableWeapon(19);
        }

        if (woodAmount <= 0)
        {
            DisableWeapon(20);
        }

        if (stoneAmount <= 0)
        {
            DisableWeapon(21);
        }

        if (wallAmount <= 0)
        {
            DisableWeapon(22);
        }

        if (metalWallAmount <= 0)
        {
            DisableWeapon(23);
        }

        if (woodDoorAmount <= 0)
        {
            DisableWeapon(24);
        }

        if (metalDoorAmount <= 0)
        {
            DisableWeapon(25);
        }

        if (fuelAmount <= 0)
        {
            DisableWeapon(26);
        }

        if (minigunAmount <= 0)
        {
            DisableWeapon(27);
        }
    }

    public void SelectWeapon(int choice)
    {
        for (int i = 0; i < borders.Length; i++)
        {
            borders[i].SetActive(false);
        }
        selectedWeapon = choice;
        borders[choice].SetActive(true);

        CheckCurrentWeapon(selectedWeapon);

        if(choice <= 11 && totalSRV > 0)
        {
            setSRVWeapon.SetActive(true);
        }else
        {
            setSRVWeapon.SetActive(false);
        }
    }

    void GetData()
    {
        if (PlayerPrefs.GetInt("STG glock17") >= 1)
        {
            EnableWeapon(0);
        }

        if (PlayerPrefs.GetInt("STG kriss") >= 1)
        {
            EnableWeapon(1);
        }

        if (PlayerPrefs.GetInt("STG MP7") >= 1)
        {
            EnableWeapon(2);
        }

        if (PlayerPrefs.GetInt("STG MP5") >= 1)
        {
            EnableWeapon(3);
        }

        if (PlayerPrefs.GetInt("STG UMP") >= 1)
        {
            EnableWeapon(4);
        }

        if (PlayerPrefs.GetInt("STG Tec9") >= 1)
        {
            EnableWeapon(5);
        }

        if (PlayerPrefs.GetInt("STG UZI") >= 1)
        {
            EnableWeapon(6);
        }

        if (PlayerPrefs.GetInt("STG AK12") >= 1)
        {
            EnableWeapon(7);
        }

        if (PlayerPrefs.GetInt("STG AK74") >= 1)
        {
            EnableWeapon(8);
        }

        if (PlayerPrefs.GetInt("STG G3A4") >= 1)
        {
            EnableWeapon(9);
        }

        if (PlayerPrefs.GetInt("STG G36C") >= 1)
        {
            EnableWeapon(10);
        }

        if (PlayerPrefs.GetInt("STG Flamethrower") >= 1)
        {
            EnableWeapon(11);
        }

        if (PlayerPrefs.GetInt("STG ammo") >= 1)
        {
            EnableWeapon(12);
        }

        if (PlayerPrefs.GetInt("STG medkit") >= 1)
        {
            EnableWeapon(13);
        }

        if (PlayerPrefs.GetInt("STG medkit") >= 1)
        {
            EnableWeapon(14);
        }

        if (PlayerPrefs.GetInt("STG bandage") >= 1)
        {
            EnableWeapon(15);
        }

        if (PlayerPrefs.GetInt("STG grenade") >= 1)
        {
            EnableWeapon(16);
        }

        if (PlayerPrefs.GetInt("STG molotov") >= 1)
        {
            EnableWeapon(17);
        }

        if (PlayerPrefs.GetInt("STG smoke") >= 1)
        {
            EnableWeapon(18);
        }

        if (PlayerPrefs.GetInt("STG landmine") >= 1)
        {
            EnableWeapon(19);
        }

        if (PlayerPrefs.GetInt("STG chicken") >= 1)
        {
            EnableWeapon(20);
        }

        if (PlayerPrefs.GetInt("STG wood") >= 1)
        {
            EnableWeapon(21);
        }

        if (PlayerPrefs.GetInt("STG stone") >= 1)
        {
            EnableWeapon(22);
        }

        if (PlayerPrefs.GetInt("STG wall") >= 1)
        {
            EnableWeapon(23);
        }

        if (PlayerPrefs.GetInt("STG metalWall") >= 1)
        {
            EnableWeapon(24);
        }

        if (PlayerPrefs.GetInt("STG woodDoor") >= 1)
        {
            EnableWeapon(25);
        }

        if (PlayerPrefs.GetInt("STG metalDoor") >= 1)
        {
            EnableWeapon(26);
        }

        if (PlayerPrefs.GetInt("STG fuel") >= 1)
        {
            EnableWeapon(27);
        }

        if (PlayerPrefs.GetInt("STG minigun") >= 1)
        {
            EnableWeapon(28);
        }
    }

    public void EnableWeapon(int choice)
    {
        Weapons[choice].SetActive(true);

        if (choice == 0)
        {
            PlayerPrefs.SetInt("STG glock17", 1);
        }

        if (choice == 1)
        {
            PlayerPrefs.SetInt("STG kriss", 1);
        }

        if (choice == 2)
        {
            PlayerPrefs.SetInt("STG MP7", 1);
        }

        if (choice == 3)
        {
            PlayerPrefs.SetInt("STG MP5", 1);
        }

        if (choice == 4)
        {
            PlayerPrefs.SetInt("STG UMP", 1);
        }

        if (choice == 5)
        {
            PlayerPrefs.SetInt("STG Tec9", 1);
        }

        if (choice == 6)
        {
            PlayerPrefs.SetInt("STG UZI", 1);
        }

        if (choice == 7)
        {
            PlayerPrefs.SetInt("STG AK12", 1);
        }

        if (choice == 8)
        {
            PlayerPrefs.SetInt("STG AK74", 1);
        }

        if (choice == 9)
        {
            PlayerPrefs.SetInt("STG G3A4", 1);
        }

        if (choice == 10)
        {
            PlayerPrefs.SetInt("STG G36C", 1);
        }

        if (choice == 11)
        {
            PlayerPrefs.SetInt("STG Flamethrower", 1);
        }

        if (choice == 12)
        {
            PlayerPrefs.SetInt("STG ammo", 1);
        }

        if (choice == 13)
        {
            PlayerPrefs.SetInt("STG medkit", 1);
        }

        if (choice == 14)
        {
            PlayerPrefs.SetInt("STG bandage", 1);
        }

        if (choice == 15)
        {
            PlayerPrefs.SetInt("STG grenade", 1);
        }

        if (choice == 16)
        {
            PlayerPrefs.SetInt("STG molotov", 1);
        }

        if (choice == 17)
        {
            PlayerPrefs.SetInt("STG smoke", 1);
        }

        if (choice == 18)
        {
            PlayerPrefs.SetInt("STG landmine", 1);
        }

        if (choice == 19)
        {
            PlayerPrefs.SetInt("STG chicken", 1);
        }

        if (choice == 20)
        {
            PlayerPrefs.SetInt("STG wood", 1);
        }

        if (choice == 21)
        {
            PlayerPrefs.SetInt("STG stone", 1);
        }

        if (choice == 22)
        {
            PlayerPrefs.SetInt("STG wall", 1);
        }

        if (choice == 23)
        {
            PlayerPrefs.SetInt("STG metalWall", 1);
        }

        if (choice == 24)
        {
            PlayerPrefs.SetInt("STG woodDoor", 1);
        }

        if (choice == 25)
        {
            PlayerPrefs.SetInt("STG metalDoor", 1);
        }

        if (choice == 26)
        {
            PlayerPrefs.SetInt("STG fuel", 1);
        }

        if (choice == 27)
        {
            PlayerPrefs.SetInt("STG minigun", 1);
        }

    }

    public void DisableWeapon(int choice)
    {
        Weapons[choice].SetActive(false);
    }

    public void ClickSet()
    {
        if(kriss)
        {
            database.krissAmount = 0;
            database.SendData("Weapon Kriss", 0.ToString());
            database.SendData("Weapon Kriss_SRV", 1.ToString());
            DisableWeapon(selectedWeapon);
            srvShop.EnableWeapon(selectedWeapon);

            PlayerPrefs.SetInt("STG kriss", 0);
        }

        if (mp7)
        {
            database.MP7Amount = 0;
            database.SendData("Weapon MP7", 0.ToString());
            database.SendData("Weapon MP7_SRV", 1.ToString());
            DisableWeapon(selectedWeapon);
            srvShop.EnableWeapon(selectedWeapon);

            PlayerPrefs.SetInt("STG MP7", 0);
        }

        if (mp5)
        {
            database.MP5Amount = 0;
            database.SendData("Weapon MP5", 0.ToString());
            database.SendData("Weapon MP5_SRV", 1.ToString());
            DisableWeapon(selectedWeapon);
            srvShop.EnableWeapon(selectedWeapon);

            PlayerPrefs.SetInt("STG MP5", 0);
        }

        if (ump)
        {
            database.UMPAmount = 0;
            database.SendData("Weapon UMP", 0.ToString());
            database.SendData("Weapon UMP_SRV", 1.ToString());
            DisableWeapon(selectedWeapon);
            srvShop.EnableWeapon(selectedWeapon);

            PlayerPrefs.SetInt("STG UMP", 0);
        }

        if (tec9)
        {
            database.Tec9Amount = 0;
            database.SendData("Weapon Tec9", 0.ToString());
            database.SendData("Weapon Tec9_SRV", 1.ToString());
            DisableWeapon(selectedWeapon);
            srvShop.EnableWeapon(selectedWeapon);

            PlayerPrefs.SetInt("STG Tec9", 0);
        }

        if (uzi)
        {
            database.UZIAmount = 0;
            database.SendData("Weapon UZI", 0.ToString());
            database.SendData("Weapon UZI_SRV", 1.ToString());
            DisableWeapon(selectedWeapon);
            srvShop.EnableWeapon(selectedWeapon);

            PlayerPrefs.SetInt("STG UZI", 0);
        }

        if (ak12)
        {
            database.ak12Amount = 0;
            database.SendData("Weapon AK12", 0.ToString());
            database.SendData("Weapon AK12_SRV", 1.ToString());
            DisableWeapon(selectedWeapon);
            srvShop.EnableWeapon(selectedWeapon);

            PlayerPrefs.SetInt("STG AK12", 0);
        }

        if (ak74)
        {
            database.ak74Amount = 0;
            database.SendData("Weapon AK74", 0.ToString());
            database.SendData("Weapon AK74_SRV", 1.ToString());
            DisableWeapon(selectedWeapon);
            srvShop.EnableWeapon(selectedWeapon);

            PlayerPrefs.SetInt("STG AK74", 0);
        }

        if (g3a4)
        {
            database.G3A4Amount = 0;
            database.SendData("Weapon G3A4", 0.ToString());
            database.SendData("Weapon G3A4_SRV", 1.ToString());
            DisableWeapon(selectedWeapon);
            srvShop.EnableWeapon(selectedWeapon);

            PlayerPrefs.SetInt("STG G3A4", 0);
        }


        if (g36c)
        {
            database.G36CAmount = 0;
            database.SendData("Weapon G36C", 0.ToString());
            database.SendData("Weapon G36C_SRV", 1.ToString());
            DisableWeapon(selectedWeapon);
            srvShop.EnableWeapon(selectedWeapon);

            PlayerPrefs.SetInt("STG G36C", 0);
        }

        if (flamethrower)
        {
            database.flamethrowerAmount = 0;
            database.SendData("Weapon Flamethrower", 0.ToString());
            database.SendData("Weapon Flamethrower_SRV", 1.ToString());
            DisableWeapon(selectedWeapon);
            srvShop.EnableWeapon(selectedWeapon);

            PlayerPrefs.SetInt("STG Flamethrower", 0);
        }


    }

    public void CheckCurrentWeapon(int selectedWeapon)
    {
        CancelBools();

        if (selectedWeapon == 0)
        {
            glock17 = true;
        }

        if (selectedWeapon == 1)
        {
            kriss = true;
        }

        if (selectedWeapon == 2)
        {
            mp7 = true;
        }
        
        if (selectedWeapon == 3)
        {
            mp5 = true;
        }
        
        if (selectedWeapon == 4)
        {
            ump = true;
        }
        
        if (selectedWeapon == 5)
        {
            tec9 = true;
        }
        
        if (selectedWeapon == 6)
        {
            uzi = true;
        }
        
        if (selectedWeapon == 7)
        {
            ak12 = true;
        }
        
        if (selectedWeapon == 8)
        {
            ak74 = true;
        }
        
        if (selectedWeapon == 9)
        {
            g3a4 = true;
        }
        
        if (selectedWeapon == 10)
        {
            g36c = true;
        }

        if (selectedWeapon == 11)
        {
            flamethrower = true;
        }

        if (selectedWeapon == 12)
        {
            ammo = true;
        }

        if (selectedWeapon == 13)
        {
            medkit = true;
        }

        if (selectedWeapon == 14)
        {
            bandage = true;
        }

        if (selectedWeapon == 15)
        {
            grenade = true;
        }

        if (selectedWeapon == 16)
        {
            molotov = true;
        }

        if (selectedWeapon == 17)
        {
            smoke = true;
        }

        if (selectedWeapon == 18)
        {
            landmine = true;
        }

        if (selectedWeapon == 19)
        {
            chicken = true;
        }

        if (selectedWeapon == 20)
        {
            wood = true;
        }

        if (selectedWeapon == 21)
        {
            stone = true;
        }

        if (selectedWeapon == 22)
        {
            wall = true;
        }

        if (selectedWeapon == 23)
        {
            metalWall = true;
        }

        if (selectedWeapon == 24)
        {
            woodDoor = true;
        }

        if (selectedWeapon == 25)
        {
            metalDoor = true;
        }

        if (selectedWeapon == 26)
        {
            fuel = true;
        }

        if (selectedWeapon == 27)
        {
            minigun = true;
        }
    }

    void CancelBools()
    {
        glock17 = false;
        kriss = false;
        mp7 = false;
        mp5 = false;
        ump = false;
        tec9 = false;
        uzi = false;
        ak12 = false;
        ak74 = false;
        g3a4 = false;
        g36c = false;
        flamethrower = false;
        ammo = false;

        medkit = false;
        bandage = false;
        grenade = false;
        molotov = false;
        smoke = false;
        landmine = false;
        chicken = false;
        wood = false;
        stone = false;
        wall = false;
        metalWall = false;
        woodDoor = false;
        metalDoor = false;
        fuel = false;
        minigun = false;
}

    public void ClickToInventory()
    {
        if (glock17)
        {
            database.Glock17Amount = glock17Amount;

            DisableWeapon(selectedWeapon);
            inventory.AddItem(new Item { itemType = Item.ItemType.glock17, amount = glock17Amount });

            database.SendData("Weapon Glock17", inventory.glock17Amount.ToString());

            PlayerPrefs.SetInt("STG glock17", 0);
            PlayerPrefs.SetInt("glock17 amt", 0);
        }

        if (kriss)
        {
            database.krissAmount = krissAmount;

            DisableWeapon(selectedWeapon);
            inventory.AddItem(new Item { itemType = Item.ItemType.kriss, amount = krissAmount });

            database.SendData("Weapon Kriss", inventory.krissAmount.ToString());

            PlayerPrefs.SetInt("STG kriss", 0);
            PlayerPrefs.SetInt("kriss amt", 0);
        }

        if (mp7)
        {
            database.MP7Amount = mp7Amount;

            DisableWeapon(selectedWeapon);
            inventory.AddItem(new Item { itemType = Item.ItemType.mp7, amount = mp7Amount });

            database.SendData("Weapon MP7", inventory.mp7Amount.ToString());

            PlayerPrefs.SetInt("STG MP7", 0);
            PlayerPrefs.SetInt("MP7 amt", 0);
        }

        if (mp5)
        {
            database.MP5Amount = mp5Amount;

            DisableWeapon(selectedWeapon);
            inventory.AddItem(new Item { itemType = Item.ItemType.mp5, amount = mp5Amount });

            database.SendData("Weapon MP5", inventory.mp5Amount.ToString());

            PlayerPrefs.SetInt("STG MP5", 0);
            PlayerPrefs.SetInt("mp5 amt", 0);
        }

        if (ump)
        {

            database.UMPAmount = ump45Amount;

            DisableWeapon(selectedWeapon);
            inventory.AddItem(new Item { itemType = Item.ItemType.ump45, amount = ump45Amount });

            database.SendData("Weapon UMP", inventory.ump45Amount.ToString());

            PlayerPrefs.SetInt("STG UMP", 0);
            PlayerPrefs.SetInt("ump amt", 0);
        }

        if (tec9)
        {
            database.Tec9Amount = tec9Amount;

            DisableWeapon(selectedWeapon);
            inventory.AddItem(new Item { itemType = Item.ItemType.tec9, amount = tec9Amount });

            database.SendData("Weapon Tec9", inventory.tec9Amount.ToString());

            PlayerPrefs.SetInt("STG Tec9", 0);
            PlayerPrefs.SetInt("tec9 amt", 0);
        }

        if (uzi)
        {

            database.UZIAmount = uziAmount;

            DisableWeapon(selectedWeapon);
            inventory.AddItem(new Item { itemType = Item.ItemType.uzi, amount = uziAmount });

            database.SendData("Weapon UZI", inventory.uziAmount.ToString());

            PlayerPrefs.SetInt("STG UZI", 0);
            PlayerPrefs.SetInt("uzi amt", 0);
        }

        if (ak12)
        {

            database.ak12Amount = ak12Amount;

            DisableWeapon(selectedWeapon);
            inventory.AddItem(new Item { itemType = Item.ItemType.ak74, amount = ak12Amount });

            database.SendData("Weapon AK12", inventory.ak12Amount.ToString());


            PlayerPrefs.SetInt("STG AK12", 0);
            PlayerPrefs.SetInt("ak12 amt", 0);
        }

        if (ak74)
        {
            database.ak74Amount = ak74Amount;

            DisableWeapon(selectedWeapon);
            inventory.AddItem(new Item { itemType = Item.ItemType.ak74, amount = ak74Amount });

            database.SendData("Weapon AK74", inventory.ak74Amount.ToString());

            PlayerPrefs.SetInt("STG AK74", 0);
            PlayerPrefs.SetInt("ak74 amt", 0);
        }

        if (g3a4)
        {
            database.G3A4Amount = g3a4Amount;

            DisableWeapon(selectedWeapon);
            inventory.AddItem(new Item { itemType = Item.ItemType.g3a4, amount = g3a4Amount });

            database.SendData("Weapon G3A4", inventory.g36cAmount.ToString());

            PlayerPrefs.SetInt("STG G3A4", 0);
            PlayerPrefs.SetInt("g3a4 amt", 0);
        }

        if (g36c)
        {
            database.G36CAmount = g36cAmount;

            DisableWeapon(selectedWeapon);
            inventory.AddItem(new Item { itemType = Item.ItemType.g36c, amount = g36cAmount });

            database.SendData("Weapon G36C", inventory.g36cAmount.ToString());

            PlayerPrefs.SetInt("STG G36C", 0);
            PlayerPrefs.SetInt("g36c amt", 0);
        }

        if (flamethrower)
        {
            database.flamethrowerAmount = flamethrowerAmount;

            DisableWeapon(selectedWeapon);
            inventory.AddItem(new Item { itemType = Item.ItemType.flamethrower, amount = flamethrowerAmount});

            database.SendData("Weapon Flamethrower", inventory.flamethrowerAmount.ToString());

            PlayerPrefs.SetInt("STG Flamethrower", 0);
            PlayerPrefs.SetInt("flamethrower amt", 0);
        }

        if (minigun)
        {
            database.MinigunAmount = minigunAmount;

            DisableWeapon(selectedWeapon);
            inventory.AddItem(new Item { itemType = Item.ItemType.minigun, amount = minigunAmount });

            database.SendData("Weapon Minigun", inventory.minigunAmount.ToString());

            PlayerPrefs.SetInt("STG minigun", 0);
            PlayerPrefs.SetInt("minigun amt", 0);
        }

        if (ammo)
        {
            database.ammoAmount = ammoAmount;
            

            DisableWeapon(selectedWeapon);
            inventory.AddItem(new Item { itemType = Item.ItemType.ammo, amount = ammoAmount});

            database.SendData("Item Ammo", inventory.ammoAmount.ToString());

            PlayerPrefs.SetInt("STG ammo", 0);
            PlayerPrefs.SetInt("ammo amt", 0);
        }

        if (medkit)
        {
            database.medkitAmount = medkitAmount;


            DisableWeapon(selectedWeapon);
            inventory.AddItem(new Item { itemType = Item.ItemType.medkit, amount = medkitAmount });

            database.SendData("Item Medkit", inventory.medkitAmount.ToString());

            PlayerPrefs.SetInt("STG medkit", 0);
            PlayerPrefs.SetInt("medkit amt", 0);
        }

        if (bandage)
        {
            database.bandageAmount = bandageAmount;


            DisableWeapon(selectedWeapon);
            inventory.AddItem(new Item { itemType = Item.ItemType.bandage, amount = bandageAmount });

            database.SendData("Item Bandage", inventory.bandageAmount.ToString());

            PlayerPrefs.SetInt("STG bandage", 0);
            PlayerPrefs.SetInt("bandage amt", 0);
        }

        if (grenade)
        {
            database.bandageAmount = grenadeAmount;


            DisableWeapon(selectedWeapon);
            inventory.AddItem(new Item { itemType = Item.ItemType.grenade, amount = grenadeAmount });

            database.SendData("Item Grenade", inventory.grenadeAmount.ToString());

            PlayerPrefs.SetInt("STG grenade", 0);
            PlayerPrefs.SetInt("grenade amt", 0);
        }

        if (molotov)
        {
            database.bandageAmount = molotovAmount;


            DisableWeapon(selectedWeapon);
            inventory.AddItem(new Item { itemType = Item.ItemType.molotov, amount = molotovAmount });

            database.SendData("Item Molotov", inventory.molotovAmount.ToString());

            PlayerPrefs.SetInt("STG molotov", 0);
            PlayerPrefs.SetInt("molotov amt", 0);
        }

        if (smoke)
        {
            database.bandageAmount = smokeAmount;


            DisableWeapon(selectedWeapon);
            inventory.AddItem(new Item { itemType = Item.ItemType.smoke, amount = smokeAmount });

            database.SendData("Item Smoke", inventory.smokeAmount.ToString());

            PlayerPrefs.SetInt("STG smoke", 0);
            PlayerPrefs.SetInt("smoke amt", 0);
        }

        if (landmine)
        {
            database.bandageAmount = landmineAmount;


            DisableWeapon(selectedWeapon);
            inventory.AddItem(new Item { itemType = Item.ItemType.landmine, amount = landmineAmount });

            database.SendData("Item Landmine", inventory.landmineAmount.ToString());

            PlayerPrefs.SetInt("STG landmine", 0);
            PlayerPrefs.SetInt("landmine amt", 0);
        }

        if (chicken)
        {
            database.bandageAmount = chickenAmount;


            DisableWeapon(selectedWeapon);
            inventory.AddItem(new Item { itemType = Item.ItemType.chicken, amount = chickenAmount });

            database.SendData("Item Landmine", inventory.chickenAmount.ToString());

            PlayerPrefs.SetInt("STG chicken", 0);
            PlayerPrefs.SetInt("chicken amt", 0);
        }

        if (wood)
        {
            database.bandageAmount = woodAmount;


            DisableWeapon(selectedWeapon);
            inventory.AddItem(new Item { itemType = Item.ItemType.wood, amount = woodAmount });

            database.SendData("Item Wood", inventory.woodAmount.ToString());

            PlayerPrefs.SetInt("STG wood", 0);
            PlayerPrefs.SetInt("wood amt", 0);
        }

        if (stone)
        {
            database.bandageAmount = stoneAmount;


            DisableWeapon(selectedWeapon);
            inventory.AddItem(new Item { itemType = Item.ItemType.stone, amount = stoneAmount });

            database.SendData("Item Wood", inventory.stoneAmount.ToString());

            PlayerPrefs.SetInt("STG stone", 0);
            PlayerPrefs.SetInt("stone amt", 0);
        }

        if (wall)
        {
            database.bandageAmount = wallAmount;


            DisableWeapon(selectedWeapon);
            inventory.AddItem(new Item { itemType = Item.ItemType.wall, amount = wallAmount });

            database.SendData("Item WoodWall", inventory.wallAmount.ToString());

            PlayerPrefs.SetInt("STG wall", 0);
            PlayerPrefs.SetInt("wall amt", 0);
        }

        if (metalWall)
        {
            database.bandageAmount = metalWallAmount;


            DisableWeapon(selectedWeapon);
            inventory.AddItem(new Item { itemType = Item.ItemType.metalWall, amount = metalWallAmount });

            database.SendData("Item MetalWall", inventory.metalWallAmount.ToString());

            PlayerPrefs.SetInt("STG metalWall", 0);
            PlayerPrefs.SetInt("metalWall amt", 0);
        }

        if (woodDoor)
        {
            database.bandageAmount = woodDoorAmount;


            DisableWeapon(selectedWeapon);
            inventory.AddItem(new Item { itemType = Item.ItemType.woodDoor, amount = woodDoorAmount });

            database.SendData("Item WoodDoor", inventory.woodDoorAmount.ToString());

            PlayerPrefs.SetInt("STG woodDoor", 0);
            PlayerPrefs.SetInt("woodDoor amt", 0);
        }

        if (metalDoor)
        {
            database.bandageAmount = metalDoorAmount;


            DisableWeapon(selectedWeapon);
            inventory.AddItem(new Item { itemType = Item.ItemType.metalDoor, amount = metalDoorAmount });

            database.SendData("Item MetalDoor", inventory.metalDoorAmount.ToString());

            PlayerPrefs.SetInt("STG metalDoor", 0);
            PlayerPrefs.SetInt("metalDoor amt", 0);
        }

        if (fuel)
        {
            database.bandageAmount = fuelAmount;


            DisableWeapon(selectedWeapon);
            inventory.AddItem(new Item { itemType = Item.ItemType.fuel, amount = fuelAmount });

            database.SendData("Item Fuel", inventory.fuelAmount.ToString());

            PlayerPrefs.SetInt("STG fuel", 0);
            PlayerPrefs.SetInt("fuel amt", 0);
        }
    }
}
