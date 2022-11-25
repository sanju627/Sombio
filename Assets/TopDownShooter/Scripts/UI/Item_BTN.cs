using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Item_BTN : MonoBehaviour
{
    public Item item;
    public TMP_Text sendingTXT;
    public GameObject FunctionsOBJ;
    public GameObject DropBTNOBJ;
    public GameObject SetBTNOBJ;
    public GameObject SendBTNOBJ;
    public GameObject SendDirectSRVOBJ;
    [Space]
    public bool functionOpen;
    public bool isWeapon;
    [Space]
    public bool inInventory;
    public bool inDog;
    public bool inSurvivor;
    public int dogIndex;

    [Header("Items")]
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
    [Space]
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

    Player player;
    DataImporter dataImporter;
    WeaponManger weapon;
    bool switched;
    ItemAssets itemAssets;
    UI_Inventory uI_Inventory;
    SurvivalShop sShop;
    WeaponManger weaponManger;
    PlayfabManager database;
    Inventory inventory;
    Storage storage;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        itemAssets = player.GetComponent<ItemAssets>();
        weapon = player.GetComponent<WeaponManger>();
        dataImporter = player.GetComponent<DataImporter>();
        inventory = player.inventory;

        database = PlayfabManager.database;

        uI_Inventory = player.ui_Inventory;
        sShop = uI_Inventory.survivalShop;

        functionOpen = false;
        FunctionsOBJ.SetActive(false);
        weaponManger = player.GetComponent<WeaponManger>();

        SetItem();

        SetPositions();

        storage = uI_Inventory.GetComponent<Storage>();

        //inInventory = true;

        if (item.IsWeapon() && !item.IsPistol())
        {
            SetBTNOBJ.SetActive(true);
        } else
        {
            SetBTNOBJ.SetActive(false);
        }
    }

    private void Update()
    {
        dogIndex = PlayerPrefs.GetInt("DogIndex");
        if (dogIndex > 0)
        {
            SendBTNOBJ.SetActive(true);
        } else
        {
            SendBTNOBJ.SetActive(false);
        }

        int totalSRV = database.srvAmount + database.srvRedFox + database.srvRomanCaeser + database.srvRomanLegion +
                    database.srvSoldierBlue + database.srvSoldierRed + database.srvSTPMale + database.srvSTPFemale +
                    database.srvTrump + database.srvBiden + database.srvPickle + database.srvFrenchFry + database.srvCowboy + database.srvPirate + database.srvSWAT;

        if (weaponManger.withinStorage)
        {
            SetBTNOBJ.SetActive(true);
        }
        else if(!weaponManger.withinStorage)
        {
            SetBTNOBJ.SetActive(false);
        }

        if (totalSRV > 0 && item.IsWeapon() && !item.IsPistol() && !item.IsMinigun())
        {
            //SendDirectSRVOBJ.SetActive(true);
        }
        else
        {
           // SendDirectSRVOBJ.SetActive(false);
        }


        /*if(weaponManger.withinShop)
        {
            SendToInventory();
        }else
        {
            
        }*/

        //SetPositions();

        //DestroyClone();

    }

    public void SetItem(Item item)
    {
    	this.item = item;
    }

    public void ClickFunction()
    {
        switch (item.itemType)
        {
            case Item.ItemType.wall:
                weapon.SelectItem(0);
                break;
            case Item.ItemType.metalWall:
                weapon.SelectItem(1);
                break;
            case Item.ItemType.woodDoor:
                weapon.SelectItem(2);
                break;
            case Item.ItemType.metalDoor:
                weapon.SelectItem(3);
                break;
            case Item.ItemType.grenade:
                weapon.SelectItem(4);
                break;
            case Item.ItemType.smoke:
                weapon.SelectItem(5);
                break;
            case Item.ItemType.molotov:
                weapon.SelectItem(6);
                break;
            case Item.ItemType.medkit:
                if(player.currentHealth < player.maxHealth && !player.healing)
                {
                    player.HealOrder(50f, false);
                }
                break;
            case Item.ItemType.bandage:
                if (player.currentHealth < player.maxHealth && !player.healing)
                {
                    player.HealOrder(5f, true);
                }
                break;
            case Item.ItemType.chicken:
                if (player.hungerValue <= 80f && !player.healing)
                {
                    player.HungerOrder(50f);
                }
                break;
            case Item.ItemType.glock17:
                weapon.SecondryWeaponIndex = 12;
                weapon.SwitchTwo();
                weapon.SelectGun(12);
                break;
            case Item.ItemType.kriss:
                weapon.PrimaryWeaponIndex = 1;
                weapon.SwitchOne();
                weapon.SelectGun(1);
                break;
            case Item.ItemType.mp7:
                weapon.PrimaryWeaponIndex = 2;
                weapon.SwitchOne();
                weapon.SelectGun(2);
                break;
            case Item.ItemType.mp5:
                weapon.PrimaryWeaponIndex = 3;
                weapon.SwitchOne();
                weapon.SelectGun(3);
                break;
            case Item.ItemType.tec9:
                weapon.PrimaryWeaponIndex = 4;
                weapon.SwitchOne();
                weapon.SelectGun(4);
                break;
            case Item.ItemType.ump45:
                weapon.PrimaryWeaponIndex = 5;
                weapon.SwitchOne();
                weapon.SelectGun(5);
                break;
            case Item.ItemType.uzi:
                weapon.PrimaryWeaponIndex = 6;
                weapon.SwitchOne();
                weapon.SelectGun(6);
                break;
            case Item.ItemType.ak12:
                weapon.PrimaryWeaponIndex = 7;
                weapon.SwitchOne();
                weapon.SelectGun(7);
                break;
            case Item.ItemType.ak74:
                weapon.PrimaryWeaponIndex = 8;
                weapon.SwitchOne();
                weapon.SelectGun(8);
                break;
            case Item.ItemType.g3a4:
                weapon.PrimaryWeaponIndex = 9;
                weapon.SwitchOne();
                weapon.SelectGun(9);
                break;
            case Item.ItemType.g36c:
                weapon.PrimaryWeaponIndex = 10;
                weapon.SwitchOne();
                weapon.SelectGun(10);
                break;
            case Item.ItemType.flamethrower:
                weapon.PrimaryWeaponIndex = 11;
                weapon.SwitchOne();
                weapon.SelectGun(11);
                break;
            case Item.ItemType.minigun:
                weapon.PrimaryWeaponIndex = 12;
                weapon.SwitchOne();
                weapon.SelectGun(13);
                break;
        }
    }

    public void ItemClick()
    {
        if(!functionOpen)
        {
            FunctionsOBJ.SetActive(true);
            functionOpen = true;
        }else
        {
            FunctionsOBJ.SetActive(false);
            functionOpen = false;
        }
    }

    public void Drop()
    {
        // if(isWeapon && player.weaponAmount <= 1)return;

        if (item.IsWeapon())
        {
            weaponManger.SwitchThree();
            weaponManger.resetWeapon();
        }

        Item duplicateItem = new Item { itemType = item.itemType, amount = item.amount };
        inventory.RemoveItem(item);
        ItemWorld.DropItem(player.dropPosition, duplicateItem);

        switch (item.itemType)
        {
            case Item.ItemType.ammo:
                database.SendData("Item Ammo", inventory.ammoAmount.ToString());
                break;
            case Item.ItemType.medkit:
                database.SendData("Item Medkit", inventory.medkitAmount.ToString());
                break;
            case Item.ItemType.bandage:
                database.SendData("Item Bandage", inventory.bandageAmount.ToString());
                break;
            case Item.ItemType.grenade:
                database.SendData("Item Grenade", inventory.grenadeAmount.ToString());
                break;
            case Item.ItemType.molotov:
                database.SendData("Item Molotov", inventory.molotovAmount.ToString());
                break;
            case Item.ItemType.smoke:
                database.SendData("Item Smoke", inventory.smokeAmount.ToString());
                break;
            case Item.ItemType.landmine:
                database.SendData("Item Landmine", inventory.landmineAmount.ToString());
                break;
            case Item.ItemType.chicken:
                database.SendData("Item Chicken", inventory.chickenAmount.ToString());
                break;
            case Item.ItemType.stone:
                database.SendData("Item Stone", inventory.stoneAmount.ToString());
                break;
            case Item.ItemType.wood:
                database.SendData("Item Wood", inventory.woodAmount.ToString());
                break;
            case Item.ItemType.wall:
                database.SendData("Item WoodWall", inventory.wallAmount.ToString());
                break;
            case Item.ItemType.metalWall:
                database.SendData("Item MetalWall", inventory.metalWallAmount.ToString());
                break;
            case Item.ItemType.woodDoor:
                database.SendData("Item WoodDoor", inventory.woodDoorAmount.ToString());
                break;
            case Item.ItemType.metalDoor:
                database.SendData("Item MetalDoor", inventory.metalDoorAmount.ToString());
                break;
            case Item.ItemType.kriss:
                database.SendData("Weapon Kriss", inventory.krissAmount.ToString());
                break;
            case Item.ItemType.mp7:
                database.SendData("Weapon MP7", inventory.mp7Amount.ToString());
                break;
            case Item.ItemType.mp5:
                database.SendData("Weapon MP5", inventory.mp5Amount.ToString());
                break;
            case Item.ItemType.ump45:
                database.SendData("Weapon UMP", inventory.ump45Amount.ToString());
                break;
            case Item.ItemType.tec9:
                database.SendData("Weapon Tec9", inventory.tec9Amount.ToString());
                break;
            case Item.ItemType.uzi:
                database.SendData("Weapon UZI", inventory.uziAmount.ToString());
                break;
            case Item.ItemType.ak12:
                database.SendData("Weapon AK12", inventory.ak12Amount.ToString());
                break;
            case Item.ItemType.ak74:
                database.SendData("Weapon AK74", inventory.ak74Amount.ToString());
                break;
            case Item.ItemType.g3a4:
                database.SendData("Weapon G3A4", inventory.g3a4Amount.ToString());
                break;
            case Item.ItemType.g36c:
                database.SendData("Weapon G36C", inventory.g36cAmount.ToString());
                break;
            case Item.ItemType.flamethrower:
                database.SendData("Weapon Flamethrower", inventory.flamethrowerAmount.ToString());
                break;
            case Item.ItemType.glock17:
                database.SendData("Weapon Glock17", inventory.glock17Amount.ToString());
                break;
            case Item.ItemType.fuel:
                database.SendData("Item Fuel", inventory.fuelAmount.ToString());
                break;
            case Item.ItemType.minigun:
                database.SendData("Item Minigun", inventory.minigunAmount.ToString());
                break;
        }
    }

    public void ClickSend()
    {
        if(ammo)
        {
            if (inDog)
            {
                SendToInventory();
                SendPlayerData("ammo", 0);
            }
            else if (!inDog)
            {
                SendToDog();
                SendPlayerData("ammo", 1);
            }
        }

        if (medkit)
        {
            if (inDog)
            {
                SendToInventory();
                SendPlayerData("medkit", 0);
            }
            else if (!inDog)
            {
                SendToDog();
                SendPlayerData("medkit", 1);
            }
        }

        if (bandage)
        {
            if (inDog)
            {
                SendToInventory();
                SendPlayerData("bandage", 0);
            }
            else if (!inDog)
            {
                SendToDog();
                SendPlayerData("bandage", 1);
            }
        }

        if (grenade)
        {
            if (inDog)
            {
                SendToInventory();
                SendPlayerData("grenade", 0);
            }
            else if (!inDog)
            {
                SendToDog();
                SendPlayerData("grenade", 1);
            }
        }

        if (molotov)
        {
            if (inDog)
            {
                SendToInventory();
                SendPlayerData("molotov", 0);
            }
            else if (!inDog)
            {
                SendToDog();
                SendPlayerData("molotov", 1);
            }
        }

        if (smoke)
        {
            if (inDog)
            {
                SendToInventory();
                SendPlayerData("smoke", 0);
            }
            else if (!inDog)
            {
                SendToDog();
                SendPlayerData("smoke", 1);
            }
        }

        if (landmine)
        {
            if (inDog)
            {
                SendToInventory();
                SendPlayerData("landmine", 0);
            }
            else if (!inDog)
            {
                SendToDog();
                SendPlayerData("landmine", 1);
            }
        }
        
        if (chicken)
        {
            if (inDog)
            {
                SendToInventory();
                SendPlayerData("chicken", 0);
            }
            else if (!inDog)
            {
                SendToDog();
                SendPlayerData("chicken", 1);
            }
        }
        
        if (wood)
        {
            if (inDog)
            {
                SendToInventory();
                SendPlayerData("wood", 0);
            }
            else if (!inDog)
            {
                SendToDog();
                SendPlayerData("wood", 1);
            }
        }
        
        if (stone)
        {
            if (inDog)
            {
                SendToInventory();
                SendPlayerData("stone", 0);
            }
            else if (!inDog)
            {
                SendToDog();
                SendPlayerData("stone", 1);
            }
        }
        
        if (wall)
        {
            if (inDog)
            {
                SendToInventory();
                SendPlayerData("wall", 0);
            }
            else if (!inDog)
            {
                SendToDog();
                SendPlayerData("wall", 1);
            }
        }
        
        if (metalWall)
        {
            if (inDog)
            {
                SendToInventory();
                SendPlayerData("metalWall", 0);
            }
            else if (!inDog)
            {
                SendToDog();
                SendPlayerData("metalWall", 1);
            }
        }
        
        if (woodDoor)
        {
            if (inDog)
            {
                SendToInventory();
                SendPlayerData("woodDoor", 0);
            }
            else if (!inDog)
            {
                SendToDog();
                SendPlayerData("woodDoor", 1);
            }
        }
        
        if (metalDoor)
        {
            if (inDog)
            {
                SendToInventory();
                SendPlayerData("metalDoor", 0);
            }
            else if (!inDog)
            {
                SendToDog();
                SendPlayerData("metalDoor", 1);
            }
        }
        
        if (kriss)
        {
            if (inDog)
            {
                SendToInventory();
                SendPlayerData("kriss", 0);
            }
            else if (!inDog)
            {
                SendToDog();
                SendPlayerData("kriss", 1);
            }
        }
        
        if (mp7)
        {
            if (inDog)
            {
                SendToInventory();
                SendPlayerData("mp7", 0);
            }
            else if (!inDog)
            {
                SendToDog();
                SendPlayerData("mp7", 1);
            }
        }
        
        if (mp5)
        {
            if (inDog)
            {
                SendToInventory();
                SendPlayerData("mp5", 0);
            }
            else if (!inDog)
            {
                SendToDog();
                SendPlayerData("mp5", 1);
            }
        }
        
        if (ump45)
        {
            if (inDog)
            {
                SendToInventory();
                SendPlayerData("ump45", 0);
            }
            else if (!inDog)
            {
                SendToDog();
                SendPlayerData("ump45", 1);
            }
        }
        
        if (tec9)
        {
            if (inDog)
            {
                SendToInventory();
                SendPlayerData("tec9", 0);
            }
            else if (!inDog)
            {
                SendToDog();
                SendPlayerData("tec9", 1);
            }
        }
        
        if (uzi)
        {
            if (inDog)
            {
                SendToInventory();
                SendPlayerData("uzi", 0);
            }
            else if (!inDog)
            {
                SendToDog();
                SendPlayerData("uzi", 1);
            }
        }
        
        if (ak12)
        {
            if (inDog)
            {
                SendToInventory();
                SendPlayerData("ak12", 0);
            }
            else if (!inDog)
            {
                SendToDog();
                SendPlayerData("ak12", 1);
            }
        }
        
        if (ak74)
        {
            if (inDog)
            {
                SendToInventory();
                SendPlayerData("ak74", 0);
            }
            else if (!inDog)
            {
                SendToDog();
                SendPlayerData("ak74", 1);
            }
        }
        
        if (g3a4)
        {
            if (inDog)
            {
                SendToInventory();
                SendPlayerData("g3a4", 0);
            }
            else if (!inDog)
            {
                SendToDog();
                SendPlayerData("g3a4", 1);
            }
        }
        
        if (g36c)
        {
            if (inDog)
            {
                SendToInventory();
                SendPlayerData("g36c", 0);
            }
            else if (!inDog)
            {
                SendToDog();
                SendPlayerData("g36c", 1);
            }
        }
        
        if (flamethrower)
        {
            if (inDog)
            {
                SendToInventory();
                SendPlayerData("flamethrower", 0);
            }
            else if (!inDog)
            {
                SendToDog();
                SendPlayerData("flamethrower", 1);
            }
        }
        
        if (glock17)
        {
            if (inDog)
            {
                SendToInventory();
                SendPlayerData("glock17", 0);
            }
            else if (!inDog)
            {
                SendToDog();
                SendPlayerData("glock17", 1);
            }
        }


        if (fuel)
        {
            if (inDog)
            {
                SendToInventory();
                SendPlayerData("fuel", 0);
            }
            else if (!inDog)
            {
                SendToDog();
                SendPlayerData("fuel", 1);
            }
        }

        if (minigun)
        {
            if (inDog)
            {
                SendToInventory();
                SendPlayerData("minigun", 0);
            }
            else if (!inDog)
            {
                SendToDog();
                SendPlayerData("minigun", 1);
            }
        }
    }

    void SendPlayerData(string name, int value)
    {
        PlayerPrefs.SetInt(name, value);
    }

    public void SetPositions()
    {
        //if (uI_Inventory.shopOpen) return;

        Debug.Log("positions set");

        if (ammo)
        {
            if (PlayerPrefs.GetInt("ammo") == 0)
            {
                SendToInventory();
            }

            if (PlayerPrefs.GetInt("ammo") == 1)
            {
                SendToDog();
            }
        }

        if (medkit)
        {
            if (PlayerPrefs.GetInt("medkit") == 0)
            {
                SendToInventory();
            }

            if (PlayerPrefs.GetInt("medkit") == 1)
            {
                SendToDog();
            }
        }

        if (bandage)
        {
            if (PlayerPrefs.GetInt("bandage") == 0)
            {
                SendToInventory();
            }

            if (PlayerPrefs.GetInt("bandage") == 1)
            {
                SendToDog();
            }
        }

        if (grenade)
        {
            if (PlayerPrefs.GetInt("grenade") == 0)
            {
                SendToInventory();
            }

            if (PlayerPrefs.GetInt("grenade") == 1)
            {
                SendToDog();
            }
        }
        
        if (molotov)
        {
            if (PlayerPrefs.GetInt("molotov") == 0)
            {
                SendToInventory();
            }

            if (PlayerPrefs.GetInt("molotov") == 1)
            {
                SendToDog();
            }
        }
        
        if (smoke)
        {
            if (PlayerPrefs.GetInt("smoke") == 0)
            {
                SendToInventory();
            }

            if (PlayerPrefs.GetInt("smoke") == 1)
            {
                SendToDog();
            }
        }
        
        if (landmine)
        {
            if (PlayerPrefs.GetInt("landmine") == 0)
            {
                SendToInventory();
            }

            if (PlayerPrefs.GetInt("landmine") == 1)
            {
                SendToDog();
            }
        }
        
        if (chicken)
        {
            if (PlayerPrefs.GetInt("chicken") == 0)
            {
                SendToInventory();
            }

            if (PlayerPrefs.GetInt("chicken") == 1)
            {
                SendToDog();
            }
        }
        
        if (wood)
        {
            if (PlayerPrefs.GetInt("wood") == 0)
            {
                SendToInventory();
            }

            if (PlayerPrefs.GetInt("wood") == 1)
            {
                SendToDog();
            }
        }
        
        if (stone)
        {
            if (PlayerPrefs.GetInt("stone") == 0)
            {
                SendToInventory();
            }

            if (PlayerPrefs.GetInt("stone") == 1)
            {
                SendToDog();
            }
        }
        
        if (wall)
        {
            if (PlayerPrefs.GetInt("wall") == 0)
            {
                SendToInventory();
            }

            if (PlayerPrefs.GetInt("wall") == 1)
            {
                SendToDog();
            }
        }
        
        if (metalWall)
        {
            if (PlayerPrefs.GetInt("metalWall") == 0)
            {
                SendToInventory();
            }

            if (PlayerPrefs.GetInt("metalWall") == 1)
            {
                SendToDog();
            }
        }
        
        if (woodDoor)
        {
            if (PlayerPrefs.GetInt("woodDoor") == 0)
            {
                SendToInventory();
            }

            if (PlayerPrefs.GetInt("woodDoor") == 1)
            {
                SendToDog();
            }
        }
        
        if (metalDoor)
        {
            if (PlayerPrefs.GetInt("metalDoor") == 0)
            {
                SendToInventory();
            }

            if (PlayerPrefs.GetInt("metalDoor") == 1)
            {
                SendToDog();
            }
        }
        
        if (kriss)
        {
            if (PlayerPrefs.GetInt("kriss") == 0)
            {
                SendToInventory();
            }

            if (PlayerPrefs.GetInt("kriss") == 1)
            {
                SendToDog();
            }
        }
        
        if (mp7)
        {
            if (PlayerPrefs.GetInt("mp7") == 0)
            {
                SendToInventory();
            }

            if (PlayerPrefs.GetInt("mp7") == 1)
            {
                SendToDog();
            }
        }
        
        if (mp5)
        {
            if (PlayerPrefs.GetInt("mp5") == 0)
            {
                SendToInventory();
            }

            if (PlayerPrefs.GetInt("mp5") == 1)
            {
                SendToDog();
            }
        }
        
        if (ump45)
        {
            if (PlayerPrefs.GetInt("ump45") == 0)
            {
                SendToInventory();
            }

            if (PlayerPrefs.GetInt("ump45") == 1)
            {
                SendToDog();
            }
        }
        
        if (tec9)
        {
            if (PlayerPrefs.GetInt("tec9") == 0)
            {
                SendToInventory();
            }

            if (PlayerPrefs.GetInt("tec9") == 1)
            {
                SendToDog();
            }
        }
        
        if (uzi)
        {
            if (PlayerPrefs.GetInt("uzi") == 0)
            {
                SendToInventory();
            }

            if (PlayerPrefs.GetInt("uzi") == 1)
            {
                SendToDog();
            }
        }
        
        if (ak12)
        {
            if (PlayerPrefs.GetInt("ak12") == 0)
            {
                SendToInventory();
            }

            if (PlayerPrefs.GetInt("ak12") == 1)
            {
                SendToDog();
            }
        }
        
        if (ak74)
        {
            if (PlayerPrefs.GetInt("ak74") == 0)
            {
                SendToInventory();
            }

            if (PlayerPrefs.GetInt("ak74") == 1)
            {
                SendToDog();
            }
        }
        
        if (g3a4)
        {
            if (PlayerPrefs.GetInt("g3a4") == 0)
            {
                SendToInventory();
            }

            if (PlayerPrefs.GetInt("g3a4") == 1)
            {
                SendToDog();
            }
        }
        
        if (g36c)
        {
            if (PlayerPrefs.GetInt("g36c") == 0)
            {
                SendToInventory();
            }

            if (PlayerPrefs.GetInt("g36c") == 1)
            {
                SendToDog();
            }
        }
        
        if (flamethrower)
        {
            if (PlayerPrefs.GetInt("flamethrower") == 0)
            {
                SendToInventory();
            }

            if (PlayerPrefs.GetInt("flamethrower") == 1)
            {
                SendToDog();
            }
        }
        
        if (glock17)
        {
            if (PlayerPrefs.GetInt("glock17") == 0)
            {
                SendToInventory();
            }

            if (PlayerPrefs.GetInt("glock17") == 1)
            {
                SendToDog();
            }
        }

        if (fuel)
        {
            if (PlayerPrefs.GetInt("fuel") == 0)
            {
                SendToInventory();
            }

            if (PlayerPrefs.GetInt("fuel") == 1)
            {
                SendToDog();
            }
        }

        if (minigun)
        {
            if (PlayerPrefs.GetInt("minigun") == 0)
            {
                SendToInventory();
            }

            if (PlayerPrefs.GetInt("minigun") == 1)
            {
                SendToDog();
            }
        }
    }

    public void SendToInventory()
    {
        if (inInventory) return;

        sendingTXT.fontSize = 15f;

        sendingTXT.text = "Send To Shiba";

        transform.parent = uI_Inventory.itemSlotContainer;

        inDog = false;
        inInventory = true;


        DropBTNOBJ.SetActive(true);
    }

    public void SendToDog()
    {
        if (inDog) return;

        sendingTXT.fontSize = 15f;

        sendingTXT.text = "Send To Inventory";

        transform.parent = uI_Inventory.dogSlot;

        inDog = true;
        inInventory = false;


        DropBTNOBJ.SetActive(false);
    }

    //----------------------------------------Sent To Storage-----------------------------------//
    public void SendToSRV()
    {
        if(inDog)
        {
            SendToInventory();
            return;
        }


        storage = uI_Inventory.GetComponent<Storage>();

        if (glock17)
        {
            storage.EnableWeapon(0);
            PlayerPrefs.SetInt("glock17 amt", player.inventory.glock17Amount);
            inventory.RemoveItem(new Item { itemType = Item.ItemType.kriss, amount = player.inventory.glock17Amount });

            database.SendData("Weapon Glock17", player.inventory.krissAmount.ToString());
        }

        if (kriss)
        {
            storage.EnableWeapon(1);
            PlayerPrefs.SetInt("kriss amt", player.inventory.krissAmount);
            inventory.RemoveItem(new Item { itemType = Item.ItemType.kriss, amount = player.inventory.krissAmount });

            database.SendData("Weapon Kriss", player.inventory.krissAmount.ToString()); 
        }

        if (mp7)
        {
            storage.EnableWeapon(2);
            PlayerPrefs.SetInt("mp7 amt", player.inventory.mp7Amount);
            inventory.RemoveItem(new Item { itemType = Item.ItemType.mp7, amount = player.inventory.mp7Amount });

            database.SendData("Weapon MP7", player.inventory.mp7Amount.ToString()); 
        }

        if (mp5)
        {
            storage.EnableWeapon(3);
            PlayerPrefs.SetInt("mp5 amt", player.inventory.mp5Amount);
            inventory.RemoveItem(new Item { itemType = Item.ItemType.mp5, amount = player.inventory.mp5Amount });

            database.SendData("Weapon MP5", player.inventory.mp5Amount.ToString()); 
        }

        if (ump45)
        {
            storage.EnableWeapon(4);
            PlayerPrefs.SetInt("ump45 amt", player.inventory.ump45Amount);
            inventory.RemoveItem(new Item { itemType = Item.ItemType.ump45, amount = player.inventory.ump45Amount });

            database.SendData("Weapon UMP", player.inventory.ump45Amount.ToString()); 
        }

        if (tec9)
        {
            storage.EnableWeapon(5);
            PlayerPrefs.SetInt("tec9 amt", player.inventory.tec9Amount);
            inventory.RemoveItem(new Item { itemType = Item.ItemType.tec9, amount = player.inventory.tec9Amount });

            database.SendData("Weapon Tec9", player.inventory.tec9Amount.ToString()); 
        }

        if (uzi)
        {
            storage.EnableWeapon(6);
            PlayerPrefs.SetInt("uzi amt", player.inventory.uziAmount);
            inventory.RemoveItem(new Item { itemType = Item.ItemType.uzi, amount = player.inventory.uziAmount });

            database.SendData("Weapon UZI", player.inventory.uziAmount.ToString()); 
        }

        if (ak12)
        {
            storage.EnableWeapon(7);
            PlayerPrefs.SetInt("ak12 amt", player.inventory.ak12Amount);
            inventory.RemoveItem(new Item { itemType = Item.ItemType.ak12, amount = player.inventory.ak12Amount });

            database.SendData("Weapon AK12", player.inventory.ak12Amount.ToString()); 
        }

        if (ak74)
        {
            storage.EnableWeapon(8);
            PlayerPrefs.SetInt("ak74 amt", player.inventory.ak74Amount);
            inventory.RemoveItem(new Item { itemType = Item.ItemType.ak74, amount = player.inventory.ak74Amount });

            database.SendData("Weapon AK74", player.inventory.ak74Amount.ToString()); 
        }

        if (g3a4)
        {
            storage.EnableWeapon(9);
            PlayerPrefs.SetInt("g3a4 amt", player.inventory.g3a4Amount);
            inventory.RemoveItem(new Item { itemType = Item.ItemType.g3a4, amount = player.inventory.g3a4Amount });

            database.SendData("Weapon G3A4", player.inventory.g3a4Amount.ToString()); 
        }

        if (g36c)
        {
            storage.EnableWeapon(10);
            PlayerPrefs.SetInt("g36c amt", player.inventory.g36cAmount);
            inventory.RemoveItem(new Item { itemType = Item.ItemType.g36c, amount = player.inventory.g36cAmount });

            database.SendData("Weapon G36C", player.inventory.g36cAmount.ToString()); 
        }

        if(flamethrower)
        {
            storage.EnableWeapon(11);
            PlayerPrefs.SetInt("flamethrower amt", player.inventory.flamethrowerAmount);
            inventory.RemoveItem(new Item { itemType = Item.ItemType.flamethrower, amount = player.inventory.flamethrowerAmount });

            database.SendData("Weapon Flamethrower", player.inventory.g36cAmount.ToString());
        }

        //------------------------------------------------------------Items----------------------------------------------------------------///

        if (ammo)
        {
            storage.EnableWeapon(12);
            PlayerPrefs.SetInt("ammo amt", player.inventory.ammoAmount);
            inventory.RemoveItem(new Item { itemType = Item.ItemType.ammo, amount = player.inventory.ammoAmount });

            database.SendData("Item Ammo", player.inventory.ammoAmount.ToString());
        }

        if (medkit)
        {
            storage.EnableWeapon(13);
            PlayerPrefs.SetInt("medkit amt", player.inventory.medkitAmount);
            inventory.RemoveItem(new Item { itemType = Item.ItemType.medkit, amount = player.inventory.medkitAmount });

            database.SendData("Item Medkit", player.inventory.medkitAmount.ToString());
        }

        if (bandage)
        {
            storage.EnableWeapon(14);
            PlayerPrefs.SetInt("bandage amt", player.inventory.bandageAmount);
            inventory.RemoveItem(new Item { itemType = Item.ItemType.bandage, amount = player.inventory.bandageAmount });

            database.SendData("Item Bandage", player.inventory.bandageAmount.ToString());
        }

        if (grenade)
        {
            storage.EnableWeapon(15);
            PlayerPrefs.SetInt("grenade amt", player.inventory.grenadeAmount);
            inventory.RemoveItem(new Item { itemType = Item.ItemType.grenade, amount = player.inventory.grenadeAmount });

            database.SendData("Item Grenade", player.inventory.grenadeAmount.ToString());
        }

        if (molotov)
        {
            storage.EnableWeapon(16);
            PlayerPrefs.SetInt("molotov amt", player.inventory.molotovAmount);
            inventory.RemoveItem(new Item { itemType = Item.ItemType.molotov, amount = player.inventory.molotovAmount });

            database.SendData("Item Molotov", player.inventory.molotovAmount.ToString());
        }

        if (smoke)
        {
            storage.EnableWeapon(17);
            PlayerPrefs.SetInt("smoke amt", player.inventory.smokeAmount);
            inventory.RemoveItem(new Item { itemType = Item.ItemType.smoke, amount = player.inventory.smokeAmount });

            database.SendData("Item Smoke", player.inventory.smokeAmount.ToString());
        }

        if (landmine)
        {
            storage.EnableWeapon(18);
            PlayerPrefs.SetInt("landmine amt", player.inventory.landmineAmount);
            inventory.RemoveItem(new Item { itemType = Item.ItemType.landmine, amount = player.inventory.landmineAmount });

            database.SendData("Item Landmine", player.inventory.landmineAmount.ToString());
        }

        if (chicken)
        {
            storage.EnableWeapon(19);
            PlayerPrefs.SetInt("chicken amt", player.inventory.chickenAmount);
            inventory.RemoveItem(new Item { itemType = Item.ItemType.chicken, amount = player.inventory.chickenAmount });

            database.SendData("Item Landmine", player.inventory.chickenAmount.ToString());
        }

        if (wood)
        {
            storage.EnableWeapon(20);
            PlayerPrefs.SetInt("wood amt", player.inventory.woodAmount);
            inventory.RemoveItem(new Item { itemType = Item.ItemType.wood, amount = player.inventory.woodAmount });

            database.SendData("Item Wood", player.inventory.woodAmount.ToString());
        }

        if (stone)
        {
            storage.EnableWeapon(21);
            PlayerPrefs.SetInt("stone amt", player.inventory.stoneAmount);
            inventory.RemoveItem(new Item { itemType = Item.ItemType.stone, amount = player.inventory.stoneAmount });

            database.SendData("Item Stone", player.inventory.stoneAmount.ToString());
        }

        if (wall)
        {
            storage.EnableWeapon(22);
            PlayerPrefs.SetInt("wall amt", player.inventory.wallAmount);
            inventory.RemoveItem(new Item { itemType = Item.ItemType.wall, amount = player.inventory.wallAmount });

            database.SendData("Item WoodWall", player.inventory.wallAmount.ToString());
        }

        if (metalWall)
        {
            storage.EnableWeapon(23);
            PlayerPrefs.SetInt("metalWall amt", player.inventory.metalWallAmount);
            inventory.RemoveItem(new Item { itemType = Item.ItemType.metalWall, amount = player.inventory.metalWallAmount });

            database.SendData("Item MetalWall", player.inventory.metalWallAmount.ToString());
        }

        if (woodDoor)
        {
            storage.EnableWeapon(24);
            PlayerPrefs.SetInt("woodDoor amt", player.inventory.stoneAmount);
            inventory.RemoveItem(new Item { itemType = Item.ItemType.woodDoor, amount = player.inventory.metalWallAmount });

            database.SendData("Item WoodDoor", player.inventory.woodDoorAmount.ToString());
        }

        if (metalDoor)
        {
            storage.EnableWeapon(25);
            PlayerPrefs.SetInt("metalDoor amt", player.inventory.metalDoorAmount);
            inventory.RemoveItem(new Item { itemType = Item.ItemType.metalDoor, amount = player.inventory.metalDoorAmount });

            database.SendData("Item MetalDoor", player.inventory.metalDoorAmount.ToString());
        }

        if (fuel)
        {
            storage.EnableWeapon(26);
            PlayerPrefs.SetInt("fuel amt", player.inventory.fuelAmount);
            inventory.RemoveItem(new Item { itemType = Item.ItemType.fuel, amount = player.inventory.fuelAmount });

            database.SendData("Item Fuel", player.inventory.fuelAmount.ToString());
        }

        if (minigun)
        {
            storage.EnableWeapon(27);
            PlayerPrefs.SetInt("minigun amt", player.inventory.minigunAmount);
            inventory.RemoveItem(new Item { itemType = Item.ItemType.minigun, amount = player.inventory.minigunAmount });

            database.SendData("Item Fuel", player.inventory.minigunAmount.ToString());
        }

        weaponManger.SwitchThree();
        weaponManger.resetWeapon();
    }

    public void DirectSendToSRV()
    {
        if (inDog)
        {
            SendToInventory();
            return;
        }


        weaponManger.SwitchThree();
        weaponManger.resetWeapon();
    }

    public void SetItem()
    {
        if (item.itemType == Item.ItemType.ammo)
        {
            ammo = true;
        }

        if (item.itemType == Item.ItemType.fuel)
        {
            fuel = true;
        }

        if (item.itemType == Item.ItemType.medkit)
        {
            medkit = true;
        }

        if (item.itemType == Item.ItemType.bandage)
        {
            bandage = true;
        }

        if (item.itemType == Item.ItemType.grenade)
        {
            grenade = true;
        }

        if (item.itemType == Item.ItemType.molotov)
        {
            molotov = true;
        }

        if (item.itemType == Item.ItemType.smoke)
        {
            smoke = true;
        }

        if (item.itemType == Item.ItemType.landmine)
        {
            landmine = true;
        }

        if (item.itemType == Item.ItemType.chicken)
        {
            chicken = true;
        }

        if (item.itemType == Item.ItemType.wood)
        {
            wood = true;
        }

        if (item.itemType == Item.ItemType.stone)
        {
            stone = true;
        }

        if (item.itemType == Item.ItemType.wall)
        {
            wall = true;
        }

        if (item.itemType == Item.ItemType.metalWall)
        {
            metalWall = true;
        }

        if (item.itemType == Item.ItemType.woodDoor)
        {
            woodDoor = true;
        }

        if (item.itemType == Item.ItemType.metalDoor)
        {
            metalDoor = true;
        }

        if (item.itemType == Item.ItemType.kriss)
        {
            kriss = true;
        }

        if (item.itemType == Item.ItemType.mp7)
        {
            mp7 = true;
        }

        if (item.itemType == Item.ItemType.mp5)
        {
            mp5 = true;
        }

        if (item.itemType == Item.ItemType.ump45)
        {
            ump45 = true;
        }

        if (item.itemType == Item.ItemType.tec9)
        {
            tec9 = true;
        }

        if (item.itemType == Item.ItemType.uzi)
        {
            uzi = true;
        }

        if (item.itemType == Item.ItemType.ak12)
        {
            ak12 = true;
        }

        if (item.itemType == Item.ItemType.ak74)
        {
            ak74 = true;
        }

        if (item.itemType == Item.ItemType.g3a4)
        {
            g3a4 = true;
        }

        if (item.itemType == Item.ItemType.g36c)
        {
            g36c = true;
        }

        if (item.itemType == Item.ItemType.flamethrower)
        {
            flamethrower = true;
        }

        if (item.itemType == Item.ItemType.glock17)
        {
            glock17 = true;
        }

        if (item.itemType == Item.ItemType.minigun)
        {
            minigun = true;
        }
    }

}
